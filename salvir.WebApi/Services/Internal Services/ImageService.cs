using depross.Common;
using depross.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace depross.Service
{
    public class ImageService
    {

        private const string Imagefolder = "/Images/";


        /// <summary>
        /// Creates a folder, if it doesnt exists, else it returns the name.
        /// ATTENTION!!
        ///     We need to place it in the correct place.
        /// ATTENTION!!
        /// </summary>
        /// <param name="imagetype"></param>
        /// <returns></returns>
        public string GetFolder(eImageType imagetype)
        {
            var folderpath = Path.Combine(Imagefolder, Enum.GetName(typeof(eImageType), imagetype));
            string directorypath;
            if (!Directory.Exists(folderpath))
            {
                Directory.CreateDirectory(folderpath);
            }

            directorypath = Path.GetDirectoryName(folderpath);            
            return directorypath;
        }

        public bool ValidateExtension(string extension)
        {
            extension = extension.ToLower();
            switch (extension)
            {
                case ".jpg":
                case ".png":
                case ".jpeg":
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Saves the image, and returns the path for the image.
        /// If return value is null, something went wrong.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public string SaveImageToFolder(ImageUploadDTO imageviewmodel)
        {
            if (ValidateExtension(Path.GetExtension(imageviewmodel.FileName)))
            {
                string imagename = GenerateImageName(imageviewmodel.FileName);
                string imagePathTemp = GetFolder(imageviewmodel.ImageType);
                var imagePathFinal = Path.Combine(imagePathTemp, imagename);

                using (FileStream fs = File.Create(imagePathFinal))
                {
                    for (byte i = 0; i < imageviewmodel.Content.Length; i++)
                    {
                        fs.WriteByte(i);
                    }
                }
                return imagePathFinal;
            }
            return null;
        }

        //public void SaveImageToDatabase(ImageUploadDTO imageviewmodel)
        //{
        //    if (ValidateExtension(Path.GetExtension(imageviewmodel.FileName)){

        //    }
        //}

        /// <summary>
        /// Returns true if it is removed successfully
        /// Returns false if its either not found, or not removed.
        /// </summary>
        /// <param name="imageurl"></param>
        /// <returns></returns>
        public bool RemoveImageFromFolder(string imageurl)
        {
            bool isRemoved = false;
            FileInfo file = new FileInfo(imageurl);
            if (file.Exists)
            {
                file.Delete();
                if (!file.Exists)
                {
                    isRemoved = true;
                }
            }
            return isRemoved;
        }
        public string GenerateImageName(string imagename)
        {
            var guidpart = Guid.NewGuid();
           var newimagename = imagename + guidpart;
            return newimagename;
        }
    }
}
