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
    public partial class frmLogin : Form
    {
        public int loginId = 0;
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            using(UnitOfWork db = new UnitOfWork())
            {
                if (BaseValidator.IsFormValid(this.components))
                {
                    if (loginId == 0)
                    {
                        if (db.LoginRepository.Get(l => l.Username == txtUser.Text && l.Password == txtPassword.Text).Any())
                        {
                            DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            RtlMessageBox.Show("کاربری یافت نشد !");
                        }
                    }
                    else
                    {
                        var login = db.LoginRepository.Get().FirstOrDefault();
                        login.Username = txtUser.Text;
                        login.Password = txtPassword.Text;
                        db.LoginRepository.Update(login);
                        db.Save();
                        Application.Restart();
                    }
                }

            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            if (loginId != 0)
            {
                this.Text = "تنظیمات ورود به برنامه";
                btnLogin.Text = "ذخیره تنظیمات";
                using(UnitOfWork db = new UnitOfWork())
                {
                    var login = db.LoginRepository.Get().FirstOrDefault();
                    txtUser.Text = login.Username;
                    txtPassword.Text = login.Password;
                }
                
            }
        }
    }
}
