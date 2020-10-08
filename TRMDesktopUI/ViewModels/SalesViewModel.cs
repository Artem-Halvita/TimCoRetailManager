using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRMDesktopUI.Library.Api;
using TRMDesktopUI.Library.Models;

namespace TRMDesktopUI.ViewModels
{
    public class SalesViewModel : Screen
    {
        IProductEndpoint _productEndpoint;

        public SalesViewModel(IProductEndpoint productEndpoint)
        {
            _productEndpoint = productEndpoint;
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadProducts();
        }

        public async Task LoadProducts()
        {
            var productList = await _productEndpoint.GetAll();
            Products = new BindingList<ProductModel>(productList);
        }

        private BindingList<ProductModel> _products;

        public BindingList<ProductModel> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                NotifyOfPropertyChange(() => Products);
            }
        }

        private BindingList<string> _cart;

        public BindingList<string> Cart
        {
            get { return _cart; }
            set
            {
                _cart = value;
                NotifyOfPropertyChange(() => Cart);
            }
        }


        private int _itemQuality;

        public int ItemQuality
        {
            get { return _itemQuality; }
            set
            {
                _itemQuality = value;
                NotifyOfPropertyChange(() => Products);
            }
        }

        public string SubTotal
        {
            get
            {
                // TODO - Replace with calculation
                return $"0.00";
            }
        }

        public string Tax
        {
            get
            {
                // TODO - Replace with calculation
                return $"0.00";
            }
        }

        public string Total
        {
            get
            {
                // TODO - Replace with calculation
                return $"0.00";
            }
        }


        public bool CanAddToCart
        {
            get
            {
                bool output = false;

                // Make sure something is selected
                // Makr sure there is an item quantity

                return output;
            }
        }


        public void AddToCart()
        {

        }

        public bool CanRemoveToCart
        {
            get
            {
                bool output = false;

                // Make sure something is selected
                // Make sure there is an item quantity

                return output;
            }
        }

        public bool CanCheckOut
        {
            get
            {
                bool output = false;

                // Make sure there is something is the cart

                return output;
            }
        }


        public void RemoveFromCart()
        {

        }
    }
}
