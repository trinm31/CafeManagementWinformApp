using QuanLyQuanCafe.DAO;
using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanCafe
{
    public partial class fAccountProfile : Form
    {
        private Account loginAccount;
        public Account LoginAccount
        {
            get => loginAccount;
            set
            {
                loginAccount = value;
                ChangeAccount(loginAccount);
            }

        }
        public fAccountProfile(Account acc)
        {
            InitializeComponent();
            this.LoginAccount = acc;
        }
        void ChangeAccount(Account acc)
        {
            txbDisplayName.Text = LoginAccount.DisplayName;
            txbUserName.Text = LoginAccount.UserName;
        }
        void updateAccountInfor()
        {
            string DisplayName = txbDisplayName.Text;
            string UserName = txbUserName.Text;
            string Password = txbPassWord.Text;
            string NewPassWord = txbNewPass.Text;
            string ReEnterPassWord = txbReEnterPass.Text;

            if (!ReEnterPassWord.Equals(NewPassWord))
            {
                MessageBox.Show("Your new pass word not match", "Warmming", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (AccountDAO.Instance.UpdateAccount(UserName, DisplayName, Password, NewPassWord))
                {
                    MessageBox.Show("Update successfull");
                    if(updateAccount != null)
                    {
                        updateAccount(this,new AccountEvent(AccountDAO.Instance.GetAccountByUserName(UserName)));
                    }
                }
                else
                {
                    MessageBox.Show("Your new pass word not match", "Warmming", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private event EventHandler<AccountEvent> updateAccount;
        public event EventHandler<AccountEvent> UpdateAccount
        {
            add { updateAccount += value; }
            remove { updateAccount -= value; }
        }

        private void btnExti_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            updateAccountInfor();
        }
    }
    public class AccountEvent : EventArgs
    {
        private Account acc;

        public Account Acc { get => acc; set => acc = value; }
        public AccountEvent(Account acc)
        {
            this.Acc = acc;
        }
    }
}
