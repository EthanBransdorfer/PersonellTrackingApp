using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DAL;
using DAL.DTO;
using System.IO;

namespace Personnel_Tracking_App
{
    public partial class FrmEmployee : Form
    {
        public FrmEmployee()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();  
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(txtUserNo.Text.Trim()=="")
            {
                MessageBox.Show("User no is empty.");
            }
            else if (!EmployeeBLL.isUnique(Convert.ToInt32(txtUserNo.Text)))
            {
                MessageBox.Show("User no is already taken.");
            }
            else if(txtPassword.Text.Trim()=="")
            {
                MessageBox.Show("Password is empty.");
            }
            else if(txtName.Text.Trim()=="")
            {
                MessageBox.Show("Name is empty.");
            }
            else if (txtSurname.Text.Trim() == "")
            {
                MessageBox.Show("Surname is empty.");
            }
            else if (txtSalary.Text.Trim() == "")
            {
                MessageBox.Show("Salary is empty.");
            }
            else if (cmbDepartment.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a department.");
            }
            else if (cmbPosition.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a position.");
            }
            else if (txtAddress.Text.Trim() == "")
            {
                MessageBox.Show("Address is empty.");
            }
            else
            {
                EMPLOYEE employee = new EMPLOYEE();
                employee.UserNo = Convert.ToInt32(txtUserNo.Text);
                employee.Password = txtPassword.Text;
                employee.isAdmin = chAdmin.Checked;
                employee.Name = txtName.Text;
                employee.Surname = txtSurname.Text;
                employee.Salary = Convert.ToInt32(txtSalary.Text);
                employee.DepartmentID = Convert.ToInt32(cmbDepartment.SelectedValue);
                employee.PositionID = Convert.ToInt32(cmbPosition.SelectedValue);
                employee.Address = txtAddress.Text;
                employee.BirthDay = dateTimePicker1.Value;
                employee.ImagePath = fileName;
                EmployeeBLL.AddEmployee(employee);
                File.Copy(txtImagePath.Text, @"images\\" + fileName);
                MessageBox.Show("Employee was added.");
                txtUserNo.Clear();
                txtPassword.Clear();
                txtSalary.Clear();
                txtAddress.Clear();
                txtSurname.Clear();
                txtName.Clear();
                chAdmin.Checked = false;
                txtImagePath.Clear();
                pictureBox1.Image = null;
                combofull = false;
                cmbDepartment.SelectedIndex = -1;
                cmbPosition.DataSource = dto.Positions;
                cmbPosition.SelectedIndex = -1;
                combofull = true;
                dateTimePicker1.Value = DateTime.Today;

            }
        }

        private void txtUserNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }
        private void txtSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }
        EmployeeDTO dto = new EmployeeDTO();
        private void FrmEmployee_Load(object sender, EventArgs e)
        {
            dto = EmployeeBLL.GetALL();
            cmbDepartment.DataSource = dto.Departments;
            cmbDepartment.DisplayMember = "DepartmentName";
            cmbDepartment.ValueMember = "ID";
            cmbPosition.DataSource = dto.Positions;
            cmbPosition.DisplayMember = "PositionName";
            cmbPosition.ValueMember = "ID";
            cmbDepartment.SelectedIndex = -1;
            cmbPosition.SelectedIndex = -1;
            combofull = true;
        }
        bool combofull = false;
        private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combofull )
            {
                int departmentID = Convert.ToInt32(cmbDepartment.SelectedValue);
                cmbPosition.DataSource = dto.Positions.Where(x => x.DepartmentID == departmentID).ToList();
            }
        }
        string fileName = "";
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Load(openFileDialog1.FileName);
                txtImagePath.Text = openFileDialog1.FileName;
                string Unique = Guid.NewGuid().ToString();
                fileName += Unique + openFileDialog1.SafeFileName;
            }
        }
        bool isUnique = false; //Unique user name check
        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (txtUserNo.Text.Trim() == "")
            {
                MessageBox.Show("User no is empty.");
            }
            else
            {
                isUnique = EmployeeBLL.isUnique(Convert.ToInt32(txtUserNo.Text));
                if (!isUnique)
                {
                    MessageBox.Show("User no is already taken.");
                }
                else
                {
                    MessageBox.Show("User no is available.");
                }
            }
        }
    }
}
