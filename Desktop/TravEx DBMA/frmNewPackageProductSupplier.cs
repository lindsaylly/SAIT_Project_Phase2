using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TravelExpertsPackages;
/*
 * Purpose: ASP.NET Workshop 5
 * Author:  Joel Barr
 * Date:    July, 2018 
 */
namespace TravEx_DBMA
{
    public partial class frmNewPackageProductSupplier : Form
    {
        Product[] products;
        List<Supplier> suppliers;

        public TravelPackage Package { get; set; }
        public frmNewPackageProductSupplier()
        {
            InitializeComponent();
        }

        private void frmNewPackageProductSupplier_Load(object sender, EventArgs e)
        {
            //Populate product list
            products = ProductDB.GetProducts().ToArray();
            cboProduct.DataSource = products;
            cboProduct.SelectedIndex = 0;
        }

        private void cboProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            suppliers = new List<Supplier>();

            int productID = (int)cboProduct.SelectedValue;
            //Populate supplier list for all suppliers offering the selected product
            //Get supplierIDs
            var possibleSuppliers = ProductSupplierDB.GetProductSuppliers();
            var availableSupplierIDs = from supplier in possibleSuppliers
                                     where supplier.ProductId == productID
                                     select supplier.SupplierId;

            //Add suppliers to our list
            foreach (int id in availableSupplierIDs)
            {
                suppliers.Add(SupplierDB.GetSupplier(id));
            }

            //Add list to the dropdown
            cboSuppliers.DataSource = suppliers;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (Package == null) throw new MissingMemberException("Package is not assigned");

            ProductSupplier prodSup = GetProductSupplier();
            if (prodSup == null)
            {
                MessageBox.Show("Product/Supplier not found in database", "ERROR");
                return;
            }

            // Add ProductPackageSupplier to database
            PackageProdSupplier pkgProdSup = new PackageProdSupplier
            {
                PackageID = Package.ID,
                ProdSuppID = prodSup.ProductSupplierId
            };

            PackageProdSuppDB.Insert(pkgProdSup);

            //Return
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        ProductSupplier GetProductSupplier()
        {
            int productID = 0;
            int supplierID = 0;

            productID = Convert.ToInt32(cboProduct.SelectedValue);
            supplierID = Convert.ToInt32(cboSuppliers.SelectedValue);

            return ProductSupplierDB.GetProductSupplier(productID, supplierID);
        }
    }
}
