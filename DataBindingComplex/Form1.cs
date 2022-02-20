using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBindingComplex
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void BindGridButton_Click(object sender, EventArgs e)
        {
    //Создается новый компонент BindingSource для таблицы Products:
BindingSource productsBindingSource = new
BindingSource(northwindDataSet1, "Products");
   // Связывается сетка с компонентом BindingSource:
            ProductsGrid.DataSource = productsBindingSource;
    //Навигатор связывается с компонентом BindingSource:
            bindingNavigator1.BindingSource = productsBindingSource;
    //Заполняется таблица Products данными из базы данных:
            productsTableAdapter1.Fill(northwindDataSet1.Products);
        }
    }
}
