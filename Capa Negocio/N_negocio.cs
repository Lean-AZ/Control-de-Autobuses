using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidades;
using Capa_Datos;
using System.Data;
using System.Data.SqlClient;
namespace Capa_Negocio
{
    public class N_negocio
    {


        D_datos objdatos = new D_datos(); // Instancia de la capa de acceso a datos

        // Método para registrar un nuevo chofer utilizando la capa de acceso a datos

        public void RegistrarChofer(Chofer chofer)
        {
            objdatos.RegistrarChofer(chofer);
        }

        // Método para registrar un nuevo autobús utilizando la capa de acceso a datos
        public void RegistrarAutobus(Autobus autobus)
        {
            objdatos.RegistrarAutobus(autobus);
        }

        // Método para registrar una nueva ruta utilizando la capa de acceso a datos
        public void RegistrarRuta(Ruta ruta)
        {
            objdatos.RegistrarRuta(ruta);
        }

        // Método para verificar la disponibilidad de chofer, autobús y ruta utilizando la capa de acceso a datos
        public bool VerificarDisponibilidad(int choferId, int autobusId, int rutaId)
        {
            return objdatos.VerificarDisponibilidad(choferId, autobusId, rutaId);
        }

        // Método para asignar un chofer a un autobús y ruta específicos utilizando la capa de acceso a datos
        public void AsignarChoferAutobusRuta(int choferId, int autobusId, int rutaId, DateTime fechaAsignacion)
        {
            if (VerificarDisponibilidad(choferId, autobusId, rutaId))
            {
                objdatos.AsignarChoferAutobusRuta(choferId, autobusId, rutaId, fechaAsignacion);
            }
            else
            {
                throw new Exception("Uno de los elementos ya está asignado.");
            }

        }
        // Método para obtener todas las asignaciones registradas utilizando la capa de acceso a datos
        public List<Asignacion> ObtenerAsignaciones()
        {
            return objdatos.ObtenerAsignaciones();
        }

        public bool EliminarAsignacion(int asignacionID)
        {
            // Llamar al método de eliminación en la capa de datos
            return objdatos.EliminarAsignacion(asignacionID);
        }


    }


}


    



