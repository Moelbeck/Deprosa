using deprosa.ViewModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using deprosa.Common;

namespace deprosa.Web.Data.Model.ViewModel
{
    public class CategoryViewModel
    {
        [Display(Name = "Kategorier:")]
        public List<MainCategoryDTO> MainCategories { get; set; } = new List<MainCategoryDTO>();

        [Display(Name = "Valgte kategori")]
        public int SelectedMainCategoryId { get; set; }

        public string SelectedMainCategoryName
        {
            get
            {
                if (SelectedMainCategoryId > 0)
                {
                    return MainCategories.FirstOrDefault(e => e.ID == SelectedMainCategoryId)?.Name;
                }
                return null;
            }
        }

        public SelectList MainCategoriesSelectList
        {
            get
            {
                var selectedmainid = SelectedMainCategoryId;
                return new SelectList(MainCategories, "ID", "Name", selectedmainid);
            }
        }

        public List<SubCategoryDTO> SubCategories { get; set; } = new List<SubCategoryDTO>();
        [Display(Name = "Under kategorier:")]
        public List<SubCategoryDTO> CurrentSubCategories { get; set; }

        [Display(Name = "Valgte under kategori")]
        public int SelectedSubCategoryId { get; set; }
        public SelectList SubCategoriesSelectList => new SelectList(CurrentSubCategories, "ID", "Name");

        public List<ProductTypeDTO> ProductTypes { get; set; } = new List<ProductTypeDTO>();
        [Display(Name = "Type:")]
        public List<ProductTypeDTO> CurrentProductTypes { get; set; }

        [Display(Name = "Valgte type")]
        public ProductTypeDTO SelectedProductType { get; set; }
        public int SelectedProductTypeId { get; set; }
        public SelectList ProductTypesSelectList => new SelectList(CurrentProductTypes, "ID", "Name");

        public void SetCategoryStructure(CategoryStructureRequest categorystructure)
        {
            MainCategories = categorystructure.MainCategories;
            SubCategories = categorystructure.SubCategories;
            ProductTypes = categorystructure.ProductTypes;
            SelectedMainCategoryId = 0;
            SelectedSubCategoryId = 0;
            SelectedProductTypeId = 0;
        }

        public void SetCategoryStructure(CategoryViewModel categorystructure)
        {
            MainCategories = categorystructure.MainCategories;
            SubCategories = categorystructure.SubCategories;
            ProductTypes = categorystructure.ProductTypes;
            CurrentSubCategories = categorystructure.CurrentSubCategories;
            CurrentProductTypes = categorystructure.CurrentProductTypes;
            SelectedMainCategoryId = categorystructure.SelectedMainCategoryId;
            SelectedSubCategoryId = categorystructure.SelectedSubCategoryId;
            SelectedProductTypeId = categorystructure.SelectedProductTypeId;
        }
    }
}