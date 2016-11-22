using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_3
{
    public partial class AddCoffee : Form
    {
        string CommandText;
        string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source";
        public static string coffeDB = Application.StartupPath + "\\DB.mdb;";
        OleDbConnection oleDbConnect;
        OleDbCommand oleDbCommand;
        OleDbDataAdapter oleDbAdapter;
        DataSet dataSet = new DataSet();

        public AddCoffee()
        {
            InitializeComponent();
        }

        private void addCoffeeButton_Click(object sender, EventArgs e)
        {
            try
            {

            oleDbConnect = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + coffeDB);
            oleDbConnect.Open();
             
            string coffeeName = nameBox.Text;
            float price = Convert.ToSingle(priceBox.Text);
            int weight = Convert.ToInt32(weightBox.Text);
            int manufacturerID = Convert.ToInt32(listBox1.Text);
            int dealerID = Convert.ToInt32(listBox2.Text);

                if ((price <= 0) || (weight <= 0))
                {
                    MessageBox.Show("Проверьте введенные данные!");
                }
                else
                {

                    CommandText =
                        "INSERT INTO Coffee ([CoffeeID], [CoffeName], [Price], [Weight], [ManufacturerID], [DealerID]) " +
                        "VALUES(" + (CountCoffeeID() + 1) + ", '" + coffeeName + "' ,'" + price + "','" + weight + "','" +
                        manufacturerID + "','" + dealerID + "')";

                    oleDbCommand = new OleDbCommand(CommandText, oleDbConnect);
                    int result = oleDbCommand.ExecuteNonQuery();
                    if (result != 0)
                    {
                        this.Close();
                        MessageBox.Show("Продукт добавлен!");
                    }
                    else
                    {
                        MessageBox.Show("Продукт не был добавлен.");
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        public int CountCoffeeID()
        {
            int coffeeID = 0;
            string CommandText;
            CommandText = "SELECT Count(CoffeeID) From Coffee";
            oleDbCommand = new OleDbCommand(CommandText, oleDbConnect);
            int count = Convert.ToInt32(oleDbCommand.ExecuteScalar());
            CommandText = "SELECT CoffeeID FROM Coffee";
            oleDbCommand = new OleDbCommand(CommandText, oleDbConnect);
            oleDbAdapter = new OleDbDataAdapter();
            oleDbAdapter.SelectCommand = oleDbCommand;
            oleDbAdapter.Fill(dataSet, "Coffee");
            int[] arr = new int[count];

            DataTable table = new DataTable();
            oleDbAdapter.Fill(table);

            for (int i = 0; i < count; ++i)
            {
                arr[i] = Convert.ToInt32(table.Rows[i]["CoffeeID"]);
            }
            for (int j = 0; j < count; ++j)
            {
                if (coffeeID < arr[j])
                {
                    coffeeID = arr[j];
                }
            }
            return coffeeID;
        }
    }
}
