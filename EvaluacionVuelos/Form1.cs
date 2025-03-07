using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EvaluacionVuelos
{
    public partial class Form1 : Form
    {
        List<clsVuelos> lstVuelos;
        public Form1()
        {
            InitializeComponent();
            lstVuelos = new List<clsVuelos>();
        }

       private void btnCrearVuelo_Click(object sender, EventArgs e)
        {
            try
            {
               
                // Validar que el código del vuelo no esté vacío
                string codigo = txtCodigoVuelo.Text.Trim();
                if (string.IsNullOrEmpty(codigo))
                {
                    MessageBox.Show("Ingrese un código de vuelo válido.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Validar que el origen no esté vacío
                string origen = txtOrigen.Text.Trim();
                if (string.IsNullOrEmpty(origen))
                {
                    MessageBox.Show("Ingrese el origen del vuelo.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Validar que el destino no esté vacío
                string destino = txtDestino.Text.Trim();
                if (string.IsNullOrEmpty(destino))
                {
                    MessageBox.Show("Ingrese el destino del vuelo.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Validar que la fecha de salida sea una fecha futura
                DateTime fechaSalida = dtpFechaSalida.Value;
                if (fechaSalida <= DateTime.Now)
                {
                    MessageBox.Show("La fecha de salida debe ser futura.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Validar que los asientos disponibles sean un número entero positivo
                if (!int.TryParse(txtAsientosDisponibles.Text, out int asientosDisponibles) || asientosDisponibles <= 0)
                {
                    MessageBox.Show("Ingrese un número válido de asientos disponibles.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Crear el vuelo
                clsVuelos nuevoVuelo = new clsVuelos(codigo, origen, destino, fechaSalida, asientosDisponibles);

                // Agregar el vuelo a la lista
                lstVuelos.Add(nuevoVuelo);
                listBoxVuelos.Items.Add(nuevoVuelo);  // Se mostrará usando el método ToString()

                // Mostrar confirmación
                MessageBox.Show("Vuelo agregado correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpiar los campos
                txtCodigoVuelo.Clear();
                txtOrigen.Clear();
                txtDestino.Clear();
                txtAsientosDisponibles.Clear();
                dtpFechaSalida.Value = DateTime.Now.AddDays(1);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnReservarVuelo_Click(object sender, EventArgs e)
        {
           
            try
            {
                if (txtCodigoReserva.Text == "")
                {
                    MessageBox.Show("Debe ingresar un código de vuelo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Guardar el código que el usuario escribió
                string codigoVuelo = txtCodigoReserva.Text;

                // Buscar el vuelo en la lista
                clsVuelos vueloEncontrado = null;
                int posicion;
                int cantidadReservas;
                foreach (var vuelo in lstVuelos)
                {
                    if (vuelo.GetCodigo() == codigoVuelo)
                    {
                        posicion = lstVuelos.IndexOf(vuelo);
                        vueloEncontrado = vuelo;
                        // Validaciones antes de restar los asientos
                        if (vueloEncontrado == null)
                        {
                            MessageBox.Show("El vuelo no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (!int.TryParse(txtCantidadReservas.Text, out cantidadReservas) || cantidadReservas <= 0)
                        {
                            MessageBox.Show("Ingrese una cantidad válida de asientos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (vueloEncontrado.getAsientosDisponibles() < cantidadReservas)
                        {
                            MessageBox.Show("No hay suficientes asientos disponibles.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                          
                            lstVuelos[posicion].RestarAsientos(cantidadReservas);

                        }
                        ActualizarListaVuelos();

                        MessageBox.Show("Reserva realizada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }
                }

               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarListaVuelos()
        {
            listBoxVuelos.Items.Clear();
            foreach (var vuelo in lstVuelos)
            {
                listBoxVuelos.Items.Add(vuelo.ToString());
            }
        }
        
       
    }
}
