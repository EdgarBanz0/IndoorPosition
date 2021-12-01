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
    public partial class Locate2 : ContentPage
    {
        private Timer freq;
        int start = 1;
        int seconds = 0;
        int repMax;
        string[] coordenades = { "-0.5,2", "0,0", "4,0", "4.5,2", "2,5", "3,1", "0,6", "8,9", "10,2", "5,5" };
        string[] strKs;
        List<string> ssids = new List<string>();
        private double progress = 0;
        List<decimal[]> distances = new List<decimal[]>();
        List<int[]> rssis = new List<int[]>();
        List<Entry> entries = new List<Entry>();
        Values.WifiSSIDs scan;
        int numero = 0;
        int bandera = 0;
        List<int> lastRssi;
        List<decimal> disNN = new List<decimal>();

        public Task<List<string>> SSIDsTask { get; private set; }
        public Task<List<int>> rssiTask { get; private set; }

        public Locate2()
        {
            InitializeComponent();
            AskConfiguration();

        }



        private async void AskConfiguration()
        {

            var action = await DisplayAlert("Configuracion", "Usar la ultima calibracion de routers", "Si", "No");
            if (action)
            {
                numero = Connection.Instance.returnNM();
                otro();
                InitPicker();
                SetTimer();

                string k = Connection.Instance.ReturnK();

                Console.WriteLine("Valores K: " + k);
                strKs = k.Split(':');   //Valores de k por cada modem
            }
            else
            {
                Connection.Instance.bandCal(0);
                await Navigation.PushModalAsync(new Interface(1)
                {
                    Detail = new NavigationPage(new Calibration())//envio un 0
                });
            }

        }

        private void otro()
        {
            if (numero < 10)
            {
                Modem10.IsVisible = false;
                labelRSSI10.IsVisible = false;
            }
            if (numero < 9)
            {
                Modem9.IsVisible = false;
                labelRSSI9.IsVisible = false;
            }
            if (numero < 8)
            {
                Modem8.IsVisible = false;
                labelRSSI8.IsVisible = false;
            }
            if (numero < 7)
            {
                Modem7.IsVisible = false;
                labelRSSI7.IsVisible = false;
            }
            if (numero < 6)
            {
                Modem6.IsVisible = false;
                labelRSSI6.IsVisible = false;
            }
            if (numero < 5)
            {
                Modem5.IsVisible = false;
                labelRSSI5.IsVisible = false;
            }

        }

        private async void InitPicker()
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                peripheralLabel.IsVisible = true;
            }

            Task<List<string>> SSIDsTask = DependencyService.Get<WifiSSIDs>().SSIDsAsync(); //obtiene routers al alcance
            string ssidCalib = Connection.Instance.ReturnSSID();
            string[] strSSIDs = ssidCalib.Split(':');                                       //obtiene routers calibrados

            List<string> SSIDs = await SSIDsTask;
            peripheralLabel.IsVisible = false;

            List<string> sameSSID = new List<string>();
            foreach (string item in SSIDs)                                                  //conserva los routers calibrados y al alcance         
            {
                for (int i = 0; i < 10; i++)
                {
                    if (strSSIDs[i] == item)
                    {
                        sameSSID.Add(item);
                        break;
                    }
                }
            }

            if (sameSSID.Count == 0)
                await DisplayAlert("Error", "No existen routers calibrados al alcance", "OK");
            else
                await DisplayAlert("Completado", "lista de Routers actualizada", "OK");

            wifiPicker1.ItemsSource = sameSSID;
        }


        private void SetTimer()
        {
            // compilacion condicional:Intervalo de 1 segundo para Android, 5 para IOS
            if (Device.RuntimePlatform == Device.Android)
            {
                freq = new System.Timers.Timer(1000);
                repMax = 10;
            }
            else if (Device.RuntimePlatform == Device.iOS)
            {
                freq = new System.Timers.Timer(5000);
                repMax = 12;
            }

            freq.AutoReset = true;
            freq.Elapsed += OnTimedEvent;
        }

        private async void wifiList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Modem1.Text == null)
            {
                Modem1.Text = wifiPicker1.SelectedItem.ToString();
                ssids.Add(wifiPicker1.SelectedItem.ToString());
                entries.Add(labelRSSI1);
            }

            else if (Modem2.Text == null)
            {
                Modem2.Text = wifiPicker1.SelectedItem.ToString();
                ssids.Add(wifiPicker1.SelectedItem.ToString());
                entries.Add(labelRSSI2);
            }
            else if (Modem3.Text == null)
            {
                Modem3.Text = wifiPicker1.SelectedItem.ToString();
                ssids.Add(wifiPicker1.SelectedItem.ToString());
                entries.Add(labelRSSI3);
            }
            else if (Modem4.Text == null)
            {
                Modem4.Text = wifiPicker1.SelectedItem.ToString();
                ssids.Add(wifiPicker1.SelectedItem.ToString());
                entries.Add(labelRSSI4);
            }
            else if (Modem5.Text == null)
            {
                Modem5.Text = wifiPicker1.SelectedItem.ToString();
                ssids.Add(wifiPicker1.SelectedItem.ToString());
                entries.Add(labelRSSI5);
            }
            else if (Modem6.Text == null)
            {
                Modem6.Text = wifiPicker1.SelectedItem.ToString();
                ssids.Add(wifiPicker1.SelectedItem.ToString());
                entries.Add(labelRSSI6);
            }
            else if (Modem7.Text == null)
            {
                Modem7.Text = wifiPicker1.SelectedItem.ToString();
                ssids.Add(wifiPicker1.SelectedItem.ToString());
                entries.Add(labelRSSI7);
            }
            else if (Modem8.Text == null)
            {
                Modem8.Text = wifiPicker1.SelectedItem.ToString();
                ssids.Add(wifiPicker1.SelectedItem.ToString());
                entries.Add(labelRSSI8);
            }
            else if (Modem9.Text == null)
            {
                Modem9.Text = wifiPicker1.SelectedItem.ToString();
                ssids.Add(wifiPicker1.SelectedItem.ToString());
                entries.Add(labelRSSI9);
            }
            else if (Modem10.Text == null)
            {
                Modem10.Text = wifiPicker1.SelectedItem.ToString();
                ssids.Add(wifiPicker1.SelectedItem.ToString());
                entries.Add(labelRSSI10);
            }
            else
                await DisplayAlert("Error", "Se alcanzo el maximo de Routers", "OK");

            Console.WriteLine("Sali a wifi");
        }

        private int MeanValues(List<int> rssi, List<decimal> distance)
        {
            int[] rssisV = new int[2];
            decimal[] distancesV = new decimal[2];

            if (seconds < repMax)
            {
                /*for (int i = 0; i < rssi.Count; i++)
                {
                    distancesV[0] = i + 1;                    //numero de router
                    distancesV[1] = distance.ElementAt(i);  //distancia al router
                    distances.Add(distancesV);

                    rssisV[0] = i;
                    rssisV[1] = rssi.ElementAt(i);
                    rssis.Add(rssisV);
                }*/
                seconds++;
                Console.WriteLine("segundos: " + seconds);
                if (seconds == repMax - 1)
                {
                    freq.Stop();

                    decimal[] auxNN = new decimal[4];

                    for (int i = 0; i < disNN.Count / 4; i++)
                    {
                        auxNN[0] += disNN[i * 4];
                        auxNN[1] += disNN[i * 4 + 1];
                        auxNN[2] += disNN[i * 4 + 2];
                        auxNN[3] += disNN[i * 4 + 3];
                    }
                    auxNN[0] /= disNN.Count / 4;
                    auxNN[0] = decimal.Round(auxNN[0], 2);
                    auxNN[1] /= disNN.Count / 4;
                    auxNN[1] = decimal.Round(auxNN[1], 2);
                    auxNN[2] /= disNN.Count / 4;
                    auxNN[2] = decimal.Round(auxNN[2], 2);
                    auxNN[3] /= disNN.Count / 4;
                    auxNN[3] = decimal.Round(auxNN[3], 2);

                    disNN.AddRange(auxNN);

                    return 1;
                }

                return 0;
            }
            return 1;
        }

        private List<decimal> EstimatedDistance(List<int> rssi)
        {
            List<decimal> dis = new List<decimal>();

            int index = 0;
            foreach (int rssis in rssi)
            {
                float kf = float.Parse(strKs[index]);//selecciona el coeficiente k correspondiente al modem
                double d = Math.Pow(10, ((rssis - 20 * Math.Log10(2437) - kf) / 20));//Emplea un valor promedio de frecuencia MHz
                dis.Add(decimal.Round((decimal)d, 2));
                index++;
            }

            NeuronalNNF envioDR = new NeuronalNNF(dis);
            disNN.AddRange(envioDR.DistanceReturn());

            return dis;// devuelve distancias calculadas en metros
        }

        private async void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            int index = 0;
            int end = 0;
            int validate = 1;

            if (Device.RuntimePlatform == Device.Android)
            {
                progress += 0.091;
            }
            else if (Device.RuntimePlatform == Device.iOS)
            {
                progress += 0.083;
            }
            List<decimal> distance;
            await progressBar.ProgressTo(progress, 1000, Easing.Linear);



            try
            {
                Task<List<int>> rssiTask = scan.RSSIs(ssids);
                List<int> rssi = await rssiTask;

                if (bandera == 0)
                {
                    lastRssi = rssi;
                    bandera = 1;
                }

                for (int i = 0; i < rssi.Count; i++)
                {
                    if ((Math.Abs(rssi[i] - lastRssi[i]) > 30) && (rssi[i] <= 100)) //desprecia valores anormales +-30
                    {
                        rssi[i] = lastRssi[i];
                    }

                    lastRssi[i] = rssi[i];
                }


                distance = EstimatedDistance(rssi);
                end = MeanValues(rssi, distance);
                //Muestreo de RSS

                await Task.Run(() =>
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        if (end == 1)
                        {
                            //Mostrar promedio de Distancias determinadas por el ajuste neuronal 
                            seconds = 0;
                            progress = 0;
                            start = 1;
                            btnRead.BackgroundColor = Xamarin.Forms.Color.Black;
                            labelIdT.IsEnabled = true;
                            DisplayAlert("Listo", "Promedio de distancia por NN: \n" + disNN.ElementAt(disNN.Count - 4).ToString() +
                                                "\n" + disNN.ElementAt(disNN.Count - 3).ToString() +
                                                "\n" + disNN.ElementAt(disNN.Count - 2).ToString() +
                                                "\n" + disNN.ElementAt(disNN.Count - 1).ToString(), "OK");

                        }

                        else
                        {
                            foreach (Entry item in entries)
                            {
                                if (rssi.Count < index)
                                {
                                    item.Text = " RSSI: x +  Dist: x";
                                    validate = 0;
                                }
                                item.Text = " Dist:" + distance.ElementAt(index).ToString() + " DistN:" + disNN.ElementAt(disNN.Count - 4 + index).ToString();//" RSSI:" + rssi.ElementAt(index).ToString() + 
                                index++;
                            }
                        }
                    });
                });

                if (validate == 1)
                    TestRegistration(rssi, distance);

            }
            catch
            {
                freq.Stop();
                progress = 0;
                start = 1;
                btnRead.BackgroundColor = Xamarin.Forms.Color.Black;
            }
        }

        private void TestRegistration(List<int> rssi, List<decimal> distance)//registra datos en la base de datos
        {
            int index = 0;
            foreach (decimal d in distance)
            {
                Connection.Instance.AddTest(Int32.Parse(labelIdT.Text), index + 1, coordenades[index], rssi.ElementAt(index), d);
                index++;
            }

        }

        private async void btnRead_Clicked(object sender, EventArgs e)
        {
            if ((ssids.Count > 3) && (labelIdT.Text != null))
            {
                switch (start)
                {
                    case 1:
                        start = 2;
                        wifiPicker1.IsEnabled = false;
                        labelIdT.IsEnabled = false;
                        btnRead.BackgroundColor = Xamarin.Forms.Color.Red;
                        progressBar.IsVisible = true;
                        freq.Start();
                        scan = DependencyService.Get<WifiSSIDs>();
                        break;
                    case 2:
                        start = 1;
                        seconds = 0;
                        freq.Stop();
                        freq.Close();

                        wifiPicker1.IsEnabled = true;
                        labelIdT.IsEnabled = true;
                        progressBar.IsVisible = false;
                        progress = 0;
                        btnRead.BackgroundColor = Xamarin.Forms.Color.Black;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                await DisplayAlert("Error", "No se han ingresado suficientes routers o un numero de prueba valido", "OK");
            }
        }
    }
}