using System;
using System.Collections.Generic;

#nullable disable

namespace CarRental.Models
{
    public partial class NhanVien
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public string Pass { get; set; }
        public string ChucVu { get; set; }
        public string Sdt { get; set; }
        public string Email { get; set; }
        public string CaLam { get; set; }
    }
}
