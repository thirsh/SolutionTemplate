using SolutionTemplate.DataModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SolutionTemplate.DataAccess.Configurations
{
    internal class DoodadConfig : EntityTypeConfiguration<Doodad>
    {
        public DoodadConfig()
        {
            ToTable("dbo.Doodads");

            HasKey(m => m.Id);

            Property(m => m.CreatedUtc)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

            Property(m => m.UpdatedUtc)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }
}