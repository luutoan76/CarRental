using System;
using System.Collections.Generic;

#nullable disable

namespace CarRental.Models
{
    public partial class Customer
    {
        public Customer()
        {
            DatXes = new HashSet<DatXe>();
            ThanhToans = new HashSet<ThanhToan>();
        }

        public int Id { get; set; }
        public string TenKhach { get; set; }
        public string Sdt { get; set; }
        public string Email { get; set; }
        public int? Tuoi { get; set; }
        public string Pass { get; set; }
        public string DiaChi { get; set; }

        public virtual ICollection<DatXe> DatXes { get; set; }
        public virtual ICollection<ThanhToan> ThanhToans { get; set; }
    }
}
