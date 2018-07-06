using Microsoft.EntityFrameworkCore;
using Netnr.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Netnr.Mapping
{
	class TempExampleMapping : IEntityTypeConfiguration<TempExample>
	{
		public void Configure(EntityTypeBuilder<TempExample> builder)
		{
			builder.HasKey(x => x.ID);
		}
	}
}
