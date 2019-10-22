using EFTutorials.CRUD.ConsoleApp.Action;
using EFTutorials.CRUD.ConsoleApp.Common;
using EFTutorials.CRUD.ConsoleApp.Helper;
using EFTutorials.CRUD.Service;
using EFTutorials.CRUD.Service.Enum;
using EFTutorials.CRUD.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTutorials.CRUD.ConsoleApp.Command
{
    public class CheckoutCommand : ICommand
    {
        private readonly CheckoutAction checkoutAction;
        private IProductService productService;
        private IPromotionService promotionService;
        public CheckoutModel Checkout { get; set; }

        public string Description => "Checkout...";
        public CheckoutCommand(CheckoutModel checkout, CheckoutAction action)
        {
            checkoutAction = action;
            Checkout = checkout;
            productService = new ProductService();
            promotionService = new PromotionService();
        }

        public void ExecuteAction()
        {
            if (checkoutAction == CheckoutAction.AddOrderedItem)
            {
                AddOrderItem();
            }          
            else if (checkoutAction == CheckoutAction.CheckForMembership)
            {
                CheckForMembership();
            }
            else if (checkoutAction == CheckoutAction.ShowCheckoutResult)
            {
                ShowCheckoutResult();
            }
        }
        private void AddOrderItem()
        {
            float quantityInput;
            bool isExist = false;
            Checkout.Products = new List<CheckoutProductModel>();
            try
            {
                ConsoleHelper.CleanTheConsole(this.Description);
                while (isExist == false)
                {
                    ProductModel selectedProduct = null;
                    var inputProductCodeCommand = new ProductCommand(selectedProduct, ProductAction.InputProductCode);
                    inputProductCodeCommand.ExecuteAction();
                    selectedProduct = inputProductCodeCommand.Product;
                    if (selectedProduct != null)
                    {
                        isExist = selectedProduct.Code.Equals(CommonUtils.EXIT_STRING);
                    }
                    if (isExist == false)
                    {
                        quantityInput = this.InputQuantity();
                        AddQuantityToCheckoutProductModel(selectedProduct, quantityInput);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void AddQuantityToCheckoutProductModel(ProductModel selectedProduct, float quantityInput)
        {
            CheckoutProductModel addedProduct = Checkout.Products.FirstOrDefault(p => p.Id == selectedProduct.Id);
            if (addedProduct != null)
            {
                addedProduct.Quantity++;
            }
            else
            {
                CheckoutProductModel checkoutProduct = new CheckoutProductModel
                {
                    Id = selectedProduct.Id,
                    Code = selectedProduct.Code,
                    Name = selectedProduct.Name,
                    Quantity = quantityInput,
                    Price = selectedProduct.Price,
                    Categories = selectedProduct.Categories,
                    Promotions = selectedProduct.Promotions
                };
                Checkout.Products.Add(checkoutProduct);
            }
        }
        private float InputQuantity()
        {
            bool isFloat = false;
            float quantityInput = 0;
            do
            {
                Console.Write("Quantity: ");
                isFloat = float.TryParse(Console.ReadLine(), out quantityInput);
            }
            while (isFloat == false && quantityInput < 0);
            return quantityInput;
        }  
        
        private void CheckForMembership()
        {
            bool isMembership = false;
            string membershipInput;

            do
            {
                Console.Write("Membership (Y/N)");
                membershipInput = Console.ReadLine();
            } while (membershipInput.ToUpper().Equals("Y") == false
                           && membershipInput.ToUpper().Equals("N") == false);

            isMembership = membershipInput.ToUpper().Equals("Y");
            if (isMembership)
            {
                var promotion = promotionService.GetPromotionForMembership();
                if (promotion != null)
                {
                    Checkout.IsMembership = isMembership;
                    Checkout.MembershipDiscount = promotion.Saleoff ?? 0;
                }
            }
        }
        
        private void ShowCheckoutResult()
        {
            try
            {
                ConsoleHelper.DrawHorizontalLine('=', 50);
                double total = 0;
                double membershipDiscount = 0;
                double totalDiscount = 0;
                List<GiftModel> gifts = new List<GiftModel>();

                foreach (var product in Checkout.Products)
                {
                    double productTotal = product.Quantity * product.Price;
                    total = total + productTotal;
                    Console.WriteLine($"{product.Code} - {product.Name} - {product.Quantity}: {productTotal}");
                    if (product.Promotions.Count > 0)
                    {
                        Console.WriteLine($"------ Promotions -------");
                        foreach (var promotion in product.Promotions)
                        {
                            if (promotion.OnlyMembership == false)
                            {
                                if (promotion.PromotionTypeId == (int)PromotionTypeEnum.Buy_X_Give_Y && product.Quantity >= promotion.RequiredQuantity)
                                {
                                    int giftCount = promotion.DiscountQuantity ?? 0;
                                    gifts.Add(new GiftModel { ProductName = product.Name, GiftCount = giftCount });
                                    Console.WriteLine($"{promotion.Name} - Discount Quantity: {giftCount}");
                                }

                                if (promotion.PromotionTypeId == (int)PromotionTypeEnum.SaleOff_X_Percent)
                                {
                                    double discountPercent = (productTotal * (promotion.Saleoff ?? 0)) / 100;
                                    totalDiscount = totalDiscount + discountPercent;
                                    Console.WriteLine($"{promotion.Name} - Discount ({promotion.Saleoff}): -{discountPercent}");
                                }
                            }
                        }
                    }
                    ConsoleHelper.DrawHorizontalLine('-', 50);
                }
                Console.WriteLine("----------- SUMMARY -----------");
                ConsoleHelper.DrawHorizontalLine('=', 50);
                Console.WriteLine($"Total: {total}");

                if (Checkout.IsMembership)
                {
                    membershipDiscount = total * Checkout.MembershipDiscount / 100;
                    total = total - membershipDiscount;
                    Console.WriteLine($"Discount Membership: -{membershipDiscount}");
                }

                if (gifts.Count > 0)
                {
                    Console.WriteLine($"--- Additional Gifts ----");
                    foreach (var gift in gifts)
                    {
                        Console.WriteLine($"{gift.ProductName}: {gift.GiftCount}");
                    }
                    ConsoleHelper.DrawHorizontalLine('-', 50);
                }

                if (totalDiscount > 0)
                {
                    total = total - totalDiscount;
                    Console.WriteLine($"Discount Programs: -{totalDiscount}");
                    ConsoleHelper.DrawHorizontalLine('-', 50);
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
}
