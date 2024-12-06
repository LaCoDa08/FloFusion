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
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.IO;

namespace FloFusion
{
    public partial class RegisterForm : Form
    {
        Connection conn = new Connection();
        Dictionary<int, int> selectedProducts= new Dictionary<int, int>();

        public RegisterForm()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.RegisterForm_Load);
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            LoadProductList();
        }

        private void LoadProductList() 
        {
            try
            {
                conn.OpenConnection();
                string query = "SELECT productID, productName, productPrice FROM product";
                DataSet ds = conn.DataSet(query);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    dgvProductList.AutoGenerateColumns = true;
                    dgvProductList.DataSource = ds.Tables[0];
                }
                else
                {
                    MessageBox.Show("No products available to display.");
                }

                conn.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading product list: " + ex.Message);
            }
        }

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (dgvProductList.SelectedRows.Count > 0)
            {
                int productId = (int)dgvProductList.SelectedRows[0].Cells["productID"].Value;
                string productName = dgvProductList.SelectedRows[0].Cells["productName"].Value.ToString();
                decimal productPrice = (decimal)dgvProductList.SelectedRows[0].Cells["productPrice"].Value;

                if (selectedProducts.ContainsKey(productId))
                {
                    selectedProducts[productId]++;
                }
                else
                {
                    selectedProducts.Add(productId, 1);
                }

                lbCart.Items.Add($"{productName} - {productPrice:C} x {selectedProducts[productId]}");
                UpdateTotal();
            }
            else
            {
                MessageBox.Show("Please select a product to add to the cart.");
            }
        }

        private void btnCompleteSale_Click(object sender, EventArgs e)
        {
            if (selectedProducts.Count == 0)
            {
                MessageBox.Show("No items in the cart to complete the sale.");
                return;
            }

            try
            {
                conn.OpenConnection();

                // Create transaction record
                int companyId = 1;
                string itemList = string.Join(",", selectedProducts);
                string query = "INSERT INTO `transaction` (employeeID, companyID, itemList, timeAndDate) " +
                               $"VALUES ({LoginInfo.EmployeeID}, '{companyId}', '{itemList}', " +
                               $"'{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')";

                conn.ExecuteNonQuery(query);

                // Update inventory
                foreach (var item in selectedProducts)
                {
                    string updateQuery = $"UPDATE inventory SET numberOfItemInStock = numberOfItemInStock - " +
                        $"{item.Value} WHERE productID = {item.Key}";
                    conn.ExecuteNonQuery(updateQuery);
                }

                MessageBox.Show("Sale completed successfully!");

                // Generate the PDF receipt
                GenerateTxtReceipt();

                // Clear cart
                selectedProducts.Clear();
                lbCart.Items.Clear();
                UpdateTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error completing sale: " + ex.Message);
            }
            finally
            {
                conn.CloseConnection();
            }
        }

        private void GenerateTxtReceipt()
        {
            try
            {
                // Define the file path where the receipt will be saved
                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"Receipt_{DateTime.Now:yyyyMMdd_HHmmss}.txt");

                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    // Write header information
                    writer.WriteLine("FloFusion Receipt");
                    writer.WriteLine("-------------------------------");
                    writer.WriteLine($"Date: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                    writer.WriteLine($"Employee ID: {LoginInfo.EmployeeID}");
                    writer.WriteLine();

                    // Write product details
                    writer.WriteLine("Product ID\tProduct Name\tQuantity\tPrice");

                    decimal total = 0;
                    foreach (var item in selectedProducts)
                    {
                        int productId = item.Key;
                        int quantity = item.Value;

                        // Get product details from database
                        conn.OpenConnection();
                        string query = $"SELECT productName, productPrice FROM product WHERE productID = {productId}";
                        MySqlDataReader reader = conn.DataReader(query);
                        if (reader.Read())
                        {
                            string productName = reader["productName"].ToString();
                            decimal productPrice = reader.GetDecimal("productPrice");

                            // Write product details to receipt
                            writer.WriteLine($"{productId}\t{productName}\t{quantity}\t{productPrice:C}");

                            // Calculate total price
                            total += productPrice * quantity;
                        }
                        conn.CloseConnection();
                    }

                    // Write total amount
                    writer.WriteLine();
                    writer.WriteLine($"Total Amount: {total:C}");
                    writer.WriteLine("-------------------------------");

                    writer.WriteLine("Thank you for your purchase!");
                }

                MessageBox.Show($"Receipt saved successfully as {filePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating receipt: " + ex.Message);
            }
        }

        private void UpdateTotal()
        {
            decimal total = 0;
            foreach (var item in selectedProducts)
            {
                // Fetch product price
                conn.OpenConnection();
                string query = $"SELECT productPrice FROM product WHERE productID = {item.Key}";
                MySqlDataReader reader = conn.DataReader(query);
                if (reader.Read())
                {
                    decimal productPrice = reader.GetDecimal("productPrice");
                    total += productPrice * item.Value;
                }
                conn.CloseConnection();
            }

            lblTotal.Text = $"Total: {total:C}";
        }

        private void btnRemoveFromCart_Click(object sender, EventArgs e)
        {
            if (lbCart.SelectedItem == null)
            {
                MessageBox.Show("Please select an item to remove from the cart.");
                return;
            }

            try
            {
                // Parse the selected item to determine productID
                string selectedItem = lbCart.SelectedItem.ToString();
                string[] parts = selectedItem.Split('-'); // Assuming the format: "ProductName - Price x Quantity"
                string productName = parts[0].Trim();

                // Find the product ID based on the product name
                int productId = -1;
                conn.OpenConnection();
                string query = $"SELECT productID FROM product WHERE productName = '{productName}'";
                MySqlDataReader reader = conn.DataReader(query);
                if (reader.Read())
                {
                    productId = reader.GetInt32("productID");
                }
                conn.CloseConnection();

                if (productId == -1)
                {
                    MessageBox.Show("Error: Unable to determine product ID.");
                    return;
                }

                // Remove one unit of the selected product from the cart
                if (selectedProducts.ContainsKey(productId))
                {
                    selectedProducts[productId]--;
                    if (selectedProducts[productId] <= 0)
                    {
                        selectedProducts.Remove(productId);
                    }

                    // Refresh the cart display
                    RefreshCartDisplay();
                    UpdateTotal();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error removing item from cart: " + ex.Message);
            }
        }

        private void RefreshCartDisplay()
        {
            lbCart.Items.Clear();
            foreach (var item in selectedProducts)
            {
                // Get product name and price from the database
                conn.OpenConnection();
                string query = $"SELECT productName, productPrice FROM product WHERE productID = {item.Key}";
                MySqlDataReader reader = conn.DataReader(query);
                if (reader.Read())
                {
                    string productName = reader["productName"].ToString();
                    decimal productPrice = reader.GetDecimal("productPrice");
                    lbCart.Items.Add($"{productName} - {productPrice:C} x {item.Value}");
                }
                conn.CloseConnection();
            }
        }
    }
}
