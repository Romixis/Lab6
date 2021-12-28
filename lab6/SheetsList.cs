using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace lab6
{
    public partial class SheetsList : Form
    {
        private Main _form1 { get; set; }
        private Sheets Table { get; set; }
        public SheetsList(Form form, string id)
        {
            _form1 = (Main)form;
            Table = new Sheets(id);
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;  // Вывод формы по центру экрана
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            Close();
            string SelectedName = listBox1.SelectedItem.ToString();
            IList<IList<object>> list = (IList<IList<object>>)Table.ReadEntries(SelectedName);
            _form1.ValidateImportedData(list);
            _form1.Enabled = true;
            _form1.Focus();
        }

        private void SelectGoogleSheetName_FormClosed(object sender, FormClosedEventArgs e)
        {
            _form1.Enabled = true;
            _form1.Focus();
        }

        private void SelectGoogleSheetName_Load(object sender, EventArgs e)
        {
            foreach (var title in Table.GetSheetTitles())
            {
                listBox1.Items.Add(title);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }
    }
}
