using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTutorials.CRUD.Service.Model
{
    public class PromotionModel
    {
        public int Id { get; set; }
        public Nullable<int> PromotionTypeId { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<int> RequiredQuantity { get; set; }
        public Nullable<int> DiscountQuantity { get; set; }
        public Nullable<double> Saleoff { get; set; }
        public bool IsActive { get; set; }
        public bool OnlyMembership { get; set; }
        public List<ProductModel> Products { get; set; }

    }
}
