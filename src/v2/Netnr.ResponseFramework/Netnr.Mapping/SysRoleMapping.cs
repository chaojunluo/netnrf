using Microsoft.EntityFrameworkCore;
using Netnr.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Netnr.Mapping
{
	class SysRoleMapping : IEntityTypeConfiguration<SysRole>
	{
		public void Configure(EntityTypeBuilder<SysRole> builder)
		{
			builder.HasKey(x => x.ID);
		}
	}
}
