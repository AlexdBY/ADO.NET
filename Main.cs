using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_3
{
    public partial class Main : Form
    {
        public static string coffeDB = Application.StartupPath + "\\DB.mdb;";

        string CommandText;
        OleDbDataAdapter oleDbAdapter;
        OleDbConnection oleDbConnect;
        OleDbCommand oleDbCommand;
        DataSet dataSet = new DataSet();
        public Main()
        {
            InitializeComponent();

            try
            {
                oleDbConnect = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + coffeDB);
                oleDbConnect.Open();
                
                CommandText = "SELECT * FROM Coffee";
                oleDbCommand = new OleDbCommand(CommandText, oleDbConnect);
                oleDbAdapter = new OleDbDataAdapter();
                oleDbAdapter.SelectCommand = oleDbCommand;
                oleDbAdapter.Fill(dataSet, "Coffee");

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = dataSet.Tables[0];

                oleDbAdapter.Dispose();
                oleDbConnect.Close();
            }
            
            catch (OleDbException e)
            {
                MessageBox.Show("Не возможно открыть соединение!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddCoffee a = new AddCoffee();
            a.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DeleteForm d = new DeleteForm();
            d.ShowDialog();
        }

        private void saveToXMLButton_Click(object sender, EventArgs e)
        {
            oleDbConnect.Open();
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "txt files (*.xml)|*.xml";
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    myStream.Close();
                    dataSet.WriteXml(saveFileDialog1.FileName);
                    MessageBox.Show("Сохранено в xml.");
                }
                else MessageBox.Show("Ошибка сохранения.");
            }
            oleDbConnect.Close();
        }

        private void SaveToXSDButton_Click(object sender, EventArgs e)
        {
            oleDbConnect.Open();
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "txt files (*.xsd)|*.xsd";
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    myStream.Close();
                    dataSet.WriteXmlSchema(saveFileDialog1.FileName);
                    MessageBox.Show("Сохранено в xsd.");
                }
                else MessageBox.Show("Ошибка сохранения.");
            }
            oleDbConnect.Close();
        }

        private void IDSortbutton_Click(object sender, EventArgs e)
        {
            ListSortDirection direction = ListSortDirection.Ascending;
            dataGridView1.Sort(dataGridView1.Columns[2], direction);
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        public void Refresh()
        {
            for (int i = dataGridView1.Rows.Count - 1; i >= 0; i--)
            {
                dataSet.Tables["Coffee"].Rows.RemoveAt(i);
            }
            CommandText = "SELECT * FROM Coffee";
            oleDbCommand = new OleDbCommand(CommandText, oleDbConnect);
            oleDbAdapter = new OleDbDataAdapter();
            oleDbAdapter.SelectCommand = oleDbCommand;
            oleDbAdapter.Fill(dataSet, "Coffee");
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = dataSet.Tables[0]; 
        }
    }
}
