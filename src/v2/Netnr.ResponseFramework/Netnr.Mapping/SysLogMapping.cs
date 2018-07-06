using Microsoft.EntityFrameworkCore;
using Netnr.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Netnr.Mapping
{
	class SysLogMapping : IEntityTypeConfiguration<SysLog>
	{
		public void Configure(EntityTypeBuilder<SysLog> builder)
		{
			builder.HasKey(x => x.ID);
		}
	}
}
