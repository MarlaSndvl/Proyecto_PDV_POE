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
    public partial class Form4 : Form
    {
        private Conexion mConexion;
        Form1 instanciaForm1;

        public Form4(Form1 instanciaForm1)
        {
            InitializeComponent();
           
            mConexion = new Conexion();
            mConexion.getConexion();
            this.instanciaForm1 = instanciaForm1;
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        public void button1_Click(object sender, EventArgs e)
        {
            // Validación de entrada: asegúrate de que se ingresen datos en todos los campos
            if (string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text) || string.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Error de entrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validación de formato: asegúrate de que los valores de cantidad, costo y precio sean numéricos válidos
            if (!int.TryParse(textBox2.Text, out int cantidad) || cantidad <= 0)
            {
                MessageBox.Show("Por favor, ingrese una cantidad válida.", "Error de entrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textBox4.Text, out int costo) || costo <= 0)
            {
                MessageBox.Show("Por favor, ingrese un costo válido.", "Error de entrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textBox5.Text, out int precio) || precio <= 0)
            {
                MessageBox.Show("Por favor, ingrese un precio válido.", "Error de entrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Insertar los datos en la base de datos
            string consulta = "INSERT INTO productos (Cantidad, Descripcion, Costo, Precio) VALUES (@Cantidad, @Descripcion, @Costo, @Precio)";

            try
            {

                

                using (MySqlCommand mySqlCommand = new MySqlCommand(consulta))

                {
                    mySqlCommand.Connection = mConexion.getConexion();
                    mySqlCommand.Parameters.AddWithValue("@Cantidad", cantidad);
                    mySqlCommand.Parameters.AddWithValue("@Descripcion", textBox3.Text);
                    mySqlCommand.Parameters.AddWithValue("@Costo", costo);
                    mySqlCommand.Parameters.AddWithValue("@Precio", precio);

                    int rowsAffected = mySqlCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Los datos se han guardado correctamente en la base de datos.", "Operación exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        instanciaForm1.cargarProductos();
                    }
                    else
                    {
                        MessageBox.Show("Error al guardar los datos en la base de datos.", "Error de base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar con la base de datos: " + ex.Message, "Error de base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Limpiar los campos después de guardar
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
