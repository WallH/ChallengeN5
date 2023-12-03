using ApplicationService.Models.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Models.Request
{
    public class ModifyPermissionModel : IRequest<ModifyPermissionModelResponse>
    {
        public int Id { get; set; }
        public string EmployeeForename { get; set; }
        public string EmployeeSurname { get; set; } 
        public int PermissionType { get; set; }

    }
}
