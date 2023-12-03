using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Models.Response
{
    public class GetPermissionsModelResponse
    {
        public IEnumerable<PermissionModel> Permissions {get; set; }
    }
}
