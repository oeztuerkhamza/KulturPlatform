namespace KulturPlatform.Domain.Commons.ValueObjects
{
    /// <summary>
    /// Represents an image stored in the database as base64 encoded data
    /// Supports common image formats with size and quality restrictions
    /// </summary>
    public sealed record ImageData
    {
        public string Base64Data { get; init; }
        public string MimeType { get; init; }
        public string FileName { get; init; }
        public int FileSizeBytes { get; init; }

        // EF Core için
        private ImageData()
        {
            Base64Data = string.Empty;
            MimeType = string.Empty;
            FileName = string.Empty;
            FileSizeBytes = 0;
        }

        private ImageData(string base64Data, string mimeType, string fileName, int fileSizeBytes)
        {
            Base64Data = base64Data;
            MimeType = mimeType;
            FileName = fileName;
            FileSizeBytes = fileSizeBytes;
        }

        /// <summary>
        /// Creates an ImageData value object with validation
        /// </summary>
        /// <param name="base64Data">Base64 encoded image data (without data URI prefix)</param>
        /// <param name="mimeType">Image MIME type (e.g., image/jpeg, image/png)</param>
        /// <param name="fileName">Original filename</param>
        /// <param name="fileSizeBytes">Size of the decoded image in bytes</param>
        public static ImageData Create(string base64Data, string mimeType, string fileName, int fileSizeBytes)
        {
            if (string.IsNullOrWhiteSpace(base64Data))
                throw new ArgumentException("Base64 data cannot be empty.", nameof(base64Data));

            if (string.IsNullOrWhiteSpace(mimeType))
                throw new ArgumentException("MIME type cannot be empty.", nameof(mimeType));

            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("File name cannot be empty.", nameof(fileName));

            if (fileSizeBytes <= 0)
                throw new ArgumentException("File size must be greater than zero.", nameof(fileSizeBytes));

            // Validate MIME type
            var allowedMimeTypes = new[] { "image/jpeg", "image/jpg", "image/png", "image/webp" };
            if (!allowedMimeTypes.Contains(mimeType.ToLowerInvariant()))
                throw new ArgumentException($"MIME type '{mimeType}' is not supported. Allowed types: {string.Join(", ", allowedMimeTypes)}", nameof(mimeType));

            // Validate base64 format
            try
            {
                Convert.FromBase64String(base64Data);
            }
            catch (FormatException)
            {
                throw new ArgumentException("Invalid base64 format.", nameof(base64Data));
            }

            return new ImageData(base64Data, mimeType.ToLowerInvariant(), fileName, fileSizeBytes);
        }

        /// <summary>
        /// Gets the full data URI for the image (suitable for img src attribute)
        /// </summary>
        public string GetDataUri() => $"data:{MimeType};base64,{Base64Data}";

        /// <summary>
        /// Gets the image data as byte array
        /// </summary>
        public byte[] GetBytes() => Convert.FromBase64String(Base64Data);

        public override string ToString() => $"{FileName} ({MimeType}, {FileSizeBytes / 1024}KB)";
    }
}
