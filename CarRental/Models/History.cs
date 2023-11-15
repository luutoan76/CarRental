using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Models
{
    public class History
    {
        public string Ten { get; set; }
        public string Hinh { get; set; }
        public int Gia { get; set; }
        public DateTime? ngaydat { get; set; }
        public DateTime? ngaytra { get; set; }

        public string TrangThai { get; set; }
    }
}
