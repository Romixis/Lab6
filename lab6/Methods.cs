using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace lab6
{
    public partial class Methods : Form
    {
        List<Thread> threads;
        public Main Form1;
        public Methods(Main form1)
        {
            Form1 = form1;
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;  // Вывод формы по центру экрана
        }

        private void StartAlgorithms_Click(object sender, EventArgs e)
        {
            Form1.ClearTextBox();
            threads = new List<Thread>();
            double[,] Matrix = Form1.Matrix;
            int N = Form1.N;
            double eps = 0.0000000001;
            foreach (var item in CheckedListBox.CheckedItems)
            {
                AllMethods slae;
                switch (item)
                {
                    case "Гаусса":
                        slae = new Gauss(N,Matrix);
                        break;
                    case "Квадратного корня":
                        slae = new Cholesky(N, Matrix);
                        break;
                    case "Прогонки":
                        slae = new RunThrough(N, Matrix);
                        break;
                    case "Простой итерации":
                        slae = new SimpleIteration(N, Matrix, eps);
                        break;
                    case "Наискорейшего спуска":
                        slae = new GradientDescent(N, Matrix, eps);
                        break;
                    case "Сопряженных градиентов":
                        slae = new ConjugateGradient(N, Matrix, eps);
                        break;
                    default:
                        slae = new Gauss(N, Matrix);
                        break;

                }
                Thread thread = new Thread(() => 
                { 
                    CalculateIntegral(slae, item.ToString()); 
                });
                threads.Add(thread);
                thread.Start();
            }
        }
        private void SelectAlgorithmsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (threads != null)
            {
                foreach (var thread in threads)
                {
                    if (thread.IsAlive)
                    {
                        thread.Abort();
                    }
                }
            }
        }
        public void CalculateIntegral(AllMethods slae, string methodName)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            double[] X = slae.FindX();
            sw.Stop();

            string result = "время выполенение в мс: " + sw.Elapsed.TotalMilliseconds + "\n";
            if (slae.Iterations > 0)
            {
                result += "Количество итераций: " + slae.Iterations + "\n";
            }
            Form1.OutputResults(X, result, methodName);
        }
    }
}
