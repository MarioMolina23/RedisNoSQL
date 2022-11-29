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
    public partial class Estuche : Form
    {
        public Estuche()
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

        private void Escritorio_Load(object sender, EventArgs e)
        {
            using (RedisClient client = new RedisClient("localhost", 6379))
            {
                IRedisTypedClient<EscritorioClass> escritorio = client.As<EscritorioClass>();
                estucheClassBindingSource.DataSource = escritorio.GetAll();
                Edit(true); //Read-only
            }
        }

        private void buttonAgregar_Click(object sender, EventArgs e)
        {
            ClearText();
            estucheClassBindingSource.Add(new EscritorioClass());
            estucheClassBindingSource.MoveLast();
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
            estucheClassBindingSource.ResetBindings(false);
            using (RedisClient client = new RedisClient("localhost", 6379))
            {
                IRedisTypedClient<EstucheClass> estuche = client.As<EstucheClass>();
                estucheClassBindingSource.DataSource = estuche.GetAll();

            }
        }
        void ClearText()
        {
            textBoxModelo.Text = string.Empty;
            textBoxMarca.Text = string.Empty;
            textBoxPrecio.Text = string.Empty;
            textBoxColor.Text = string.Empty;
        }
        private void buttonEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "¿Está seguro que quiere eliminar?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                EstucheClass c = estucheClassBindingSource.Current as EstucheClass;
                if (c != null)
                {


                    using (RedisClient client = new RedisClient("localhost", 6379))
                    {
                        IRedisTypedClient<EstucheClass> estuche = client.As<EstucheClass>();
                        estuche.DeleteById(c.Modelo);
                        EstucheClassBindingSource.RemoveCurrent();
                        ClearText();
                    }
                }
            }
        }

       

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            using (RedisClient client = new RedisClient("localhost", 6379))
            {
                estucheClassBindingSource.EndEdit();
                IRedisTypedClient<EstucheClass> estuche = client.As<EstucheClass>();
                estuche.StoreAll(estucheClassBindingSource.DataSource as List<EstucheClass>);
                MessageBox.Show(this, "Datos han sido guardados con éxito", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearText();
                Edit(true);//Read-only
            }
        }

        private void textBoxModelo_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxMarca_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxPrecio_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxColor_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
