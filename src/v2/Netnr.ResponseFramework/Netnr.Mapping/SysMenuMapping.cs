using Microsoft.EntityFrameworkCore;
using Netnr.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Netnr.Mapping
{
	class SysMenuMapping : IEntityTypeConfiguration<SysMenu>
	{
		public void Configure(EntityTypeBuilder<SysMenu> builder)
		{
			builder.HasKey(x => x.ID);
		}
	}
}
