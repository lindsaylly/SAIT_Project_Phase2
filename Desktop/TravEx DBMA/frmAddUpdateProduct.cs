using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TravelExpertsPackages;
/*
 * Purpose: ASP.NET Workshop 4
 * Author: Lindsay
 * Date:July, 2018 
 */
namespace TravEx_DBMA
{
    public partial class frmAddUpdateProduct : Form
    {
        public frmAddUpdateProduct()
        {
            InitializeComponent();
        }

        public bool addProduct;
        public Product product;

        public List<Product> products = ProductDB.GetProducts();

        private void frmAddUpdateProduct_Load(object sender, EventArgs e)
        {
            if (addProduct)
            {
                this.Text = "Add Product";
                txtProductId.SelectedIndex = -1;
                txtProductId.Enabled = false;
            }
            else
            {
                this.Text = "Modify Product";
                txtProductId.Text = product.ProductId.ToString();
                txtProdName.Text = product.ProdName;
            }
        }

    
        //private void cmbProductId_SelectedIndexChanged_1(object sender, EventArgs e)
        //{
        //    var pro = from prod in products
        //                  where prod.ProductId == (int)txtProductId.SelectedItem
        //                  select new
        //                  {
        //                      prod.ProductId,
        //                      prod.ProdName
        //                  };

        //    int i = 0;
        //    foreach (var ps in pro)
        //    {

        //        txtProdName.Text = ps.ProdName.ToString();

        //        i++;
        //    }
        //    product = new Product();
        //    product.ProductId = Convert.ToInt32(txtProductId.SelectedItem);
        //    product.ProdName = txtProdName.Text;

        //}

            private void btnAccept_Click(object sender, EventArgs e)
        {
            if (IsValidData())
            {
                if (addProduct)
                {
                    product = new Product();
                    this.PutProductData(product);
                    try
                    {
                        product.ProductId = ProductDB.AddProduct(product);
                        this.DialogResult = DialogResult.OK;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.GetType().ToString());
                    }

                    MessageBox.Show("Product Added Successfully!");

                    //Form1.LoadProductList();
                }
                else
                {
                    Product newProduct = new Product();
                    newProduct.ProductId = product.ProductId;
                    this.PutProductData(newProduct);
                    try
                    {
                        if (!ProductDB.UpdateProduct(product, newProduct))
                        {
                            MessageBox.Show("Another user has updated or " +
                               "deleted that product.", "Database Error");
                            this.DialogResult = DialogResult.Retry;
                        }
                        else
                        {
                            product = newProduct;
                            this.DialogResult = DialogResult.OK;
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.GetType().ToString());
                    }
                }
            }
        }

        private bool IsValidData()
        {
            return
                Validator.IsPresent(txtProdName);
        }

        private void PutProductData(Product product)
        {
            product.ProdName = txtProdName.Text;
        }

    }
}

