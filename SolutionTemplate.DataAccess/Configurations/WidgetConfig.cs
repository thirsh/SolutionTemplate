using SolutionTemplate.DataModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SolutionTemplate.DataAccess.Configurations
{
    internal class WidgetConfig : EntityTypeConfiguration<Widget>
    {
        public WidgetConfig()
        {
            ToTable("dbo.Widgets");

            HasKey(m => m.Id);

            Property(m => m.CreatedUtc)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

            Property(m => m.UpdatedUtc)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

            HasMany(m => m.Doodads)
                .WithRequired(m => m.Widget)
                .HasForeignKey(m => m.WidgetId);
        }
    }
}