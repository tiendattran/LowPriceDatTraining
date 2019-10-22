using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTutorials.CRUD.Service.Model
{
    public class CheckoutModel
    {
        public IList<CheckoutProductModel> Products { get; set; }
        public bool IsMembership { get; set; }
        public double MembershipDiscount { get; set; }
    }
}
