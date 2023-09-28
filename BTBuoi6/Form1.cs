using BTBuoi6.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BTBuoi6
{
    public partial class Form1 : Form
    {
        EmployeesContextDB contextDB = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                this.Close();
        }

        private void BindGrid(List<Nhanvien> listNV)
        {
            dgvEmployees.Rows.Clear();
            foreach (var item in listNV)
            {
                dgvEmployees.Rows.Add(item.MaNV, item.TenNV, item.Ngaysinh.ToString(), item.Phongban.TenPB);
            }
        }

        private void FillDepartments(List<Phongban> listPB)
        {
            cmbDepartments.DataSource = listPB;
            cmbDepartments.ValueMember = "MaPB";
            cmbDepartments.DisplayMember = "TenPB";
        }

        private void LoadData()
        {
            try
            {
                contextDB = new EmployeesContextDB();
                List<Nhanvien> listNV = contextDB.Nhanviens.ToList();
                List<Phongban> listPB = contextDB.Phongbans.ToList();

                BindGrid(listNV);
                FillDepartments(listPB);

                txtId.Text = "";
                txtName.Text = "";
                dateTimePicker1.Value = DateTime.Today;
                cmbDepartments.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dgvEmployees_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvEmployees.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvEmployees.SelectedRows[0];

                txtId.Text = row.Cells[0].Value.ToString();
                txtName.Text = row.Cells[1].Value.ToString();
                dateTimePicker1.Value = Convert.ToDateTime(row.Cells[2].Value);
                cmbDepartments.Text = row.Cells[3].Value.ToString();
            }
        }

        private bool isValidData()
        {
            if (txtId.Text == "")
                return false;
            if (txtName.Text == "")
                return false;
            if (txtId.Text.Length != 6)
                return false;
            if (txtName.Text.Length > 20)
                return false;
            return true;
        }

        private bool isExistId()
        {
            string id = txtId.Text;
            foreach (DataGridViewRow row in dgvEmployees.Rows)
            {
                if (row.Cells[0].Value.ToString() == id)
                    return true;
            }
            return false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isValidData())
                    throw new Exception("Vui lòng nhập đủ/đúng thông tin.");
                if (isExistId())
                    throw new Exception("Mã NV đã tồn tại");
                Nhanvien nhanvien = new Nhanvien() { MaNV = txtId.Text, TenNV = txtName.Text, Ngaysinh = dateTimePicker1.Value, MaPB = cmbDepartments.SelectedValue.ToString() };

                contextDB.Nhanviens.Add(nhanvien);
                contextDB.SaveChanges();
                LoadData();
                MessageBox.Show("Thêm mới dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isExistId())
                    throw new Exception("Không tìm thấy Mã NV cần sửa.");
                if (!isValidData())
                    throw new Exception("Vui lòng nhập đủ/đúng thông tin.");

                Nhanvien nhanvienUpdate = contextDB.Nhanviens.FirstOrDefault(p => p.MaNV.ToString() == txtId.Text.ToString());
                if (nhanvienUpdate != null)
                {
                    nhanvienUpdate.TenNV = txtName.Text;

                    nhanvienUpdate.Ngaysinh = dateTimePicker1.Value;

                    nhanvienUpdate.MaPB = cmbDepartments.SelectedValue.ToString();

                    contextDB.SaveChanges();
                    LoadData();
                    MessageBox.Show("Sửa dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isExistId())
                    throw new Exception("Không tìm thấy Mã NV cần xóa.");

                Nhanvien nhanvienDel = contextDB.Nhanviens.FirstOrDefault(p => p.MaNV.ToString() == txtId.Text.ToString());
                if (nhanvienDel != null)
                {
                    DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        contextDB.Nhanviens.Remove(nhanvienDel);

                        contextDB.SaveChanges();
                        LoadData();
                        MessageBox.Show("Xóa dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
            }
        }
    }
}
