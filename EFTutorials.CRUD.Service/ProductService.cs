using EFTutorials.CRUD.Repository;
using EFTutorials.CRUD.Service.Enum;
using EFTutorials.CRUD.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTutorials.CRUD.Service
{
    public class ProductService : IProductService
    {
        private IProductRepository productRepository;
        public ProductService()
        {
            productRepository = new ProductRepository();
        }

        public ProductModel GetProductInfoByCode(string code)
        {
            ProductModel productModel = null;
            try
            {
                var product = productRepository
                                .FindBy(p => p.Code.Equals(code.ToUpper())
                                                && p.IsActive == true)
                                .FirstOrDefault();
                if (product != null)
                {
                    var price = product.Prices.OrderByDescending(p => p.ApplyDate).First();
                    productModel = new ProductModel()
                    {
                        Id = product.Id,
                        Code = product.Code,
                        IsActive = product.IsActive,
                        Name = product.Name,
                        Price = price.Value,
                        Measure = product.Measure.Name,
                        Categories = product.ProductCategories
                            .Select(c => new CategoryModel {
                                Id = c.CategoryId,
                                Name = c.Category.Name
                            }).ToList(),
                        Promotions = product.ProductPromotions                                       
                                        .Select(pr => new PromotionModel { 
                                            Id = pr.ProductId,
                                            Name = pr.Promotion.Name,
                                            IsActive = pr.IsActive,
                                            PromotionTypeId = pr.Promotion.PromotionTypeId,
                                            StartDate = pr.Promotion.StartDate,
                                            EndDate = pr.Promotion.EndDate,
                                            Saleoff = pr.Promotion.Saleoff,
                                            RequiredQuantity = pr.Promotion.RequiredQuantity,
                                            DiscountQuantity = pr.Promotion.DiscountQuantity,
                                        })
                                        .Where(p => p.IsActive == true
                                            && p.StartDate <= DateTime.Now
                                            && p.EndDate >= DateTime.Now
                                            && p.OnlyMembership == false)
                                        .ToList()
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
          
            return productModel;
        }

        public void ShowCheckoutResult(CheckoutModel checkout)
        {
            try
            {               
                Console.WriteLine("=====================================");
                double total = 0;
                double discount = 0;
                double totalDiscount = 0;
                List<GiftModel> gifts = new List<GiftModel>();
                
                foreach (var product in checkout.Products)
                {
                    double productTotal = product.Quantity * product.Price;
                    total = total + productTotal;
                    Console.WriteLine($"{product.Code} - {product.Name} - {product.Quantity}: {productTotal}");
                    if (product.Promotions.Count > 0)
                    {
                        Console.WriteLine($"------ Promotions -------");
                        foreach (var promotion in product.Promotions)
                        {
                            if (promotion.PromotionTypeId == (int)PromotionTypeEnum.Buy_X_Give_Y && product.Quantity >= promotion.RequiredQuantity)
                            {
                                int giftCount = promotion.DiscountQuantity ?? 0;
                                gifts.Add(new GiftModel { ProductName = product.Name, GiftCount = giftCount });
                                Console.WriteLine($"{promotion.Name} - Discount Quantity: {giftCount}");
                            }

                            if (promotion.PromotionTypeId == (int)PromotionTypeEnum.SaleOff_X_Percent)
                            {
                                double discountPercent = (productTotal * (promotion.Saleoff ?? 0))/100;
                                totalDiscount = totalDiscount + discountPercent;
                                Console.WriteLine($"{promotion.Name} - Discount ({promotion.Saleoff}): -{discountPercent}");
                            }
                        }
                    }
                }                
                Console.WriteLine("-------------------------------------");
                Console.WriteLine($"Total: {total}");    
                
                if (checkout.IsMembership)
                {
                    discount = total * checkout.MembershipDiscount / 100;
                    total = total - discount;
                    Console.WriteLine($"Discount Membership: -{discount}");
                }

                if (gifts.Count > 0)
                {
                    Console.WriteLine($"--- Additional Gifts ----");
                    foreach (var gift in gifts)
                    {
                        Console.WriteLine($"{gift.ProductName}: {gift.GiftCount}");
                    }
                    Console.WriteLine($"-------------------------");
                }

                if (totalDiscount > 0)
                {
                    total = total - totalDiscount;
                    Console.WriteLine($"Discount Programs: -{totalDiscount}");
                    Console.WriteLine($"-------------------------");
                }
               
                Console.WriteLine($"Final Total: {total}");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public interface IProductService
    {
        ProductModel GetProductInfoByCode(string code);
        void ShowCheckoutResult(CheckoutModel checkout);        
    }
}
