using CodeFirst;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CodeFirst.Model;


namespace CustomerManager
{
    public partial class CustomerViewer : Form
    {
        SampleContext context = new SampleContext();

        byte[] Ph;//Ссылка на массив байт, для загрузки фото заказчика.

        public CustomerViewer()
        {
           InitializeComponent();
         //  Database.SetInitializer(new DropCreateDatabaseIfModelChanges<SampleContext>());
        }
        private void Output()
        {
            if (this.CustomerradioButton.Checked == true)
                GridView.DataSource = context.Customers.ToList();
            else if (this.OrderradioButton.Checked == true)
                GridView.DataSource = context.Orders.ToList();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            
            try
            {
                Customer customer = new Customer
                {
                    FirstName = this.textBoxname.Text,
                    LastName = this.textBoxlastname.Text,
                    Email = this.textBoxmail.Text,
                    Age = Int32.Parse(this.textBoxage.Text),
                    Orders =
orderlistBox.SelectedItems.OfType<Order>().ToList(),
                    Photo = Ph
                };
                context.Customers.Add(customer);
                
                context.SaveChanges();
                Output();
                textBoxname.Text = String.Empty;
                textBoxmail.Text = String.Empty;
                textBoxage.Text = String.Empty;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.ToString());
            }

        }

        private void buttonFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog diag = new OpenFileDialog();
            if (diag.ShowDialog() == DialogResult.OK)
            {
                Image bm = new Bitmap(diag.OpenFile());
                ImageConverter converter = new ImageConverter();
                Ph = (byte[])converter.ConvertTo(bm, typeof(byte[]));
            }
        }

        private void buttonOut_Click(object sender, EventArgs e)
        {
            Output();
 //Cоздание LINQ-запроса на выборку заказчиков и сортировки их по имени:
            var query = from b in context.Customers
                        orderby b.FirstName
                        select b;
            customerList.DataSource = query.ToList();
            textBoxlastname.Text = String.Empty;//Очищаем поле фамилии.
        }

        private void customerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void CustomerViewer_Load(object sender, EventArgs e)
        {
               context.Orders.Add(new Order
                {
                    ProductName = "Аудио",
                    Quantity =
                12,
                    PurchaseDate = DateTime.Parse("12.01.2016")
                });
                context.Orders.Add(new Order
                {
                    ProductName = "Видео",
                    Quantity =
                22,
                    PurchaseDate = DateTime.Parse("10.01.2016")
                });
                context.SaveChanges();
                orderlistBox.DataSource = context.Orders.ToList();
        }

        private void GridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
                
            }

        private void GridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (GridView.CurrentRow == null) return;
            var customer = GridView.CurrentRow.DataBoundItem as
            Customer;
            if (customer == null) return;
            labelid.Text = Convert.ToString(customer.CustomerId);
            textBoxCustomer.Text = customer.ToString();
            textBoxname.Text = customer.FirstName;
            textBoxlastname.Text = customer.LastName;
            textBoxmail.Text = customer.Email;
            textBoxage.Text = Convert.ToString(customer.Age);

        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (labelid.Text == String.Empty) return;
            var id = Convert.ToInt32(labelid.Text);
            var customer = context.Customers.Find(id);
            if (customer == null) return;
            customer.FirstName = this.textBoxname.Text;
            customer.LastName = this.textBoxlastname.Text;
            customer.Email = this.textBoxmail.Text;
            customer.Age = Int32.Parse(this.textBoxage.Text);
            context.Entry(customer).State = EntityState.Modified;
            context.SaveChanges();
            Output();
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (labelid.Text == String.Empty) return;
            var id = Convert.ToInt32(labelid.Text);
            var customer = context.Customers.Find(id);
            context.Entry(customer).State = EntityState.Deleted;
            context.SaveChanges();
            Output();
        }
    }
}
