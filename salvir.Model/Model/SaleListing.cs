﻿using deprosa.Common;
using deprosa.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace deprosa.Model
{

    public class SaleListing : Entity
    {
        #region All
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual List<Image> Images { get; set; }

        [ForeignKey("Owner")]
        public int? CompanyId { get; set; }

        public virtual Company Owner { get; set; }

        [ForeignKey("CreatedBy")]
        public int? AccountId { get; set; }

        public virtual Account CreatedBy { get; set; }

        [ForeignKey("Manufacturer")]
        public int? ManufacturerId { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }

        [ForeignKey("ProductType")]
        public int? ProductTypeId { get; set; }
        public virtual ProductType ProductType { get; set; }

        public virtual List<Comment> Comments { get; set; }

        public double Price { get; set; }
        
        public DateTime ExpirationDate { get; set; }
        
        public virtual Subscription Subscription { get; set; }
        public eCondition Condition { get; set; }

        public bool IsSold { get; set; }
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
