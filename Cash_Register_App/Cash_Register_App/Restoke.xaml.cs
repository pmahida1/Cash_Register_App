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
    public partial class Restoke : ContentPage
    {
        public SQLiteConnection conn;
        public ProductsTable pTable;
        public int productId;
        private int productPrice;
        public string productName;

        public Restoke()
        {
            InitializeComponent();
            conn = DependencyService.Get<ProductSqlite>().GetConnection();
            conn.CreateTable<ProductsTable>();
            DisplayDetails();
        }

        public void DisplayDetails()
        {
            var data = (from x in conn.Table<ProductsTable>() select x);
            itemsList.ItemsSource = data;
        }

        private void Restock(object sender, EventArgs e)
        {
            if(product_Qty.Text == null || productName == null)
            {
                DisplayAlert("Error", "You have to select an Item and provide new Quantity", "Ok");
            }
            else
            {
                var newQTy = pTable.Quantity + int.Parse(product_Qty.Text);
                pTable = new ProductsTable();
                pTable.ID = productId;
                pTable.Name = productName;
                pTable.Price = productPrice;
                pTable.Quantity = newQTy;
                try
                {
                    conn.Update(pTable);
                    product_Qty.Text = "";
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            
            DisplayDetails();
        }

        private async void cancel(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ManagerPage());
            
        }

        private void select(object sender, SelectedItemChangedEventArgs e)
        {
            pTable = itemsList.SelectedItem as ProductsTable;
            productName = pTable.Name;
            productId = pTable.ID;
            productPrice = pTable.Price;
        }
    }
}