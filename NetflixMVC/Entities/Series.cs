using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NetflixMVC.Entities
{
    public partial class Series
    {
        public int? FilmId { get; set; }
        [Key]
        public int Id { get; set; }
        public int? NumdersOfViews { get; set; }
        public int? NumberOfSeries { get; set; }
        public int? Season { get; set; }

        public virtual Film? Film { get; set; }
    }
}
