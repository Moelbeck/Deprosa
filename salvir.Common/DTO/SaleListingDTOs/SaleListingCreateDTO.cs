using deprosa.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace deprosa.ViewModel
{
    public class SaleListingCreateDTO
    {
        #region All
        public int ID { get; set; }
        [Display(Name = "Titel")]
        public string Title { get; set; }
        [Display(Name = "Beskrivelse")]
        public string Description { get; set; }

        [Display(Name = "Billeder")]
        public virtual List<ImageUploadDTO> Images { get; set; } = new List<ImageUploadDTO>();

        [Display(Name = "Ejer")]
        public virtual CompanyDTO Owner { get; set; }

        [Display(Name = "Oprettet af")]
        public virtual AccountDTO CreatedBy { get; set; }

        [Display(Name = "Fabrikant")]
        public virtual ManufacturerDTO Manufacturer { get; set; }
        [Display(Name = "Produkt type")]
        public virtual ProductTypeDTO ProductType { get; set; }

        [Display(Name = "Pris")]
        public double Price { get; set; }

        [Display(Name = "Udløbs dato")]
        public DateTime ExpirationDate { get; set; }

        [Display(Name = "Tilstand")]
        public eCondition Condition { get; set; }

        #endregion

        #region Dimensions
        [Display(Name = "Højde (cm)")]
        public int Height { get; set; }
        [Display(Name = "Bredde (cm)")]
        public int Width { get; set; }
        [Display(Name = "Dybde (cm)")]
        public int Depth { get; set; }
        #endregion

        #region Weight
        [Display(Name = "Vægt")]
        public int Weight { get; set; }
        #endregion
        #region Thickness
        [Display(Name = "Tykkelse")]
        public int Thickness { get; set; }
        #endregion
        #region Length
        [Display(Name = "Længde")]
        public int Length { get; set; }
        #endregion
        #region Processor
        [Display(Name = "Processor (GHz)")]
        public string CPU { get; set; }
        #endregion
        #region RAM
        [Display(Name = "RAM (GB)")]
        public int RAM { get; set; }
        #endregion
        #region Screen
        [Display(Name = "Skærm størrelse (\")")]
        public double ScreenSize { get; set; }
        #endregion
        #region Harddisk
        [Display(Name = "Harddisk (GB)")]
        public int Harddisk { get; set; }
        #endregion
        #region Car
        [Display(Name = "Model")]
        public string Model { get; set; }
        [Display(Name = "Årgang")]
        public int Year { get; set; }
        [Display(Name = "Antal kørte Km")]
        public int Kilometers { get; set; }
        [Display(Name = "Brændstof type")]
        public string FuelType { get; set; }
        [Display(Name = "Km/l")]
        public double KmPrLiter { get; set; }
        [Display(Name = "Farve")]
        public string Color { get; set; }
        [Display(Name = "Sidste service år")]
        public string LastService { get; set; }
        #endregion

        #region PrivateCar
        [Display(Name = "Antal døre")]
        public int NoOfDoors { get; set; }
        #endregion
        #region CompanyCar
        [Display(Name = "Moms betalt")]
        public bool VatPayed { get; set; }
        #endregion
    }
}
