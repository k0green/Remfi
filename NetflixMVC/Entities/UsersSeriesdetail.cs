using System;
using System.Collections.Generic;

namespace NetflixMVC.Entities
{
    public partial class Usersseriesdetail
    {
        public int Id { get; set; }
        public int? SeriesId { get; set; }
        public int? UserId { get; set; }
        public string? Comment { get; set; }
        public float Mark { get; set; }
        public int? NumberOfView { get; set; }

        public virtual Series? Series { get; set; }
    }
}
