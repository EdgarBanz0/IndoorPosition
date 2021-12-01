using IndoorPositionApp.Model;
using IndoorPositionApp.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IndoorPositionApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Calibration : ContentPage
    {
        private Timer freq;
        List<decimal> ks = new List<decimal>();
        List<string> ssids = new List<string>();
        int routers;
        int routerCount = 0;
        int start = 1;
        int error = 0;
        int rep = 0;
        int repMax;
        int lastRssi;
        double progress = 0;
        Values.WifiSSIDs scan;


        int bandera;
        public Calibration()
        {
            InitializeComponent();

            AskRouters();
            InitPicker();
            SetTimer();
        }

        private async void AskRouters()
        {
            bandera = Connection.Instance.returnBandCal();

            if (bandera == 0)
            {
                int flag;
                do
                {
                    try
                    {
                        string result = await DisplayPromptAsync("# Routers", "Ingrese el numero de routers a configurar");
                        routers = int.Parse(result);
                        flag = 0;
                        if (routers > 10 || routers < 4)
                        {
                            flag = 1;
                            await DisplayAlert("Error", "Es posible un maximo de 10 o minimo de 4 routers", "OK");
                        }
                    }
                    catch
                    {
                        flag = 1;
                        await DisplayAlert("Error", "No se ingreso un numero valido", "OK");
                    }
                } while (flag == 1);
                peripheralLabel.Text = "Routers por seleccionar..." + routers;
            }
            else
            {
                routers = 1;
            }

        }

        private async void InitPicker()
        {
            Task<List<string>> SSIDsTask = DependencyService.Get<WifiSSIDs>().SSIDsAsync();
            List<string> SSIDs = await SSIDsTask;
            wifiPicker1.ItemsSource = SSIDs;

        }

        private async void wifiList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (routerCount < routers)
            {
                ssids.Add(wifiPicker1.SelectedItem.ToString()); //se añade lo que esta en el picker a la lista


                peripheralLabel.Text = "Routers por seleccionar..." + (routers - (routerCount + 1));
            }

            routerCount++;
            if (routerCount >= routers)
            {
                wifiPicker1.IsEnabled = false;
                // peripheralLabel.Text = " SE HAN CALIBRADO TODOS LOS ROUTERS";
                calibrationLabel.IsVisible = true;
                routerCount = 0;
                calibrationLabel.Text = "Router a calibrar: " + ssids[routerCount];
                peripheralLabel.IsVisible = false;
                await DisplayAlert("Hecho", "Se alcanzo el maximo de Routers elegidos, puede modificar sus datos de manera individual en la sección 'Datos Modems'", "OK", "otro");


                Message0.Text = "Se realizara una prueba de 1 min para estimar los parametros de conexion, porfavor permanezca a 1 metro del modem mientras la prueba se ejecuta";
                labelRssi.IsVisible = true;
                labelValueRssi.IsVisible = true;
                labelValueK.IsVisible = true;
                labelk.IsVisible = true;
                wifiPicker1.IsVisible = false;
                btnStart.IsVisible = true;
            }

        }

        private void SetTimer()
        {
            // compilacion condicional:Intervalo de 1 segundo para Android, 3 para IOS
            if (Device.RuntimePlatform == Device.Android)
            {
                freq = new System.Timers.Timer(1000);
                repMax = 11;
            }
            else if (Device.RuntimePlatform == Device.iOS)
            {
                freq = new System.Timers.Timer(3000);
                repMax = 20;
            }

            freq.AutoReset = true;
            freq.Elapsed += OnTimedEvent;
        }

        private async void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            rep++;
            if (Device.RuntimePlatform == Device.Android)
            {
                progress += 0.071;
            }
            else if (Device.RuntimePlatform == Device.iOS)
            {
                progress += 0.0526;
            }

            if (rep < repMax)
            {
                try
                {
                    Task<int> rssiTask = DependencyService.Get<WifiSSIDs>().SingleRssi(ssids[routerCount]);
                    int rssi = await rssiTask;
                    Console.WriteLine("RSSI VALOR " + rssi);
                    if ((ks.Count > 1) && (Math.Abs(rssi - lastRssi) > 30) && (rssi <= -100)) //desprecia valores anormales +-30
                    {
                        rssi = lastRssi;
                        Console.WriteLine("ENTRE AL TRIO");
                    }

                    lastRssi = rssi;
                    //Muestreo de RSSI
                    await Task.Run(() =>
                    {
                        progressBar.ProgressTo(progress, 1000, Easing.Linear);
                        ks.Add(EstimatedK(rssi));
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            labelValueRssi.Text = rssi.ToString();
                            labelValueK.Text = ks.ElementAt(rep - 1).ToString();
                        });
                    });
                }
                catch (Exception x)
                {
                    Console.WriteLine(x);
                }
            }
            else
            {
                freq.Stop();
                start = 1;
                rep = 0;
                progress = 0;
                routerCount++;
                decimal AvgK = AverageK();
                error = Connection.Instance.UpdateK(AvgK, routerCount, ssids[routerCount - 1], labelValueRssi.Text);
                await Task.Run(() =>
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        if (error == 0)
                        {
                            DisplayAlert("Error", "No se pudo actualizar K en la base de datos ", "OK");
                        }
                        else
                        {
                            if (routers == routerCount)

                            {
                                btnStart.IsEnabled = false;
                                calibrationLabel.IsVisible = false;
                                progressBar.IsVisible = false;
                                Message0.Text = "CALIBRACIONES FINALIZADAS";
                                imagen.Source = "m.png";
                                labelRssi.IsVisible = false;
                                labelValueRssi.IsVisible = false;
                                labelValueK.IsVisible = false;
                                labelk.IsVisible = false;
                                wifiPicker1.IsVisible = false;
                                btnStart.IsVisible = false;
                                fondo.BackgroundColor = Color.Black;
                                btnIrDat.IsVisible = true;
                                btnIrLoc.IsVisible = true;


                                //Preconfiguration.lastSSIDs = ssids;                     //Exporta lista de routers calibrados
                            }
                            else
                            {
                                btnStart.Text = "Calibrar Siguiente";
                                calibrationLabel.Text = "Router a calibrar: " + ssids[routerCount];
                            }
                            DisplayAlert("Hecho", "La prueba Finalizo\nEl valor K promedio es: " + AvgK.ToString() + "\nEl valor RSSi es: " + labelValueRssi.Text, "OK");
                        }
                    });
                });

            }
        }

        private decimal AverageK()
        {
            return decimal.Round((ks.Average()), 2);
        }

        private decimal EstimatedK(int rssi)
        {
            double k = -20 * Math.Log10(1) - 20 * Math.Log10(2437) - rssi;//Emplea un valor promedio de frecuencia MHz
            return decimal.Round((decimal)k, 2);
        }

        private void btnStart_Clicked(object sender, EventArgs e)
        {
            switch (start)
            {
                case 1:
                    start = 2;
                    btnStart.Text = "Detener";
                    progressBar.IsVisible = true;
                    scan = DependencyService.Get<WifiSSIDs>();
                    freq.Start();
                    break;
                case 2:
                    start = 1;
                    freq.Stop();
                    freq.Close();
                    progress = 0;
                    rep = 0;
                    btnStart.Text = "Empezar";
                    break;
                default:
                    break;
            }
        }

        private async void btnIrLoc_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Locate2());
        }

        private async void btnIrDat_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DataModem());
        }
    }
}