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
    
    public partial class Contacts
    {
        public long IdUser { get; set; }
        public long IdUserContact { get; set; }
        public System.DateTime DateAdd { get; set; }
    
        public virtual Users Users { get; set; }
        public virtual Users Users1 { get; set; }
    }
}
