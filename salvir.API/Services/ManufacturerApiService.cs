﻿using AutoMapper;
using deprosa.Interfaces;
using deprosa.Model;
using deprosa.Repository;
using deprosa.Repository.Abstract;
using deprosa.Repository.DatabaseContext;
using deprosa.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace deprosa.WebService
{
    public class ManufacturerApiService : IManufacturerService

    {
        private GenericRepository<Manufacturer> _manufacturerRepository;
        public ManufacturerApiService()
        {
            BzaleDatabaseContext context = new BzaleDatabaseContext();
            _manufacturerRepository = new GenericRepository<Manufacturer>(context);
        }

        public void CreateManufacturer(ManufacturerDTO viewmodel)
        {
            try
            {
                var manufacturer = Mapper.Map<ManufacturerDTO, Manufacturer>(viewmodel);
                _manufacturerRepository.Add(manufacturer);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<ManufacturerDTO> GetManufacturersInCategory(int categoryid)
        {
            var manufacturers = _manufacturerRepository.Get(e=>e.ProductTypes.All(a => a.Category.ID == categoryid) && e.Deleted == null);
            return manufacturers.Select(Mapper.Map<Manufacturer, ManufacturerDTO>).ToList();
        }

        public ManufacturerDTO GetManuFacturer(int id)
        {
            var manufacturer = _manufacturerRepository.GetSingle(e=>e.ID == id);
            return Mapper.Map<Manufacturer, ManufacturerDTO>(manufacturer);
        }

    }
}
