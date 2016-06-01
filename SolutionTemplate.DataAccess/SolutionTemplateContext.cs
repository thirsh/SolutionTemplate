using SolutionTemplate.DataModel;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;

namespace SolutionTemplate.DataAccess
{
    public class SolutionTemplateContext : DbContext
    {
        public SolutionTemplateContext() : base("SolutionTemplate")
        {
            Database.SetInitializer<SolutionTemplateContext>(null);

            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Doodad> Doodads { get; set; }
        public DbSet<Widget> Widgets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            GetType().Assembly.GetTypes()
                .Where(t => t.BaseType.IsGenericType
                    && t.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>))
                .ToList()
                .ForEach(config => modelBuilder.Configurations.Add((dynamic)Activator.CreateInstance(config)));

            base.OnModelCreating(modelBuilder);
        }
    }
}