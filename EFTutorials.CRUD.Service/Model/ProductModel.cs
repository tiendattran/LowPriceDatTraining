using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTutorials.CRUD.Service.Model
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
        public int MeasureId { get; set; }
        public double Price { get; set; }
        public string Measure { get; set; }
        public List<CategoryModel> Categories { get; set; }
        public List<PromotionModel> Promotions { get; set; }
    }
}
