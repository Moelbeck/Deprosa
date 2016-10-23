using depross.Common;

namespace depross.ViewModel
{
    public class ImageUploadDTO
    {
        public string FileName { get; set; }

        //public eJobType JobType { get; set; }
        public eImageType ImageType { get; set; }
        public byte[] Content { get; set; }
        
    }
}
