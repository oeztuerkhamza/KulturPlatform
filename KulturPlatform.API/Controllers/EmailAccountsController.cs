using KulturPlatform.Domain.Commons.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace KulturPlatform.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = Policies.RequireSystemAdmin)]
    public partial class EmailAccountsController : ControllerBase
    {
        private const string ContainerName = "kpf_mailserver";
        private const string Domain = "kulturplattformfreiburg.org";

        [GeneratedRegex(@"^[a-zA-Z0-9._%+-]+$", RegexOptions.Compiled)]
        private static partial Regex EmailLocalPartRegex();

        [GeneratedRegex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", RegexOptions.Compiled)]
        private static partial Regex EmailFullRegex();

        // GET: api/emailaccounts
        [HttpGet]
        public async Task<ActionResult<EmailAccountListResponse>> GetAll()
        {
            var (exitCode, output) = await RunSetupCommand("email", "list");
            if (exitCode != 0)
            {
                // Try parsing even on error (list command may return partial results)
            }

            var accounts = ParseAccountList(output);
            return Ok(new EmailAccountListResponse { Accounts = accounts });
        }

        // POST: api/emailaccounts
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateEmailAccountRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.LocalPart) || string.IsNullOrWhiteSpace(request.Password))
                return BadRequest(new { message = "E-posta adresi ve şifre gereklidir." });

            if (!EmailLocalPartRegex().IsMatch(request.LocalPart))
                return BadRequest(new { message = "Geçersiz e-posta adresi formatı." });

            if (request.Password.Length < 8)
                return BadRequest(new { message = "Şifre en az 8 karakter olmalıdır." });

            var email = $"{request.LocalPart}@{Domain}";
            var (exitCode, output) = await RunSetupCommand("email", "add", email, request.Password);

            if (exitCode != 0 && output.Contains("already exists", StringComparison.OrdinalIgnoreCase))
                return Conflict(new { message = "Bu e-posta adresi zaten mevcut." });

            if (exitCode != 0)
                return StatusCode(500, new { message = $"Hesap oluşturulamadı: {output}" });

            return Ok(new { message = $"{email} başarıyla oluşturuldu." });
        }

        // DELETE: api/emailaccounts/{localPart}
        [HttpDelete("{localPart}")]
        public async Task<ActionResult> Delete(string localPart)
        {
            if (!EmailLocalPartRegex().IsMatch(localPart))
                return BadRequest(new { message = "Geçersiz e-posta adresi." });

            var email = $"{localPart}@{Domain}";
            var (exitCode, output) = await RunSetupCommand("email", "del", "-y", email);

            if (exitCode != 0)
                return StatusCode(500, new { message = $"Hesap silinemedi: {output}" });

            return Ok(new { message = $"{email} başarıyla silindi." });
        }

        // PUT: api/emailaccounts/{localPart}/password
        [HttpPut("{localPart}/password")]
        public async Task<ActionResult> ChangePassword(string localPart, [FromBody] ChangePasswordRequest request)
        {
            if (!EmailLocalPartRegex().IsMatch(localPart))
                return BadRequest(new { message = "Geçersiz e-posta adresi." });

            if (string.IsNullOrWhiteSpace(request.NewPassword) || request.NewPassword.Length < 8)
                return BadRequest(new { message = "Yeni şifre en az 8 karakter olmalıdır." });

            var email = $"{localPart}@{Domain}";
            var (exitCode, output) = await RunSetupCommand("email", "update", email, request.NewPassword);

            if (exitCode != 0)
                return StatusCode(500, new { message = $"Şifre değiştirilemedi: {output}" });

            return Ok(new { message = $"{email} şifresi başarıyla güncellendi." });
        }

        // POST: api/emailaccounts/alias
        [HttpPost("alias")]
        public async Task<ActionResult> CreateAlias([FromBody] CreateAliasRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.AliasLocalPart) || string.IsNullOrWhiteSpace(request.TargetEmail))
                return BadRequest(new { message = "Alias ve hedef e-posta gereklidir." });

            if (!EmailLocalPartRegex().IsMatch(request.AliasLocalPart))
                return BadRequest(new { message = "Geçersiz alias formatı." });

            if (!EmailFullRegex().IsMatch(request.TargetEmail))
                return BadRequest(new { message = "Geçersiz hedef e-posta formatı." });

            var alias = $"{request.AliasLocalPart}@{Domain}";
            var (exitCode, output) = await RunSetupCommand("alias", "add", alias, request.TargetEmail);

            if (exitCode != 0)
                return StatusCode(500, new { message = $"Alias oluşturulamadı: {output}" });

            return Ok(new { message = $"{alias} → {request.TargetEmail} alias oluşturuldu." });
        }

        // DELETE: api/emailaccounts/alias/{aliasLocalPart}
        [HttpDelete("alias/{aliasLocalPart}")]
        public async Task<ActionResult> DeleteAlias(string aliasLocalPart)
        {
            if (!EmailLocalPartRegex().IsMatch(aliasLocalPart))
                return BadRequest(new { message = "Geçersiz alias formatı." });

            var alias = $"{aliasLocalPart}@{Domain}";
            var (exitCode, output) = await RunSetupCommand("alias", "del", alias);

            if (exitCode != 0)
                return StatusCode(500, new { message = $"Alias silinemedi: {output}" });

            return Ok(new { message = $"{alias} alias silindi." });
        }

        private static async Task<(int ExitCode, string Output)> RunSetupCommand(params string[] args)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "docker",
                    ArgumentList = { "exec", ContainerName, "setup" },
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };

            foreach (var arg in args)
                process.StartInfo.ArgumentList.Add(arg);

            process.Start();

            var stdout = await process.StandardOutput.ReadToEndAsync();
            var stderr = await process.StandardError.ReadToEndAsync();

            await process.WaitForExitAsync();

            var output = string.IsNullOrWhiteSpace(stdout) ? stderr : stdout;
            return (process.ExitCode, output.Trim());
        }

        private static List<EmailAccountDto> ParseAccountList(string output)
        {
            var accounts = new List<EmailAccountDto>();
            var lines = output.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                var trimmed = line.Trim();
                if (!trimmed.StartsWith('*'))
                    continue;

                // Format: "* user@domain ( SIZE / QUOTA ) [PERCENT%]"
                var emailMatch = Regex.Match(trimmed, @"\*\s+(\S+@\S+)\s+\(\s*(\S+)\s*/\s*(\S+)\s*\)");
                if (emailMatch.Success)
                {
                    var email = emailMatch.Groups[1].Value;
                    var parts = email.Split('@');
                    accounts.Add(new EmailAccountDto
                    {
                        Email = email,
                        LocalPart = parts[0],
                        Domain = parts.Length > 1 ? parts[1] : Domain,
                        UsedSpace = emailMatch.Groups[2].Value,
                        Quota = emailMatch.Groups[3].Value,
                    });
                }
            }

            return accounts;
        }
    }

    public class EmailAccountListResponse
    {
        public List<EmailAccountDto> Accounts { get; set; } = [];
    }

    public class EmailAccountDto
    {
        public string Email { get; set; } = "";
        public string LocalPart { get; set; } = "";
        public string Domain { get; set; } = "";
        public string UsedSpace { get; set; } = "";
        public string Quota { get; set; } = "";
    }

    public class CreateEmailAccountRequest
    {
        public string LocalPart { get; set; } = "";
        public string Password { get; set; } = "";
    }

    public class ChangePasswordRequest
    {
        public string NewPassword { get; set; } = "";
    }

    public class CreateAliasRequest
    {
        public string AliasLocalPart { get; set; } = "";
        public string TargetEmail { get; set; } = "";
    }
}
