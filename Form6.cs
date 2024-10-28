using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Proyecto_Punto_de_Venta
{
    public partial class Form6 : Form
    {
        private DataGridView dataGridView1;

        private string consulta;
        private string codigo;
        private string cantidad;
        private int rCodigo;
        private string rDescripcion;
        private int rPrecio;
        private int subtotal;
        private Conexion mConexion;
        public Form6()
        {
            InitializeComponent();
            mConexion = new Conexion();
            mConexion.getConexion();
            dataGridView1 = this.Controls["dataGridView1"] as DataGridView;



        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        private void textBox1_Enter(object sender, EventArgs e)
        {
            // Borrar el texto gris cuando el usuario hace clic en el TextBox
            if (textBox1.Text == "Codigo")
            {
                textBox1.Clear();
                textBox1.ForeColor = Color.Black; // Cambiar el color del texto a negro
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            // Borrar el texto gris cuando el usuario hace clic en el TextBox
            if (textBox2.Text == "Cantidad")
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.Black; // Cambiar el color del texto a negro
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            // Restablecer el texto gris si el TextBox está vacío cuando pierde el foco
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.Text = "Codigo";
                textBox1.ForeColor = Color.Gray; // Restablecer el color del texto a gris
            }

        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            // Restablecer el texto gris si el TextBox está vacío cuando pierde el foco
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                textBox2.Text = "Cantidad";
                textBox2.ForeColor = Color.Gray; // Restablecer el color del texto a gris
            }
        }

        public void busqueda()
        {
            MySqlDataReader mySqlDataReader = null;
            string consulta = "SELECT Codigo, Descripcion, Precio FROM productos WHERE Codigo = @Codigo";

            try
            {
                using (MySqlCommand mySqlCommand = new MySqlCommand(consulta))
                {
                    mySqlCommand.Parameters.AddWithValue("@Codigo", codigo);
                    mySqlCommand.Connection = mConexion.getConexion();

                    mySqlDataReader = mySqlCommand.ExecuteReader();

                    while (mySqlDataReader.Read())
                    {
                        rCodigo = mySqlDataReader.GetInt32("Codigo");
                        rDescripcion = mySqlDataReader.GetString("Descripcion");
                        rPrecio = mySqlDataReader.GetInt32("Precio");

                        // Aquí puedes hacer algo con los valores, como agregarlos a una lista o mostrarlos en un control
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


        public void agregar(object sender, KeyEventArgs e)
        {
            codigo = textBox1.Text;
            cantidad = textBox2.Text;


            if (e.KeyCode == Keys.Enter)
            {

                if (codigo != "Codigo" && cantidad != "Cantidad")
                {

                    busqueda();


                    Convert.ToInt32(codigo);
                    subtotal = rPrecio * Convert.ToInt32(cantidad);


                    dataGridView1.Rows.Add(rCodigo, cantidad, rDescripcion, rPrecio, subtotal);

                    textBox1.Clear();
                    textBox2.Clear();
                    textBox1.Focus();

                }
                else
                {
                    MessageBox.Show("El campo codigo o cantidad esta vacio");
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox1.Focus();

                }

            }
            else
            {

            }

        }








    }
}
