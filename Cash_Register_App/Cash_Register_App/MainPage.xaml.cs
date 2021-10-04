using Cash_Register_App.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Cash_Register_App
{
    public partial class MainPage : ContentPage
    {
        public SQLiteConnection conn;
        public ProductsTable pTable;
        public int productId;
        private int productPrice;
        int currentState = 1;
        string mathOperator;
        double firstNumber, secondNumber;
        public MainPage()
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

        

        private void Number_Button_Clicked(object sender, EventArgs e)
        {
            if (Type.Text == "Type")
            {
                DisplayAlert("Alert!", "Please Select an Item from Below List!", "Ok");
            }
            else
            {
                Button button = (Button)sender;
                string pressed = button.Text;
                if (this.Quantity.Text == "0" || currentState < 0)
                {
                    this.Quantity.Text = "";
                    if (currentState < 0)
                        currentState *= -1;
                }

                this.Quantity.Text += pressed;
                if(int.Parse(Quantity.Text) > pTable.Quantity)
                {
                    DisplayAlert("Error", "quantity exceed from the stock", "Ok");
                    Quantity.Text = "0";
                    totalPrice.Text = "0";
                }
                else
                {
                    double number;
                    if (double.TryParse(this.Quantity.Text, out number))
                    {
                        this.Quantity.Text = number.ToString("N0");
                        if (currentState == 1)
                        {
                            firstNumber = number;
                        }
                        else
                        {
                            secondNumber = number;
                        }
                    }
                    int quantity = int.Parse(Quantity.Text);
                    var productsTable = itemsList.SelectedItem as ProductsTable;
                    totalPrice.Text = (productsTable.Price * quantity).ToString();
                    Type.Text = productsTable.Name;
                   
                }
               
            }
        }

        private async void managerButton_Clicked(object sender, EventArgs e)
        {
           await Navigation.PushAsync(new ManagerPage());
            Navigation.RemovePage(this);
            
        }

        private void select(object sender, SelectedItemChangedEventArgs e)
        {
            int quantity = int.Parse(Quantity.Text);
            pTable = itemsList.SelectedItem as ProductsTable;
            totalPrice.Text = (pTable.Price * quantity).ToString();
            Type.Text = pTable.Name ;
            productId = pTable.ID;
            productPrice = pTable.Price;
        }
       

        private void buy_Product(object sender, EventArgs e)
        {
            if(Quantity.Text != "0")
            {
                int quantity = int.Parse(Quantity.Text);
                var newQTy = pTable.Quantity - quantity;
                pTable = new ProductsTable();
                pTable.ID = productId;
                pTable.Name = Type.Text;
                pTable.Price = productPrice;
                pTable.Quantity = newQTy;
                try
                {
                    conn.Update(pTable);
                    Type.Text = "Type";
                    Quantity.Text = "0";
                    totalPrice.Text = "0";

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                DisplayDetails();
            }
            else
            {
                DisplayAlert("Not Selected", "Please Select the Product", "Ok");
            }
            
        }

    }
}
