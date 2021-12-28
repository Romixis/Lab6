using System;
using System.Windows.Forms;

namespace lab6
{
    public partial class GoogleSheets : Form
    {
        private Main _form1 { get; set; }
        public GoogleSheets(Form form)
        {
            _form1 = (Main)form;
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;  // Вывод формы по центру экрана
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            string link = textBox1.Text;
            if (CheckUrl(link))
            {
                string id = GetDocumentId(link);
                Close();
                SheetsList list = new SheetsList(_form1, id);
                list.Show();
            }
            else
            {
                MessageBox.Show("Проверьте ссылку", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public string GetDocumentId(string link)
        {
            string id = link.Substring(link.IndexOf("d/") + 2);
            id = id.Substring(0, id.IndexOf('/'));
            return id;
        }
        public bool CheckUrl(string link)
        {
            bool result = Uri.TryCreate(link, UriKind.Absolute, out Uri uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            if (result && link.Contains("docs.google.com/spreadsheets/d/"))
            {
                return true;
            }
            return false;
        }
        private void OpenGoogleTable_FormClosed_1(object sender, FormClosedEventArgs e)
        {
            _form1.Enabled = true;
            _form1.Focus();
        }

        private void Export_Click(object sender, EventArgs e)
        {
            _form1.isExport = false;
            string link = textBox1.Text;
            if (CheckUrl(link))
            {
                string id = GetDocumentId(link);
                Sheets Table = new Sheets(id);
                bool isExist = false;
                string Name = "Export";
                foreach (var title in Table.GetSheetTitles())
                {
                    if(title == Name)
                    {
                        isExist = true;
                        break;
                    }
                }
                if (!isExist)
                {
                    Table.AddNewSheet(Name);
                }
                double[,] export = new double[_form1.N,_form1.N+2];
                for (int i = 0; i < export.GetLength(0); ++i)
                {
                    for (int j = 0; j < _form1.N + 1; ++j)
                    {
                        export[i, j] = _form1.Matrix[i, j];
                    }
                    if (_form1.X != null)
                    {
                        export[i, _form1.N + 1] = _form1.X[i];
                    }
                }
                Table.ExportToSheet(Name, export);
                Close();
            }
        }

        private void OpenGoogleTable_Load(object sender, EventArgs e)
        {
            Export.Enabled = _form1.isExport;
        }
    }
}
