using SQLite;
using System;

namespace IndoorPositionApp.Model
{
    class User
    {
        internal object pruebaRSSI;

        //numero de identificacion, autoincremental y unico
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        //Nombre de usuario tamano de 1000 caracteres y unico
        [MaxLength(50)]
        public String Name { get; set; }

        //Edad del usuario
        public string Age { get; set; }

        //Email de usuario unico
        [MaxLength(50), Unique]
        public String Email { get; set; }

        //contrasena de usuario
        [MaxLength(50)]
        public String Password { get; set; }

        //Id del usuario en seguimiento
        public string IdFollow { get; set; }

        //Constante k para estimacion de distancia
        public string ConsK { get; set; }

        //Nombres de routers calibrados
        public string CalibratedRouters { get; set; }
        public string pruebRSSI { get; set; }

        public string permanente { get; set; }
    }
}
