//------------------------------------------------------------------------------
// <auto-generated>
//    Ce code a été généré à partir d'un modèle.
//
//    Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//    Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CML.Intercamber.Business
{
    using System;
    using System.Collections.Generic;
    
    public partial class Languages
    {
        public Languages()
        {
            this.UsersLearnLanguages = new HashSet<UsersLearnLanguages>();
            this.UsersSpokenLanguages = new HashSet<UsersSpokenLanguages>();
        }
    
        public string IdLanguage { get; set; }
    
        public virtual ICollection<UsersLearnLanguages> UsersLearnLanguages { get; set; }
        public virtual ICollection<UsersSpokenLanguages> UsersSpokenLanguages { get; set; }
    }
}
