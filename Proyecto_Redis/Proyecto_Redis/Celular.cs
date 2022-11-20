using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
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
    public partial class Celular : Form
    {
        public Celular()
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

        void Edit(bool value) {
            textBoxModelo.ReadOnly = value;
            textBoxMarca.ReadOnly = value;
            textBoxPrecio.ReadOnly = value;
            textBoxAlmacenamiento.ReadOnly = value;
            textBoxColor.ReadOnly = value;

        }

        private void Celular_Load(object sender, EventArgs e)
        {
            using (RedisClient client = new RedisClient("localhost", 6379))
            {
                IRedisTypedClient<CelularClass> celular = client.As<CelularClass>();
                celularClassBindingSource.DataSource= celular.GetAll();
                Edit(true); //Read-only
            }
        }

        private void buttonAgregar_Click(object sender, EventArgs e)
        {
            ClearText();
            celularClassBindingSource.Add(new CelularClass());
            celularClassBindingSource.MoveLast();
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
            celularClassBindingSource.ResetBindings(false);
            using (RedisClient client = new RedisClient("localhost", 6379))
            {
                IRedisTypedClient<CelularClass> celular = client.As<CelularClass>();
                celularClassBindingSource.DataSource = celular.GetAll();
                
            }
        }

        void ClearText() {
            textBoxModelo.Text = string.Empty;
            textBoxMarca.Text = string.Empty;
            textBoxPrecio.Text = string.Empty;
            textBoxAlmacenamiento.Text = string.Empty;
            textBoxColor.Text = string.Empty;
        }

        private void buttonEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "¿Está seguro que quiere eliminar?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {

                CelularClass c = celularClassBindingSource.Current as CelularClass;
                if (c != null) {
                    
                
                    using (RedisClient client = new RedisClient("localhost", 6379))
                    {
                        IRedisTypedClient<CelularClass> celular = client.As<CelularClass>();
                        celular.DeleteById(c.Modelo);
                        celularClassBindingSource.RemoveCurrent();
                        ClearText();
                    }
                }
            }
                
        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            using (RedisClient client = new RedisClient("localhost", 6379))
            {
                celularClassBindingSource.EndEdit();
                IRedisTypedClient<CelularClass> celular = client.As<CelularClass>();
                celular.StoreAll(celularClassBindingSource.DataSource as List<CelularClass>);
                MessageBox.Show(this, "Datos han sido guardados con éxito", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearText();
                Edit(true);//Read-only
            }
        }
    }
}
