using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NetflixMVC.Entities
{
    public partial class Usersseriesdetail
    {
        [Key]
        public int? Id { get; set; }
        public int? SeriesId { get; set; }
        public int? UserId { get; set; }
        public string? Comment { get; set; }
        public float Mark { get; set; }
        public int? NumberOfView { get; set; }

        public virtual Series? Series { get; set; }
    }
}
