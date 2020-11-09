using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class Food
    {
        private int id;
        private string name;
        private int categoryId;
        private float price;


        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int CategoryId { get => categoryId; set => categoryId = value; }
        public float Price { get => price; set => price = value; }
        public Food(int id, string name, int categoryID, float price)
        {
            this.Id = id;
            this.Name = name;
            this.CategoryId = categoryID;
            this.Price = price;
        }
        public Food(DataRow row)
        {
            this.Id = (int)row["id"];
            this.Name = row["name"].ToString();
            this.CategoryId = (int)row["idCategory"];
            this.Price = (float)Convert.ToDouble(row["price"].ToString());
        }
    }
}
