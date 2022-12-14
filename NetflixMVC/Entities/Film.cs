using System;
using System.Collections.Generic;

namespace NetflixMVC.Entities
{
    public partial class Film
    {
        public Film()
        {
            SeriesNavigation = new HashSet<Series>();
            Userfilms = new HashSet<Userfilm>();
            Usersfilmsdetails = new HashSet<Usersfilmsdetail>();
        }

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
        public int? NumberOfView { get; set; }
        public bool? CheckSeries { get; set; }

        public virtual ICollection<Series> SeriesNavigation { get; set; }
        public virtual ICollection<Userfilm> Userfilms { get; set; }
        public virtual ICollection<Usersfilmsdetail> Usersfilmsdetails { get; set; }
    }
}
