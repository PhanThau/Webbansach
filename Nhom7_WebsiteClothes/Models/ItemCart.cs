using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nhom7_WebsiteClothes.Models
{
    public class ItemCart
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public string Image { get; set; }

        public int Quantity { get; set; }
        public decimal Money { get; set; }
    }
}