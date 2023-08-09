using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class SalaryDAO : EmployeeContext
    {
        public static void Add(SALARY salary)
        {
            try
            {
                db.SALARies.InsertOnSubmit(salary);
                db.SubmitChanges();
            }
            catch(Exception ex) 
            {
                throw(ex);
            }
        }

        public static List<MONTH> GetMonths()
        {
            return db.MONTHs.ToList();
        }
    }
}
