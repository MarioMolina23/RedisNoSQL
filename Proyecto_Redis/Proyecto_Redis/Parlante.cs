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
    public partial class Parlante : Form
    {
        public Parlante()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
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

        private void Parlente_Load(object sender, EventArgs e)
        {
            using (RedisClient client = new RedisClient("localhost", 6379))
            {
                IRedisTypedClient<ParlenteClass> Parlente = client.As<ParlenteClass>();
                ParlenteClassBindingSource.DataSource = Parlente.GetAll();
                Edit(true); //Read-only
            }
        }

        private void buttonAgregar_Click(object sender, EventArgs e)
        {
            ClearText();
            ParlenteClassBindingSource.Add(new ParlenteClass());
            ParlenteClassBindingSource.MoveLast();
            Edit(false);//Permite editar

        }

        private void buttonEditar_Click(object sender, EventArgs e)
        {
            Edit(false);//Permite Editar
            textBoxModelo.Focus();
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            ClearText();
            Edit(true); //read-only
            ParlenteClassBindingSource.ResetBindings(false);
            using (RedisClient client = new RedisClient("localhost", 6379))
            {
                IRedisTypedClient<ParlenteClass> Parlente = client.As<ParlenteClass>();
                ParlenteClassBindingSource.DataSource = Parlente.GetAll();

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

        private void buttonEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "¿Está seguro que quiere eliminar?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                ParlenteClass c = ParlenteClassBindingSource.Current as ParlenteClass;
                if (c != null)
                {


                    using (RedisClient client = new RedisClient("localhost", 6379))
                    {
                        IRedisTypedClient<ParlenteClass> Parlente = client.As<ParlenteClass>();
                        Parlente.DeleteById(c.Modelo);
                        ParlenteClassBindingSource.RemoveCurrent();
                        ClearText();
                    }
                }
            }

        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            using (RedisClient client = new RedisClient("localhost", 6379))
            {
                ParlenteClassBindingSource.EndEdit();
                IRedisTypedClient<ParlenteClass> Parlente = client.As<ParlenteClass>();
                Parlente.StoreAll(ParlenteClassBindingSource.DataSource as List<ParlenteClass>);
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

        private void textBoxModelo_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
