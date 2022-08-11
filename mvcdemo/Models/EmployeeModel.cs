using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mvcdemo.Data;

namespace mvcdemo.Models
{
    public class EmployeeModel
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public string EmpAddress { get; set; }
        public Nullable<decimal> EmpSalary { get; set; }
        public string Job { get; set; }
        public List<EmployeeModel> GetEmpList()
        {
            mvcdemoEntities db = new mvcdemoEntities();
            List<EmployeeModel> lstemp = new List<EmployeeModel>();
            var list = db.tblEmployees.ToList();
            if (list != null)
            {
                foreach (var listemp in list)
                {
                    lstemp.Add(new EmployeeModel()
                    {
                        EmpId = listemp.EmpId,
                        EmpName = listemp.EmpName,
                        EmpAddress = listemp.EmpAddress,
                        EmpSalary = listemp.EmpSalary,
                        Job = listemp.Job
                    });
                }
            }
            return lstemp;
        }
    }
}