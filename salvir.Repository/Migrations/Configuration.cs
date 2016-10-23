namespace Repository.Migrations
{
    using depross.Common;
    using depross.Model;
    using depross.Repository.DatabaseContext;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BzaleDatabaseContext>
    {
        private bool ShouldSeed;
        public Configuration()
        {
            ShouldSeed = false;
            AutomaticMigrationsEnabled = true;
        }

        public void Seed(BzaleDatabaseContext context)
        {
            if (ShouldSeed)
            {
                #region Main categories
                context.MainCategories.AddOrUpdate(e => e.Name,
                     new MainCategory
                     {
                         Name = "Biler",
                         Created = DateTime.Now,

                     }, new MainCategory
                     {
                         Name = "Elektronik",
                         Created = DateTime.Now,

                     },
                     new MainCategory
                     {
                         Name = "Bygge materiale",
                         Created = DateTime.Now,

                     });
                context.SaveChanges();

                #endregion

                #region Sub categories
                var carmain = context.MainCategories.FirstOrDefault(e => e.Name == "Biler");
                var byggemain = context.MainCategories.FirstOrDefault(e => e.Name == "Bygge materiale");
                var elektromain = context.MainCategories.FirstOrDefault(e => e.Name == "Elektronik");

                context.SubCategories.AddOrUpdate(e => e.Name,
                    new SubCategory
                    {
                        Name = "Personbil",
                        MainCategory = carmain,
                        Created = DateTime.Now
                    },
                    new SubCategory
                    {
                        Name = "Varevogn",
                        MainCategory = carmain,
                        Created = DateTime.Now
                    }, new SubCategory
                    {
                        Name = "Mursten",
                        MainCategory = byggemain,
                        Created = DateTime.Now
                    },
                    new SubCategory
                    {
                        Name = "Sten",
                        MainCategory = byggemain,
                        Created = DateTime.Now
                    },
                    new SubCategory
                    {
                        Name = "Sand",
                        MainCategory = byggemain,
                        Created = DateTime.Now
                    },
                    new SubCategory
                    {
                        Name = "Jord",
                        MainCategory = byggemain,
                        Created = DateTime.Now
                    },
                    new SubCategory
                    {
                        Name = "Søm & skruer",
                        MainCategory = byggemain,
                        Created = DateTime.Now
                    },
                    new SubCategory
                    {
                        Name = "Computer",
                        MainCategory = elektromain,
                        Created = DateTime.Now
                    },
                    new SubCategory
                    {
                        Name = "Computerdele",
                        MainCategory = elektromain,
                        Created = DateTime.Now
                    },
                    new SubCategory
                    {
                        Name = "Skærme",
                        MainCategory = elektromain,
                        Created = DateTime.Now
                    },
                new SubCategory
                {
                    Name = "Telefon",
                    MainCategory = elektromain,
                    Created = DateTime.Now
                },
                new SubCategory
                {
                    Name = "Diverse",
                    MainCategory = elektromain,
                    Created = DateTime.Now
                });

                context.SaveChanges();

                #endregion

                #region ProductTypes
                var personbilSub = context.SubCategories.FirstOrDefault(e => e.Name == "Personbil");
                var varevognSub = context.SubCategories.FirstOrDefault(e => e.Name == "Varevogn");
                var murstenSub = context.SubCategories.FirstOrDefault(e => e.Name == "Mursten");
                var stenSub = context.SubCategories.FirstOrDefault(e => e.Name == "Sten");
                var sandSub = context.SubCategories.FirstOrDefault(e => e.Name == "Sand");
                var jordSub = context.SubCategories.FirstOrDefault(e => e.Name == "Jord");
                var soemogskruerSub = context.SubCategories.FirstOrDefault(e => e.Name == "Søm & skruer");
                var computerSub = context.SubCategories.FirstOrDefault(e => e.Name == "Computer");
                var computerdeleSub = context.SubCategories.FirstOrDefault(e => e.Name == "Computerdele");
                var skaermeSub = context.SubCategories.FirstOrDefault(e => e.Name == "Skærme");
                var telefonSub = context.SubCategories.FirstOrDefault(e => e.Name == "Telefon");
                var diverseelektroSub = context.SubCategories.FirstOrDefault(e => e.Name == "Diverse");

                context.ProductTypes.AddOrUpdate(e => e.Name,
                #region sand type
                    new ProductType { Name = "Strand sand", Types = (eSalelistingTypes.Weight), Category = sandSub },
                    new ProductType { Name = "Fugesand", Types = (eSalelistingTypes.Weight), Category = sandSub },
                    new ProductType { Name = "Pudssand", Types = (eSalelistingTypes.Weight), Category = sandSub },
                    new ProductType { Name = "Vasket sand", Types = (eSalelistingTypes.Weight), Category = sandSub },
                    new ProductType { Name = "Anden sandtype", Types = (eSalelistingTypes.Weight), Category = sandSub },

                #endregion
                #region Jord type
                    new ProductType { Name = "Muld", Types = (eSalelistingTypes.Weight), Category = jordSub },
                    new ProductType { Name = "Jord", Types = (eSalelistingTypes.Weight), Category = jordSub },
                    new ProductType { Name = "Ler", Types = (eSalelistingTypes.Weight), Category = jordSub },
                    new ProductType { Name = "Anden jordtype", Types = (eSalelistingTypes.Weight), Category = jordSub },
                #endregion
                #region Sten type
                    new ProductType { Name = "Grus", Types = (eSalelistingTypes.Weight | eSalelistingTypes.Dimensions), Category = stenSub },
                    new ProductType { Name = "Kampsten", Types = (eSalelistingTypes.Weight | eSalelistingTypes.Dimensions), Category = stenSub },
                    new ProductType { Name = "Anden stentype", Types = (eSalelistingTypes.Weight | eSalelistingTypes.Dimensions), Category = stenSub },
                #endregion
                #region Søm og skruer
                    new ProductType { Name = "Træ", Types = (eSalelistingTypes.Lenght | eSalelistingTypes.Thickness), Category = soemogskruerSub },
                    new ProductType { Name = "Beton", Types = (eSalelistingTypes.Weight | eSalelistingTypes.Dimensions), Category = soemogskruerSub },
                    new ProductType { Name = "Anden skrue/søm type", Types = (eSalelistingTypes.Weight | eSalelistingTypes.Dimensions), Category = soemogskruerSub },
                #endregion
                #region Computer typer
                    new ProductType { Name = "Stationære", Types = (eSalelistingTypes.Processor | eSalelistingTypes.RAM | eSalelistingTypes.Harddisk), Category = computerSub },
                    new ProductType { Name = "Bærebare", Types = (eSalelistingTypes.Processor | eSalelistingTypes.RAM | eSalelistingTypes.Harddisk | eSalelistingTypes.Screen), Category = computerSub },
                    new ProductType { Name = "Andre computere", Types = (eSalelistingTypes.Processor | eSalelistingTypes.RAM | eSalelistingTypes.Harddisk | eSalelistingTypes.Screen), Category = computerSub },
                #endregion
                #region Computerdele typer
                    new ProductType { Name = "HHD", Types = (eSalelistingTypes.Harddisk | eSalelistingTypes.Screen), Category = computerdeleSub },
                    new ProductType { Name = "SSD", Types = (eSalelistingTypes.Harddisk | eSalelistingTypes.Screen), Category = computerdeleSub },
                    new ProductType { Name = "Grafikkort", Types = (eSalelistingTypes.RAM), Category = computerdeleSub },
                    new ProductType { Name = "Mus", Types = (eSalelistingTypes.None), Category = computerdeleSub },
                    new ProductType { Name = "Tastatur", Types = (eSalelistingTypes.None), Category = computerdeleSub },
                #endregion
                #region Skærm typer
                    new ProductType { Name = "Fladskærm", Types = (eSalelistingTypes.Dimensions | eSalelistingTypes.Screen), Category = skaermeSub },
                    new ProductType { Name = "TV", Types = (eSalelistingTypes.Dimensions | eSalelistingTypes.Screen), Category = skaermeSub },
                    new ProductType { Name = "Fladskærm", Types = (eSalelistingTypes.Dimensions | eSalelistingTypes.Screen), Category = skaermeSub },
                    new ProductType { Name = "Anden skærm", Types = (eSalelistingTypes.Dimensions | eSalelistingTypes.Screen), Category = skaermeSub }

                    #endregion
                    #region Bil typer
                    #endregion

                    );
                #endregion
                context.SaveChanges();
            }
        }
    }
}
