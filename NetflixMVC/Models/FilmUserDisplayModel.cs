namespace NetflixMVC.Models
{
    public class FilmUserDisplayModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? DurationTime { get; set; }
        public float? Rating { get; set; }
        public string? ReleaseData { get; set; }
        public string? Country { get; set; }
        public string? Producer { get; set; }
        public string? Genre { get; set; }
        public string? Description { get; set; }
        public string? Link { get; set; }
        public bool? CheckSeries { get; set; }
        public int? UserId { get; set; }
        public int? FilmId { get; set; }
        public int NumberOfView { get; set; }
        public bool Favorites { get; set; }
    }
}
