
using deprosa.Model;
using deprosa.Model.Base;
using deprosa.Model.Log;
using System.Data.Entity;
//using Repository.Migrations;

namespace deprosa.Repository.DatabaseContext
{
    public class BzaleDatabaseContext : DbContext
    {

        public BzaleDatabaseContext()
            : base("ConnectionString")
        {
            Configuration.LazyLoadingEnabled = false;
            //InitializeDatabase();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<BzaleDatabaseContext>(null);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<Advertiser> Advertisers { get; set; }
        public DbSet<MainCategory> MainCategories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<CategoryPreferences> CategoryPreferenceses { get; set; }
        public DbSet<BaseComment> Comments { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<SaleListing> SaleListings { get; set; }
        public DbSet<LogCategory> LogCategories { get; set; }
        public DbSet<LogLogin> LogLogins { get; set; }
        public DbSet<LogSaleListing> LogSaleListings{ get; set; }
        public DbSet<LogSearch> LogSearches{ get; set; }

        //private void InitializeDatabase()
        //{
        //    if (Database.Exists())
        //    {
        //        //Database.Initialize(true);
        //        //var config = new Configuration();
        //        //config.Seed(this);
        //    }
        //}
    }
}
