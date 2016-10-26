using deprosa.ViewModel;

namespace deprosa.Common
{
    public class ImageUploadRequest
    {
        public SaleListingDTO SaleListing { get; set; }

        public ImageUploadDTO Image { get; set; }
    }
}
