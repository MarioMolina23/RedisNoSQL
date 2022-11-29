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
    public partial class Silla_gamer : Form
    {
        public Silla_gamer()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 llamar = new Form1();
            llamar.Show();

            this.Hide();
        }
        void Edit(bool value)
        {
            textBoxModelo.ReadOnly = value;
            textBoxMarca.ReadOnly = value;
            textBoxPrecio.ReadOnly = value;
            textBoxColor.ReadOnly = value;

        }

        private void Silla_gamer_Load(object sender, EventArgs e)
        {
            using (RedisClient client = new RedisClient("localhost", 6379))
            {
                IRedisTypedClient<Silla_gamerClass> silla_gamer = client.As<Silla_gamerClass>();
                silla_gamerClassBindingSource.DataSource = silla_gamer.GetAll();
                Edit(true); //Read-only
            }
        }

        private void buttonAgregar_Click(object sender, EventArgs e)
        {
            ClearText();
            silla_gamerClassBindingSource.Add(new Silla_gamerClass());
            silla_gamerClassBindingSource.MoveLast();
            Edit(false);//Permite editar

        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            ClearText();
            Edit(true); //read-only
            silla_gamerClassBindingSource.ResetBindings(false);
            using (RedisClient client = new RedisClient("localhost", 6379))
            {
                IRedisTypedClient<Silla_gamerClass> silla_gamer = client.As<Silla_gamerClass>();
                silla_gamerClassBindingSource.DataSource = silla_gamer.GetAll();

            }

        }
        private void buttonEditar_Click(object sender, EventArgs e)
        {
            Edit(false);//Permite Editar
            textBoxModelo.Focus();

        }
        void ClearText()
        {
            textBoxModelo.Text = string.Empty;
            textBoxMarca.Text = string.Empty;
            textBoxPrecio.Text = string.Empty;
            textBoxAlmacenamiento.Text = string.Empty;
            textBoxColor.Text = string.Empty;
        }
        private void buttonEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "¿Está seguro que quiere eliminar?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                Silla_gamerClass c = silla_gamerClassBindingSource.Current as Silla_gamerClass;
                if (c != null)
                {


                    using (RedisClient client = new RedisClient("localhost", 6379))
                    {
                        IRedisTypedClient<Silla_gamerClass> computadora = client.As<Silla_gamerClass>();
                        silla_gamer.DeleteById(c.Modelo);
                        silla_gamerClassBindingSource.RemoveCurrent();
                        ClearText();
                    }
                }
            }

        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            using (RedisClient client = new RedisClient("localhost", 6379))
            {
                silla_gamerClassBindingSource.EndEdit();
                IRedisTypedClient<Silla_gamerClass> celular = client.As<Silla_gamerClass>();
                silla_gamer.StoreAll(silla_gamerClassBindingSource.DataSource as List<Silla_gamerClass>);
                MessageBox.Show(this, "Datos han sido guardados con éxito", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearText();
                Edit(true);//Read-only
            }

        }

        private void textBoxColor_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
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

        private void textBoxModelo_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
