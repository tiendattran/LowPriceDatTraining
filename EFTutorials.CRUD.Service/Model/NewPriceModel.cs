using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTutorials.CRUD.Service.Model
{
    public class NewPriceModel
    {
        public int ProductId { get; set; }
        public double Price { get; set; }
        public DateTime ApplyDate { get; set; }
        public bool IsActive { get; set; }
    }
}
