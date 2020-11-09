using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class Bill
    {
        public Bill(int id, DateTime? dateCheckIn, DateTime? dateCheckOut, int status, int discount)
        {
            this.Id = id;
            this.DateCheckIn = dateCheckIn;
            this.DateCheckOut = dateCheckOut;
            this.Status = status;
            this.Discount = discount;
        }
        public Bill(DataRow row)
        {
            this.Id = (int)row["id"];
            this.DateCheckIn = (DateTime?)row["DateCheckIn"];
            var DateCheckOutNotNull = row["DateCheckOut"];
            if (DateCheckOutNotNull.ToString() != "")
            {
                this.DateCheckOut = (DateTime?)row["DateCheckOut"];
            }
            
            this.Status = (int)row["status"];
            var DiscountnotNull = row["Discount"];
            if (DiscountnotNull.ToString() != "")
            {
                this.Discount = (int)row["Discount"];
            }
        }

        private int id;
        private DateTime? dateCheckIn;
        private DateTime? dateCheckOut;
        private int status;
        private int discount;

        public int Id { get => id; set => id = value; }
        
        public int Status { get => status; set => status = value; }
        public DateTime? DateCheckIn { get => dateCheckIn; set => dateCheckIn = value; }
        public DateTime? DateCheckOut { get => dateCheckOut; set => dateCheckOut = value; }
        public int Discount { get => discount; set => discount = value; }
    }
}
