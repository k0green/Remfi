using System;
using System.Collections.Generic;

namespace NetflixMVC.Entities
{
    public partial class User
    {
        public User()
        {
            Userfilms = new HashSet<Userfilm>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public int? RoleId { get; set; }

        public virtual Role? Role { get; set; }
        public virtual ICollection<Userfilm> Userfilms { get; set; }
    }
}
