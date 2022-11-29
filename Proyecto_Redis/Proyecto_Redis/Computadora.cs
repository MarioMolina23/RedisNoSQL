using ServiceStack.Redis.Generic;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_Redis
{
    public partial class Computadora : Form
    {
        public Computadora()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 llamar = new Form1();
            llamar.Show();

            this.Hide();
        }


        private void button2_Click(object sender, EventArgs e)
        {

        }
        void Edit(bool value)
        {
            textBoxModelo.ReadOnly = value;
            textBoxMarca.ReadOnly = value;
            textBoxPrecio.ReadOnly = value;
            textBoxAlmacenamiento.ReadOnly = value;
            textBoxColor.ReadOnly = value;

        }

        private void Computadora_Load(object sender, EventArgs e)
        {
            using (RedisClient client = new RedisClient("localhost", 6379))
            {
                IRedisTypedClient<ComputadoraClass> celular = client.As<ComputadoraClass>();
                computadoraClassBindingSource.DataSource = celular.GetAll();
                Edit(true); //Read-only
            }
        }

        private void buttonAgregar_Click_1(object sender, EventArgs e)
        {
            ClearText();
            computadoraClassBindingSource.Add(new ComputadoraClass());
            computadoraClassBindingSource.MoveLast();
            Edit(false);//Permite editar

        }

        private void buttonEditar_Click_1(object sender, EventArgs e)
        {
            Edit(false);//Permite Editar
            textBoxModelo.Focus();
        }

        private void buttonCancelar_Click_1(object sender, EventArgs e)
        {
            ClearText();
            Edit(true); //read-only
            computadoraClassBindingSource.ResetBindings(false);
            using (RedisClient client = new RedisClient("localhost", 6379))
            {
                IRedisTypedClient<ComputadoraClass> computadora = client.As<ComputadoraClass>();
                computadoraClassBindingSource.DataSource = computadora.GetAll();

            }
        }

        void ClearText()
        {
            textBoxModelo.Text = string.Empty;
            textBoxMarca.Text = string.Empty;
            textBoxPrecio.Text = string.Empty;
            textBoxAlmacenamiento.Text = string.Empty;
            textBoxColor.Text = string.Empty;
        }

        private void buttonEliminar_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "¿Está seguro que quiere eliminar?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                ComputadoraClass c = computadoraClassBindingSource.Current as ComputadoraClass;
                if (c != null)
                {


                    using (RedisClient client = new RedisClient("localhost", 6379))
                    {
                        IRedisTypedClient<ComputadoraClass> computadora = client.As<ComputadoraClass>();
                        computadora.DeleteById(c.Modelo);
                        computadoraClassBindingSource.RemoveCurrent();
                        ClearText();
                    }
                }
            }

        }

        private void buttonGuardar_Click_1(object sender, EventArgs e)
        {
            using (RedisClient client = new RedisClient("localhost", 6379))
            {
                computadoraClassBindingSource.EndEdit();
                IRedisTypedClient<ComputadoraClass> computadora = client.As<ComputadoraClass>();
                computadora.StoreAll(computadoraClassBindingSource.DataSource as List<ComputadoraClass>);
                MessageBox.Show(this, "Datos han sido guardados con éxito", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearText();
                Edit(true);//Read-only
            }
        }
        private void textBoxModelo_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBoxColor_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBoxAlmacenamiento_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBoxPrecio_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBoxMarca_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBoxModelo_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBoxColor_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBoxMarca_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBoxPrecio_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBoxAlmacenamiento_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}
