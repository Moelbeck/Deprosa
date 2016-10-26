using deprosa.Model;
using System.Collections.Generic;
using System.Linq;

namespace deprosa.Interfaces
{
    public interface IManufacturerRepository
    {

        Manufacturer GetManufacturer(int id);

        Manufacturer GetManufacturer(string name);

        IQueryable<Manufacturer> GetAllManufacturers(int page, int size);
        IQueryable<Manufacturer> GetManufacturersForCategory(int categoryid, int page, int size);

        Manufacturer AddNewManufacturer(Manufacturer newManufacturer);
        

        Manufacturer UpdateManufacturer(Manufacturer updated);
        bool IsManufacturerInDatabase(Manufacturer m);


    }
}
