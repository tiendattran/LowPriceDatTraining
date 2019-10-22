using EFTutorials.CRUD.ConsoleApp.Action;
using EFTutorials.CRUD.ConsoleApp.Common;
using EFTutorials.CRUD.ConsoleApp.Helper;
using EFTutorials.CRUD.Service;
using EFTutorials.CRUD.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTutorials.CRUD.ConsoleApp.Command
{
    public class PriceCommand : ICommand
    {
        public string Description => "Price Management";
        private readonly PriceAction priceAction;
        private IPriceService priceService;

        public NewPriceModel PriceModel { get; set; }

        public PriceCommand(NewPriceModel model, PriceAction priceAction)
        {
            this.priceAction = priceAction;
            this.PriceModel = model;
            priceService = new PriceService();
        }

        public void ExecuteAction()
        {
            if (priceAction == PriceAction.AddNewPrice)
            {
                AddNewPrice();
            }
        }

        private void AddNewPrice()
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
                        ShowAndUpdatePrice(selectedProduct);
                    }
                    ConsoleHelper.DrawHorizontalLine('-', 50);
                }
                ConsoleHelper.DrawHorizontalLine('=', 50);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        private void ShowAndUpdatePrice(ProductModel selectedProduct)
        {
            Console.WriteLine($"Current Price: {selectedProduct.Price}/{selectedProduct.Measure}");
            double newPriceInput = this.InputNewPrice();
            this.PriceModel = new NewPriceModel
            {
                ProductId = selectedProduct.Id,
                Price = newPriceInput,
                ApplyDate = DateTime.Now,
                IsActive = true
            };
            priceService.AddNewPrice(PriceModel);
        }

        private double InputNewPrice()
        {
            bool isDouble = false;
            double newPriceInput = -1;
            do
            {
                Console.Write("New Price: ");
                isDouble = double.TryParse(Console.ReadLine(), out newPriceInput);
            }
            while (isDouble == false && newPriceInput < 0);
            return newPriceInput;
        }
    }
}
