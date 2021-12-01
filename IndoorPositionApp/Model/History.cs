using SQLite;
using System;

namespace IndoorPositionApp.Model
{
    class History
    {
        [PrimaryKey, AutoIncrement]
        public int IdH { get; set; }

        //numero de identificacion asociado al usuario
        public int IdU { get; set; }

        //Fecha y hora del registro
        [MaxLength(50)]
        public String Date { get; set; }

        //Distancia registrada
        public decimal Distance { get; set; }
    }
}
