using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IndoorPositionApp.Model
{
    class Connection
    {

        private SQLiteConnection connect;
        private static Connection instance;
        public String Message;
        private int idu = 0; //id del usuario que inicio sesion
        public int numMod = 0;
        int permanente = 0;
        string corGua = "";
        string conGua = "";
        int banderaCal = 0;
        string dbcPath = "";

        public static Connection Instance
        {
            get
            {
                if (instance == null)
                    throw new Exception("Debe inicializar la conexion");
                return instance;
            }
        }

        public void Permanente(int perma)
        {
            permanente = perma;
        }

        public int returnPerma()
        {
            return permanente;
        }

        public void guardados(string email, string pass)
        {
            corGua = email;
            conGua = pass;
        }

        public string returnContra()
        {
            return conGua;
        }

        public string returnCor()
        {
            return corGua;
        }
        public void bandCal(int bandC)
        {
            banderaCal = bandC;
        }
        public int returnBandCal()
        {
            return banderaCal;
        }

        public static void Initialize(String filename)
        {
            if (filename == null)
                throw new ArgumentNullException();

            //Si la consulta se realiza, la conexion debera cerrarse primero
            if (instance != null)
                instance.connect.Close();

            instance = new Connection(filename);
        }

        private Connection(String dbPath)
        {
            //Conexion a la ruta de base de datos
            dbcPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), dbPath);
            connect = new SQLiteConnection(dbPath);
            connect.CreateTable<User>();
            connect.CreateTable<History>();
            connect.CreateTable<TestData>();
        }


        public string GetDBPath()
        {
            return dbcPath;
        }


        public int AddUser(string name, string age, string email, string password)
        {
            int result = 0;
            try
            {
                result = connect.Insert(new User()
                {
                    Name = name,
                    Age = age,
                    Email = email,
                    Password = password,
                    IdFollow = "",
                    ConsK = "0:0:0:0:0:0:0:0:0:0",
                    CalibratedRouters = "0:0:0:0:0:0:0:0:0:0",
                    pruebRSSI = "0:0:0:0:0:0:0:0:0:0"

                });

                Message = string.Format("Filas cambiadas : {0}", result);
            }
            catch (Exception e)
            {
                Message = e.Message;
            }

            return result;

        }





        public User PerUsuario()
        {
            return connect.Table<User>().FirstOrDefault(u => u.Id == idu);
        }

        public void ModPer(string name, string age, string email, string pass)
        {

            int result = 0;

            User update = connect.Table<User>().FirstOrDefault(u => u.Id == idu);
            try
            {
                result = connect.Update(new User()
                {
                    Id = update.Id,
                    Name = name,
                    Age = age,
                    Email = email,
                    Password = pass,
                    ConsK = update.ConsK,
                    CalibratedRouters = update.CalibratedRouters,
                    pruebRSSI = update.pruebRSSI
                });

                Message = string.Format("Filas cambiadas : {0}", result);
            }
            catch (Exception e)
            {
                Message = e.Message;
            }

        }



        public int FollowUser(int idFollowing)
        {
            int result = 0;
            string[] strIds;
            int[] intIds;

            //anadir el id a la cadena de IDs
            string idStr = idFollowing.ToString();

            //Seperacion de string en IDs de seguimiento
            User update = connect.Table<User>().FirstOrDefault(u => u.Id == idu);

            if (update.IdFollow != "")
            {
                strIds = update.IdFollow.Split(',');
                intIds = Array.ConvertAll<string, int>(strIds, int.Parse);

                if (intIds.Length == 3)//revisa que el usuario no este ya en seguimiento
                    return result = 1;

                for (int i = 0; i < intIds.Length; i++)
                {
                    if (intIds[i] == idFollowing)//revisa que el usuario no este ya en seguimiento
                        return result = 0;
                    else
                        idStr += "," + intIds[i].ToString();
                }

            }

            try
            {
                result = connect.Update(new User()
                {
                    Id = update.Id,
                    Name = update.Name,
                    Age = update.Age,
                    Email = update.Email,
                    Password = update.Password,
                    IdFollow = idStr,
                    ConsK = update.ConsK,
                    CalibratedRouters = update.CalibratedRouters,
                    pruebRSSI = update.pruebRSSI
                });

                Message = string.Format("Filas cambiadas : {0}", result);
            }
            catch (Exception e)
            {
                Message = e.Message;
            }

            return result = 3;

        }

        //Consulta un usuario
        public User SearchUser(int id)
        {
            return connect.Table<User>().FirstOrDefault(u => u.Id == id);

        }




        public User LoginUser(string email, string password)
        {
            User login = connect.Table<User>().FirstOrDefault(u => u.Email == email && u.Password == password);
            idu = login.Id;
            return login;
        }

        public int ReturnID(string email)
        {
            int ID = 0;
            try
            {
                User result = connect.Table<User>().FirstOrDefault(u => u.Email == email);
                ID = result.Id;

            }
            catch (Exception e)
            {
                Message = e.Message;
            }

            return ID;
        }



        //Elimina a un usuario
        public int DeleteUser(int id)
        {
            int result = 0;
            try
            {
                result = connect.Table<User>().Delete(u => u.Id == id);

                Message = string.Format("Filas cambiadas : {0}", result);
            }
            catch (Exception e)
            {
                Message = e.Message;
            }

            return result;
        }




        public int returnNM()
        {
            return numMod;
        }

        //Asigna la constante K a un usuario
        public int UpdateK(decimal k, int modem, string ssid, string rssi)
        {
            Console.WriteLine("ENTRE A UPDATE");
            numMod++;
            int result = 0;
            string[] strKs;
            string[] strSsid;
            string[] strRssi;

            Console.WriteLine("ENTRE A CONNECTION");

            //Seperacion de string en Ks del modem
            User update = connect.Table<User>().FirstOrDefault(u => u.Id == idu);
            strKs = update.ConsK.Split(':');
            strSsid = update.CalibratedRouters.Split(':');
            strRssi = update.pruebRSSI.Split(':');

            strKs[modem - 1] = k.ToString();//agrega k al respectivo modem
            strSsid[modem - 1] = ssid;//agrega el ssid que coincide con su calibracion
            strRssi[modem - 1] = rssi;

            string kStr = strKs[0];
            string ssidStr = strSsid[0];
            string rssiStr = strRssi[0];

            for (int i = 1; i < 10; i++)
            {
                kStr += ":" + strKs[i];
                ssidStr += ":" + strSsid[i];
                rssiStr += ":" + strRssi[i];
            }

            try
            {
                Console.WriteLine("ENTRE A TRY");
                result = connect.Update(new User()
                {
                    Id = update.Id,
                    Name = update.Name,
                    Age = update.Age,
                    Email = update.Email,
                    Password = update.Password,
                    IdFollow = update.IdFollow,
                    ConsK = kStr,
                    CalibratedRouters = ssidStr,
                    pruebRSSI = rssiStr
                });

                Message = string.Format("Filas cambiadas : {0}", result);

            }
            catch (Exception e)
            {
                Message = e.Message;
            }
            Console.WriteLine("Entre a CATCH");
            return result = 4;
        }

        public string ReturnK()
        {
            Console.WriteLine("Entre a return k");
            User search = connect.Table<User>().FirstOrDefault(u => u.Id == idu);
            return search.ConsK;
        }



        public string ReturnRSSI(int id)
        {
            User update = connect.Table<User>().FirstOrDefault(u => u.Id == idu);
            string[] rssiE = update.pruebRSSI.Split(':');

            return rssiE[id - 1];

        }

        public string ReturnK(int id)
        {
            User update = connect.Table<User>().FirstOrDefault(u => u.Id == idu);
            string[] valork = update.ConsK.Split(':');

            return valork[id - 1];

        }

        public string ReturnMod(int id)
        {
            User update = connect.Table<User>().FirstOrDefault(u => u.Id == idu);
            string[] mod = update.CalibratedRouters.Split(':');

            return mod[id - 1];

        }

        public string ReturnSSID()
        {
            User search = connect.Table<User>().FirstOrDefault(u => u.Id == idu);
            return search.CalibratedRouters;
        }






        //Consulta a todo los usuarios
        public IEnumerable<User> GetAllUsers()
        {
            try
            {
                return connect.Table<User>();

            }
            catch (Exception e)
            {
                Message = e.Message;
            }
            return Enumerable.Empty<User>();
        }

        //consulta usuarios en seguimiento
        public IEnumerable<User> AllFollowedUsers()
        {

            //Seperacion de string en IDs de seguimiento
            User update = connect.Table<User>().FirstOrDefault(u => u.Id == idu);
            int search = 0;
            int search1 = 0;
            int search2 = 0;


            if (update.IdFollow.Length == 0)
                return Enumerable.Empty<User>();


            if (update.IdFollow.Length > 0)
            {
                string[] strIds = update.IdFollow.Split(',');
                int[] intIds = Array.ConvertAll<string, int>(strIds, int.Parse);

                switch (intIds.Length)
                {
                    case 1:
                        search = intIds[0];
                        break;
                    case 2:
                        search = intIds[0];
                        search1 = intIds[1];
                        break;
                    case 3:
                        search = intIds[0];
                        search1 = intIds[1];
                        search2 = intIds[2];
                        break;
                    default:
                        break;
                }

                try
                {
                    return connect.Table<User>().Where(h => h.Id == search || h.Id == search1 || h.Id == search2);
                }
                catch (Exception e)
                {
                    Message = e.Message;
                }
            }

            return Enumerable.Empty<User>();
        }


        ////////////////////////// History User Data /////////////////////////////////////////////////

        public string AddRecord(string date, decimal distance)
        {
            int result = 0;

            try
            {
                result = connect.Insert(new History()
                {
                    IdU = idu,
                    Date = date,
                    Distance = distance,
                });

                Message = string.Format("Filas cambiadas : {0}", result);
            }
            catch (Exception e)
            {
                Message = e.Message;
            }


            return Message;
        }

        public IEnumerable<History> GetHistory(int idFind)
        {
            try
            {
                return connect.Table<History>().Where(h => h.IdU == idFind);
            }
            catch (Exception e)
            {
                Message = e.Message;
            }
            return Enumerable.Empty<History>();
        }

        public int DeleteRecord(int idLook)
        {
            int result = 0;
            try
            {
                result = connect.Table<History>().Delete(h => h.IdU == idLook);

                Message = string.Format("Filas cambiadas : {0}", result);
            }
            catch (Exception e)
            {
                Message = e.Message;
            }

            return result;
        }

        public int DeleteFollowing(int idLook)
        {
            int result = 0;
            //Seperacion de string en IDs de seguimiento
            User update = connect.Table<User>().FirstOrDefault(u => u.Id == idu);
            string[] strIds = update.IdFollow.Split(',');
            int[] intIds = Array.ConvertAll<string, int>(strIds, int.Parse);


            string idStr = "";
            for (int i = 0; i < intIds.Length; i++)
            {
                if (idStr == "" && intIds[i] != idLook)
                    idStr = intIds[i].ToString();
                else
                {
                    if (intIds[i] != idLook && intIds[i] != 0)//revisa que el usuario no este ya en seguimiento
                        idStr += ',' + intIds[i].ToString();
                }


            }

            try
            {
                result = connect.Update(new User()
                {
                    Id = update.Id,
                    Name = update.Name,
                    Age = update.Age,
                    Email = update.Email,
                    Password = update.Password,
                    IdFollow = idStr,
                    ConsK = update.ConsK,
                    CalibratedRouters = update.CalibratedRouters,
                    pruebRSSI = update.pruebRSSI
                });

                Message = string.Format("Filas cambiadas : {0}", result);
            }
            catch (Exception e)
            {
                Message = e.Message;
            }

            return result;
        }

        ////////////////////////// Modem Test Data /////////////////////////////////////////////////
        public int AddTest(int idt, int idm, string coord, int rssi, decimal dis)
        {
            int result = 0;

            try
            {
                result = connect.Insert(new TestData()
                {
                    IdU = idu,
                    IdT = idt,
                    IdM = idm,
                    XY = coord,
                    RSSI = rssi,
                    Distance = dis
                });

                Message = string.Format("Filas cambiadas : {0}", result);
            }
            catch (Exception e)
            {
                Message = e.Message;
                Console.WriteLine("Existe un error: " + Message);
            }

            return result;

        }

        //todos los registro
        public IEnumerable<TestData> GetAllTests()
        {
            try
            {
                return connect.Table<TestData>();

            }
            catch (Exception e)
            {
                Message = e.Message;
            }
            return Enumerable.Empty<TestData>();
        }

        //elimina toda una prueba
        public int DeleteTest(int idfind)
        {
            int result = 0;
            try
            {
                result = connect.Table<TestData>().Delete(t => t.IdT == idfind);

                Message = string.Format("Filas cambiadas : {0}", result);
            }
            catch (Exception e)
            {
                Message = e.Message;
            }

            return result;
        }


        //buscar registros con la misma prueba y por modem
        public IEnumerable<TestData> GetTest(int testFind, int modemFind)
        {
            try
            {
                if (modemFind != 0)
                    return connect.Table<TestData>().Where(t => t.IdT == testFind && t.IdM == modemFind && t.IdU == idu);
                else
                    return connect.Table<TestData>().Where(t => t.IdT == testFind && t.IdU == idu);
            }
            catch (Exception e)
            {
                Message = e.Message;
            }
            return Enumerable.Empty<TestData>();
        }

    }
}
