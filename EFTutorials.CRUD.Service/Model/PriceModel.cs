using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTutorials.CRUD.Service.Model
{
    public class PriceModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public double Value { get; set; }
    }
}
