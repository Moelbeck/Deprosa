using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace deprosa.Common
{
    public enum eAccountType
    {
        None=0,
        Administrator=1,
        Owner =2,
        Worker=4
    }
    public enum eRating
    {
       None =0,
       One =1,
       Two = 2,
       Three =3,
       Four = 4,
       Five =5
    }
    public enum eCommentType
    {
        None=0,
        Rating =1,
        SaleListing =2
    }
    public enum eGender
    {
        [Display(Name = "Ukendt")]
        None = 0,
        [Display(Name="Mand")]
        Male=1,
        [Display(Name = "Kvinde")]
        Female = 2
    }
    public enum eImageType
    {
        None=0,
        SaleListing = 1,
        Category=2,
        Advertisement=4,
        CompanyImage=8
    }

    public enum eSubscription
    {
        None=0,
        Basic=1, //Giver en uge som fremhævet annonce
        Medium=2, //Giver en uge som fremhævet annonce, samt to dage i top galleri
        Premium=4, // Giver som Medium, samt top annonce og forside
    }

    public enum eSubscriptionType
    {
        None=0,
        SaleListing=1,
        Account=2
    }

    public enum eLogSaleListingType
    {
        None =0,
        Created =1,
        Update = 2,
        Deleted=4,
        Search=8,
    }

    public enum eLoginType
    {
        None=0,
        Login=1,
        Logout=2,
        Created=4,
        Deleted=8,
        Updated= 16
    }

    [Flags]
    public enum eSalelistingTypes
    {
        None =0,
        Dimensions =1,
        Weight =2,
        Thickness =4,
        Length= 8,
        Processor = 16,
        RAM = 32,
        Harddisk = 64,
        Screen = 128,
        Car = 256,
        PrivateCar = 512,
        CompanyCar = 1024,
    }


    public enum eCountryCode
    {
        None,
        /// Austria --> 
        //[Display(ResourceType = typeof(Resources), Name = "AT")]
        AT, 
        /// Belgium --> 
        BE, 
        /// Bulgaria --> 
        BG, 
        /// Cyprus -->
        CY, 
        /// Czech Republic --> 
        CZ, 
        /// Germany --> 
        DE, 
        /// Denmark --> 
        DK, 
        /// Estonia 
        EE, 
        /// Greece 
        EL, 
        /// Spain 
        ES, 
        /// Finland 
        FI, 
        /// France 
        FR,
        /// United Kingdom 
        GB, 
        /// Hungary 
        HU, 
        /// Ireland 
        IE, 
        /// Italy 
        IT, 
        /// Lithuania 
        LT, 
        /// Luxembourg 
        LU, 
        /// Malta 
        MT, 
        /// The Netherlands 
        NL, 
        /// Poland 
        PL, 
        /// Portugal 
        PT, 
        /// Romania 
        RO, 
        /// Sweden 
        SE, 
        /// Slovenia 
        SI, 
        /// Slovakia 
        SK,
    }

    public enum eCondition
    {
        [Display(Name = "Ukendt")]
        None = 0,
        [Display(Name = "Ny")]
        New = 1,
        [Display(Name = "Som ny")]
        AsNew = 2,
        [Display(Name = "Brugt")]
        Used = 4,
        [Display(Name = "Fungere ikke")]
        NotFunctioning = 8
    }
}
