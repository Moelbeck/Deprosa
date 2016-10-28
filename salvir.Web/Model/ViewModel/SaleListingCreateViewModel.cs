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
                return new SelectList(MainCategories, "ID", "Name");
            }
        }

        [Display(Name = "Under kategorier:")]
        public List<SubCategoryDTO> SubCategories { get; set; }

        [Display(Name = "Valgte under kategori")]
        public SubCategoryDTO SelectedSubCategory { get; set; }
        public SelectList SubCategoriesSelectList
        {
            get
            {
                return new SelectList(SubCategories, "ID", "Name");
            }
        }

        [Display(Name = "Type:")]
        public List<ProductTypeDTO> ProductTypes { get; set; }

        [Display(Name = "Valgte type")]
        public ProductTypeDTO SelectedProductType { get; set; }
        public SelectList ProductTypesSelectList
        {
            get
            {
                return new SelectList(ProductTypes, "ID", "Name");
            }
        }

        public SaleListingDTO SaleListing { get; set; }
    }
}