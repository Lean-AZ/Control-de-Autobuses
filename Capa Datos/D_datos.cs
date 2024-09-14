using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidades;
using System.Configuration;
using System.Data;



namespace Capa_Datos
{
    public class D_datos
    {
        // Establecer la conexión a la base de datos utilizando la cadena de conexión configurada
        SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conectar"].ConnectionString);


        // Método para registrar un nuevo chofer en la base de datos
        public void RegistrarChofer(Chofer chofer)
        {
            try
            {
                SqlCommand command = new SqlCommand("RegistrarChofer", conexion);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Nombre", chofer.Nombre);
                command.Parameters.AddWithValue("@Apellido", chofer.Apellido);
                command.Parameters.AddWithValue("@FechaNacimiento", chofer.FechaNacimiento);
                command.Parameters.AddWithValue("@Cedula", chofer.Cedula);

                conexion.Open();
                command.ExecuteNonQuery();
            }
            finally
            {
                conexion.Close();
            }
        }

        // Método para registrar un nuevo autobús en la base de datos
        public void RegistrarAutobus(Autobus autobus)
        {
            try
            {
                SqlCommand command = new SqlCommand("RegistrarAutobus", conexion);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Marca", autobus.Marca);
                command.Parameters.AddWithValue("@Modelo", autobus.Modelo);
                command.Parameters.AddWithValue("@Placa", autobus.Placa);
                command.Parameters.AddWithValue("@Color", autobus.Color);
                command.Parameters.AddWithValue("@Año", autobus.Año);

                conexion.Open();
                command.ExecuteNonQuery();
            }
            finally
            {
                conexion.Close();
            }
        }
        // Método para registrar una nueva ruta en la base de datos
        public void RegistrarRuta(Ruta ruta)
        {
            try
            {
                SqlCommand command = new SqlCommand("RegistrarRuta", conexion);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Ruta", ruta.Nombre);

                conexion.Open();
                command.ExecuteNonQuery();
            }
            finally
            {
                conexion.Close();
            }
        }

        // Método para verificar la disponibilidad de chofer, autobús y ruta para asignación
        public bool VerificarDisponibilidad(int choferId, int autobusId, int rutaId)
        {
            try
            {
                SqlCommand command = new SqlCommand("VerificarDisponibilidad", conexion);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ChoferID", choferId);
                command.Parameters.AddWithValue("@AutobusID", autobusId);
                command.Parameters.AddWithValue("@RutaID", rutaId);

                SqlParameter outputParam = new SqlParameter("@Disponible", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(outputParam);

                conexion.Open();
                command.ExecuteNonQuery();

                return (bool)outputParam.Value;
            }
            finally
            {
                conexion.Close();
            }
        }




        public void AsignarChoferAutobusRuta(int choferId, int autobusId, int rutaId, DateTime fechaAsignacion)
        {
            try
            {
                SqlCommand command = new SqlCommand("AsignarChoferAutobusRuta", conexion);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ChoferID", choferId);
                command.Parameters.AddWithValue("@AutobusID", autobusId);
                command.Parameters.AddWithValue("@RutaID", rutaId);
                command.Parameters.AddWithValue("@FechaAsignacion", fechaAsignacion);

                conexion.Open();
                command.ExecuteNonQuery();
            }
            finally
            {
                conexion.Close();
            }


        }

        public List<Asignacion> ObtenerAsignaciones()
        {
            var asignaciones = new List<Asignacion>();

            string query = @"
    SELECT ar.ChoferID,
           c.Nombre AS NombreChofer,
           ar.AutobusID,
           a.Marca AS MarcaAutobus,
           ar.RutaID,
           r.Ruta AS NombreRuta,
           ar.FechaAsignacion
    FROM Asignaciones ar
    INNER JOIN Choferes c ON ar.ChoferID = c.ChoferID
    INNER JOIN Autobuses a ON ar.AutobusID = a.AutobusID
    INNER JOIN Rutas r ON ar.RutaID = r.RutaID;";

     
            SqlCommand cmd = new SqlCommand(query, conexion);

            try
            {
                conexion.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var asignacion = new Asignacion
                        {
                            ChoferID = reader.GetInt32(0),
                            NombreChofer = reader.GetString(1),
                            AutobusID = reader.GetInt32(2),
                            MarcaAutobus = reader.GetString(3),
                            RutaID = reader.GetInt32(4),
                            NombreRuta = reader.GetString(5),
                            FechaAsignacion = reader.GetDateTime(6)
                        };
                        asignaciones.Add(asignacion);
                    }
                }
            }
            catch (SqlException ex)
            {
                // Manejar excepciones de SQL aquí
                Console.WriteLine("Error al obtener asignaciones: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión en el bloque finally para asegurar que siempre se cierre
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return asignaciones;
        }


        public bool EliminarAsignacion(int asignacionID)
        {
            string query = "DELETE FROM Asignaciones WHERE ID = @AsignacionID";
     {
                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@AsignacionID", asignacionID);

                try
                {
                    conexion.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
                catch (SqlException ex)
                {
                    // Manejar cualquier excepción de SQL aquí
                    Console.WriteLine("Error al eliminar asignación: " + ex.Message);
                    return false;
                }
                finally
                {
                    // Asegurar que la conexión se cierre siempre
                    if (conexion.State == ConnectionState.Open)
                    {
                        conexion.Close();
                    }
                }
            }
        }




    }






















}



    





    






