using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SqlPersistence.Configurations
{
    public class PermissionConfiguration : EntityBaseConfiguration<Permission>
    {
        public override void Configure(EntityTypeBuilder<Permission> entity)
        {

        }
    }
}
