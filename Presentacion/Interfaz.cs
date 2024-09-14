using Capa_Datos;
using Capa_Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Capa_Negocio;
using Capa_Datos;
using Capa_Entidades;

namespace Presentacion
{
    public partial class Interfaz : Form
    {
        private N_negocio negocio = new N_negocio();   // Instancia de la capa de negocio


        private DataTable dtA; // / Objeto Da se refiere a un objeto de acceso a datos
        public Interfaz()
        {
            InitializeComponent();


        }


        // Método para cerrar la aplicación
        private void Salir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        // Evento para agregar un nuevo chofer
        private void btagregarchofer_Click(object sender, EventArgs e)
        {
            // Crear una instancia de Chofer y llenarla con los datos del formulario
            Chofer chofer = new Chofer
            {
                Nombre = txtnombre.Text,
                Apellido = txtapellido.Text,
                FechaNacimiento = datefecha.Value,
                Cedula = txtcedula.Text
            };
            negocio.RegistrarChofer(chofer);
            MessageBox.Show("Chofer registrado exitosamente.");


        }

        private void btagregarautobus_Click(object sender, EventArgs e)
        {
            // Crear una instancia de Autobus y llenarla con los datos del formulario
            Autobus autobus = new Autobus
            {
                Marca = txtmarca.Text,
                Modelo = txtmodelo.Text,
                Placa = txtplaca.Text,
                Color = txtcolor.Text,
                Año = Convert.ToInt32(txtaño.Text)
            };

            // Llamar al método RegistrarAutobus de la instancia de BusinessLogic
            negocio.RegistrarAutobus(autobus);

            // Mostrar un mensaje de éxito
            MessageBox.Show("Autobús registrado exitosamente.");



        }

        private void btagregarruta_Click(object sender, EventArgs e)
        {
            // Crear una instancia de Ruta y llenarla con los datos del formulario
            Ruta ruta = new Ruta
            {
                Nombre = comborutas.Text
            };

            // Llamar al método RegistrarRuta de la instancia de logica de negocios
            negocio.RegistrarRuta(ruta);

            // Mostrar un mensaje de éxito
            MessageBox.Show("Ruta registrada exitos");


        }




        // Evento para guardar una asignación
        private void Guardar_Click(object sender, EventArgs e)
        {     // Validar que los valores ingresados para Chofer ID, Autobús ID y Ruta ID sean números válidos
            if (!int.TryParse(txtChoferId.Text, out int choferId) ||
         !int.TryParse(txtAutobusId.Text, out int autobusId) ||
         !int.TryParse(txtRutaId.Text, out int rutaId))
            {
                MessageBox.Show("Por favor, ingrese valores numéricos válidos para Chofer ID, Autobús ID y Ruta ID.");
                return;
            }

            DateTime fechaAsignacion = datefecha.Value;

            try
            {      // Intentar asignar un chofer a un autobús y ruta utilizando la instancia de negocio
                negocio.AsignarChoferAutobusRuta(choferId, autobusId, rutaId, fechaAsignacion);
                MessageBox.Show("Asignación realizada exitosamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }




        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
            // Llama al método para obtener las asignaciones y mostrar en el DataGridView
            List<Asignacion> asignaciones = negocio.ObtenerAsignaciones();
            Lista.DataSource = asignaciones;
        }



        private void Eliminar_Click(object sender, EventArgs e)
        {
            if (Lista.SelectedRows.Count > 0)
            {
                // Iterar sobre las filas seleccionadas y obtener los IDs
                foreach (DataGridViewRow row in Lista.SelectedRows)
                {
                    int asignacionID = Convert.ToInt32(row.Cells["ChoferID"].Value);

                    // Llamar al método para eliminar la asignación de la base de datos
                    if (negocio.EliminarAsignacion(asignacionID))
                    {
                        // Si la eliminación en la base de datos fue exitosa, eliminar la fila del DataGridView
                        Lista.Rows.Remove(row);
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar la asignación con ID: " + asignacionID);
                    }
                }
            }
            else
            {
                MessageBox.Show("No se han seleccionado filas para eliminar.");
            }
        }
    
   
            



        

      
        // Método para limpiar los controles del formulario
        private void Limpiacontroles()
        {
            txtnombre.Text = "";
            txtapellido.Text = "";
            txtcedula.Text = "";
            datefecha.Text = "";
            txtChoferId.Text = "";
            txtAutobusId.Text = "";
            txtRutaId.Text = "";
            dateFechaId.Text = "";
            txtmarca.Text = "";
            txtmodelo.Text = "";
            txtaño.Text = "";
            txtplaca.Text = "";
            txtcolor.Text = "";
        }

        private void btnuevochofer_Click(object sender, EventArgs e)
        {
            Limpiacontroles();  
      
        }

        private void btnuevoautobus_Click(object sender, EventArgs e)
        {
            Limpiacontroles();
        }

        private void Nuevo_Click(object sender, EventArgs e)
        {
            Limpiacontroles();
        }


        }
    }
    
