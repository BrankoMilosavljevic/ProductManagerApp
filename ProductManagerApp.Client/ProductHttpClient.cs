using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProductManagerApp.Client.Contract;

namespace ProductManagerApp.Client
{
    public class ProductHttpClient
    {
        private readonly string _baseUri;
        public ProductHttpClient()
        {
            _baseUri = ConfigurationManager.AppSettings["webAPI"];
        }
        public async Task<List<ProductContract>> GetAllProducts()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(FormUrl("all"));
                
                if (!response.IsSuccessStatusCode)
                    return new List<ProductContract>();

                var productJsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ProductContract>>(productJsonString);
                
            }
        }

        public async Task AddProduct(string name, string path, double price)
        {
            ProductContract p = new ProductContract
            {
                Name = name,
                Photo = path,
                Price = price,
                LastUpdated = DateTime.Now
            };

            using (var client = new HttpClient())
            {
                var serializedProduct = JsonConvert.SerializeObject(p);
                var content = new StringContent(serializedProduct, Encoding.UTF8, "application/json");
                await client.PostAsync(_baseUri, content);
            }
        }

        public async Task UpdateProduct(int id, string name, string path, double price)
        {
            ProductContract p = new ProductContract
            {
                Id = id,
                Name = name,
                Photo = path,
                Price = price,
                LastUpdated = DateTime.Now
            };

            using (var client = new HttpClient())
            {
                var serializedProduct = JsonConvert.SerializeObject(p);
                var content = new StringContent(serializedProduct, Encoding.UTF8, "application/json");
                await client.PutAsync(_baseUri, content);
            }
        }

        public async Task DeleteProduct(int id)
        {
            using (var client = new HttpClient())
            {
                await client.DeleteAsync(FormUrl($"{id}"));
            }
        }

        public async Task<List<ProductContract>> SearchProduct(string name, double priceFrom, double priceTo)
        {
            using (var client = new HttpClient())
            {
                if (name == string.Empty)
                    name = "return_all";

                var response = await client.GetAsync($"{_baseUri}/{name}/{priceFrom}/{priceTo}");

                if (!response.IsSuccessStatusCode)
                    return new List<ProductContract>();

                var productJsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ProductContract>>(productJsonString);
            }
        }
        private string FormUrl(string route)
        {
            return $"{_baseUri}/{route}";
        }
    }
}
