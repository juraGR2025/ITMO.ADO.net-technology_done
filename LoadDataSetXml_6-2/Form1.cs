using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoadDataSetXml_6_2
{
    public partial class Form1 : Form
    {
        DataSet NorthwindDataSet = new DataSet();
        public Form1()
        {
            InitializeComponent();
        }

        private void LoadSchemaButton_Click(object sender, EventArgs e)
        {
        //Загрузка сведений схемы из файла.xsd:
         NorthwindDataSet.ReadXmlSchema("Northwind.xsd");
        //Связываются CustomersGrid и OrdersGrid для отображения данных:
        
            CustomersGrid.DataSource = NorthwindDataSet.Tables["Customers"];
            OrdersGrid.DataSource = NorthwindDataSet.Tables["Orders"];


        }

        private void LoadDataButton_Click(object sender, EventArgs e)
        {
            //Загрузка данных в набор данных:
            NorthwindDataSet.ReadXml("Northwind.xml");
        }
    }
}
