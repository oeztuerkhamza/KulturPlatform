namespace KulturPlatform.Application.NewFolder
{
    public class SatzungNotFoundException : Exception
    {
        public SatzungNotFoundException(Guid id)
            : base($"Satzung with id '{id}' was not found.")
        {
        }
    }
}
