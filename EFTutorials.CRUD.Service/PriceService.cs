using EFTutorials.CRUD.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTutorials.CRUD.Service
{
    public class PriceService : IPriceService
    {
        private IPriceRepository priceRepository;
        public PriceService()
        {
            priceRepository = new PriceRepository();
        }

        public void AddNewPrice(int productId, double price)
        {
            try
            {
                var newPrice = new Entity.Price
                {
                    ProductId = productId,
                    Value = price,
                    ApplyDate = DateTime.Now,
                    IsActive = true
                };
                priceRepository.Insert(newPrice);
            }
            catch (Exception ex)
            {
                throw ex;
            }         
        }
    }

    public interface IPriceService
    {
        void AddNewPrice(int productId, double price);
    }
}
