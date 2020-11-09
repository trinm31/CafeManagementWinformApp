using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class DataProvider
    {
        private static DataProvider instance;
        public static DataProvider Instance 
        {
            get { if (instance == null) instance = new DataProvider(); return DataProvider.instance; }
            private set { DataProvider.instance = value; } 
        }
        private DataProvider() { }
        private string ConnectionStr = @"Data Source=MINHTRI0370;Initial Catalog=QuanLyQuanCafes;Integrated Security=True";

        

        public DataTable ExecuteQuery(string query, object[] paramenter = null)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(ConnectionStr))
            {
                connection.Open();

                SqlCommand sqlCommand = new SqlCommand(query, connection);


                if (paramenter != null)
                {
                    string[] para = query.Split(' ');
                    int i = 0;
                    
                    foreach (var item in para)
                    {
                        if (item.Contains('@'))
                        {
                            sqlCommand.Parameters.AddWithValue(item, paramenter[i]);
                            i++;
                        }
                    }
                    
                   
                }
              
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(dataTable);

                connection.Close();
            }
            return dataTable;
        }
        public int ExecuteNoneQuery(string query, object[] paramenter = null)
        {
            int data = 0;
            using (SqlConnection connection = new SqlConnection(ConnectionStr))
            {
                connection.Open();

                SqlCommand sqlCommand = new SqlCommand(query, connection);


                if (paramenter != null)
                {
                    string[] para = query.Split(' ');
                    int i = 0;

                    foreach (var item in para)
                    {
                        if (item.Contains('@'))
                        {
                            sqlCommand.Parameters.AddWithValue(item, paramenter[i]);
                            i++;
                        }
                    }


                }

                data = sqlCommand.ExecuteNonQuery();

                connection.Close();
            }
            return data;
        }
        public object ExecuteScalar(string query, object[] paramenter = null)
        {
            object data = 0;
            using (SqlConnection connection = new SqlConnection(ConnectionStr))
            {
                connection.Open();

                SqlCommand sqlCommand = new SqlCommand(query, connection);


                if (paramenter != null)
                {
                    string[] para = query.Split(' ');
                    int i = 0;

                    foreach (var item in para)
                    {
                        if (item.Contains('@'))
                        {
                            sqlCommand.Parameters.AddWithValue(item, paramenter[i]);
                            i++;
                        }
                    }


                }

                data = sqlCommand.ExecuteScalar();

                connection.Close();
            }
            return data;
        }
    }
}
