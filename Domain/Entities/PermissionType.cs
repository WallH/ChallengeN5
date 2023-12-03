using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("PermissionTypes")]
    public class PermissionType : IEntityBaseInt
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
