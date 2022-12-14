using System;
using System.Collections.Generic;

namespace NetflixMVC.Entities
{
    public partial class Usersfilmsdetail
    {
        public int Id { get; set; }
        public int? FilmId { get; set; }
        public int? UserId { get; set; }
        public string? Comment { get; set; }
        public float Mark { get; set; }
        public bool Favorites { get; set; }

        public virtual Film? Film { get; set; }
    }
}
