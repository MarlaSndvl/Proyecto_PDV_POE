using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_Punto_de_Venta
{
    public partial class Form1 : Form
    {

        private Conexion mConexion;
        private int codigo;
        private int cantidad;
        private string descripcion;
        private int costo;
        private int precio;
        public Form1()
        {
            InitializeComponent();
           
            mConexion = new Conexion();
            mConexion.getConexion();
            cargarProductos(); // Llama a CargarProductos al iniciar la aplicación
            Form4 form4 = new Form4(this);
            
        }

        

        public void cargarProductos()
        {
            MySqlDataReader mySqlDataReader = null;
            string consulta = "SELECT * from productos";

            try
            {
                using (MySqlCommand mySqlCommand = new MySqlCommand(consulta))
                {
                    mySqlCommand.Connection = mConexion.getConexion();

                    mySqlDataReader = mySqlCommand.ExecuteReader();

                    while (mySqlDataReader.Read())
                    {
                        codigo = mySqlDataReader.GetInt32("Codigo");
                        cantidad = mySqlDataReader.GetInt32("Cantidad");
                        descripcion = mySqlDataReader.GetString("Descripcion");
                        costo = mySqlDataReader.GetInt32("Costo");
                        precio = mySqlDataReader.GetInt32("Precio");


                        dataGridView1.Rows.Add(codigo, cantidad, descripcion, costo, precio);


                        // Aquí puedes hacer algo con los valores, como agregarlos a una lista o mostrarlos en un control
                        // Por ejemplo, podrías crear una clase Producto y almacenar los productos en una lista de esa clase.
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción adecuadamente
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                if (mySqlDataReader != null)
                {
                    mySqlDataReader.Close();
                }
            }
        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        

        private void ventaToolStripMenuItem_Click(object sender, EventArgs e)
        {
         Form1_Load(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Crear e instanciar la pantalla emergente de agregar producto
            Form4 agregarForm = new Form4(this);

            // Mostrar la pantalla emergente
            agregarForm.ShowDialog();

            // Actualizar la tabla de productos después de cerrar la pantalla emergente (si es necesario)
            dataGridView1.Rows.Clear();
            cargarProductos(); // Método que carga los productos en la tabla
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Verificar si se ha seleccionado un producto en la tabla
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Obtener los detalles del producto seleccionado

                codigo = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Codigo"].Value);
                cantidad = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Cantidad"].Value);
                descripcion = Convert.ToString(dataGridView1.SelectedRows[0].Cells["Descripcion"].Value);
                costo = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Costo"].Value);
                precio = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Precio"].Value);

                // Crear e instanciar la pantalla emergente de modificación del producto
                Form5 modificarForm = new Form5(codigo, cantidad, descripcion, costo, precio);

                // Mostrar la pantalla emergente
                modificarForm.ShowDialog();

                // Obtener los nuevos valores después de cerrar Form5
                cantidad = modificarForm.NuevaCantidad;
                descripcion = modificarForm.NuevaDescripcion;
                costo = modificarForm.NuevoCosto;
                precio = modificarForm.NuevoPrecio;

                // Actualizar la tabla de productos después de cerrar la pantalla emergente (si es necesario)
                dataGridView1.Rows.Clear();
                cargarProductos(); // Método que carga los productos en la tabla
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un producto para modificar.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Verificar si se ha seleccionado un producto en la tabla
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Obtener el ID del producto seleccionado
                int idProducto = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);

                // Mostrar un mensaje de confirmación al usuario
                DialogResult result = MessageBox.Show("¿Estás seguro de que deseas eliminar este producto?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Realizar la operación de eliminación del producto en la base de datos
                    EliminarProducto(idProducto);

                    // Actualizar la tabla de productos después de la eliminación (si es necesario)
                   
                    cargarProductos(); // Método que carga los productos en la tabla
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un producto para eliminar.");
            }
        }

        private void EliminarProducto(int idProducto)
        {
            // Consulta SQL para eliminar el producto con el ID especificado
            string consulta = "DELETE FROM Tabla_Productos WHERE Id = @IdProducto";

            try
            {
                
                {
                    // Abrir la conexión
                   

                    // Crear un comando SQL con la consulta y la conexión
                    using (MySqlCommand command = new MySqlCommand(consulta))
                    {
                        // Agregar el parámetro del ID del producto
                        command.Parameters.AddWithValue("@IdProducto", idProducto);

                        // Ejecutar la consulta
                        int rowsAffected = command.ExecuteNonQuery();

                        // Comprobar si se eliminó el producto
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Producto eliminado correctamente.");
                        }
                        else
                        {
                            MessageBox.Show("No se pudo eliminar el producto.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el producto: " + ex.Message);
            }
        }
    }
}
