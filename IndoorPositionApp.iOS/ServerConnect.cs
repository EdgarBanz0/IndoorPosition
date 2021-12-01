using IndoorPositionApp.iOS;
using IndoorPositionApp.Values;
using MySql.Data.MySqlClient;
using Xamarin.Forms;

[assembly: Dependency(typeof(ServerConnect))]
namespace IndoorPositionApp.iOS
{
    class ServerConnect //: DBconnection
    {
        public bool TryConnection()
        {
            MySqlConnectionStringBuilder Builder = new MySqlConnectionStringBuilder();
            Builder.Port = 3306;
            // usar la ip del servidor
            Builder.Server = "sql3.freemysqlhosting.net";
            Builder.Database = "sql3407330";
            Builder.UserID = "sql3407330"; //usuario de la base de datos
            Builder.Password = "ahprawp9lP"; //La contraseña del usuario
            try
            {
                MySqlConnection ms = new MySqlConnection(Builder.ToString());
                ms.Open(); //agregar la referencia System.Data
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}