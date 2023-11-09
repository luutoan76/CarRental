using System;
using System.Collections.Generic;

#nullable disable

namespace CarRental.Models
{
    public partial class Car
    {
        public int CarId { get; set; }
        public string TenXe { get; set; }
        public string LoaiXe { get; set; }
        public string BienSo { get; set; }
        public string PhanKhoi { get; set; }
        public string MoTa { get; set; }
        public string ImagePath { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public virtual Category Category { get; set; }
    }
}
