using Microsoft.EntityFrameworkCore;
using Netnr.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Netnr.Mapping
{
	class SysTableConfigMapping : IEntityTypeConfiguration<SysTableConfig>
	{
		public void Configure(EntityTypeBuilder<SysTableConfig> builder)
		{
			builder.HasKey(x => x.ID);
		}
	}
}
