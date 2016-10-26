using System.Collections.Generic;
using System.Linq;
using deprosa.Repository.DatabaseContext;
using deprosa.Repository.Abstract;
using deprosa.Model;
using deprosa.Interfaces;
using System;

namespace deprosa.Repository
{
    public class ManufacturerRepository : GenericRepository<Manufacturer>, IManufacturerRepository
    {
        public ManufacturerRepository(BzaleDatabaseContext context) : base(context)
        {

        }

        #region Manufacturer
        public Manufacturer GetManufacturer(int id)
        {
            return GetSingle(e => e.ID == id && e.Deleted == null);
        }

        public Manufacturer GetManufacturer(string name)
        {
            return GetSingle(e => e.Name.ToLower() == name.ToLower() && e.Deleted == null);
        }

        public IQueryable<Manufacturer> GetAllManufacturers(int page, int size)
        {
            return Get(e => e.Deleted == null).OrderBy(e => e.ID);

        }

        public IQueryable<Manufacturer> GetManufacturersForCategory(int categoryid, int page, int size)
        {
            return Get(e => e.ProductTypes.All(a=>a.Category.ID == categoryid) && e.Deleted == null);
        }

        public Manufacturer AddNewManufacturer(Manufacturer newManufacturer)
        {
            //We need to check if the person have verified it 
                Add(newManufacturer);
                Save();
            return newManufacturer;
        }

        public Manufacturer UpdateManufacturer(Manufacturer updated)
        {
            Edit(updated);
            Save();
            return GetSingle(e => e.ID == updated.ID);
        }

        public bool IsManufacturerInDatabase(Manufacturer m)
        {
            return GetSingle(e => e.Name.ToLower() == m.Name.ToLower())!=null;
        }
        #endregion
        
    }
}
