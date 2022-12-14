using System;
using System.Collections.Generic;

namespace NetflixMVC.Entities
{
    public partial class Series
    {
        public Series()
        {
            Usersseriesdetails = new HashSet<Usersseriesdetail>();
        }

        public int? FilmId { get; set; }
        public int? NumdersOfViews { get; set; }
        public int? NumberOfSeries { get; set; }
        public int? Season { get; set; }
        public int Id { get; set; }

        public virtual Film? Film { get; set; }
        public virtual ICollection<Usersseriesdetail> Usersseriesdetails { get; set; }
    }
}
