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
    
    public partial class ContactRequests
    {
        public long IdContactRequest { get; set; }
        public long IdUserRequester { get; set; }
        public long IdUserAsk { get; set; }
        public System.DateTime DateAsk { get; set; }
        public Nullable<System.DateTime> DateValidated { get; set; }
        public Nullable<System.DateTime> DateDismissed { get; set; }
        public string Message { get; set; }
    
        public virtual Users Users { get; set; }
        public virtual Users Users1 { get; set; }
    }
}
