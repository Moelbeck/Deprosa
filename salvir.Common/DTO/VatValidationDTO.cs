using System;

namespace deprosa.ViewModel
{
    public class VatValidationDTO
    {
        public string VATNumber { get; set; }

        /// <summary>
        /// The country code of the uid to check
        /// </summary>
        /// <remarks>
        /// This parameter can be one of these country codes
        /// 
        /// country --> code to use
        /// ************************************************
        /// Austria --> AT 
        /// Belgium --> BE 
        /// Bulgaria --> BG 
        /// Cyprus --> CY 
        /// Czech Republic --> CZ 
        /// Germany --> DE 
        /// Denmark --> DK 
        /// Estonia EE 
        /// Greece EL 
        /// Spain ES 
        /// Finland FI 
        /// France FR 
        /// United Kingdom GB 
        /// Hungary HU 
        /// Ireland IE 
        /// Italy IT 
        /// Lithuania LT 
        /// Luxembourg LU 
        /// Malta MT 
        /// The Netherlands NL 
        /// Poland PL 
        /// Portugal PT 
        /// Romania RO 
        /// Sweden SE 
        /// Slovenia SI 
        /// Slovakia SK
        /// </remarks>
        public string CountryCode { get; set; }
        public string Address { get; set; }
        public bool IsValid { get; set; }
        public string Name { get; set; }
        public DateTime RetDate { get; set; }
    }
}
