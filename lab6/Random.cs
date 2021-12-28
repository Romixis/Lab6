using System;
using System.Windows.Forms;

namespace lab6
{
    public partial class Random : Form
    {
        Main Form1 { get; set; }
        public Random(Main form1)
        {
            Form1 = form1;
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;  // Вывод формы по центру экрана
        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {
            try
            {
                int n = Convert.ToInt32(textBox1.Text);

                if (n < 2)
                {
                    MessageBox.Show("Матрица не может быть меньше 1", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (n > 50)
                {
                    MessageBox.Show("Матрица не может быть больше 50", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                System.Random rand = new System.Random();

                double[,] Matrix = new double[n, n + 1];

                for (int i = 0; i < n; ++i)
                {
                    for (int j = 0; j < n; ++j)
                    {
                        Matrix[i, j] = rand.Next(-10, 10);
                    }
                    Matrix[i, n] = rand.Next(-10, 10);
                }
                Form1.InitDataGrid(Matrix, n);
                this.Close();
            }
            catch
            {
                MessageBox.Show("Проверьте введённую матрицу", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void GenerateData_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1.Enabled = true;
            Form1.Focus();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox2.Text = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox1.Text = textBox2.Text;
        }
    }
}
