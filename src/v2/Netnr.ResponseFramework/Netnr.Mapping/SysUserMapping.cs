using Microsoft.EntityFrameworkCore;
using Netnr.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Netnr.Mapping
{
	class SysUserMapping : IEntityTypeConfiguration<SysUser>
	{
		public void Configure(EntityTypeBuilder<SysUser> builder)
		{
			builder.HasKey(x => x.ID);
		}
	}
}
