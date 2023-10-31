using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace CarRental.Models
{
    public partial class DatXe
    {
        public DatXe()
        {
            ThanhToans = new HashSet<ThanhToan>();
        }

        public int Id { get; set; }
        public string TenKhach { get; set; }
        public string BienSo { get; set; }
        public DateTime? NgayDat { get; set; }
        public DateTime? NgayTra { get; set; }
        public string MoTa { get; set; }
        [NotMapped]
        public string SDT { get; set; }
        public virtual Xe BienSoNavigation { get; set; }
        public virtual Customer TenKhachNavigation { get; set; }
        public virtual ICollection<ThanhToan> ThanhToans { get; set; }
    }
}
