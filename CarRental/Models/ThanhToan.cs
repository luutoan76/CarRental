using System;
using System.Collections.Generic;

#nullable disable

namespace CarRental.Models
{
    public partial class ThanhToan
    {
        public int IdBill { get; set; }
        public string TenKhach { get; set; }
        public int? Id { get; set; }
        public string BienSo { get; set; }
        public int? PhiTre { get; set; }
        public DateTime? NgayIn { get; set; }
        public int? Total { get; set; }

        public virtual Xe BienSoNavigation { get; set; }
        public virtual DatXe IdNavigation { get; set; }
        public virtual Customer TenKhachNavigation { get; set; }
    }
}
