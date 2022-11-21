using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_Redis
{
    public partial class Monitor : Form
    {
        public Monitor()
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
            txt_Modelo.ReadOnly = value;
            txt_Marca.ReadOnly = value;
            txt_Color.ReadOnly = value;
            txt_Tamano.ReadOnly = value;
            txt_Unidades.ReadOnly = value;
            txt_Precio.ReadOnly = value;
        }

        void ClearText()
        {
            txt_Modelo.Text = string.Empty;
            txt_Marca.Text = string.Empty;
            txt_Color.Text = string.Empty;
            txt_Tamano.Text = string.Empty;
            txt_Unidades.Text = string.Empty;
            txt_Precio.Text = string.Empty;
        }

        private void Monitor_Load(object sender, EventArgs e)
        {
            using (RedisClient client = new RedisClient("localhost", 6379))
            {
                IRedisTypedClient<MonitorClass> monitor = client.As<MonitorClass>();
                monitorClassBindingSource.DataSource = monitor.GetAll();
                Edit(true);
            }
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonAgregar_Click(object sender, EventArgs e)
        {
            ClearText();
            monitorClassBindingSource.Add(new MonitorClass());
            monitorClassBindingSource.MoveLast();
            Edit(false);
            txt_Modelo.Focus(); //Veo que en su parte usted no pone esto
        }

        private void buttonEditar_Click(object sender, EventArgs e)
        {
            Edit(false);
            txt_Modelo.Focus();
        }

        private void buttonEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "¿Está seguro que quiere eliminar?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                MonitorClass p = monitorClassBindingSource.Current as MonitorClass;
                if (p != null)
                {
                    using (RedisClient client = new RedisClient("localhost", 6379))
                    {
                        IRedisTypedClient<MonitorClass> monitor = client.As<MonitorClass>();
                        monitor.DeleteById(p.Modelo);
                        monitorClassBindingSource.RemoveCurrent();
                        ClearText();
                    }
                }
            }
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            Edit(true);
            monitorClassBindingSource.ResetBindings(false);
            ClearText();
            /*using (RedisClient client = new RedisClient("localhost", 6379))
            {
                IRedisTypedClient<MonitorClass> monitor = client.As<MonitorClass>();
                monitorClassBindingSource.DataSource = monitor.GetAll();

            }*/ //Esto lo tenemos distinto
        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            using (RedisClient client = new RedisClient("localhost", 6379))
            {
                monitorClassBindingSource.EndEdit();
                IRedisTypedClient<MonitorClass> monitor = client.As<MonitorClass>();
                monitor.StoreAll(monitorClassBindingSource.DataSource as List<MonitorClass>);
                MessageBox.Show(this, "Datos han sido guardados con éxito", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearText();
                Edit(true);
            }
        }
    }
}
