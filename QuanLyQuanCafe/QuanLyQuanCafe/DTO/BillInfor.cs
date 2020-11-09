using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class BillInfor
    {
        public BillInfor(int id, int billid, int foodid, int count)
        {
            this.Id = id;
            this.BillId = billid;
            this.FoodId = foodid;
            this.Count = count;
        }
        public BillInfor(DataRow row)
        {
            this.Id = (int)row["id"];
            this.BillId = (int)row["idBill"];
            this.FoodId = (int)row["idFood"];
            this.Count = (int)row["count"];
        }

        private int id;
        private int billId;
        private int foodId;
        private int count;

        public int Id { get => id; set => id = value; }
        public int BillId { get => billId; set => billId = value; }
        public int FoodId { get => foodId; set => foodId = value; }
        public int Count { get => count; set => count = value; }
    }
}
