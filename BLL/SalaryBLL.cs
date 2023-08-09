using DAL.DAO;
using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Runtime.Remoting.Messaging;

namespace BLL
{
    public class SalaryBLL
    {
        public static void AddSalary(SALARY salary, bool control)
        {
            SalaryDAO.Add(salary);
            if (control)
            {
                EmployeeDAO.UpdateEmployee(salary.EmployeeID, salary.Amount);
            }
        }

        public static SalaryDTO GetALL()
        {
            SalaryDTO salarydto = new SalaryDTO();
            salarydto.Employees = EmployeeDAO.GetEmployees();
            salarydto.Departments = DepartmentDAO.GetDepartments();
            salarydto.Positions = PositionDAO.GetPositions();
            salarydto.Months = SalaryDAO.GetMonths();
            salarydto.Salaries = SalaryDAO.GetSalaries();
            return salarydto;
        }

        public static void UpdateSalary(SALARY update, bool control)
        {
            SalaryDAO.UpdateSalary(update);
            if (control)
            {
                EmployeeDAO.UpdateEmployee(update.EmployeeID, update.Amount);
            }
        }
    }
}
