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
 * Author: Lindsay, Joel, Carol
 * Date:July, 2018 
 */

namespace TravEx_DBMA
{
    public partial class Form1 : Form
    {
        enum AccessMode
        {
            Read,
            Edit,
            Add
        }

        AccessMode accessMode = AccessMode.Read;

        public Form1()
        {
            InitializeComponent();
        }
//======= Start of Product Region ====================== Author: Carol He =========================================================================================
        #region PRODUCT_TAB

        public List<Product> products = ProductDB.GetProducts(); // get product List form Products table
        public List<Supplier> suppliers = SupplierDB.GetAllSuppliers(); // get supplier List from Suppliers table
        public List<ProductSupplier> products_suppliers = ProductSupplierDB.GetProductSuppliers();
        public Product product;

        //----Author: Carol He ----- Load ProductIdComboBox, ProductList and SupplierList when Product Tab is entered
        public void tabProducts_Enter(object sender, EventArgs e)
        {
            //productIdComboBox.SelectedIndex = 1;

            loadProductIdComboBox();

            LoadProductList();

            LoadSupplierList();

            productIdComboBox.SelectedIndex = 0;

        }

        public void loadProductIdComboBox()
        {
            products = ProductDB.GetProducts();
            foreach (var p in products)   //add all the ProductId values to ProductIdComboBox
            {
                productIdComboBox.Items.Add(p.ProductId);
            }
        }

        public void LoadProductList()
        {
            //get data from Products table and display in list view
            products = ProductDB.GetProducts();

            var product = from pro in products
                          select new
                          {
                              pro.ProductId,
                              pro.ProdName
                          };

            lvProProducts.Items.Clear();

            int j = 0;
            foreach (var p in products)
            {
                lvProProducts.Items.Add(p.ProductId.ToString());

                lvProProducts.Items[j].SubItems.Add(p.ProdName.ToString());

                j++;
            }
        }

        public void LoadSupplierList()
        {
            //get data from Suppliers table and display in list view
            var supplier = from sup in suppliers
                           select new
                           {
                               sup.SupplierId,
                               sup.SupName
                           };

            lvSuppliers.Items.Clear();

            int k = 0;
            foreach (var s in suppliers)
            {
                lvSuppliers.Items.Add(s.SupplierId.ToString());

                lvSuppliers.Items[k].SubItems.Add(s.SupName.ToString());

                k++;
            }
        }

        //----- Author: Carol He ------Display ProdName in txtProdName Textbox when user selected a ProductId from productIdComboBox -------------------------------------------------------------------------------
        //---get the values of product.ProductId and product.ProdName for the current Product instance
        private void productIdComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadProductSupplierList();

            //Display ProdName in txtProdName Textbox according to productId given in ProductIdComboBox
            int productId = Convert.ToInt32(productIdComboBox.SelectedItem);
            product = ProductDB.GetProduct(productId);
            txtProdName.Text = product.ProdName;

            //product = new Product();
            //product.ProductId = Convert.ToInt32(productIdComboBox.SelectedItem);
            //product.ProdName = txtProdName.Text;
            
        }

        public void LoadProductSupplierList()
        {
            suppliers = SupplierDB.GetAllSuppliers();
            products_suppliers = ProductSupplierDB.GetProductSuppliers();

            var sup = from prod_supp in products_suppliers
                           join supp in suppliers             
                           on prod_supp.SupplierId equals supp.SupplierId
                           where prod_supp.ProductId == (int)productIdComboBox.SelectedItem
                           select new
                           {
                               prod_supp.SupplierId,
                               supp.SupName,
                               prod_supp.ProductId,
                               prod_supp.ProductSupplierId
                           };
            lvProductSupplier.Items.Clear();
            int i = 0;
            foreach (var s in sup)
            {
                lvProductSupplier.Items.Add(s.SupplierId.ToString());

                lvProductSupplier.Items[i].SubItems.Add(s.SupName);
                lvProductSupplier.Items[i].SubItems.Add(s.ProductId.ToString());
                lvProductSupplier.Items[i].SubItems.Add(s.ProductSupplierId.ToString());

                i++;
            }
        }



 //------ Author: Carol He ----------------------------------------------------------------------------------------


        private void DisplayProduct()
        {
           
            txtProdName.Text = product.ProdName;
            //btnModify.Enabled = true;
            //btnDelete.Enabled = true;
        }
        private void GetProduct(int productID)
        {
            try
            {
                product = ProductDB.GetProduct(productID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void ClearControls()
        {

            txtProdName.Text = "";
            productIdComboBox.Text = "";
            //btnModify.Enabled = false;
            //btnDelete.Enabled = false;
            
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            frmAddUpdateProduct modifyProductForm = new frmAddUpdateProduct();
            modifyProductForm.addProduct = false;
            modifyProductForm.product = product;
            DialogResult result = modifyProductForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                product = modifyProductForm.product;
                this.DisplayProduct();
                //modifyProductForm.txtProductId = 

            }
            else if (result == DialogResult.Retry)
            {
                this.GetProduct(product.ProductId);
                if (product != null)
                    this.DisplayProduct();
                else
                    this.ClearControls();
            }

            LoadProductList();
            ClearControls();
        }

        //--------Author: Carol He ----------Add New Product---------------------------------------------------------------------------------------
        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddUpdateProduct addProductForm = new frmAddUpdateProduct();
            addProductForm.addProduct = true;
            //addProductForm.product = product;
            DialogResult result = addProductForm.ShowDialog();

            productIdComboBox.Items.Clear();
            loadProductIdComboBox();
            LoadProductList();
            ClearControls();


            //if (result == DialogResult.OK)
            //{
            //    product = addProductForm.product;
            //    txtCustomerID.Text = customer.CustomerID.ToString();
            //    this.DisplayCustomer();
            //}


            //frmAddModifyCustomer addCustomerForm = new frmAddModifyCustomer();
            //addCustomerForm.addCustomer = true;
            //DialogResult result = addCustomerForm.ShowDialog();
            //if (result == DialogResult.OK)
            //{
            //    customer = addCustomerForm.customer;
            //    txtCustomerID.Text = customer.CustomerID.ToString();
            //    this.DisplayCustomer();
            //}
        }

 //--------Author: Carol He ----------Delete Product---------------------------------------------------------------------------------------
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Delete " + product.ProdName + "?",
               "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    if (!ProductDB.DeleteProduct(product))
                    {
                        MessageBox.Show("Another user has updated or deleted " +
                            "that product.", "Database Error");
                        this.GetProduct(product.ProductId);
                        if (product != null)
                            this.DisplayProduct();
                        else
                            this.ClearControls();
                    }
                    else
                        this.ClearControls();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString());
                }
                MessageBox.Show("Product has been deleted successfully");

                ClearControls();
                LoadProductList();
                productIdComboBox.Items.Clear();
                loadProductIdComboBox();
            }
        }
        //--------Author: Carol He ----------Clear Button-------------------------------------------------------
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearControls();
        }
        //--------Author: Carol He ----------Exit Button------------------------------------------------

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //====== End of Product Region ======================================================================================
        #endregion

        #region SUPPLIER_TAB  
        //========== Author: Lindsay ================================================================================

        AccessMode tabSupplierAccessMode;

        Supplier sup, oldSup;

        //handles the Leave event of the tabSuppliers control
        private void tabSuppliersLeave(object sender, EventArgs e)
        {
            tabSupplierAccessMode = AccessMode.Read;
            txtSupplierId.Text = "";
            cmbSupId.Text = "";
            cmbSupId.Items.Clear();
            lvSuppliedProds.Items.Clear();
            lvUnsuppliedProducts.Items.Clear();
            lblSupMessage.Text = "";
        }

        // fills the suppliers information when enter the tabSuppliers
        private void tabSuppliersEnter(object sender, EventArgs e)
        {
            tabSuppliersDefaultStatus();
        }
        
        // the default setting of tha tabSuppliers control
        private void tabSuppliersDefaultStatus()
        {
            try
            {
                tabSupplierAccessMode = AccessMode.Edit;
                refreshCmbSupIdItems(); // get the latest suppliers list
                cmbSupId.SelectedIndex = 0;
                btnSaveSup.Enabled = false;
                lblSupMessage.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while loading supplier data: " + ex.Message,
                    ex.GetType().ToString());
            }
        }

        // get the lastest suppliers list
        private void refreshCmbSupIdItems()
        {
            List<Supplier> suppliers;
            cmbSupId.Items.Clear();
            suppliers = SupplierDB.GetAllSuppliers(); // get the latest suppliers' data
            //add items to the SupId combobox which the displaymember is SupName and valuemember is SupplierID
            cmbSupId.Items.AddRange(suppliers.ToArray());
            //initialize the selected index and the method of cmbSupId_SelectedValueChanged would excute
        }

        //when selecting the existed supplier name
        private void cmbSupId_SelectedValueChanged(object sender, EventArgs e)
        {
            tabSupplierAccessMode = AccessMode.Edit;
            btnSaveSup.Enabled = false;
            lblSupMessage.Text = "";
            btnDeleteSup.Enabled = true;// let the Delete button enabled 
            btnAddSuppliedProd.Enabled = true;// let the supplied products can be edited
            btnRemoveSuppliedProd.Enabled = true;// let the supplied products can be edited

            sup = cmbSupId.SelectedItem as Supplier; //get the selected supplier's data
            oldSup = sup.CopySupplier(); // make a  separate copy before update
            txtSupplierId.Text = sup.SupplierId.ToString(); //display the supplierid
            
            refreshTabSuppliersListViews();
        }

        //get the latest relative products' data and dispaly in the lvSuppliedProds and lvProducts
        private void refreshTabSuppliersListViews()
        {
            if (tabSupplierAccessMode == AccessMode.Add) //if the New button is clicked
            {
                int i = 0;
                lvSuppliedProds.Items.Clear();//clear the ListView for a new supplier

                //get all products available from the Products table
                List<Product> products;
                products = ProductDB.GetProducts();
                //display the data in the lvProducts
                lvUnsuppliedProducts.Items.Clear();
                foreach (var prod in products)
                {
                    lvUnsuppliedProducts.Items.Add(prod.ProductId.ToString());
                    lvUnsuppliedProducts.Items[i].SubItems.Add(prod.ProdName);
                    i++;
                }
            }
            else if (tabSupplierAccessMode == AccessMode.Edit)
            {
                List<Product> suppliedProds, unsuppliedProds;
                int i = 0, j = 0;
                //get all products supplied by the supplier
                suppliedProds = SupplierDB.GetProductsBySupplier(sup.SupplierId);
                lvSuppliedProds.Items.Clear();
                foreach (var supProd in suppliedProds)
                {
                    lvSuppliedProds.Items.Add(supProd.ProductId.ToString());
                    lvSuppliedProds.Items[i].SubItems.Add(supProd.ProdName);
                    i++;
                }

                //get all products not supplied by the supplier
                unsuppliedProds = SupplierDB.GetProdsUnsuppliedBySup(sup.SupplierId);
                lvUnsuppliedProducts.Items.Clear();
                foreach (var supProd in unsuppliedProds)
                {
                    lvUnsuppliedProducts.Items.Add(supProd.ProductId.ToString());
                    lvUnsuppliedProducts.Items[j].SubItems.Add(supProd.ProdName);
                    j++;
                }
            }
        }

        //modify the supplier name which can happend when the accessMode is either Add or Edit;
        private void cmbSupId_TextChanged(object sender, EventArgs e)
        {
            btnSaveSup.Enabled = true;
            lblSupMessage.Text = "";
        }

        //clicks the New button to let user add a new supplier
        private void btnNewSup_Click(object sender, EventArgs e)
        {            
            refreshCmbSupIdItems();
            cmbSupId.SelectedIndex = 0;
            txtSupplierId.Text = "";
            cmbSupId.Text = "";            
            lblSupMessage.Text = "";
            btnDeleteSup.Enabled = false;//Can not be used before a new supplier is created.
            btnAddSuppliedProd.Enabled = false; //Can not be used before a new supplier is created.
            btnRemoveSuppliedProd.Enabled = false; //Can not be used before a new supplier is created.
            tabSupplierAccessMode = AccessMode.Add;
            refreshTabSuppliersListViews();
        }

        //clicks the button to delete the supplier from the batabase
        private void btnDeleteSup_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                sup.SupplierId = Convert.ToInt32(txtSupplierId.Text);
                sup.SupName = cmbSupId.Text;
                try
                {
                    if (!SupplierDB.DeleteSupplier(sup))
                    {
                        MessageBox.Show("Another user has updated or deleted that supplier.", "Database Error");
                    }
                    else
                    {
                        tabSuppliersDefaultStatus();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString());
                }    
            }
                   
        }

        //click the Save button to save the new data or updated data
        private void btnSaveSup_Click(object sender, EventArgs e)
        {
            List<Supplier> suppliers;
            suppliers = SupplierDB.GetAllSuppliers(); // get the lastest suppliers' data
            Product removeSupProd = new Product();
            List<Product> newSuppliedProds = new List<Product>();
            ProductSupplier supProd = new ProductSupplier();
            //validate the cmbSupName
            if (cmbSupId.Text =="")
            {
                lblSupMessage.Text = "Note:Please enter a supplier name.";
            }
            else
            {
                if (tabSupplierAccessMode == AccessMode.Edit)
                {
                    sup.SupName = cmbSupId.Text;
                    sup.SupplierId = Convert.ToInt32(txtSupplierId.Text);
                    try
                    {
                        if (!SupplierDB.UpdateSupplier(oldSup, sup))
                        {
                            MessageBox.Show("Another user has updated or deleted that supplier.", "Database Error");
                        }
                        else
                        {
                            lblSupMessage.Text = "Note:The supplier updated.";
                            refreshCmbSupIdItems();
                            oldSup = sup.CopySupplier();
                            btnSaveSup.Enabled = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.GetType().ToString());
                    }
                }
                else if (tabSupplierAccessMode == AccessMode.Add)
                {
                    sup.SupplierId = SupplierDB.GetNewSupplierId();//create a new supplierId
                    sup.SupName = cmbSupId.Text; //get the entered name

                    try
                    {
                        SupplierDB.AddSupplier(sup);
                        // once a new supplier's data is inserted into the Suppliers table
                        txtSupplierId.Text = sup.SupplierId.ToString();//display the new supplierId                   
                        btnDeleteSup.Enabled = true;// let the Delete button enabled 
                        btnAddSuppliedProd.Enabled = true;// let the supplied products can be edited
                        btnRemoveSuppliedProd.Enabled = true;// let the supplied products can be edited
                        lblSupMessage.Text = "Note:The supplier added.";
                        refreshCmbSupIdItems();
                        btnSaveSup.Enabled = false;
                        tabSupplierAccessMode = AccessMode.Edit;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.GetType().ToString());
                    }
                }

            }
            
        }

        //clicks the button to add one supplied product into the database and refresh the ListViews
        private void btnAddSuppliedProd_Click(object sender, EventArgs e)
        {
            tabSupplierAccessMode = AccessMode.Edit;
            ProductSupplier newSupProd = new ProductSupplier();
            //get the selected Items
            ListView.SelectedListViewItemCollection selectedProds = lvUnsuppliedProducts.SelectedItems;

            if (lvUnsuppliedProducts.SelectedItems.Count > 0)//if there is selected item
            {
                //add the data of each item into the database
                foreach (ListViewItem item in selectedProds)
                {
                    newSupProd.ProductId = Convert.ToInt32(item.SubItems[0].Text);
                    newSupProd.SupplierId = Convert.ToInt32(txtSupplierId.Text);
                    // add the data to the Products_Suppliers table and get the ProductsSupplierId
                    try
                    {
                        newSupProd.ProductSupplierId = ProductSupplierDB.AddSupProd(newSupProd);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.GetType().ToString());
                    }
                }
                refreshTabSuppliersListViews(); //refresh the ListViews
                
            }
            else
            {
                lblSupMessage.Text = "Note:Please select a product";
            }
        }

        //clicks the button to remove one product from the suppliedProd listView
        private void btnRemoveSuppliedProd_Click(object sender, EventArgs e)
        {
            tabSupplierAccessMode = AccessMode.Edit;
            ProductSupplier removeSupProd = new ProductSupplier();
            //get the selected Items
            ListView.SelectedListViewItemCollection selectedProds = lvSuppliedProds.SelectedItems;
       
            if (lvSuppliedProds.SelectedItems.Count > 0)//if there is selected item
            {
                //remove each product from the database
                foreach (ListViewItem item in selectedProds)
                {
                    removeSupProd.ProductId = Convert.ToInt32(item.SubItems[0].Text);
                    removeSupProd.SupplierId = Convert.ToInt32(txtSupplierId.Text);
                    // delete the data from the Products_Suppliers table
                    try
                    {
                        if (!(ProductSupplierDB.DeleteSupProd(removeSupProd)))
                        {
                            MessageBox.Show("Another user has updated or deleted that product.", "Database Error");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.GetType().ToString());
                    }                  
                }
                refreshTabSuppliersListViews(); //refresh the ListViews
            }
            else
            {
                lblSupMessage.Text = "Note:Please select a product.";
            }
        }
     
        //========== Author: Lindsay ================================================================================
 

        #endregion

        #region PACKAGE_TAB

        /// <summary>
        /// The selected package on the Package tab
        /// </summary>
        TravelPackage selectedPackage;

        /// <summary>
        /// Fills the package ComboBox.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void FillPackageComboBox(object sender, EventArgs e)
        {
            //Fills the selection box with package items
            cmbPackageID.Items.Clear();
            cmbPackageID.Items.AddRange(PackageDB.GetTravelPackages().ToArray());
            cmbPackageID.SelectedIndex = 0;
            accessMode = AccessMode.Read;
        }

        /// <summary>
        /// Handles the Leave event of the tabPackages control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void tabPackages_Leave(object sender, EventArgs e)
        {
            selectedPackage = null;
            accessMode = AccessMode.Read;
            lblPkgStatus.Text = "";
        }

        /// <summary>
        /// Handles the SelectedValueChanged event of the cmbPackageID control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmbPackageID_SelectedValueChanged(object sender, EventArgs e)
        {
            if ((sender as ComboBox).SelectedItem == null)
            {
                selectedPackage = null;
                return;
            }

            selectedPackage = cmbPackageID.SelectedItem as TravelPackage;
            FillPackageControls(selectedPackage);
            FillPackageProductList(selectedPackage);
            accessMode = AccessMode.Read;
            btnPkgSave.Enabled = false;
            lblPkgStatus.Text = "Package loaded";
        }
        
        /// <summary>
        /// Fills the package product list box with named Product_Supplier objects.
        /// </summary>
        /// <param name="selectedPackage">The selected package.</param>
        /// <exception cref="NotImplementedException"></exception>
        private void FillPackageProductList(TravelPackage selectedPackage)
        {
            lstPkgProductSuppliers.Items.Clear();
            foreach (NamedPackageProductSupplier prodSup in selectedPackage.ProductsAndSuppliers)
            {
                ListViewItem item = new ListViewItem(prodSup.ProductName);
                item.SubItems.Add(prodSup.SupplierName);
                lstPkgProductSuppliers.Items.Add(item);
            }
        }

        /// <summary>
        /// Fills the package controls.
        /// </summary>
        /// <param name="package">The package.</param>
        private void FillPackageControls(TravelPackage package)
        {
            txtPkgDesc.Text = package.Description;
            datPkgStart.Value = package.StartDate;
            datPkgEnd.Value = package.EndDate;
            txtPkgBasePrice.Text = package.BasePrice.ToString();
            txtPkgCommission.Text = package.Commission.ToString();
        }

        //Put the form into edit mode when necessary
        private void OnPackageDataModified(object sender, EventArgs e)
        {
            //Skip this if in add mode, or already in edit mode 
            if (accessMode == AccessMode.Add)
                return;

            accessMode = AccessMode.Edit;
            btnPkgSave.Enabled = true;
            lblPkgStatus.Text = "Modifying package";
        }

        /// <summary>
        /// Handles the Click event of the btnPkgSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnPkgSave_Click(object sender, EventArgs e)
        {
            if (accessMode == AccessMode.Read)
            {
                MessageBox.Show("Cannot save changes in edit mode");
                return;
            }

            if (!ValidatePkgTab())
                return;

            if (accessMode == AccessMode.Add)
            {
                TravelPackage newPackage = new TravelPackage()
                {
                    Name = cmbPackageID.Text,
                    Description = txtPkgDesc.Text,
                    StartDate = datPkgStart.Value,
                    EndDate = datPkgEnd.Value,
                    BasePrice = double.Parse(txtPkgBasePrice.Text),
                    Commission = null
                };
                if (double.TryParse(txtPkgCommission.Text, out double c))
                    newPackage.Commission = c;
                //Add package
                newPackage = PackageDB.Insert(newPackage);

                //Refresh
                FillPackageComboBox(sender, e);
                // Maybe loop through packages to find the new ID?
                cmbPackageID.SelectedIndex = cmbPackageID.Items.Count - 1;
                lblPkgStatus.Text = "Package created";
            }
            else if (accessMode == AccessMode.Edit)
            {
                TravelPackage newPackage = new TravelPackage(selectedPackage.ID)
                {
                    Name = cmbPackageID.Text,
                    Description = txtPkgDesc.Text,
                    StartDate = datPkgStart.Value,
                    EndDate = datPkgEnd.Value,
                    BasePrice = double.Parse(txtPkgBasePrice.Text),
                    Commission = null
                };
            if (double.TryParse(txtPkgCommission.Text, out double c))
                newPackage.Commission = c;

            //Update Package
            PackageDB.Update(selectedPackage, newPackage);

                //Refresh
                int selectedIndex = cmbPackageID.SelectedIndex;
                FillPackageComboBox(sender, e);
                cmbPackageID.SelectedIndex = selectedIndex;
                lblPkgStatus.Text = "Package updated";
            }
        }

        private bool ValidatePkgTab()
        {
            string message = string.Empty;
            //Check that Name is not empty
            if (cmbPackageID.Text.Length < 0)
                message = "Name cannot be left empty";
            //Check that start date is before end date
            else if (datPkgStart.Value != null && datPkgEnd.Value != null && datPkgStart.Value > datPkgEnd.Value)
                message = "End date cannot be before start date";
            //Check that Price is not empty, not negative
            else if (string.IsNullOrWhiteSpace(txtPkgBasePrice.Text)
                    || !double.TryParse(txtPkgBasePrice.Text, out double price)
                    || price < 0)
                message = "Base Price must have a non-negative numeric value";
            // Check that Commission is not negative
            else if (double.TryParse(txtPkgCommission.Text, out double commission)
                        && (commission < 0 || commission > price))
            {
                message = "Commission must be greater than 0 and less than base price";
            }

            //IF this is reached with no message, return true
            if (message == string.Empty)
                return true;

            //If an error was found, show message and return false
            MessageBox.Show(message, "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return false;
        }

        /// <summary>
        /// Handles the Click event of the btnAddPackage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnAddPackage_Click(object sender, EventArgs e)
        {
            //Clear form
            ResetPkgTab();

            //Set status to adding
            accessMode = AccessMode.Add;
            lblPkgStatus.Text = "Adding new package";
        }

        /// <summary>
        /// Resets the PKG tab.
        /// </summary>
        private void ResetPkgTab()
        {
            //Clear form
            cmbPackageID.SelectedItem = null;
            cmbPackageID.Select();
            txtPkgDesc.ResetText();
            datPkgStart.Value = DateTime.Today;
            datPkgEnd.Value = DateTime.Today + TimeSpan.FromDays(1); //Tomorrow
            txtPkgBasePrice.ResetText();
            txtPkgCommission.ResetText();
        }

        // Remove a product from the current package
        private void btnDeleteProd_Supplier_Click(object sender, EventArgs e)
        {
            //Confirm
            DialogResult confirmation = DialogResult.No;
            confirmation = MessageBox.Show("Are you sure you want to remove the selected product(s) from '" + 
                                            selectedPackage.Name + "'?", "Confirm Delete", MessageBoxButtons.YesNo, 
                                            MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (confirmation != DialogResult.Yes)
                return; //Escape if user does not confirm
            int rowCount = lstPkgProductSuppliers.SelectedIndices.Count;
            int rowsDeleted = 0;
            NamedPackageProductSupplier[] toDelete = new NamedPackageProductSupplier[rowCount];
            for (int i = 0; i < rowCount; i++)
            {
                //Set each selected product to be deleted
                int selectedIndex = lstPkgProductSuppliers.SelectedIndices[i];
                toDelete[i] = selectedPackage.ProductsAndSuppliers[selectedIndex];
            }
            for (int i = 0; i < rowCount;i++)
            { 
                //Delete each row selected above
                rowsDeleted += PackageProdSuppDB.Delete(toDelete[i]);
            }

            //Display result
            FillPackageProductList(selectedPackage);
            lblPkgStatus.Text = "Product(s) removed";
        }

        // Deletes the currently selected package
        private void btnPkgDelete_Click(object sender, EventArgs e)
        {
            //Delete Package
            DialogResult confirmation = DialogResult.No;
            confirmation = MessageBox.Show("Are you sure you want to delete the package '" +
                                            selectedPackage.Name + "'?", "Confirm Delete", MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (confirmation != DialogResult.Yes)
                return;

            int rowsDeleted = 0;
            try
            {
                rowsDeleted = PackageDB.Delete(selectedPackage);
                if (rowsDeleted > 0)
                {
                    //package deleted. Refill combo box and display result
                    FillPackageComboBox(btnPkgDelete, EventArgs.Empty);
                    lblPkgStatus.Text = "Package deleted";
                    MessageBox.Show("Package deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Deleting Package");
            }
        }

        private void tabProducts_Click(object sender, EventArgs e)
        {

        }

        // Show "Add Product to Package" dialog
        private void btnAddProduct_Supplier_Click(object sender, EventArgs e)
        {
            frmNewPackageProductSupplier addDialog = new frmNewPackageProductSupplier
            {
                Package = selectedPackage
            };
            DialogResult result = addDialog.ShowDialog();
            if (result != DialogResult.OK) return; //Escape if OK is not returned

            FillPackageProductList(selectedPackage);
            lblPkgStatus.Text = "Product added to package";
        }



        #endregion

    }
}



