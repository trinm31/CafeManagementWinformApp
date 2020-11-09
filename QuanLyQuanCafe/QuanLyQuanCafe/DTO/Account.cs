using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class Account
    {
        private string userName;
        private string displayName;
        private int type;
        private string password;

        public string UserName { get => userName; set => userName = value; }
        public string DisplayName { get => displayName; set => displayName = value; }
        public int Type { get => type; set => type = value; }
        public string Password { get => password; set => password = value; }
        public Account(string username, string displayname, int type, string passWord = null)
        {
            this.UserName = username;
            this.DisplayName = displayname;
            this.Type = type;
            this.Password = passWord;
        }
        public Account(DataRow row)
        {
            this.UserName = row["UserName"].ToString();
            this.DisplayName = row["DisplayName"].ToString();
            this.Type = (int)row["Type"];
            this.Password = row["PassWord"].ToString();
        }
    }
}
