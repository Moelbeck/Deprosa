using deprosa.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace deprosa.Web.Model
{
    public class SaleListingCreateViewModel
    {

        [Display(Name = "Kategorier:")]
        public List<MainCategoryDTO> MainCategories { get; set; }
    
        [Display(Name="Valgte kategori")]
        public MainCategoryDTO SelectedMainCategory { get; set; }
        public SelectList MainCategoriesSelectList
        {
            get
            {
                var selectedmainid = SelectedMainCategory != null ? SelectedMainCategory.ID : 0;
                return new SelectList(MainCategories, "ID", "Name",MainCategories.First(e=>e.ID == selectedmainid));
            }
        }

        public List<SubCategoryDTO> SubCategories { get; set; }
        [Display(Name = "Under kategorier:")]
        public List<SubCategoryDTO> CurrentSubCategories { get; set; }

        [Display(Name = "Valgte under kategori")]
        public SubCategoryDTO SelectedSubCategory { get; set; }
        public SelectList SubCategoriesSelectList
        {
            get
            {
                return new SelectList(CurrentSubCategories, "ID", "Name");
            }
        }

        public List<ProductTypeDTO> ProductTypes { get; set; }
        [Display(Name = "Type:")]
        public List<ProductTypeDTO> CurrentProductTypes { get; set; }

        [Display(Name = "Valgte type")]
        public ProductTypeDTO SelectedProductType { get; set; }
        public SelectList ProductTypesSelectList
        {
            get
            {
                return new SelectList(CurrentProductTypes, "ID", "Name");
            }
        }

        public SaleListingDTO SaleListing { get; set; }
    }
}