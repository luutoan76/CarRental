using System;
using System.Collections.Generic;

#nullable disable

namespace CarRental.Models
{
    public partial class Loaixe
    {
        public Loaixe()
        {
            Xes = new HashSet<Xe>();
        }

        public int Id { get; set; }
        public string TenLoai { get; set; }

        public virtual ICollection<Xe> Xes { get; set; }
    }
}
