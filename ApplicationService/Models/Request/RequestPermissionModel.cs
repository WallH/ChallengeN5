using ApplicationService.Models.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Models.Request
{
    public class RequestPermissionModel : IRequest<RequestPermissionModelResponse>
    {
        public int Id { get; set; }
    }
}
