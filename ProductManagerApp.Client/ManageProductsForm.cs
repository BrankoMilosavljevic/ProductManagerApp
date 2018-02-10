using ProductManagerApp.Client.ClientModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Configuration;
using System.Globalization;

namespace ProductManagerApp.Client
{
    public partial class ManageProductsForm : Form
    {
        string _uri = string.Empty;

        public ManageProductsForm()
        {
            InitializeComponent();
            InitializeControl();
        }

        private void InitializeControl()
        {
            _uri = ConfigurationManager.AppSettings["webAPI"];
            GetAllProducts();
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            AddProduct(this.textBoxName.Text, this.textBoxPhoto.Text, double.Parse(this.textBoxPrice.Text.Replace(',', '.'), CultureInfo.InvariantCulture));
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridViewProducts.SelectedRows.Count == 1)
            {
                UpdateProduct(Int32.Parse(dataGridViewProducts.SelectedRows[0].Cells[0].Value.ToString()), textBoxName.Text, this.textBoxPhoto.Text, double.Parse(this.textBoxPrice.Text.Replace(',', '.'), CultureInfo.InvariantCulture));
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewProducts.SelectedRows.Count == 1)
            {
                DeleteProduct(Int32.Parse(dataGridViewProducts.SelectedRows[0].Cells[0].Value.ToString()));
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (textBoxPriceFrom.Text != string.Empty && textBoxPriceTo.Text != string.Empty)
            {
                SearchProduct(textBoxSearchName.Text, double.Parse(textBoxPriceFrom.Text.Replace(',','.'), CultureInfo.InvariantCulture), double.Parse(textBoxPriceTo.Text.Replace(',', '.'), CultureInfo.InvariantCulture));
            }
            else
                MessageBox.Show("You must enter the price range", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void buttonShowAll_Click(object sender, EventArgs e)
        {
            GetAllProducts();
        }

        private async void GetAllProducts()
        {
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(FormUrl("all")))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var productJsonString = await response.Content.ReadAsStringAsync();

                        dataGridViewProducts.DataSource = JsonConvert.DeserializeObject<List<Product>>(productJsonString);

                    }
                }
            }
        }

        private async void AddProduct(string name, string path, double price)
        {
            Product p = new Product();
            p.Name = name;
            p.Photo = path;
            p.Price = price;
            p.LastUpdated = DateTime.Now;

            using (var client = new HttpClient())
            {
                var serializedProduct = JsonConvert.SerializeObject(p);
                var content = new StringContent(serializedProduct, Encoding.UTF8, "application/json");
                var result = await client.PostAsync(_uri, content);
            }
            GetAllProducts();
        }

        private async void UpdateProduct(int id, string name, string path, double price)
        {
            Product p = new Product();
            p.Id = id;
            p.Name = name;
            p.Photo = path;
            p.Price = price;
            p.LastUpdated = DateTime.Now;

            using (var client = new HttpClient())
            {
                var serializedProduct = JsonConvert.SerializeObject(p);
                var content = new StringContent(serializedProduct, Encoding.UTF8, "application/json");
                var result = await client.PutAsync(_uri, content);
            }
            GetAllProducts();
        }

        private async void DeleteProduct(int id)
        {
            using (var client = new HttpClient())
            {
                var result = await client.DeleteAsync(FormUrl($"{id}"));
            }
            GetAllProducts();
        }

        private async void SearchProduct(string name, double priceFrom, double priceTo)
        {
            using (var client = new HttpClient())
            {
                if (name == string.Empty)
                    name = "return_all";

                using (var response = await client.GetAsync($"{_uri}/{name}/{priceFrom}/{priceTo}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var productJsonString = await response.Content.ReadAsStringAsync();

                        dataGridViewProducts.DataSource = JsonConvert.DeserializeObject<List<Product>>(productJsonString);
                    }
                }
            }
        }

        private void dataGridViewProducts_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridViewProducts.SelectedRows.Count == 1)
            {
                textBoxName.Text = dataGridViewProducts.SelectedRows[0].Cells[1].Value.ToString();
                textBoxPhoto.Text = dataGridViewProducts.SelectedRows[0].Cells[2].Value.ToString();
                textBoxPrice.Text = dataGridViewProducts.SelectedRows[0].Cells[3].Value.ToString();
            }
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialogPhoto.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBoxPhoto.Text = openFileDialogPhoto.FileName;
            }
        }

        private string FormUrl(string route)
        {
            return $"{_uri}/{route}";
        }
    }
}
