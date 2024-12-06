using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FloFusion
{
    public partial class AddProductForm : Form
    {
        Connection conn = new Connection();
        public AddProductForm()
        {
            InitializeComponent();
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            string productName = txtProductName.Text.Trim();
            decimal productPrice;
            int initialStock;

            if (string.IsNullOrEmpty(productName) || !decimal.TryParse(txtProductPrice.Text.Trim(), out productPrice) || 
                !int.TryParse(txtInitialStock.Text.Trim(), out initialStock))
            {
                MessageBox.Show("Please enter valid product details.");
                return;
            }

            try
            {
                conn.OpenConnection();
                long productId = 0;
                string query = "INSERT INTO product (productName, productPrice) VALUES (@productName, @productPrice)";
                using (MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@productName", productName);
                    cmd.Parameters.AddWithValue("@productPrice", productPrice);
                    cmd.ExecuteNonQuery();
                    productId = cmd.LastInsertedId;
                }

                // Insert the initial stock into inventory table
                query = "INSERT INTO inventory (productID, numberOfItemInStock) VALUES (@productID, @initialStock)";
                using (MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@productID", productId);
                    cmd.Parameters.AddWithValue("@initialStock", initialStock);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Product added successfully!");
                txtProductName.Clear();
                txtProductPrice.Clear();
                txtInitialStock.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding product: " + ex.Message);
            }
            finally
            {
                conn.CloseConnection();
            }
        }
    }
}
