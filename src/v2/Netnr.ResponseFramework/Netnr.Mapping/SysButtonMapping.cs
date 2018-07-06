using Microsoft.EntityFrameworkCore;
using Netnr.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Netnr.Mapping
{
	class SysButtonMapping : IEntityTypeConfiguration<SysButton>
	{
		public void Configure(EntityTypeBuilder<SysButton> builder)
		{
			builder.HasKey(x => x.ID);
		}
	}
}
