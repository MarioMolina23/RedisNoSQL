namespace Proyecto_Redis
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Celulares_Click(object sender, EventArgs e)
        {
            Celular llamar = new Celular();
            llamar.Show();

            this.Hide();
        }

        private void PC_Click(object sender, EventArgs e)
        {
            Computadora llamar = new Computadora();
            llamar.Show();

            this.Hide();
        }

        private void Audifonos_Click(object sender, EventArgs e)
        {
            Audifono llamar = new Audifono();
            llamar.Show();

            this.Hide();
        }

        private void Parlante_Click(object sender, EventArgs e)
        {
            Parlante llamar = new Parlante();
            llamar.Show();

            this.Hide();
        }

        private void Estuches_Click(object sender, EventArgs e)
        {
            Estuche llamar = new Estuche();
            llamar.Show();

            this.Hide();
        }

        private void Sillas_Gamers_Click(object sender, EventArgs e)
        {
            Silla_gamer llamar = new Silla_gamer();
            llamar.Show();

            this.Hide();
        }

        private void Monitores_Click(object sender, EventArgs e)
        {
            Monitor llamar = new Monitor();
            llamar.Show();

            this.Hide();
        }

        private void Escritorio_Click(object sender, EventArgs e)
        {
            Escritorio llamar = new Escritorio();
            llamar.Show();

            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}