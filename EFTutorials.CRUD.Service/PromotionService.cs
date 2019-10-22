using EFTutorials.CRUD.Repository;
using EFTutorials.CRUD.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace EFTutorials.CRUD.Service
{
    public class PromotionService : IPromotionService
    {
        private IPromotionRepository promotionRepository;
        private IProductRepository productRepository;
        public PromotionService()
        {
            promotionRepository = new PromotionRepository();
            productRepository = new ProductRepository();
        }

        public void AddPromotion(PromotionModel model)
        {
            try
            {

                var promotion = new Entity.Promotion
                {
                    IsActive = model.IsActive,
                    OnlyMembership = model.OnlyMembership,
                    DiscountQuantity = model.DiscountQuantity,
                    RequiredQuantity = model.RequiredQuantity,
                    PromotionTypeId = model.PromotionTypeId,
                    Saleoff = model.Saleoff,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    ProductPromotions = model.Products.Select(p => new Entity.ProductPromotion
                                                        { 
                                                            IsActive = true, 
                                                            ProductId = p.Id 
                                                        }).ToList()
                };
                promotionRepository.Insert(promotion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PromotionModel GetPromotionForMembership()
        {
            PromotionModel promotion = null;
            try
            {
                promotion = promotionRepository
                            .FindBy(p => p.IsActive == true
                                        && p.OnlyMembership == true
                                        && p.StartDate <= DateTime.Now && p.EndDate >= DateTime.Now)
                            .OrderByDescending(p => p.StartDate)
                            .Select(p => new PromotionModel
                            {
                                Id = p.Id,
                                Name = p.Name,
                                StartDate = p.StartDate,
                                EndDate = p.EndDate,
                                Saleoff = p.Saleoff,
                                OnlyMembership = p.OnlyMembership,
                            }).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return promotion;
        }

        public List<PromotionModel> GetPromotionOfAProduct(int productId)
        {
            List<PromotionModel> promotions = null;
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return promotions;
        }
    }

    public interface IPromotionService
    {
        PromotionModel GetPromotionForMembership();
        List<PromotionModel> GetPromotionOfAProduct(int productId);
        void AddPromotion(PromotionModel model);
    }
}
