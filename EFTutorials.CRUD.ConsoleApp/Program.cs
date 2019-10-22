using EFTutorials.CRUD.ConsoleApp.Command;
using EFTutorials.CRUD.ConsoleApp.Common;
using EFTutorials.CRUD.ConsoleApp.Handler;
using EFTutorials.CRUD.ConsoleApp.Helper;
using EFTutorials.CRUD.Service;
using EFTutorials.CRUD.Service.Enum;
using EFTutorials.CRUD.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTutorials.CRUD.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            bool showMenu = true;
            while (showMenu)
            {
                showMenu = ShowMenu();
            }
        }
        static void ManagePrice()
        {
            string productCodeInput = "";
            double newPriceInput = -1;
            bool isDouble = false;
            bool isExist = false;
            IPriceService priceService = null;
            IProductService productService = null;
            try
            {
                priceService = new PriceService();
                productService = new ProductService();
                Console.Clear();
                Console.WriteLine("-- Price Management --");
                Console.WriteLine("==============================================");
                while (isExist == false)
                {
                    ProductModel selectedProduct = null;
                    do
                    {
                        Console.Write("Product Code: ");
                        productCodeInput = Console.ReadLine();
                        isExist = productCodeInput.ToUpper().Equals("E0");
                        if (isExist == false)
                        {
                            selectedProduct = productService.GetProductInfoByCode(productCodeInput);
                        }
                    }
                    while (isExist == false && selectedProduct == null);

                    if (isExist == false)
                    {
                        Console.WriteLine($"Current Price: {selectedProduct.Price}/{selectedProduct.Measure}");
                        do
                        {
                            Console.Write("New Price: ");
                            isDouble = double.TryParse(Console.ReadLine(), out newPriceInput);
                        }
                        while (isDouble == false && newPriceInput < 0);
                        priceService.AddNewPrice(selectedProduct.Id, newPriceInput);
                    }
                    ConsoleHelper.DrawHorizontalLine('-', 50);
                }
                ConsoleHelper.DrawHorizontalLine('=', 50);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                priceService = null;
                productService = null;
            }
        }
        static void Checkout()
        {
            string productCodeInput = "";
            float quantityInput = -1;
            bool isFloat = false;
            bool isExist = false;
            bool isMembership = false;
            double discountPercent = 10;
            string membershipInput;
            CheckoutModel checkout = new CheckoutModel();
            List<CheckoutProductModel> products = new List<CheckoutProductModel>();
            IProductService productService = null;
            IPromotionService promotionService = null;
            try
            {
                productService = new ProductService();
                promotionService = new PromotionService();
                Console.Clear();
                Console.WriteLine("-- Checkout --");
                while (isExist == false)
                {
                    ProductModel selectedProduct = null;

                    do
                    {
                        Console.Write("Product Code: ");
                        productCodeInput = Console.ReadLine();
                        isExist = productCodeInput.ToUpper().Equals("E0");
                        if (isExist == false)
                        {
                            selectedProduct = productService.GetProductInfoByCode(productCodeInput);
                        }
                    }
                    while (isExist == false && selectedProduct == null);

                    if (isExist == false)
                    {
                        do
                        {
                            Console.Write("Quantity: ");
                            isFloat = float.TryParse(Console.ReadLine(), out quantityInput);
                        }
                        while (isFloat == false && quantityInput < 0);

                        CheckoutProductModel addedProduct = products.FirstOrDefault(p => p.Id == selectedProduct.Id);
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
                            products.Add(checkoutProduct);
                        }
                    }
                }
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
                        checkout.IsMembership = isMembership;
                        checkout.MembershipDiscount = promotion.Saleoff ?? 0;
                    }
                }
                checkout.Products = products;
                productService.ShowCheckoutResult(checkout);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                productService = null;
                promotionService = null;
            }
        }
        static void ManagePromotion()
        {
            string productCodeInput = "";
            int productTypeInput = 0;
            int requiredQuantity = 0;
            int discountQuantity = 0;
            int discountPercent = 0;
            bool isInt = false;
            bool isExist = false;
            PromotionModel promotion = new PromotionModel();
            IProductService productService = null;
            IPromotionService promotionService = null; 
            try
            {
                productService = new ProductService();
                promotionService = new PromotionService();
                Console.Clear();
                Console.WriteLine("-- Promotion Management --");
                while (isExist == false)
                {
                    ProductModel selectedProduct = null;
                    do
                    {
                        Console.Write("Product Code: ");
                        productCodeInput = Console.ReadLine();
                        isExist = productCodeInput.ToUpper().Equals("E0");
                        if (isExist == false)
                        {
                            selectedProduct = productService.GetProductInfoByCode(productCodeInput);
                        }
                    }
                    while (isExist == false && selectedProduct == null);
                    
                    if (isExist == false)
                    {
                        do
                        {
                            Console.Write("Promotion Type:");
                            isInt = int.TryParse(Console.ReadLine(), out productTypeInput);
                        } while (isInt == false || 
                                (productTypeInput != (int)PromotionTypeEnum.Buy_X_Give_Y 
                                    && productTypeInput != (int)PromotionTypeEnum.SaleOff_X_Percent));

                        promotion.Products = new List<ProductModel>();
                        promotion.Products.Add(selectedProduct);
                        promotion.IsActive = true;
                        promotion.OnlyMembership = false;
                        promotion.StartDate = DateTime.Now;
                        promotion.EndDate = DateTime.Now.AddDays(15);

                        if (productTypeInput == (int)PromotionTypeEnum.Buy_X_Give_Y)
                        {
                            do
                            {
                                Console.Write("Quantity Required: ");
                                isInt = int.TryParse(Console.ReadLine(), out requiredQuantity);
                                Console.Write("Quantity Discount: ");
                                isInt = isInt && int.TryParse(Console.ReadLine(), out discountQuantity);
                            } while (isInt == false || (discountQuantity > requiredQuantity));
                            promotion.PromotionTypeId = (int)PromotionTypeEnum.Buy_X_Give_Y;
                            promotion.RequiredQuantity = requiredQuantity;
                            promotion.DiscountQuantity = discountQuantity;
                        }
                        else if (productTypeInput == (int)PromotionTypeEnum.SaleOff_X_Percent)
                        {
                            do
                            {
                                Console.Write("Discount (percentage): ");
                                isInt = int.TryParse(Console.ReadLine(), out discountPercent);
                            } while (isInt == false || (discountPercent < 0 || discountPercent >= 100));
                            promotion.PromotionTypeId = (int)PromotionTypeEnum.SaleOff_X_Percent;
                            promotion.Saleoff = discountPercent;
                        }
                        promotionService.AddPromotion(promotion);
                    } 
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                productService = null;
            }
        }

        static bool ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("-- LOW PRICE MANAGEMENT -- ");
            Console.WriteLine("1) Exit");
            Console.WriteLine("2) Checkout");
            Console.WriteLine("3) Price Management");
            Console.WriteLine("4) Promotion Management");
            Console.Write("\r\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    return false;
                case "2":
                    //Checkout();
                    RunCheckoutProcess();
                    return true;
                case "3":
                    //ManagePrice();
                    RunPriceManagementProcess();
                    return true;
                case "4":
                    //ManagePromotion();
                    RunPromotionManagementProcess();
                    return true;
                default:
                    // EXIT                   
                    return false;
            }
        }

        static void RunCheckoutProcess()
        {            
            CheckoutModel model = new CheckoutModel();
            ICommandHandler checkoutHandler = new CommandHandler();
            CheckoutCommand checkoutCmd = new CheckoutCommand(model, ConsoleApp.Action.CheckoutAction.AddOrderedItem);
            CommonUtils.ExecuteCommand(checkoutHandler, checkoutCmd);
            model = checkoutCmd.Checkout;
            checkoutCmd = new CheckoutCommand(model, ConsoleApp.Action.CheckoutAction.CheckForMembership);
            CommonUtils.ExecuteCommand(checkoutHandler, checkoutCmd);
            model = checkoutCmd.Checkout;
            checkoutCmd = new CheckoutCommand(model, ConsoleApp.Action.CheckoutAction.ShowCheckoutResult);
            CommonUtils.ExecuteCommand(checkoutHandler, checkoutCmd);
        }        

        static void RunPriceManagementProcess()
        {
            NewPriceModel model = new NewPriceModel();
            ICommandHandler handler = new CommandHandler();
            PriceCommand priceCmd = new PriceCommand(model, ConsoleApp.Action.PriceAction.AddNewPrice);
            CommonUtils.ExecuteCommand(handler, priceCmd);
        }

        static void RunPromotionManagementProcess()
        {
            PromotionModel model = new PromotionModel();
            ICommandHandler handler = new CommandHandler();
            PromotionCommand promotionCmd = new PromotionCommand(model, ConsoleApp.Action.PromotionAction.ManagePromotion);
            CommonUtils.ExecuteCommand(handler, promotionCmd);
        }
    }
}
