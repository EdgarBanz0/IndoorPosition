using SQLite;

namespace IndoorPositionApp.Model
{
    public class TestData
    {
        [PrimaryKey, AutoIncrement]
        public int T { get; set; }

        //numero de identificacion asociado al usuario
        public int IdU { get; set; }

        //numero de identificacion asociado al test
        public int IdT { get; set; }

        //numero de identificacion asociado al modem
        public int IdM { get; set; }

        //Coordenadas del router (configurable)
        public string XY { get; set; }

        //RSSI registrado
        public int RSSI { get; set; }

        //distancia registrada a cada modem
        public decimal Distance { get; set; }

    }
}
