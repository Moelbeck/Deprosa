using deprosa.ViewModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace deprosa.Web.Data.Model.ViewModel
{
    public class CategoryViewModel
    {
        [Display(Name = "Kategorier:")]
        public List<MainCategoryDTO> MainCategories { get; set; }

        [Display(Name = "Valgte kategori")]
        public int SelectedMainCategoryId { get; set; }
        public SelectList MainCategoriesSelectList
        {
            get
            {
                var selectedmainid = SelectedMainCategoryId != null ? SelectedMainCategoryId : MainCategories.First().ID;
                return new SelectList(MainCategories, "ID", "Name", selectedmainid);
            }
        }

        public List<SubCategoryDTO> SubCategories { get; set; }
        [Display(Name = "Under kategorier:")]
        public List<SubCategoryDTO> CurrentSubCategories { get; set; }

        [Display(Name = "Valgte under kategori")]
        public int SelectedSubCategoryId { get; set; }
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
        public int SelectedProductTypeId { get; set; }
        public SelectList ProductTypesSelectList
        {
            get
            {
                return new SelectList(CurrentProductTypes, "ID", "Name");
            }
        }
    }
}