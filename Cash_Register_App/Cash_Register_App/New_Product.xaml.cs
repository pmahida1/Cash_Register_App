using Cash_Register_App.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cash_Register_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class New_Product : ContentPage
    {
        private SQLiteConnection conn;
        public ProductsTable pTable;
        public New_Product()
        {
            InitializeComponent();
            conn = DependencyService.Get<ProductSqlite>().GetConnection();
            conn.CreateTable<ProductsTable>();
        }

        //private void save_ToolbarItem_Clicked(object sender, EventArgs e)
        //{
           
        //}

        //private void cancel_ToolbarItem_Clicked(object sender, EventArgs e)
        //{

        //}

        private void Save_Button_Clicked(object sender, EventArgs e)
        {
            pTable = new ProductsTable();
            pTable.Name = productName.Text;
            pTable.Price = int.Parse(productPrice.Text);
            pTable.Quantity = int.Parse(productQuantity.Text);
            int x = 0;
            try
            {
                if(productName.Text != " " && productPrice.Text!= " " && productQuantity.Text != " ")
                {
                    x = conn.Insert(pTable);
                    productName.Text = "";
                    productPrice.Text = "";
                    productQuantity.Text = "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (x == 1)
            {
                DisplayAlert("Done!", "New Product Added Successfully!", "Ok");
            }
            else
            {
                DisplayAlert("Failed!", "New Product is not Added Successfully!", "Ok");

            }
        }

        private async void Cancel_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ManagerPage());
        }
    }
}