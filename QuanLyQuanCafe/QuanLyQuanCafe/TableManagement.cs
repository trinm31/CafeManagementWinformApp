using QuanLyQuanCafe.DAO;
using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Menu = QuanLyQuanCafe.DTO.Menu;

namespace QuanLyQuanCafe
{
    [System.Runtime.InteropServices.Guid("72E0D48B-8BF7-4C59-9F8D-BE78AA4FCBEA")]
    public partial class fTableManagement : Form
    {
        private Account loginAccount;
        public Account LoginAccount 
        { 
            get => loginAccount;
            set
            {
                loginAccount = value;
                ChangeAccount(loginAccount.Type);
            }

        }
        public fTableManagement(Account acc)
        {
            InitializeComponent();
            this.LoginAccount = acc;
            LoadTable();
            LoadCategories();
            LoadComboBoxTable(cbSwitchTable);
            
        }
        #region Method
        void ChangeAccount(int type)
        {
            adminToolStripMenuItem.Enabled = type == 1;
            thôngTinTàiKhoảnToolStripMenuItem.Text += " (" + LoginAccount.DisplayName + ")";
        }
        void LoadCategories()
        {
            List<Category> categories = CategoryDAO.Instance.GetListCategories();
            cbCategory.DataSource = categories;
            cbCategory.DisplayMember = "name";
        }
        void LoadFoodByCatagoriID(int id)
        {
            List<Food> foods = FoodDAO.Instance.GetFoodByCategoryID(id);
            cbFood.DataSource = foods;
            cbFood.DisplayMember = "name";
        }
        void LoadTable()
        {
            flpTable.Controls.Clear();
            List<Table> tableList = TableDAO.Instance.LoadTableList();
            foreach (Table item in tableList)
            {
                Button btn = new Button() { Width = TableDAO.TableWidth, Height = TableDAO.TableHeight };
                btn.Text = item.Name + Environment.NewLine + item.Status;
                btn.Click += Btn_Click;
                btn.Tag = item;
                switch (item.Status)
                {
                    case "Trống":
                        btn.BackColor = Color.Aqua;
                        break;
                    default:
                        btn.BackColor = Color.DarkBlue;
                        break;
                }

                flpTable.Controls.Add(btn);
            }
        }
        private List<Menu> billInfors;
        public float ShowBill(int id)
        {
            lsvBill.Items.Clear();
            this.billInfors = new List<Menu>();
            float totalPrice = 0;
            foreach ( Menu item in MenuDAO.Instance.GetListMenuByTable(id))
            {
                this.billInfors.Add(item);
            }
            foreach (Menu item in billInfors)
            {
                ListViewItem lsvItem = new ListViewItem(item.FoodName.ToString());
                lsvItem.SubItems.Add(item.Count.ToString());
                lsvItem.SubItems.Add(item.Price.ToString());
                lsvItem.SubItems.Add(item.TotalPrice.ToString());
                totalPrice += item.TotalPrice;

                lsvBill.Items.Add(lsvItem);
            }
            CultureInfo culture = new CultureInfo("vi-VN");
            txbTotalPrice.Text = totalPrice.ToString("c",culture);
            return totalPrice;

        }
        void LoadComboBoxTable(ComboBox cb)
        {
            cb.DataSource = TableDAO.Instance.LoadTableList();
            cb.DisplayMember = "name";
        }

        #endregion


        #region Events
        private void Btn_Click(object sender, EventArgs e)
        {
            int TableId = ((sender as Button).Tag as Table).Id;
            lsvBill.Tag = (sender as Button).Tag;
            ShowBill(TableId);
        }
        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            fAccountProfile f = new fAccountProfile(LoginAccount);
            f.UpdateAccount += F_UpdateAccount;
            f.ShowDialog();
        }

        private void F_UpdateAccount(object sender, AccountEvent e)
        {
            thôngTinTàiKhoảnToolStripMenuItem.Text = "Thong tin tai khoan (" + e.Acc.DisplayName + ")";
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAdmin f = new fAdmin();
            f.loginAccount = LoginAccount;
            f.InsertFood += F_InsertFood;
            f.DeleteFood += F_DeleteFood;
            f.UpdateFood += F_UpdateFood;
            f.InsertCategory += F_InsertCategory;
            f.UpdateCategory += F_UpdateCategory;
            f.DeleteCategory += F_DeleteCategory;
            f.InsertTable += F_InsertTable;
            f.UpdateTable += F_UpdateTable;
            f.DeleteTable += F_DeleteTable;
            f.ShowDialog();
        }

        private void F_DeleteTable(object sender, EventArgs e)
        {
            LoadTable();
        }

        private void F_UpdateTable(object sender, EventArgs e)
        {
            LoadTable();
        }

        private void F_InsertTable(object sender, EventArgs e)
        {
            LoadTable();
        }

        private void F_DeleteCategory(object sender, EventArgs e)
        {
            LoadCategories();
        }

        private void F_UpdateCategory(object sender, EventArgs e)
        {
            LoadCategories();
        }

        private void F_InsertCategory(object sender, EventArgs e)
        {
            LoadCategories();
        }

        private void F_UpdateFood(object sender, EventArgs e)
        {
            LoadFoodByCatagoriID((cbCategory.SelectedItem as Category).Id);
            if(lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).Id);
        }

        private void F_DeleteFood(object sender, EventArgs e)
        {
            LoadFoodByCatagoriID((cbCategory.SelectedItem as Category).Id);
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).Id);
            LoadTable();
        }

        private void F_InsertFood(object sender, EventArgs e)
        {
            LoadFoodByCatagoriID((cbCategory.SelectedItem as Category).Id);
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).Id);
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedItem == null)
            {
                return;
            }
            Category selected = cb.SelectedItem as Category;
            id = selected.Id;
            LoadFoodByCatagoriID(id);
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;
            if (table == null)
            {
                MessageBox.Show("Hay chon ban!");
                return;
            }
            int idBill = BillDAO.Instance.GetUncheckBillIDByTableID(table.Id);
            int foodID = (cbFood.SelectedItem as Food).Id;
            int count = (int)nmFoodCount.Value;

            if (idBill == -1)
            {
                BillDAO.Instance.InsertBill(table.Id);
                BillInfoDAO.Instance.InsertBillInfo(BillDAO.Instance.GetMaxIDBill(), foodID, count);
            }
            else
            {
                BillInfoDAO.Instance.InsertBillInfo(idBill, foodID, count);
            }

            ShowBill(table.Id);
            LoadTable();
        }
        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;
            int idBill = BillDAO.Instance.GetUncheckBillIDByTableID(table.Id);
            int discount = (int)nmDisCount.Value;
            double totalPrice = ShowBill(table.Id);
            double finalPrice = totalPrice - totalPrice / 100 * discount;
            if (idBill != -1)
            {
                if (MessageBox.Show($"Ban co muon thanh toan {table.Name} vs discount la {discount} thanh gia {finalPrice} ", "Thong bao", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    BillDAO.Instance.CheckOut(idBill, discount, (float)finalPrice);
                    ShowBill(table.Id);
                    LoadTable();
                }
            }
        }
        private void btnSwitchTable_Click(object sender, EventArgs e)
        {
            int id1 = (lsvBill.Tag as Table).Id;
            int id2 = (cbSwitchTable.SelectedItem as Table).Id;
            if ((MessageBox.Show($"Ban co muon chuyen tu ban {(lsvBill.Tag as Table).Name} sang ban {(cbSwitchTable.SelectedItem as Table).Name}")) == System.Windows.Forms.DialogResult.OK)
            {
                TableDAO.Instance.SwitchTable(id1, id2);
                LoadTable();
            }

        }
        #endregion


    }
}
