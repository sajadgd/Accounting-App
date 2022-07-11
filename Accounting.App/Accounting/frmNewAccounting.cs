using Accounting.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ValidationComponents;

namespace Accounting.App
{
    public partial class frmNewAccounting : Form
    {
        private UnitOfWork db;
        public int AccountId = 0;
        public frmNewAccounting()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmNewAccounting_Load(object sender, EventArgs e)
        {
            db = new UnitOfWork();
            dgvCustomers.AutoGenerateColumns = false;
            dgvCustomers.DataSource = db.CustomerRepository.GetCustomerName();
            if (AccountId != 0)
            {
                var accounting = db.AccountingRepository.GetById(AccountId);
                this.Text = "ویرایش";
                btnSave.Text = "ویرایش";
                txtName.Text = db.CustomerRepository.GetCustomerNameById(accounting.CustomerId);
                txtAmount.Value = Convert.ToInt32(accounting.Amount.ToString());
                txtDescription.Text = accounting.Description;
                if (accounting.TypeId == 1)
                {
                    rbReceive.Checked = true;
                }
                else
                {
                    rbPay.Checked = true;
                }
            }
            db.Dispose();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            dgvCustomers.AutoGenerateColumns = false;
            dgvCustomers.DataSource = db.CustomerRepository.GetCustomerName(txtFilter.Text);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtName.Text = dgvCustomers.CurrentRow.Cells[0].Value.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            db = new UnitOfWork();
            if (BaseValidator.IsFormValid(this.components))
            {
                if (rbPay.Checked || rbReceive.Checked)
                {
                    DataLayer.Accounting accounting = new DataLayer.Accounting()
                    {
                        Amount = Convert.ToInt32(txtAmount.Value.ToString()),
                        CustomerId = db.CustomerRepository.GetCustomerIdByName(txtName.Text),
                        TypeId = (rbReceive.Checked) ? 1 : 2,
                        DateTitle = DateTime.Now,
                        Description = txtDescription.Text
                    };
                    if (AccountId == 0)
                    {
                        db.AccountingRepository.Insert(accounting);
                    }
                    else
                    {
                        accounting.ID = AccountId;
                        db.AccountingRepository.Update(accounting);
                    }
                    db.Save();
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    RtlMessageBox.Show("لطفا نوع تراکنش را انتخاب کنید !");
                }
            }
            db.Dispose();
        }
    }
}
