using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Dm = SolutionTemplate.DataModel;

namespace SolutionTemplate.DataAccess.Configurations
{
    internal class Doodad : EntityTypeConfiguration<Dm.Doodad>
    {
        public Doodad()
        {
            ToTable("dbo.Doodads");

            HasKey(m => m.Id);

            Property(m => m.Created)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

            Property(m => m.Updated)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }
}