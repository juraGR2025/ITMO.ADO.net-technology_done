using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataAdapterProgram
{
    public partial class Form1 : Form
    {//Программноe добавление в поле класса формы объекта SqlConnection для 
    //соединения с базой данных на локальном сервере с передачей в конструктор
    //строки соединения.
        static SqlConnection NorthwindConnection = new
SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=Northwind;Integrated Security=True");
        public Form1()
        {
            InitializeComponent();
        }
        static string query = "SELECT * FROM Customers";//Строка запроса для получения всех записей из таблицы Customers.
 //Создаем объект адаптера (SqlDataAdapter) с передачей в конструктор переменной запроса и объекта подключения.
        static SqlDataAdapter SqlDataAdapter1 = new SqlDataAdapter(query, NorthwindConnection);
        //Создаем объект DataSet.
        DataSet NorthwindDataset = new DataSet("Northwind");
    // Для применения класса SqlCommandBuilder, который позволяет 
    // автоматически сгенерировать выражения вставки, обновления и удаления,
    // вызывается его конструктор, в который передается используемый адаптер.
        SqlCommandBuilder commands = new SqlCommandBuilder(SqlDataAdapter1);

        private void Form1_Load(object sender, EventArgs e)
        {
            SqlDataAdapter1.Fill(NorthwindDataset, "Customers");
            dataGridView1.DataSource = NorthwindDataset.Tables["Customers"];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NorthwindDataset.EndInit();
            SqlDataAdapter1.Update(NorthwindDataset.Tables["Customers"]);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataRow CustRow = NorthwindDataset.Tables["Customers"].NewRow();
//Строка для добавления в таблицу: 
 Object[] CustRecord = {"AAAAC", "Alfreds Futterkiste", "Maria Anders", "Sales Representative", "Obere Str. 57", "Berlin", null, 
"12209", "Germany", "030-0074321","030-0076545"};
            CustRow.ItemArray = CustRecord;
            NorthwindDataset.Tables["Customers"].Rows.Add(CustRow);

            SqlDataAdapter1.Update(NorthwindDataset.Tables["Customers"]);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            NorthwindDataset.EndInit();
            var index = dataGridView1.CurrentRow.Index;//Получаем индекс строки с активной ячейкой.
            NorthwindDataset.Tables["Customers"].Rows[index].Delete();//Вызывается метод Delete()для удаления строки с активной ячейкой.
            SqlDataAdapter1.Update(NorthwindDataset.Tables["Customers"]);

        }
    }
}
