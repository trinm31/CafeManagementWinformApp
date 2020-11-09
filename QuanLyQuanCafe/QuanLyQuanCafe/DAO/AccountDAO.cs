using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instance
        {
            get { if (instance == null) instance = new AccountDAO(); return AccountDAO.instance; }
            private set { AccountDAO.instance = value; }
        }
        private AccountDAO() { }
        public bool Login(string username, string password)
        {
            byte[] temp = ASCIIEncoding.ASCII.GetBytes(password);
            byte[] hasData = new MD5CryptoServiceProvider().ComputeHash(temp);
            string pass = "";
            foreach (byte item in hasData)
            {
                pass += item;
            }
            string query = "EXEC USP_Login @UserName , @PassWord";
            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { username, pass });
            return result.Rows.Count > 0;
        }
        public Account GetAccountByUserName(string userName)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("select * from account where username = '" + userName + "'");
            foreach (DataRow item in data.Rows)
            {
                return new Account(item);
            }
            return null;
        }
        public bool UpdateAccount(string username, string displayname, string password, string newpass)
        {
            byte[] temp = ASCIIEncoding.ASCII.GetBytes(newpass);
            byte[] hasData = new MD5CryptoServiceProvider().ComputeHash(temp);
            string newpassHash = "";
            foreach (byte item in hasData)
            {
                newpassHash += item;
            }
            byte[] temp1 = ASCIIEncoding.ASCII.GetBytes(password);
            byte[] hasData1 = new MD5CryptoServiceProvider().ComputeHash(temp1);
            string passHash = "";
            foreach (byte item in hasData1)
            {
                passHash += item;
            }
            int result = DataProvider.Instance.ExecuteNoneQuery("USP_UpdateAccount @Username , @DisplayName , @Password , @NewPassword ", new object[] { username, displayname, passHash, newpassHash });
            return result > 0;
        }
        public DataTable GetListAccount()
        {
            return DataProvider.Instance.ExecuteQuery("select UserName,DisplayName, Type from Account");
        }
        public bool InsertAccount(string name, string displayName, int type)
        {
            string query = string.Format("INSERT dbo.Account ( UserName, PassWord, DisplayName, Type ) VALUES ( N'{0}', N'20720532132149213101239102231223249249135100218' , N'{1}', {2})", name, displayName, type);
            int result = DataProvider.Instance.ExecuteNoneQuery(query);
            return result > 0;
        }
        public bool UpdateAccount(string name, string displayName, int type)
        {
            string query = string.Format("update Account set DisplayName = N'{0}' , Type = {1} where UserName = N'{2}'", displayName, type, name);
            int result = DataProvider.Instance.ExecuteNoneQuery(query);
            return result > 0;
        }
        public bool DeleteAccount(string name)
        {
            string query = string.Format("Delete Account where UserName = N'{0}'", name);
            int result = DataProvider.Instance.ExecuteNoneQuery(query);
            return result > 0;
        }
        public bool ResetPassword(string name)
        {
            string query = string.Format("Update Account set PassWord = N'20720532132149213101239102231223249249135100218' where UserName = N'{0}'", name);
            int result = DataProvider.Instance.ExecuteNoneQuery(query);
            return result > 0;
        }
    }
}


