namespace MyMediaVerse.Models
{
    public class Movies
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Genre { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public string? OwnerUserID { get; set; }


    }
}
