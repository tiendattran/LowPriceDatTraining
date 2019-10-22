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
    public class PromotionCommand : ICommand
    {
        public string Description => "Promotion Management";
        private readonly PromotionAction promotionAction;
        private IPromotionService promotionService;
        private IProductService productService;
        public PromotionModel PromotionModel { get; set; }

        public PromotionCommand(PromotionModel model, PromotionAction action)
        {
            this.PromotionModel = model;
            this.promotionAction = action;
            this.promotionService = new PromotionService();
            this.productService = new ProductService();
        }

        public void ExecuteAction()
        {
            if (promotionAction == PromotionAction.ManagePromotion)
            {
                ManagePromotion();
            }
            else if (promotionAction == PromotionAction.InputPromotionType)
            {
                InputPromotionType();
            }
        }
        private void InputPromotionType()
        {
            bool isInt = false;
            int promotionTypeInput = 0;
            do
            {
                Console.Write("Promotion Type:");
                isInt = int.TryParse(Console.ReadLine(), out promotionTypeInput);
            } while (isInt == false ||
                             (promotionTypeInput != (int)PromotionTypeEnum.Buy_X_Give_Y
                                 && promotionTypeInput != (int)PromotionTypeEnum.SaleOff_X_Percent));
            PromotionModel.PromotionTypeId = promotionTypeInput;
        }

        private void InputQuantityDiscount()
        {
            bool isInt = false;
            int requiredQuantity = 0;
            int discountQuantity = 0;
            do
            {
                Console.Write("Quantity Required: ");
                isInt = int.TryParse(Console.ReadLine(), out requiredQuantity);
                Console.Write("Quantity Discount: ");
                isInt = isInt && int.TryParse(Console.ReadLine(), out discountQuantity);
            } while (isInt == false 
                    || (discountQuantity <= 0) 
                    || (discountQuantity >= 100)
                    || (requiredQuantity <= 0)
                    || (requiredQuantity >= 100)
                    || (discountQuantity > requiredQuantity)
                    );
            
            PromotionModel.RequiredQuantity = requiredQuantity;
            PromotionModel.DiscountQuantity = discountQuantity;
        }
        private void InputPercentDiscount()
        {
            bool isFloat = false;
            float discountPercent = 0;
            do
            {
                Console.Write("Discount (percentage): ");
                isFloat = float.TryParse(Console.ReadLine(), out discountPercent);
            } while (isFloat == false 
                        || discountPercent <= 0 
                        || discountPercent >= 100);
            
            PromotionModel.Saleoff = discountPercent;
        }
        private void AddAdditionalDataToPromotion(ProductModel selectedProduct)
        {
            PromotionModel.Products = new List<ProductModel>();
            PromotionModel.Products.Add(selectedProduct);
            PromotionModel.IsActive = true;
            PromotionModel.OnlyMembership = false;
            PromotionModel.StartDate = DateTime.Now;
            PromotionModel.EndDate = DateTime.Now.AddDays(15);
        }
    
        private void ManagePromotion()
        {
            bool isExist = false;
            
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
                        PromotionModel = new PromotionModel();
                        InputPromotionType();
                        if (PromotionModel.PromotionTypeId == (int)PromotionTypeEnum.Buy_X_Give_Y)
                        {
                            InputQuantityDiscount();
                        }
                        else if (PromotionModel.PromotionTypeId == (int)PromotionTypeEnum.SaleOff_X_Percent)
                        {
                            InputPercentDiscount();
                        }
                        AddAdditionalDataToPromotion(selectedProduct);
                        promotionService.AddPromotion(PromotionModel);
                    }                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        
    }
}
