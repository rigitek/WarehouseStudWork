﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Models
{
    public class ShopCart
    {
        public int Id { get; set; }
        public int ProductAmount { get; set; }
        public double ProductPrice { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int OrderId { get; set; }
        public Order? Order { get; set; }
    }
}
