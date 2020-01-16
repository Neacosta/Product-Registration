using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Product_Registration
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Registration registration;
        private void Form1_Load(object sender, EventArgs e)
        {
 
            this.BindCustomerComboBox();
            cboCustomer.SelectedIndex = -1;
            this.BindProductComboBox();
            cboProduct.SelectedIndex = -1;
            txtRegDate.Text = DateTime.Today.ToShortDateString();
        }
        private void BindCustomerComboBox()
        {
            try
            {
                List<Customer> customers = CustomerDB.GetCustomers();
                cboCustomer.DataSource = customers;
                cboCustomer.DisplayMember = "Name";
                cboCustomer.ValueMember = "CustomerID";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }
        private void BindProductComboBox()
        {
            try
            {
                List<Product> products = ProductDB.GetProducts();
                cboProduct.DataSource = products;
                cboProduct.DisplayMember = "Name";
                cboProduct.ValueMember = "ProductCode";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

       
        private void btnRegProduct_Click(object sender, EventArgs e)
        {
            if (IsValidData())
            {
                registration = new Registration();
                this.PutRegistration();
                if (!RegistrationDB.AddRegistration(registration))
                {
                    MessageBox.Show("The registration was not added. " +
                        "Please try again.", "Database Error");
                }
                else
                {
                    MessageBox.Show("The product was registered successfully.",
                        "Registration Success");
                    this.ClearControls();
                    cboCustomer.Focus();
                }
            }
        }
        private bool IsValidData()
        {
            return
                IsPresent(cboCustomer, "Customer") &&
                IsPresent(cboProduct, "Product") &&
                IsPresent(txtRegDate, "Registration date") &&
                IsDateTime(txtRegDate, "Registration date");
        }

        private bool IsPresent(Control control, string name)
        {
            if (control.GetType().ToString() == "System.Windows.Forms.TextBox")
            {
                TextBox textBox = (TextBox)control;
                if (textBox.Text == "")
                {
                    MessageBox.Show(name + " is a required field.", "Entry Error");
                    textBox.Focus();
                    return false;
                }
            }
            else if (control.GetType().ToString() == "System.Windows.Forms.ComboBox")
            {
                ComboBox comboBox = (ComboBox)control;
                if (comboBox.SelectedIndex == -1)
                {
                    MessageBox.Show(name + " is a required field.", "Entry Error");
                    comboBox.Focus();
                    return false;
                }
            }
            return true;
        }

        private bool IsDateTime(TextBox textBox, string name)
        {
            try
            {
                Convert.ToDateTime(textBox.Text);
                return true;
            }
            catch (FormatException)
            {
                MessageBox.Show(name + " must be in a valid date format.",
                    "Entry Error");
                textBox.Focus();
                return false;
            }
        }


        private void PutRegistration()
        {
            registration.CustomerID = (int)cboCustomer.SelectedValue;
            registration.ProductCode = cboProduct.SelectedValue.ToString();
            registration.RegistrationDate = Convert.ToDateTime(txtRegDate.Text);
        }

        private void ClearControls()
        {
            cboCustomer.SelectedIndex = -1;
            cboProduct.SelectedIndex = -1;
            txtRegDate.Text = DateTime.Today.ToShortDateString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.ClearControls();
            cboCustomer.Focus();
          
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
