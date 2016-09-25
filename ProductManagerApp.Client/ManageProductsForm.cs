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

namespace ProductManagerApp.Client
{
    public partial class ManageProductsForm : Form
    {
        string uri = string.Empty;

        public ManageProductsForm()
        {
            InitializeComponent();
            InitializeControl();
        }

        private void InitializeControl()
        {
            uri = ConfigurationManager.AppSettings["webAPI"].ToString();
            GetAllProducts();
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            AddProduct(this.textBoxName.Text, this.textBoxPhoto.Text, double.Parse(this.textBoxPrice.Text));
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridViewProducts.SelectedRows.Count == 1)
            {
                UpdateProduct(Int32.Parse(dataGridViewProducts.SelectedRows[0].Cells[0].Value.ToString()), textBoxName.Text, this.textBoxPhoto.Text, double.Parse(this.textBoxPrice.Text));
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
            if (textBoxSearchName.Text != string.Empty && textBoxPriceFrom.Text != string.Empty && textBoxPriceTo.Text != string.Empty)
            {
                SearchProduct(textBoxSearchName.Text, double.Parse(textBoxPriceFrom.Text), double.Parse(textBoxPriceTo.Text));
            }
        }

        private async void GetAllProducts()
        {
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(uri))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var productJsonString = await response.Content.ReadAsStringAsync();

                        dataGridViewProducts.DataSource = JsonConvert.DeserializeObject<Product[]>(productJsonString).ToList();

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
                var result = await client.PostAsync(uri, content);
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
                var result = await client.PutAsync(uri, content);
            }
            GetAllProducts();
        }


        private async void DeleteProduct(int id)
        {
            using (var client = new HttpClient())
            {
                var result = await client.DeleteAsync(String.Format("{0}/{1}", uri, id));
            }
            GetAllProducts();
        }

        private async void SearchProduct(string name, double priceFrom, double priceTo)
        {
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(string.Format("{0}/{1}/{2}/{3}", uri, name, priceFrom, priceTo)))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var productJsonString = await response.Content.ReadAsStringAsync();

                        dataGridViewProducts.DataSource = JsonConvert.DeserializeObject<Product[]>(productJsonString).ToList();

                    }
                }
            }
        }

    }
}
