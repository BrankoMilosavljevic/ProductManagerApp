using System;
using System.Windows.Forms;
using System.Globalization;
using System.Threading.Tasks;

namespace ProductManagerApp.Client
{
    public partial class ManageProductsForm : Form
    {
        private ProductHttpClient _productHttpClient;

        public ManageProductsForm()
        {
            InitializeComponent();
            InitializeControl();
        }

        private async void InitializeControl()
        {
            _productHttpClient = new ProductHttpClient();
            await RefreshView();
        }

        private async void buttonCreate_Click(object sender, EventArgs e)
        {
            await _productHttpClient.AddProduct(textBoxName.Text, 
                                                textBoxPhoto.Text, 
                                                double.Parse(textBoxPrice.Text.Replace(',', '.'), CultureInfo.InvariantCulture));
            await RefreshView();
        }

        private async void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (SingleItemIsNotSelected())
                return;

            await _productHttpClient.UpdateProduct(Int32.Parse(dataGridViewProducts.SelectedRows[0].Cells[0].Value.ToString()), 
                                                       textBoxName.Text, 
                                                       textBoxPhoto.Text, 
                                                       double.Parse(textBoxPrice.Text.Replace(',', '.'), CultureInfo.InvariantCulture));
            await RefreshView();
        }

        private async void buttonDelete_Click(object sender, EventArgs e)
        {
            if (SingleItemIsNotSelected())
                return;

            await _productHttpClient.DeleteProduct(Int32.Parse(dataGridViewProducts.SelectedRows[0].Cells[0].Value.ToString()));
            await RefreshView();
        }

        private async void buttonSearch_Click(object sender, EventArgs e)
        {
            if (PriceRangeNotEntered())
            {
                MessageBox.Show("You must enter the price range", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            dataGridViewProducts.DataSource = await _productHttpClient.SearchProduct(textBoxSearchName.Text, 
                                                       double.Parse(textBoxPriceFrom.Text.Replace(',','.'), CultureInfo.InvariantCulture), 
                                                       double.Parse(textBoxPriceTo.Text.Replace(',', '.'), CultureInfo.InvariantCulture));
        }

        private async void buttonShowAll_Click(object sender, EventArgs e)
        {
            await RefreshView();
        }

        private async Task RefreshView()
        {
            dataGridViewProducts.DataSource = await _productHttpClient.GetAllProducts();
        }

        private void dataGridViewProducts_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (SingleItemIsNotSelected())
                return;

            textBoxName.Text = dataGridViewProducts.SelectedRows[0].Cells[1].Value.ToString();
            textBoxPhoto.Text = dataGridViewProducts.SelectedRows[0].Cells[2].Value.ToString();
            textBoxPrice.Text = dataGridViewProducts.SelectedRows[0].Cells[3].Value.ToString();
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialogPhoto.ShowDialog();

            if (result == DialogResult.OK)
                textBoxPhoto.Text = openFileDialogPhoto.FileName;
        }

        private bool SingleItemIsNotSelected()
        {
            return dataGridViewProducts.SelectedRows.Count != 1;
        }
        private bool PriceRangeNotEntered()
        {
            return textBoxPriceFrom.Text == string.Empty || textBoxPriceTo.Text == string.Empty;
        }
    }
}
