using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades
{
    public class Chofer
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Cedula { get; set; }
    }

    public class Autobus
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }
        public string Color { get; set; }
        public int Año { get; set; }
    }

    public class Ruta
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

    public class Asignacion
    {
        public int ChoferID { get; set; }
        public string NombreChofer { get; set; }
        public int AutobusID { get; set; }
        public string MarcaAutobus { get; set; }
        public int RutaID { get; set; }
        public string NombreRuta { get; set; }
        public DateTime FechaAsignacion { get; set; }
    }

}