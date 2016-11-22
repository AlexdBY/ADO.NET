using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_3
{
    public partial class DeleteForm : Form
    {
        string CommandText;
        string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source";
        public static string coffeDB = Application.StartupPath + "\\DB.mdb;";
        OleDbConnection oleDbConnect;
        OleDbCommand oleDbCommand;
        OleDbDataAdapter oleDbAdapter;
        public DeleteForm()
        {
            InitializeComponent();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                oleDbConnect = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + coffeDB);
                oleDbConnect.Open();
                int id = Convert.ToInt32(textBox1.Text);
                CommandText = "DELETE * FROM Coffee WHERE CoffeeID=" + id;
                oleDbCommand = new OleDbCommand(CommandText, oleDbConnect);
                int result = oleDbCommand.ExecuteNonQuery();
                if (result != 0)
                {
                    this.Close();
                    MessageBox.Show("Продукт удален.");
                }
                else
                {
                    MessageBox.Show("Продукта с таким ID не существует.");
                }

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            oleDbConnect.Close();
        }
    }
}
