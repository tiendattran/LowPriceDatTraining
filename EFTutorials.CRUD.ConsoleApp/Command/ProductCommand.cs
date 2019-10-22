using EFTutorials.CRUD.ConsoleApp.Action;
using EFTutorials.CRUD.ConsoleApp.Common;
using EFTutorials.CRUD.Service;
using EFTutorials.CRUD.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTutorials.CRUD.ConsoleApp.Command
{
    public class ProductCommand : ICommand
    {
        public string Description => "Input Product Code";
        private ProductAction action;
        public ProductModel Product { get; set; }
        private IProductService productService;
        public ProductCommand(ProductModel product, ProductAction action)
        {
            this.Product = product;
            this.action = action;
            productService = new ProductService();
        }

        public void ExecuteAction()
        {
            if (action == ProductAction.InputProductCode)
            {
                InputProductCode();
            }          
        }
        private void InputProductCode()
        {
            string productCodeInput = "";
            while (Product == null)
            {
                Console.Write("Product Code: ");
                productCodeInput = Console.ReadLine();
                if (productCodeInput.ToUpper().Equals(CommonUtils.EXIT_STRING))
                {
                    Product = new ProductModel { Code = CommonUtils.EXIT_STRING };
                }
                else
                {
                    Product = productService.GetProductInfoByCode(productCodeInput);
                }
            }           
        }
    }
}
