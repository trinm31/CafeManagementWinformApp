using Microsoft.SqlServer.Server;
using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class FoodDAO
    {
        private static FoodDAO instance;

        public static FoodDAO Instance 
        {   get
            {
                if(instance == null)
                {
                    instance = new FoodDAO();
                }
                return FoodDAO.instance;
            } 
            private set => FoodDAO.instance = value; 
        }
        private FoodDAO() { }
        public List<Food> GetFoodByCategoryID(int id)
        {
            List<Food> foods = new List<Food>();
            string query = "select * from Food where idCategory = " + id;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                foods.Add(food);
            }
            return foods;
        }
        public List<Food> GetListFood()
        {
            List<Food> foods = new List<Food>();
            string query = "select * from Food";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                foods.Add(food);
            }
            return foods;
        }
        public List<Food> SearchFoodByName(string name)
        {
            List<Food> foods = new List<Food>();
            string query = string.Format("select * from Food where name like N'%{0}%'", name );
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                foods.Add(food);
            }
            return foods;
        }
        public bool InsertFood(string name, int idcategory, float price)
        {
            string query = string.Format("INSERT dbo.Food ( name, idCategory, price ) VALUES ( N'{0}', {1}, {2})", name, idcategory,price);
            int result = DataProvider.Instance.ExecuteNoneQuery(query);
            return result > 0;
        }
        public bool UpdateFood(int id ,string name, int idcategory, float price)
        {
            string query = string.Format("update Food set name = N'{0}' ,  idCategory = {1} , price = {2} where id = {3}", name, idcategory, price , id);
            int result = DataProvider.Instance.ExecuteNoneQuery(query);
            return result > 0;
        }
        public bool DeleteFood(int id)
        {
            BillInfoDAO.Instance.DeleteFoodByFoodID(id);
            string query = string.Format("Delete Food where id = {0}", id);
            int result = DataProvider.Instance.ExecuteNoneQuery(query);
            return result > 0;
        }
        

    }
}
