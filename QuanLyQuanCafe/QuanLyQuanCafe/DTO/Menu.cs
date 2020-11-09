﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class Menu
    {
        private float price;
        private float totalPrice;
        private int count;
        private string foodName;
        public float Price { get => price; set => price = value; }
        public float TotalPrice { get => totalPrice; set => totalPrice = value; }
        public int Count { get => count; set => count = value; }
        public string FoodName { get => foodName; set => foodName = value; }
        public Menu(string name, int count, float price, float totalPrice )
        {
            this.FoodName = name;
            this.Count = count;
            this.Price = price;
            this.TotalPrice = totalPrice;
        }
        public Menu(DataRow row)
        {
            this.FoodName = row["name"].ToString();
            this.Count = (int)row["count"];
            this.Price = (float)Convert.ToDouble((row["price"].ToString()));
            this.TotalPrice = (float)Convert.ToDouble((row["totalPrice"].ToString()));
        }
    }
}
