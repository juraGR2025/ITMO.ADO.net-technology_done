using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CreatingDataTable
{
    public partial class Form1 : Form
    {
        private DataSet dataSet;//Объявляем объекты (ссылки) на набор данных и источник 
        //данных для привязки к элементу DataGridView(два элемента – два источника).        
        private BindingSource bindingSource1;
        private BindingSource bindingSource2;
        private void MakeDataTables()
        {
            MakeParentTable();
            MakeChildTable();
            MakeDataRelation();
            BindToDataGrid();
        }

        public Form1()
        {
            InitializeComponent();
            MakeDataTables();
        }
        private void MakeParentTable()//Определяем метод MakeParentTable(). В нем создается таблица как объект 
//класса DataTable с присвоением в конструкторе имени ParentTable и объявляются две ссылки: на столбец DataColumn и на строку DataRow
        {
            DataTable table = new DataTable("ParentTable");

            DataColumn column;
            DataRow row;
//В методе создается первый столбец с помощью констуктора класса DataColumn.
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "id";
            column.ReadOnly = true;
            column.Unique = true;

            table.Columns.Add(column);
//Далее в методе  добавляется второй столбец.
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ParentItem";
            column.AutoIncrement = false;
            column.Caption = "ParentItem";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);
            /*Третий и четвертый столбцы добавляются с помощью метода Add().
            Они принимают агументы ColumnName, DataType и Expression
            (вычисляемое значение – применяется в четвертом столбце) и создает новый
            столбец данных в качестве члена коллекции:*/
            table.Columns.Add("Total", typeof(Double));
            table.Columns.Add("SalesTax", typeof(Double), "Total * 0.13");
            /* Первый столбец указывается в качестве первичного ключа таблицы. Для этого 
            сначала создается массив из одного или в общем случае нескольких объектов 
            DataColumn, потом формируется ключ (можно из нескольких столбцов)
            добавив столбец id в массив, а затем с помощью свойства таблицы 
            PrimaryKey назначается первичный ключ*/
            DataColumn[] PrimaryKeyColumns = new DataColumn[1];
            PrimaryKeyColumns[0] = table.Columns["id"];
            table.PrimaryKey = PrimaryKeyColumns;
            //Создается набор данных (DataSet) и добавляем в него созданную таблицу.
            dataSet = new DataSet();
            dataSet.Tables.Add(table);
            /*Чтобы добавить новую строку в таблицу сначала вызоваем для таблицы метод NewRow().
            Возвращаемый результат присваивается ранее объявленной ссылке на DataRow, 
            затем, используя имя столбца или индекс заполните ячейки произвольными 
            данными и. наконец, добавьте созданный объект строки в коллекцию строк 
            таблицы (для удобства примера используется цикл):*/
            for (int i = 0; i <= 2; i++)
            {
                row = table.NewRow();
                row["id"] = i;
                row["ParentItem"] = "ParentItem " + i;
                row["Total"] = 2 * i + 0.5;
                table.Rows.Add(row);
            }

        }

        private void MakeChildTable()
        {
            DataTable table = new DataTable("childTable");
            DataColumn column;
            DataRow row;
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "ChildID";
            column.AutoIncrement = true;
            column.Caption = "ID";
            column.ReadOnly = true;
            column.Unique = true;
            table.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ChildItem";
            column.AutoIncrement = false;
            column.Caption = "ChildItem";
            column.ReadOnly = false;
            column.Unique = false;
            table.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "ParentID";
            column.AutoIncrement = false;
            column.Caption = "ParentID";
            column.ReadOnly = false;
            column.Unique = false;
            table.Columns.Add(column);
            dataSet.Tables.Add(table);
            for (int i = 0; i <= 4; i++)
            {
                row = table.NewRow();
                row["childID"] = i;
                row["ChildItem"] = "Item " + i;
                row["ParentID"] = 0;
                table.Rows.Add(row);
            }
            for (int i = 0; i <= 4; i++)
            {
                row = table.NewRow();
                row["childID"] = i + 5;
                row["ChildItem"] = "Item " + i;
                row["ParentID"] = 1;
                table.Rows.Add(row);
            }
            for (int i = 0; i <= 4; i++)
            {
                row = table.NewRow();
                row["childID"] = i + 10;
                row["ChildItem"] = "Item " + i;
                row["ParentID"] = 2;
                table.Rows.Add(row);
            }
        }
        /*Для связи таблиц друг с другом, для перехода по таблицам, а также 
для возвращения дочерних или родительских строк из связанной таблицы в наборе 
DataSet с несколькими объектами DataTable можно использовать объекты 
DataRelation*/
        private void MakeDataRelation()
        {
            // DataRelation requires two DataColumn 
            // (parent and child) and a name.
            DataColumn parentColumn =
            dataSet.Tables["ParentTable"].Columns["id"];
            DataColumn childColumn =
            dataSet.Tables["ChildTable"].Columns["ParentID"];
            DataRelation relation = new
            DataRelation("parent2Child", parentColumn, childColumn);
            dataSet.Tables["ChildTable"].ParentRelations.Add(relation);
        }
        /*Определяем метод BindToDataGrid(), в котором создается два объекта 
        BindingSource, которые будут использоваться для привязки данных к 
        элементу формы, данными будут являться созданные таблицы:*/
        private void BindToDataGrid()
        {
            bindingSource1 = new BindingSource();
            bindingSource2 = new BindingSource();
            bindingSource1.DataSource = dataSet.Tables["ParentTable"];
            bindingSource2.DataSource = dataSet.Tables["childTable"];
        }


        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = bindingSource1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = bindingSource2;
        }
    }
}
