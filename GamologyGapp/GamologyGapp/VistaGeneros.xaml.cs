using GamologyGapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GamologyGapp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VistaGeneros : ContentPage
    {
        List<Juegosapp> juegosapp = new List<Juegosapp>();
        public VistaGeneros(string juegogenero)
        {
            InitializeComponent();
            mostrarjuegos(juegogenero);
       
        }
        public async void mostrarjuegos(String genero) {
            HttpClient cliente = new HttpClient();
            string URL = "https://gamologyproject.azurewebsites.net/api/Games";

            var resultados = await cliente.GetAsync(URL);

            var json = resultados.Content.ReadAsStringAsync().Result;

            juegosapp = Juegosapp.FromJson(json);

            var layout = new StackLayout { Padding = new Thickness(10, 0, 10, 10) };

            List<Juegosapp> juegosfiltrados = new List<Juegosapp>();
            if (juegosapp.Count > 0)
            {
                gdGeneros.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                gdGeneros.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                foreach (var item in juegosapp)
                {

                    var Gneros = item.Genero.Split(','); 
                    foreach(var item2 in Gneros)
                    {
                        if(item2 == genero)
                        {
                            juegosfiltrados.Add(item);
                        }
                    }
                }

                //Agregarlos a nuestra vista:
                if(juegosfiltrados.Count > 0)
                {
                    int fila = 0;
                    int columna = 0;
                    

                    gdGeneros.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    gdGeneros.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    
                    gdGeneros.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    gdGeneros.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });


                    foreach(var item in juegosfiltrados)
                    {
                        var NombreJuego = new Label { Text = item.Nombre.ToString()};
                        NombreJuego.HorizontalOptions = LayoutOptions.Start;
                        NombreJuego.VerticalOptions = LayoutOptions.Start;

                        if (columna > 0)
                        {
                            
                            NombreJuego.Margin = new Thickness(25, 10, 25, 10);
                            gdGeneros.Children.Add(NombreJuego, columna, fila);


                            columna = 0;
                            fila++;

                            gdGeneros.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

                        }
                        else
                        {
                           
                            NombreJuego.Margin = new Thickness(20, 10, 18, 10);
                            
                            gdGeneros.Children.Add(NombreJuego, columna, fila);
                            
                            columna++;
                        }

                    }
                    layout.Children.Add(gdGeneros);
                    this.Content = layout;
                }
                else
                {
                    //No hay juegos con el Genero proporcionado 
                }



            }
            else
            {

                //En este caso no hay informacion en tu api
            }



        }
    }
}