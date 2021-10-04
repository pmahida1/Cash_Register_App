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
    public partial class ManagerPage : ContentPage
    {
        public ManagerPage()
        {
            InitializeComponent();
        }

        private async void historyButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HistoryPage());
        }

        private async void restokeButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Restoke());

        }

        private async void newProductbutton_Clicked(object sender, EventArgs e)
        {
           await Navigation.PushAsync(new New_Product());
        }

        //private async void backHome_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new MainPage());
        //    Navigation.RemovePage(this);
        //}

        private async void homeIcon_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
            Navigation.RemovePage(this);
        }
    }
}