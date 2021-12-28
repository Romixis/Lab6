using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace lab6
{
    public partial class Main : Form
    {
        public double[,] Matrix;
        public int N;
        public double[] X;
        public bool isExport = false;
        public Main()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;  // Вывод формы по центру экрана
        }

        public void InitDataGrid(double[,] matrix, int n)
        {
            N = n;
            Matrix = matrix;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            for (int i = 0; i < N; ++i)
            {
                string name = "Столбец " + (i + 1);
                dataGridView1.Columns.Add(name, name);
                dataGridView1.Rows.Add();
            }
            dataGridView1.Columns.Add("B", "B");
            dataGridView1.Columns.Add("X", "X");
            for (int i = 0; i < N; ++i)
            {
                for(int j = 0; j < N; ++j)
                {
                    dataGridView1.Rows[i].Cells[j].Value = Matrix[i, j];
                }
                dataGridView1.Rows[i].Cells[N].Value = Matrix[i, N];
            }

        }
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            for (int i = 0; i < N; ++i)
            {
                for (int j = 0; j < N; ++j)
                {
                    Matrix[i, j] = Convert.ToDouble(dataGridView1.Rows[i].Cells[j].Value);
                }
                Matrix[i,N] = Convert.ToDouble(dataGridView1.Rows[i].Cells[N].Value);
            }
        }
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            TextBox tb = (TextBox)e.Control;
            tb.KeyPress += new KeyPressEventHandler(tb_KeyPress);
        }
        void tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!Char.IsNumber(e.KeyChar)) && (e.KeyChar != ',') && (e.KeyChar != '-'))
            {
                if ((e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Delete))
                {
                    e.Handled = true;
                }
            }
        }
        private void GenerateData_Click(object sender, EventArgs e)
        {
            Random rnd = new Random(this);
            rnd.Show();
            this.Enabled = false;
        }
        private void ImportExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "XML Files (*.xml; *.xls; *.xlsx; *.xlsm; *.xlsb) |*.xml; *.xls; *.xlsx; *.xlsm; *.xlsb";
            if (opf.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = opf.FileName;
            Excel.Application app = new Excel.Application();
            Excel.Workbook workbook = app.Workbooks.Open(filename);
            Excel.Worksheet worksheet = workbook.ActiveSheet;
            Excel.Range range = worksheet.UsedRange;

            var ExcelArray = range.Value;
            IList<IList<object>> list = new List<IList<object>>();
            int rows = range.Rows.Count;
            int column = range.Columns.Count;

            for (int i = 1; i <= rows; ++i)
            {
                object[] obj = new object[column];
                for (int j = 1; j <= column; ++j)
                {
                    obj[j - 1] = ExcelArray[i, j];
                }
                list.Add(obj);
            }

            ValidateImportedData(list);

            workbook.Close(false, Type.Missing, Type.Missing);
            app.Workbooks.Close();
            Marshal.ReleaseComObject(workbook);
        }
        public bool ValidateImportedData(IList<IList<object>> data)
        {
            int n = data.Count;
            double[,] matrix = new double[n, n + 1];
            _ = new double[n];
            for (int i = 0; i < n; ++i)
            {
                if ((data[i].Count - 1) != data.Count)
                {
                    MessageBox.Show("Столбцов должно быть на 1 больше, так как ещё есть B!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                for (int j = 0; j < data[i].Count; ++j)
                {
                    if (data[i][j] != null)
                    {
                        if (!double.TryParse(data[i][j].ToString(), out double tempElem))
                        {
                            MessageBox.Show("Где-то есть недопустимые символы", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                        if (j < (data[i].Count))
                        {
                            matrix[i, j] = tempElem;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пустая ячейка", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            InitDataGrid(matrix, n);
            return true;
        }
        public bool ValidateDataGrid()
        {
            int Rows = dataGridView1.Rows.Count;
            int Column = dataGridView1.Columns.Count - 1;
            if (Rows == 0)
            {
                MessageBox.Show("Ничего нет", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            for (int i = 0; i < Rows - 1; ++i)
            {
                for (int j = 0; j < Column; ++j)
                {
                    var qq = dataGridView1.Rows[i].Cells[j].Value;
                    if (qq == null)
                    {
                        MessageBox.Show("Не все ячейки заполнены!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            return true;
        }
        private void ImportGoogleTable_Click(object sender, EventArgs e)
        {
            GoogleSheets table = new GoogleSheets(this);
            table.Show();
            Enabled = false;
        }
        private void SelectMethods_Click(object sender, EventArgs e)
        {
            if (ValidateDataGrid())
            {
                Methods alg = new Methods(this);
                alg.Show();
            }
        }
        public void OutputResults(double[] X, string results, string methodName)
        {
            this.X = X;
            string x = "";
            if (X != null)
            {
                dataGridView1.Invoke((MethodInvoker)delegate
                {
                    for (int i = 0; i < N; ++i)
                    {
                        x += X[i] + ";  ";
                        dataGridView1.Rows[i].Cells[N + 1].Value = X[i];
                    }
                });
            }
            richTextBox1.Invoke((MethodInvoker)delegate
            {
                richTextBox1.AppendText(methodName + ": ", Color.Red);
                richTextBox1.AppendText(results + "Ответы: " + x + "\n\n");
                richTextBox1.Update();
            });
        }
        public void ClearTextBox()
        {
            richTextBox1.Invoke((MethodInvoker)delegate
            {
                richTextBox1.Clear();
            });
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Matrix = new double[,]
            {
                {8, -1, 2, 0, 2.3},
                {0, 10, -2, 2, -0.5},
                {-1, 0, 6, 2, -1.2},
                {3, -1, 2, 12, 3.7}
            };
            InitDataGrid(Matrix, 4);
        }
        public void ExportDataToExcelSheet(Excel.Worksheet sheet)
        {
            for (int i = 0; i < N; ++i)
            {
                for (int j = 0; j < N + 1; ++j)
                {
                    sheet.Cells[i + 1, j + 1] = Matrix[i, j];
                }
                if (X != null)
                {
                    sheet.Cells[i + 1, N + 2] = X[i];
                }
            }
        }

        private void сExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "XML Files (*.xml; *.xls; *.xlsx; *.xlsm; *.xlsb) |*.xml; *.xls; *.xlsx; *.xlsm; *.xlsb";
            if (opf.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = opf.FileName;
            Excel.Application app = new Excel.Application();
            Excel.Workbook workbook = app.Workbooks.Open(filename);
            Excel.Worksheet worksheet = workbook.ActiveSheet;
            Excel.Range range = worksheet.UsedRange;

            var ExcelArray = range.Value;
            IList<IList<object>> list = new List<IList<object>>();
            int rows = range.Rows.Count;
            int column = range.Columns.Count;

            for (int i = 1; i <= rows; ++i)
            {
                object[] obj = new object[column];
                for (int j = 1; j <= column; ++j)
                {
                    obj[j - 1] = ExcelArray[i, j];
                }
                list.Add(obj);
            }

            ValidateImportedData(list);

            workbook.Close(false, Type.Missing, Type.Missing);
            app.Workbooks.Close();
            Marshal.ReleaseComObject(workbook);
        }

        private void сExcelToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GoogleSheets table = new GoogleSheets(this);
            table.Show();
            Enabled = false;
        }

        private void вExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "XML Files (*.xml; *.xls; *.xlsx; *.xlsm; *.xlsb) |*.xml; *.xls; *.xlsx; *.xlsm; *.xlsb";
            if (opf.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = opf.FileName;
            Excel.Application app = new Excel.Application();
            Excel.Workbook workbook = app.Workbooks.Open(filename);
            Excel.Sheets Sheets = workbook.Sheets;
            bool isExist = false;
            Excel.Worksheet sheet = null;
            foreach (Excel.Worksheet qq in Sheets)
            {
                if (qq.Name == "Export")
                {
                    sheet = qq;
                    isExist = true;
                    break;
                }
            }
            if (!isExist)
            {
                sheet = (Excel.Worksheet)Sheets.Add(Sheets[1], Type.Missing, Type.Missing, Type.Missing);
                sheet.Name = "Export";
            }
            ExportDataToExcelSheet(sheet);
            workbook.Save();
            workbook.Close(Type.Missing, Type.Missing, Type.Missing);
            app.Quit();
            Marshal.ReleaseComObject(sheet);
            Marshal.ReleaseComObject(Sheets);
            Marshal.ReleaseComObject(workbook);
            Marshal.ReleaseComObject(app);
            app = null;
        }

        private void вGoogleSheetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isExport = true;
            GoogleSheets openGoogleTable = new GoogleSheets(this);
            openGoogleTable.Show();
        }
    }
}
public static class RichTextBoxExtensions
{
    public static void AppendText(this RichTextBox box, string text, Color color)
    {
        box.SelectionStart = box.TextLength;
        box.SelectionLength = 0;
        box.SelectionColor = color;
        box.AppendText(text);
        box.SelectionColor = box.ForeColor;
    }
}