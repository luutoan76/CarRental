using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

#nullable disable

namespace CarRental.Models
{
    public partial class Xe
    {
        public Xe()
        {
            DatXes = new HashSet<DatXe>();
            ThanhToans = new HashSet<ThanhToan>();
        }

        public int Id { get; set; }
        public string BienSo { get; set; }
        public string Ten { get; set; }
        public string TenLoai { get; set; }
        public string MoTa { get; set; }
        public int Gia { get; set; }
        public string TrangThai { get; set; }
        public string Hinh { get; set; }
        [NotMapped]
        [DisplayName("Upload file")]
        public IFormFile ImageFile { get; set; }
        public virtual Loaixe TenLoaiNavigation { get; set; }
        public virtual ICollection<DatXe> DatXes { get; set; }
        public virtual ICollection<ThanhToan> ThanhToans { get; set; }
       
    }
    public class Histroy
    {
        public string Ten { get; set; }
        public string Hinh { get; set; }
        public int Gia { get; set; }
        public DateTime? ngaydat { get; set; }
        public DateTime? ngaytra { get; set; }

    }
}
