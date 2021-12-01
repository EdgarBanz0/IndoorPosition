using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IndoorPositionApp.Pages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Precalibration : TabbedPage
    {
        int modems;
        int flag = 0;
        int bandera = 0;
        public Precalibration(int numMod)
        {
            InitializeComponent();
            modems = numMod;
            inicia();

        }



        private async void inicia()
        {


            if (modems >= 5)
            {
                {
                    this.Children.Add(new Pages.Modems.Calib1());

                    this.Children.Add(new ContentPage
                    {
                        Title = "5",
                        Content = new BoxView
                        {
                            Color = Color.Blue,
                            HeightRequest = 100f,
                            VerticalOptions = LayoutOptions.Center
                        },
                    }
                    );

                }
            }
            if (modems >= 6)
            {
                {

                    this.Children.Add(new ContentPage
                    {
                        Title = "6",
                        Content = new BoxView
                        {
                            Color = Color.Blue,
                            HeightRequest = 100f,
                            VerticalOptions = LayoutOptions.Center
                        },
                    }
                    );

                }
            }
            if (modems >= 7)
            {
                {

                    this.Children.Add(new ContentPage
                    {
                        Title = "7",
                        Content = new BoxView
                        {
                            Color = Color.Blue,
                            HeightRequest = 100f,
                            VerticalOptions = LayoutOptions.Center
                        },
                    }
                    );

                }
            }
            if (modems >= 8)
            {
                {

                    this.Children.Add(new ContentPage
                    {
                        Title = "8",
                        Content = new BoxView
                        {
                            Color = Color.Blue,
                            HeightRequest = 100f,
                            VerticalOptions = LayoutOptions.Center
                        },
                    }
                    );

                }
            }
            if (modems >= 9)
            {
                {

                    this.Children.Add(new ContentPage
                    {
                        Title = "9",
                        Content = new BoxView
                        {
                            Color = Color.Blue,
                            HeightRequest = 100f,
                            VerticalOptions = LayoutOptions.Center
                        },
                    }
                    );

                }
            }
            if (modems == 10)
            {
                {

                    this.Children.Add(new ContentPage
                    {
                        Title = "10",
                        Content = new BoxView
                        {
                            Color = Color.Blue,
                            HeightRequest = 100f,
                            VerticalOptions = LayoutOptions.Center
                        },
                    }
                    );

                }
            }


        }

        private async void este_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Interface(1)
            {
                Detail = new NavigationPage(new Locate2())
            });
        }
    }




}