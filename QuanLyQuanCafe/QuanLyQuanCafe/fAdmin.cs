using QuanLyQuanCafe.DAO;
using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanCafe
{
    public partial class fAdmin : Form
    {
        BindingSource accountList = new BindingSource();
        public Account loginAccount;
        public fAdmin()
        {
            InitializeComponent();
            Load();
        }
        void Load()
        {
            LoadListCategoryIntoCombox(cbFoodCategory);
            dtgvAccount.DataSource = accountList;
            LoadDateTimePickerBill();
            LoadBill(dtpkFromDate.Value, dtpkToDate.Value);
            LoadFood();
            AddFoodBinding();
            AddAccountBinding();
            LoadAccout();
            LoadCategory();
            AddCategoryBinding();
            LoadTable();
            AddFoodTableBinding();
            
        }
        #region Method
        void AddAccountBinding()
        {
            txbUserName.DataBindings.Add(new Binding("Text",dtgvAccount.DataSource,"UserName",true,DataSourceUpdateMode.Never));
            txbDisplayName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "DisplayName", true, DataSourceUpdateMode.Never));
            nmAccount.DataBindings.Add(new Binding("Value", dtgvAccount.DataSource, "Type", true, DataSourceUpdateMode.Never));
        }
        void LoadAccout()
        {
            accountList.DataSource = AccountDAO.Instance.GetListAccount();
        }
        void LoadBill(DateTime Checkin , DateTime CheckOut)
        {
            dtgvBill.DataSource = BillDAO.Instance.GetBillListByDate(Checkin,CheckOut);
        }
        void LoadDateTimePickerBill()
        {
            DateTime datetime = DateTime.Now;
            dtpkFromDate.Value = new DateTime(datetime.Year,datetime.Month,1);
            dtpkToDate.Value = datetime.AddMonths(1).AddDays(-1);
        }
        void LoadFood()
        {
            dtgvFood.DataSource = FoodDAO.Instance.GetListFood();
        }
        void LoadCategory()
        {
            dtgvCategory.DataSource = CategoryDAO.Instance.GetListCategories();
        }
        void LoadTable()
        {
            dtgvTable.DataSource = TableDAO.Instance.LoadTableList();
        }
        void AddCategoryBinding()
        {
            txbCategoryID.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource,"Id",true,DataSourceUpdateMode.Never));
            txbcategoryName.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "Name", true, DataSourceUpdateMode.Never));
        }
        void AddFoodBinding()
        {
            txbFoodName.DataBindings.Add("Text", dtgvFood.DataSource,"Name",true,DataSourceUpdateMode.Never);
            txbFoodID.DataBindings.Add("Text", dtgvFood.DataSource, "Id", true, DataSourceUpdateMode.Never);
            nmFoodPrice.DataBindings.Add("Value", dtgvFood.DataSource, "Price", true, DataSourceUpdateMode.Never);
        }
        void AddFoodTableBinding()
        {
            txbTableName.DataBindings.Add("Text", dtgvTable.DataSource, "Name", true, DataSourceUpdateMode.Never);
            txbTableId.DataBindings.Add("Text", dtgvTable.DataSource, "Id", true, DataSourceUpdateMode.Never);
            txbStatus.DataBindings.Add("Text", dtgvTable.DataSource, "Status", true, DataSourceUpdateMode.Never);
        }
        void LoadListCategoryIntoCombox(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.GetListCategories();
            cb.DisplayMember = "Name";
        }
        List<Food> SearchFoodByName(string name)
        {
            List<Food> foods = FoodDAO.Instance.SearchFoodByName(name);
            return foods;
        }
        void AddAccount(string name, string displayName, int type)
        {
            if (AccountDAO.Instance.InsertAccount(name,displayName,type))
            {
                MessageBox.Show("Insert Successfull");
            }
            else
            {
                MessageBox.Show("Retry again");
            }
            LoadAccout();
        }
        void EditAccount(string name, string displayName, int type)
        {
            if (AccountDAO.Instance.UpdateAccount(name, displayName, type))
            {
                MessageBox.Show("Update Successfull");
            }
            else
            {
                MessageBox.Show("Retry again");
            }
            LoadAccout();
        }
        void DeleteAccount(string name)
        {
            if (loginAccount.Equals(name))
            {
                MessageBox.Show("Don't Deleted your self");
            }
            if (AccountDAO.Instance.DeleteAccount(name))
            {
                MessageBox.Show("Delete Successfull");
            }
            else
            {
                MessageBox.Show("Retry again");
            }
            LoadAccout();
        }
        void ResetPassword(string name)
        {
            if (AccountDAO.Instance.ResetPassword(name))
            {
                MessageBox.Show("Reset password Successfull");
            }
            else
            {
                MessageBox.Show("Retry again");
            }
            LoadAccout();
        }

        #endregion

        #region Event
        private void btnViewBill_Click(object sender, EventArgs e)
        {
            LoadBill(dtpkFromDate.Value, dtpkToDate.Value);
        }
        private void txbFoodID_TextChanged(object sender, EventArgs e)
        {
            if (dtgvFood.SelectedCells.Count>0)
            {
                int id = (int)dtgvFood.SelectedCells[0].OwningRow.Cells["CategoryId"].Value;
                Category category = CategoryDAO.Instance.GetCategoryById(id);
                //cbFoodCategory.SelectedItem = category;
                int index = -1;
                int i = 0;
                foreach (Category item in cbFoodCategory.Items)
                {
                    if (item.Id == category.Id)
                    {
                        index = i;
                        break;
                    }
                    i++;
                }
                cbFoodCategory.SelectedIndex = index;

            }
        }
        private void btnAddFood_Click(object sender, EventArgs e)
        {
            string name = txbFoodName.Text;
            int idcategory = (cbFoodCategory.SelectedItem as Category).Id;
            float price = (float)nmFoodPrice.Value;
            if (FoodDAO.Instance.InsertFood(name,idcategory,price))
            {
                MessageBox.Show("Insert successfull");
                LoadFood();
                if (insertFood != null)
                {
                    insertFood(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Retry again");

            }
        }
        private void btnEditFood_Click(object sender, EventArgs e)
        {
            string name = txbFoodName.Text;
            int idcategory = (cbFoodCategory.SelectedItem as Category).Id;
            float price = (float)nmFoodPrice.Value;
            int id = Convert.ToInt32( txbFoodID.Text);
            if (FoodDAO.Instance.UpdateFood(id ,name, idcategory, price))
            {
                MessageBox.Show("Update successfull");
                LoadFood();
                if (updateFood != null)
                {
                    updateFood(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Retry again");

            }
        }

        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            
            int id = Convert.ToInt32(txbFoodID.Text);
            BillInfoDAO.Instance.DeleteFoodByFoodID(id);
            if (FoodDAO.Instance.DeleteFood(id))
            {
                MessageBox.Show("Delete successfull");
                LoadFood();
                if (deleteFood != null)
                {
                    deleteFood(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Retry again");

            }
        }
        private event EventHandler insertFood;
        public event EventHandler InsertFood
        {
            add { insertFood += value; }
            remove { insertFood -= value; }
        }
        private event EventHandler updateFood;
        public event EventHandler UpdateFood
        {
            add { updateFood += value; }
            remove { updateFood -= value; }
        }
        private event EventHandler deleteFood;
        public event EventHandler DeleteFood
        {
            add { deleteFood += value; }
            remove { deleteFood -= value; }
        }

        private void btnSearchFood_Click(object sender, EventArgs e)
        { 
            dtgvFood.DataSource = SearchFoodByName(txbSearchFoodName.Text);
        }

        private void btnShowFood_Click(object sender, EventArgs e)
        {
            LoadFood();
        }

        private void btnShowAccount_Click(object sender, EventArgs e)
        {
            LoadAccout();
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            string name = txbUserName.Text;
            string displayName = txbDisplayName.Text;
            int type = (int)nmAccount.Value;
            AddAccount(name,displayName,type);
        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            string name = txbUserName.Text;
            DeleteAccount(name);
        }

        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            string name = txbUserName.Text;
            string displayName = txbDisplayName.Text;
            int type = (int)nmAccount.Value;
            EditAccount(name, displayName, type);

        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            string name = txbUserName.Text;
            ResetPassword(name);
        }

        private void btnShowCategory_Click(object sender, EventArgs e)
        {
            LoadCategory();
        }

        private void btnShowTable_Click(object sender, EventArgs e)
        {
            LoadTable();
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            string name = txbcategoryName.Text;
            if (CategoryDAO.Instance.InsertCategory(name))
            {
                MessageBox.Show("Add successfull");
                LoadCategory();
                LoadFood();
                if (insertCategory != null)
                {
                    insertCategory(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Retry again");

            }
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbCategoryID.Text);
            if (CategoryDAO.Instance.DeleteCategory(id))
            {
                MessageBox.Show("Delete successfull");
                LoadCategory();
                LoadFood();
                if (deleteCategory != null)
                {
                    deleteCategory(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Retry again");

            }
        }

        private void btnEditCategory_Click(object sender, EventArgs e)
        {
            string name = txbcategoryName.Text;
            int id = Convert.ToInt32(txbCategoryID.Text);
            if (CategoryDAO.Instance.UpdateCategory(id,name))
            {
                MessageBox.Show("Update successfull");
                LoadCategory();
                LoadFood();
                if (updateCategory != null)
                {
                    updateCategory(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Retry again");

            }
        }
        private event EventHandler insertCategory;
        public event EventHandler InsertCategory
        {
            add { insertCategory += value; }
            remove { insertCategory -= value; }
        }
        private event EventHandler updateCategory;
        public event EventHandler UpdateCategory
        {
            add { updateCategory += value; }
            remove { updateCategory -= value; }
        }
        private event EventHandler deleteCategory;
        public event EventHandler DeleteCategory
        {
            add { deleteCategory += value; }
            remove { deleteCategory -= value; }
        }

        private void btnAddTable_Click(object sender, EventArgs e)
        {
            string name = txbTableName.Text;
            if (TableDAO.Instance.InsertTable(name))
            {
                MessageBox.Show("Add successfull");
                LoadTable();
                if (insertTable != null)
                {
                    insertTable(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Retry again");

            }
        }

        private void btnDeleteTable_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbTableId.Text);
            if (TableDAO.Instance.DeleteTable(id))
            {
                MessageBox.Show("Delete successfull");
                LoadTable();
                if (deleteTable != null)
                {
                    deleteTable(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Retry again");

            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string name = txbTableName.Text;
            int id = Convert.ToInt32(txbTableId.Text);
            if (TableDAO.Instance.UpdateTable(id, name))
            {
                MessageBox.Show("Update successfull");
                LoadTable();
                if (updateTable != null)
                {
                    updateTable(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Retry again");

            }
        }
        private event EventHandler insertTable;
        public event EventHandler InsertTable
        {
            add { insertTable += value; }
            remove { insertTable -= value; }
        }
        private event EventHandler updateTable;
        public event EventHandler UpdateTable
        {
            add { updateTable += value; }
            remove { updateTable -= value; }
        }
        private event EventHandler deleteTable;
        public event EventHandler DeleteTable
        {
            add { deleteTable += value; }
            remove { deleteTable -= value; }
        }
    }
        #endregion


    
}
