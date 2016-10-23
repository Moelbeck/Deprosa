using depross.Common;
using depross.Model.Base;
using System;
using System.Collections.Generic;

namespace depross.Model
{

    public class SaleListing : Entity
    {
        #region All
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual List<Image> Images { get; set; }

        public virtual Company Owner { get; set; }

        public virtual Account CreatedBy { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }
        public virtual ProductType ProductType { get; set; }

        public virtual List<Comment> Comments { get; set; }

        public double Price { get; set; }
        
        public DateTime ExpirationDate { get; set; }
        
        public virtual Subscription Subscription { get; set; }

        #endregion

        #region Dimensions
        public int Height { get; set; }
        public int Width { get; set; }
        public int Depth { get; set; }
        #endregion

        #region Weight
        public int Weight { get; set; }
        #endregion
        #region Thickness
        public int Thickness { get; set; }
        #endregion
        #region Length
        public int Length { get; set; }
        #endregion
        #region Processor
        public string CPU { get; set; }
        #endregion
        #region RAM
        public int RAM { get; set; }
        #endregion
        #region Screen
        public double ScreenSize { get; set; }
        #endregion
        #region Harddisk
        public int Harddisk { get; set; }
        #endregion
        #region Car
        public string Model { get; set; }
        public int Year { get; set; }
        public int Kilometers { get; set; }
        public string FuelType { get; set; }
        public double KmPrLiter { get; set; }
        public string Color { get; set; }
        public string LastService { get; set; }
        #endregion

        #region PrivateCar
        public int NoOfDoors { get; set; }
        #endregion
        #region CompanyCar
        public bool VatPayed { get; set; }
        #endregion
    }
}
