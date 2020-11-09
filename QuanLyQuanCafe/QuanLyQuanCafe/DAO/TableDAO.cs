using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class TableDAO
    {
        private static TableDAO instance;
        public static int TableWidth = 90;
        public static int TableHeight = 90;

        public static TableDAO Instance 
        { 
            get
            {
                if (instance == null) instance = new TableDAO();
                return TableDAO.instance;
            } 
            private set => TableDAO.instance = value; 
        }
        private TableDAO()
        {

        }
        public void SwitchTable(int id1, int id2)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("USP_SwitchTable @idtable1 , @idtable2", new object[] { id1, id2 });
        }
        public List<Table> LoadTableList()
        {
            List<Table> tableList = new List<Table>();
            DataTable data = DataProvider.Instance.ExecuteQuery("USP_GetTableList");
            foreach (DataRow item in data.Rows)
            {
                Table table = new Table(item);
                tableList.Add(table);
            }
            return tableList;
        }
        public bool InsertTable(string name)
        {
            string query = string.Format("INSERT dbo.TableFood (name) VALUES ( N'{0}')", name);
            int result = DataProvider.Instance.ExecuteNoneQuery(query);
            return result > 0;
        }
        public bool UpdateTable(int id, string name)
        {
            string query = string.Format("update TableFood set name = N'{0}' where id = {1}", name, id);
            int result = DataProvider.Instance.ExecuteNoneQuery(query);
            return result > 0;
        }
        public bool DeleteTable(int id)
        {
            BillInfoDAO.Instance.DeleteFoodByFoodID(id);
            string query = string.Format("Delete TableFood where id = {0}", id);
            int result = DataProvider.Instance.ExecuteNoneQuery(query);
            return result > 0;
        }
       
       
    }
}
