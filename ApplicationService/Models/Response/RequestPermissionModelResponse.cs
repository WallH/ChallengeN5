using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Models.Response
{
    public class RequestPermissionModelResponse
    {
        public PermissionModel? Permission { get; set; }
        public bool Success { get; set; }
    }
}
