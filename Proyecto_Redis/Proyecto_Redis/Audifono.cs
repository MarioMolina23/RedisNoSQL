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
    public partial class Audifono : Form
    {
        public Audifono()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 llamar = new Form1();
            llamar.Show();

            this.Hide();
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

        private void Audifono_Load(object sender, EventArgs e)
        {
            using (RedisClient client = new RedisClient("localhost", 6379))
            {
                IRedisTypedClient<AudifonoClass> Audifono = client.As<AudifonoClass>();
                AudifonoClassBindingSource.DataSource = Audifono.GetAll();
                Edit(true); //Read-only
            }
        }

        private void buttonAgregar_Click(object sender, EventArgs e)
        {
            ClearText();
            AudifonoClassBindingSource.Add(new AudifonoClass());
            AudifonoClassBindingSource.MoveLast();
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
            AudifonoClassBindingSource.ResetBindings(false);
            using (RedisClient client = new RedisClient("localhost", 6379))
            {
                IRedisTypedClient<AudifonoClass> Audifono = client.As<AudifonoClass>();
                AudifonoClassBindingSource.DataSource = Audifono.GetAll();

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

                AudifonoClass c = AudifonoClassBindingSource.Current as AudifonoClass;
                if (c != null)
                {


                    using (RedisClient client = new RedisClient("localhost", 6379))
                    {
                        IRedisTypedClient<AudifonoClass> Audifono = client.As<AudifonoClass>();
                        Audifono.DeleteById(c.Modelo);
                        AudifonoClassBindingSource.RemoveCurrent();
                        ClearText();
                    }
                }
            }

        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            using (RedisClient client = new RedisClient("localhost", 6379))
            {
                AudifonoClassBindingSource.EndEdit();
                IRedisTypedClient<AudifonoClass> Audifono = client.As<AudifonoClass>();
                Audifono.StoreAll(AudifonoClassBindingSource.DataSource as List<AudifonoClass>);
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
