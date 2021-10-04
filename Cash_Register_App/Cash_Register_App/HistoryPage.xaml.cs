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
    public partial class HistoryPage : ContentPage
    {
        public SQLiteConnection conn;
        public HistoryPage()
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

        private void select(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}