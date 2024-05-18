using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proje1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnHaritaBoyutuBelirle_Click(object sender, EventArgs e)
        {
            int haritaBoyutu;
            

            if (int.TryParse(txtHaritaBoyutu.Text, out haritaBoyutu) && haritaBoyutu > 0)
            {
                Form2 form2 = new Form2(haritaBoyutu);
                form2.Show();

                this.Hide();
            }
            else
            {
                MessageBox.Show("Lütfen geçerli bir pozitif tamsayı girin.");
            }

            int id = Convert.ToInt32(textBox1.Text);
            Karakter karkater = new Karakter(id);



        }
    }
}
