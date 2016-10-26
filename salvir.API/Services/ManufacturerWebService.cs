using AutoMapper;
using deprosa.Interfaces;
using deprosa.Model;
using deprosa.Repository;
using deprosa.Repository.DatabaseContext;
using deprosa.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace deprosa.WebService
{
    public class ManufacturerWebService : IManufacturerService

    {
        private IManufacturerRepository _manufacturerRepository;
        public ManufacturerWebService()
        {
            BzaleDatabaseContext context = new BzaleDatabaseContext();
            _manufacturerRepository = new ManufacturerRepository(context);
        }

        public void CreateManufacturer(ManufacturerDTO viewmodel)
        {
            try
            {
                var manufacturer = Mapper.Map<ManufacturerDTO, Manufacturer>(viewmodel);
                _manufacturerRepository.AddNewManufacturer(manufacturer);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<ManufacturerDTO> GetManufacturersInCategory(int categoryid)
        {
            var manufacturers = _manufacturerRepository.GetManufacturersForCategory(categoryid, 1, int.MaxValue).ToList();
            return manufacturers.Select(Mapper.Map<Manufacturer, ManufacturerDTO>).ToList();
        }

        public ManufacturerDTO GetManuFacturer(int id)
        {
            var manufacturer = _manufacturerRepository.GetManufacturer(id);
            return Mapper.Map<Manufacturer, ManufacturerDTO>(manufacturer);
        }

    }
}
