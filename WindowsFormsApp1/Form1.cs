using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dbManager db = dbManager.getInstance();
            LoadProducts();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string priceText = txtPrice.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Введіть назву товару!");
                return;
            }

            if (!decimal.TryParse(priceText, out decimal price))
            {
                MessageBox.Show("Введіть правильну ціну!");
                return;
            }

            try
            {
                dbManager db = dbManager.getInstance();

                string query = "INSERT INTO Products (Name, Price) VALUES ('"
                             + name.Replace("'", "''")
                             + "', "
                             + price.ToString(System.Globalization.CultureInfo.InvariantCulture)
                             + ")";

                db.ExecuteQuery(query);

                LoadProducts();

                Logger logger = Logger.getInstance();
                logger.Log("Додано товар: " + name);

                txtName.Clear();
                txtPrice.Clear();

                MessageBox.Show("Товар успішно додано!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка додавання товару: " + ex.Message);
            }
        }

        private void LoadProducts()
        {
            dbManager db = dbManager.getInstance();

            string query = "SELECT Id, Name, Price FROM Products";

            try
            {
                dgvProducts.DataSource = db.GetData(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка завантаження товарів: " + ex.Message);
            }
        }

        private void txtDocument_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void btnSaveDocument_Click(object sender, EventArgs e)
        {
            try
            {
                string content = "";

                foreach (DataGridViewRow row in dgvProducts.Rows)
                {
                    if (row.IsNewRow)
                        continue;

                    string product = "";

                    for (int i = 0; i < dgvProducts.Columns.Count; i++)
                    {
                        product += dgvProducts.Columns[i].HeaderText + ": "
                                 + row.Cells[i].Value?.ToString() + " | ";
                    }

                    content += product + Environment.NewLine;
                }

                Document document = new Document(
                    "Документ продуктового магазину",
                    content
                );

                DocumentSaver saver = DocumentSaver.getInstance();

                saver.SaveDocument(document);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка збереження: " + ex.Message);
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvProducts.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Спочатку оберіть товар!");
                    return;
                }

                DataGridViewRow row = dgvProducts.SelectedRows[0];

                string id = row.Cells["Id"].Value.ToString();
                string name = row.Cells["Name"].Value.ToString();

                dbManager db = dbManager.getInstance();

                string query = "DELETE FROM Products WHERE Id = " + id;

                db.ExecuteQuery(query);

                LoadProducts();

                Logger logger = Logger.getInstance();
                logger.Log("Видалено товар: " + name);

                MessageBox.Show("Товар успішно видалено!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка видалення: " + ex.Message);
            }
        }
    }
}
