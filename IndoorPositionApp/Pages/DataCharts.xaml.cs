using IndoorPositionApp.Model;
using IndoorPositionApp.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IndoorPositionApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DataCharts : TabbedPage
    {
        float[] mean;
        decimal[] dis;
        string[] strKs;
        List<ChartSamples> source;
        List<ChartParameters> source2;

        public class ChartSamples
        {
            public string Samples { get; set; }

            public string Data { get; set; }
            public decimal Router1 { get; set; }
            public decimal Router2 { get; set; }
            public decimal Router3 { get; set; }
            public decimal Router4 { get; set; }
            public decimal Router5 { get; set; }
            public decimal Router6 { get; set; }
            public decimal Router7 { get; set; }
            public decimal Router8 { get; set; }
            public decimal Router9 { get; set; }
            public decimal Router10 { get; set; }

        }

        class ChartParameters
        {
            public int Routers { get; set; }

            public int Minimum { get; set; }

            public int Maximum { get; set; }

            public float Mean { get; set; }

        }

        IEnumerable<TestData> data;

        public DataCharts()
        {
            InitializeComponent();
            string k = Connection.Instance.ReturnK();
            strKs = k.Split(':');   //Valores de k por cada modem

        }

        private async void btnSearch_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSearchTest.Text))
                {
                    await DisplayAlert("Error", "Ingrese parametros de busqueda", "OK");
                    txtSearchTest.Focus();
                    return;
                }
                else if (string.IsNullOrEmpty(txtSearchModem.Text))
                    data = Connection.Instance.GetTest(int.Parse(txtSearchTest.Text), 0);
                else
                    data = Connection.Instance.GetTest(int.Parse(txtSearchTest.Text), int.Parse(txtSearchModem.Text));
                testList.ItemsSource = data;

                SamplesListFill();
            }
            catch
            {
                await DisplayAlert("Error", "No se encontro el numero de prueba", "Ok");
            }
        }

        private void SamplesListFill()
        {
            source = new List<ChartSamples>();
            source2 = new List<ChartParameters>();

            List<string> routerList = new List<string>();
            List<List<decimal>> rssix = new List<List<decimal>>();
            List<List<decimal>> disx = new List<List<decimal>>();
            List<decimal> zeros = new List<decimal>();
            int index = 0;

            mean = new float[10];
            dis = new decimal[10];
            int[] min = new int[data.Last().IdM];
            int[] max = new int[data.Last().IdM];
            decimal[,] Coord = new decimal[data.Last().IdM, 2];

            //inicializacion de arrays y Listas
            for (int i = 0; i < data.Last().IdM; i++)
            {
                rssix.Add(new List<decimal>());//crea lista por cada router de la prueba
                disx.Add(new List<decimal>());//crea lista por cada router de la prueba
                routerList.Add("Router " + (i + 1));
                mean[i] = 0;
                min[i] = 100;
                max[i] = 0;
                Coord[i, 0] = -1;
            }

            routerPicker.ItemsSource = routerList;

            foreach (TestData item in data)
            {
                switch (item.IdM)
                {
                    case 1:
                        rssix.ElementAt(0).Add(item.RSSI);
                        disx.ElementAt(0).Add(CalculateDistance(item.RSSI, strKs[0]));
                        mean[0] += item.RSSI;
                        if (item.RSSI > max[0])
                            max[0] = item.RSSI;
                        else if (item.RSSI < min[0])
                            min[0] = item.RSSI;

                        if (Coord[0, 0] == -1)
                        {
                            Coord[0, 0] = decimal.Parse(item.XY.Split(',').ElementAt(0));   //Coordenada de modem en la prueba
                            Coord[0, 1] = decimal.Parse(item.XY.Split(',').ElementAt(1));
                        }

                        index++;
                        zeros.Add(0);
                        break;
                    case 2:
                        rssix.ElementAt(1).Add(item.RSSI);
                        disx.ElementAt(1).Add(CalculateDistance(item.RSSI, strKs[1]));
                        mean[1] += item.RSSI;
                        if (item.RSSI > max[1])
                            max[1] = item.RSSI;
                        else if (item.RSSI < min[1])
                            min[1] = item.RSSI;

                        if (Coord[1, 0] == -1)
                        {
                            Coord[1, 0] = decimal.Parse(item.XY.Split(',').ElementAt(0));   //Coordenada de modem en la prueba
                            Coord[1, 1] = decimal.Parse(item.XY.Split(',').ElementAt(1));
                        }
                        break;
                    case 3:
                        rssix.ElementAt(2).Add(item.RSSI);
                        disx.ElementAt(2).Add(CalculateDistance(item.RSSI, strKs[2]));
                        mean[2] += item.RSSI;
                        if (item.RSSI > max[2])
                            max[2] = item.RSSI;
                        else if (item.RSSI < min[2])
                            min[2] = item.RSSI;

                        if (Coord[2, 0] == -1)
                        {
                            Coord[2, 0] = decimal.Parse(item.XY.Split(',').ElementAt(0));   //Coordenada de modem en la prueba
                            Coord[2, 1] = decimal.Parse(item.XY.Split(',').ElementAt(1));
                        }
                        break;
                    case 4:
                        rssix.ElementAt(3).Add(item.RSSI);
                        disx.ElementAt(3).Add(CalculateDistance(item.RSSI, strKs[3]));
                        mean[3] += item.RSSI;
                        if (item.RSSI > max[3])
                            max[3] = item.RSSI;
                        else if (item.RSSI < min[3])
                            min[3] = item.RSSI;

                        if (Coord[3, 0] == -1)
                        {
                            Coord[3, 0] = decimal.Parse(item.XY.Split(',').ElementAt(0));   //Coordenada de modem en la prueba
                            Coord[3, 1] = decimal.Parse(item.XY.Split(',').ElementAt(1));
                        }
                        break;
                    case 5:
                        rssix.ElementAt(4).Add(item.RSSI);
                        disx.ElementAt(4).Add(CalculateDistance(item.RSSI, strKs[4]));
                        mean[4] += item.RSSI;
                        if (item.RSSI > max[4])
                            max[4] = item.RSSI;
                        else if (item.RSSI < min[4])
                            min[4] = item.RSSI;

                        if (Coord[4, 0] == -1)
                        {
                            Coord[4, 0] = decimal.Parse(item.XY.Split(',').ElementAt(0));   //Coordenada de modem en la prueba
                            Coord[4, 1] = decimal.Parse(item.XY.Split(',').ElementAt(1));
                        }
                        break;
                    case 6:
                        rssix.ElementAt(5).Add(item.RSSI);
                        disx.ElementAt(5).Add(CalculateDistance(item.RSSI, strKs[5]));
                        mean[5] += item.RSSI;
                        if (item.RSSI > max[5])
                            max[5] = item.RSSI;
                        else if (item.RSSI < min[5])
                            min[5] = item.RSSI;

                        if (Coord[5, 0] == -1)
                        {
                            Coord[5, 0] = decimal.Parse(item.XY.Split(',').ElementAt(0));   //Coordenada de modem en la prueba
                            Coord[5, 1] = decimal.Parse(item.XY.Split(',').ElementAt(1));
                        }
                        break;
                    case 7:
                        rssix.ElementAt(6).Add(item.RSSI);
                        disx.ElementAt(6).Add(CalculateDistance(item.RSSI, strKs[6]));
                        mean[6] += item.RSSI;
                        if (item.RSSI > max[6])
                            max[6] = item.RSSI;
                        else if (item.RSSI < min[6])
                            min[6] = item.RSSI;

                        if (Coord[6, 0] == -1)
                        {
                            Coord[6, 0] = decimal.Parse(item.XY.Split(',').ElementAt(0));   //Coordenada de modem en la prueba
                            Coord[6, 1] = decimal.Parse(item.XY.Split(',').ElementAt(1));
                        }
                        break;
                    case 8:
                        rssix.ElementAt(7).Add(item.RSSI);
                        disx.ElementAt(7).Add(CalculateDistance(item.RSSI, strKs[7]));
                        mean[7] += item.RSSI;
                        if (item.RSSI > max[7])
                            max[7] = item.RSSI;
                        else if (item.RSSI < min[7])
                            min[7] = item.RSSI;

                        if (Coord[7, 0] == -1)
                        {
                            Coord[7, 0] = decimal.Parse(item.XY.Split(',').ElementAt(0));   //Coordenada de modem en la prueba
                            Coord[7, 1] = decimal.Parse(item.XY.Split(',').ElementAt(1));
                        }
                        break;
                    case 9:
                        rssix.ElementAt(8).Add(item.RSSI);
                        disx.ElementAt(8).Add(CalculateDistance(item.RSSI, strKs[8]));
                        mean[8] += item.RSSI;
                        if (item.RSSI > max[8])
                            max[8] = item.RSSI;
                        else if (item.RSSI < min[8])
                            min[8] = item.RSSI;

                        if (Coord[8, 0] == -1)
                        {
                            Coord[8, 0] = decimal.Parse(item.XY.Split(',').ElementAt(0));   //Coordenada de modem en la prueba
                            Coord[8, 1] = decimal.Parse(item.XY.Split(',').ElementAt(1));
                        }
                        break;
                    case 10:
                        rssix.ElementAt(9).Add(item.RSSI);
                        disx.ElementAt(9).Add(CalculateDistance(item.RSSI, strKs[9]));
                        mean[9] += item.RSSI;
                        if (item.RSSI > max[9])
                            max[9] = item.RSSI;
                        else if (item.RSSI < min[9])
                            min[9] = item.RSSI;

                        if (Coord[9, 0] == -1)
                        {
                            Coord[9, 0] = decimal.Parse(item.XY.Split(',').ElementAt(0));   //Coordenada de modem en la prueba
                            Coord[9, 1] = decimal.Parse(item.XY.Split(',').ElementAt(1));
                        }
                        break;
                    default:
                        break;
                }
            }

            //calculo de media de RSSI
            for (int i = 0; i < data.Last().IdM; i++)
            {
                mean[i] /= index;
            }

            //calculo de media de distancia
            EstimatedDistance(mean, Coord);


            for (int i = data.Last().IdM; i < 10; i++)//llenar routers inutilizados con 0
            {
                rssix.Add(zeros);
                disx.Add(zeros);
                mean[i] = 0;
                dis[i] = 0;
            }

            for (int i = 0; i < index; i++)
            {
                source.Add(new ChartSamples
                {
                    Samples = (i + 1).ToString(),
                    Data = "RSSI",
                    Router1 = rssix.ElementAt(0).ElementAt(i),
                    Router2 = rssix.ElementAt(1).ElementAt(i),
                    Router3 = rssix.ElementAt(2).ElementAt(i),
                    Router4 = rssix.ElementAt(3).ElementAt(i),
                    Router5 = rssix.ElementAt(4).ElementAt(i),
                    Router6 = rssix.ElementAt(5).ElementAt(i),
                    Router7 = rssix.ElementAt(6).ElementAt(i),
                    Router8 = rssix.ElementAt(7).ElementAt(i),
                    Router9 = rssix.ElementAt(8).ElementAt(i),
                    Router10 = rssix.ElementAt(9).ElementAt(i)
                });
                source.Add(new ChartSamples
                {
                    Samples = (i + 1).ToString(),
                    Data = "Dist",
                    Router1 = disx.ElementAt(0).ElementAt(i),
                    Router2 = disx.ElementAt(1).ElementAt(i),
                    Router3 = disx.ElementAt(2).ElementAt(i),
                    Router4 = disx.ElementAt(3).ElementAt(i),
                    Router5 = disx.ElementAt(4).ElementAt(i),
                    Router6 = disx.ElementAt(5).ElementAt(i),
                    Router7 = disx.ElementAt(6).ElementAt(i),
                    Router8 = disx.ElementAt(7).ElementAt(i),
                    Router9 = disx.ElementAt(8).ElementAt(i),
                    Router10 = disx.ElementAt(9).ElementAt(i)
                });
            }

            //Agregar filas de medias de RSSI y distancia
            source.Add(new ChartSamples
            {
                Samples = "Media",
                Data = "RSSI",
                Router1 = decimal.Round((decimal)mean[0], 2),
                Router2 = decimal.Round((decimal)mean[1], 2),
                Router3 = decimal.Round((decimal)mean[2], 2),
                Router4 = decimal.Round((decimal)mean[3], 2),
                Router5 = decimal.Round((decimal)mean[4], 2),
                Router6 = decimal.Round((decimal)mean[5], 2),
                Router7 = decimal.Round((decimal)mean[6], 2),
                Router8 = decimal.Round((decimal)mean[7], 2),
                Router9 = decimal.Round((decimal)mean[8], 2),
                Router10 = decimal.Round((decimal)mean[9], 2)
            });

            source.Add(new ChartSamples
            {
                Samples = "Media",
                Data = "Dist",
                Router1 = dis[0],
                Router2 = dis[1],
                Router3 = dis[2],
                Router4 = dis[3],
                Router5 = dis[4],
                Router6 = dis[5],
                Router7 = dis[6],
                Router8 = dis[7],
                Router9 = dis[8],
                Router10 = dis[9]
            });

            sampleList.ItemsSource = source;



            //Valores para lista de coordenadas
            for (int i = 0; i < data.Last().IdM; i++)
            {
                source2.Add(new ChartParameters { Maximum = max[i], Minimum = min[i], Mean = mean[i], Routers = i + 1 });
            }

            parameterList.ItemsSource = source2;
        }

        private async void EstimatedDistance(float[] mean, decimal[,] Coord)
        {
            try
            {
                EstimatePosition Pos = new EstimatePosition();

                for (int i = 0; i < data.Last().IdM; i++)
                {
                    float kf = float.Parse(strKs[i]);//selecciona el coeficiente k correspondiente al modem
                    double d = Math.Pow(10, ((mean[i] - 20 * Math.Log10(2437) - kf) / 20));//Emplea un valor promedio de frecuencia MHz
                    dis[i] = decimal.Round((decimal)d, 2);
                }

                try
                {
                    decimal[] XY = Pos.Matrix(Coord, dis);//Determina posicion del usuario 
                    coorX.Text = XY[0].ToString();
                    coorY.Text = XY[1].ToString();
                }
                catch
                {
                    await DisplayAlert("Error", "La localizacion de los routers produce un 0 en el determinante, realize otra prueba", "Ok");
                }
            }
            catch
            {
                await DisplayAlert("Error", "Aqui es", "Ok");
            }

        }

        private decimal CalculateDistance(int RSSI, string k)
        {
            float kf = float.Parse(k);//selecciona el coeficiente k correspondiente al modem
            double d = Math.Pow(10, ((RSSI - 20 * Math.Log10(2437) - kf) / 20));//Emplea un valor promedio de frecuencia MHz
            return decimal.Round((decimal)d, 2);
        }

        private async void btnDelete_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearchTest.Text))
            {
                await DisplayAlert("Error", "Ingrese parametros de busqueda", "OK");
                txtSearchTest.Focus();
                return;
            }
            else
            {
                Connection.Instance.DeleteTest(int.Parse(txtSearchTest.Text));
                await DisplayAlert("Listo", "Prueba Eliminada", "OK");
                testList.ItemsSource = null;
                txtSearchTest.Text = null;
            }
        }

        private void routerPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            disRouter.Text = dis[routerPicker.SelectedIndex].ToString();
            meanRouter.Text = mean[routerPicker.SelectedIndex].ToString();
        }


        //-----------------------------------Exportar Base de datos
        private async void btnExport_Clicked(object sender, EventArgs e)
        {
            try
            {
                //Copia base de datos a destino definido.
                Task<bool> DBserverAsync = DependencyService.Get<DBconnection>().SubmitEntry(data);
                bool DBserver = await DBserverAsync;
                if (DBserver)
                {
                    await DisplayAlert("Completado", "Datos nuevos registrados en el servidor", "Ok");
                }
                else
                {
                    await DisplayAlert("Error", "No se pudo efectuar la actualizacion de datos", "Ok");
                }
                //CopyFile();

            }
            catch
            {
                await DisplayAlert("Error", "No se pudo realizar la conexion al servidor", "Ok");
            }
        }

        public static void CopyFile()
        {
            //Obtiene ruta de base de datos origen.
            var bytes = System.IO.File.ReadAllBytes(Connection.Instance.GetDBPath());
            var fileCopyName = string.Format("/sdcard/Database_{0:dd-MM-yyyy_HH-mm-ss-tt}.db", System.DateTime.Now);
            System.IO.File.WriteAllBytes(fileCopyName, bytes);
        }

        private async void btnExport2_Clicked(object sender, EventArgs e)
        {
            try
            {
                CopyFile();

                //Copia base de datos a destino definido.
                Task<bool> DBserverAsync = DependencyService.Get<DBconnection>().SubmitEntry2(source);
                bool DBserver = await DBserverAsync;
                if (DBserver)
                {
                    await DisplayAlert("Completado", "Datos nuevos registrados en el servidor", "Ok");
                }
                else
                {
                    await DisplayAlert("Error", "No se pudo efectuar la actualizacion de datos", "Ok");
                }



            }
            catch
            {
                await DisplayAlert("Error", "No se pudo realizar la conexion al servidor", "Ok");
            }
        }
    }
}