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
    
    public partial class UserActivities
    {
        public long IdUserActivity { get; set; }
        public long IdUser { get; set; }
        public System.DateTime DateActivity { get; set; }
        public string ActivityMessage { get; set; }
    
        public virtual Users Users { get; set; }
    }
}
