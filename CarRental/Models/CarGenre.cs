using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
namespace CarRental.Models
{
    public class CarGenre
    {
        public List<Xe>? Xes { get; set; }
        public SelectList? Genres { get; set; }
        public string? XeGenre { get; set; }
        public string? SearchString { get; set; }

    }
}
