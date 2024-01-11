namespace MyMediaVerse.Models
{
    public class Books
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;

        public string Genre { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public string? OwnerUserID { get; set; }


    }
}
