using DAL.DAO;
using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class SalaryBLL
    {
        public static void AddSalary(SALARY salary)
        {
            SalaryDAO.Add(salary);
        }

        public static SalaryDTO GetALL()
        {
            SalaryDTO salarydto = new SalaryDTO();
            salarydto.Employees = EmployeeDAO.GetEmployees();
            salarydto.Departments = DepartmentDAO.GetDepartments();
            salarydto.Positions = PositionDAO.GetPositions();
            salarydto.Months = SalaryDAO.GetMonths();
            return salarydto;
        }
    }
}
