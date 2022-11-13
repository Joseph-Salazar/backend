using Domain.MainModule.Entity;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.MainModule.EntityConfig;

public class SavedJobOffersConfig : IEntityTypeConfiguration<SavedJobOffers>
{
    public void Configure(EntityTypeBuilder<SavedJobOffers> builder)
    {
        builder.HasKey(e => e.Id);
    }
}