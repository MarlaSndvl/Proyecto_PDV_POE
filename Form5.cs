using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Proyecto_Punto_de_Venta
{
    public partial class Form5 : Form
    {
        private Conexion mConexion;
        int codigo;

        public int NuevaCantidad { get; private set; }
        public string NuevaDescripcion { get; private set; }
        public int NuevoCosto { get; private set; }
        public int NuevoPrecio { get; private set; }

        public Form5(int codigo, int cantidad, string descripcion, int costo, int precio)
        {
            
                InitializeComponent();
                mConexion = new Conexion();
                mConexion.getConexion();
                this.codigo = codigo;
                

            // Cargar los datos del producto en los TextBoxes
            
                textBox2.Text = cantidad.ToString();
                textBox3.Text = descripcion;
                textBox4.Text = costo.ToString();
                textBox5.Text = precio.ToString();

           
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Obtener los nuevos datos ingresados por el usuario
            
            int nuevaCantidad = int.Parse(textBox2.Text);
            string nuevaDescripcion = textBox3.Text;
            int nuevoCosto = int.Parse(textBox4.Text);
            int nuevoPrecio = int.Parse(textBox5.Text);

            this.DialogResult = DialogResult.OK;
            this.Close();

            // Actualizar los datos del producto en la base de datos
            string consulta = "UPDATE productos SET Cantidad = @NuevaCantidad, Descripcion = @NuevaDescripcion, Costo = @NuevoCosto, Precio = @NuevoPrecio WHERE Codigo = @Codigo";

           
            using (MySqlCommand mySqlCommand = new MySqlCommand(consulta))
                
            {
                // Agregar parámetros para evitar la inyección de SQL
                mySqlCommand.Connection = mConexion.getConexion();
                mySqlCommand.Parameters.AddWithValue("@Codigo", codigo);
                mySqlCommand.Parameters.AddWithValue("@NuevaCantidad", nuevaCantidad);
                mySqlCommand.Parameters.AddWithValue("@NuevaDescripcion", nuevaDescripcion);
                mySqlCommand.Parameters.AddWithValue("@NuevoCosto", nuevoCosto);
                mySqlCommand.Parameters.AddWithValue("@NuevoPrecio", nuevoPrecio);


                try
                {
                    // Ejecutar el comando SQL
                    int rowsAffected = mySqlCommand.ExecuteNonQuery();

                    // Comprobar si se actualizó el registro en la base de datos
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Los datos del producto se han actualizado correctamente en la base de datos.");
                        // Establecer el resultado del diálogo como OK y cerrar el formulario
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error al actualizar los datos del producto en la base de datos.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al ejecutar la consulta SQL: " + ex.Message);
                }
            }
        }

                // Cerrar la ventana después de guardar los cambios
                
    }

        
}

