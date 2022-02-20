using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatasetDesigner
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void GetCustomersButton_Click(object sender, EventArgs e)
        {
            NorthwindDataSet NorthwindDataset1 = new NorthwindDataSet(); //Создается экземпляр типизированного набора данных Northwind.
            NorthwindDataSetTableAdapters.CustomersTableAdapter //Создается экземпляр класса CustomersTableAdapter.
     CustomersTableAdapter1 =
            new NorthwindDataSetTableAdapters.CustomersTableAdapter();
            CustomersTableAdapter1.Fill(NorthwindDataset1.Customers);//Вызывается метод для загрузки всех клиентов в DataTable.
            foreach (NorthwindDataSet.CustomersRow NWCustomer in //Значения столбца CompanyName передаются в ListBox.
 NorthwindDataset1.Customers.Rows)
            {
                CustomersListBox.Items.Add(NWCustomer.CompanyName);
            }


        }
    }
}
