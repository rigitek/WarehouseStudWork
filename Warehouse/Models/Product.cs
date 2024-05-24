using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace Warehouse.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Title {  get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
    }
}
