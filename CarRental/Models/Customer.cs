using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using MailKit.Net.Smtp;
using MimeKit;
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

    public class EmailConfiguration
    {
        public string From { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
