using deprosa.ViewModel;
using System.Collections.Generic;

namespace deprosa.Interfaces
{
    public interface IManufacturerService
    {
        List<ManufacturerDTO> GetManufacturersInCategory(int categoryid);
        ManufacturerDTO GetManuFacturer(int id);

        void CreateManufacturer(ManufacturerDTO viewmodel);
    }
}
