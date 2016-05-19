using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Dm = SolutionTemplate.DataModel;

namespace SolutionTemplate.DataAccess.Configurations
{
    internal class Widget : EntityTypeConfiguration<Dm.Widget>
    {
        public Widget()
        {
            ToTable("dbo.Widgets");

            HasKey(m => m.Id);

            Property(m => m.Created)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

            Property(m => m.Updated)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

            HasMany(m => m.Doodads)
                .WithRequired(m => m.Widget)
                .HasForeignKey(m => m.WidgetId);
        }
    }
}