using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SqlPersistence.Configurations
{
    public class PermissionTypeConfiguration : EntityBaseConfiguration<PermissionType>
    {
        public override void Configure(EntityTypeBuilder<PermissionType> entity)
        {

        }
    }
}
