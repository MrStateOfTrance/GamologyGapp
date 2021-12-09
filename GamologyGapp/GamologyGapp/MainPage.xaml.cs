using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GamologyGapp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            btnBuscar.Clicked += BtnBuscar_Clicked; 
        }

        private async void BtnBuscar_Clicked (object sender, EventArgs e)
        {
            string juegogenero = txtGenero.Text;     

            Navigation.PushAsync(new VistaGeneros(juegogenero));
        }



    }
}
