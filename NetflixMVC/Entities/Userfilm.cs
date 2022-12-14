using System;
using System.Collections.Generic;

namespace NetflixMVC.Entities
{
    public partial class Userfilm
    {
        public int? UserId { get; set; }
        public int? FilmId { get; set; }
        public int NumberOfView { get; set; }
        public int Id { get; set; }
        public bool Favorites { get; set; }

        public virtual Film? Film { get; set; }
        public virtual User? User { get; set; }
    }
}
