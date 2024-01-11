namespace MyMediaVerse.Models
{
    public class Articles
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string URL { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public string? OwnerUserID { get; set; }
    }
}
