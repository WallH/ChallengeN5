using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class PermissionModel
    {
        public int Id { get; set; }
        public string EmployeeForename { get; set; }
        public string EmployeeSurname { get; set; }
        public PermissionType PermissionType { get; set; }
        public DateTime PermissionDate { get; set; } = DateTime.Now;
    }
}
