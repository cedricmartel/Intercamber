using System;
using System.Collections.Generic;

namespace CML.Intercamber.Business.Model
{
    public partial class Contacts
    {
    	/* CODE GENERE, NE PAS MODIFIER */
    	/* pour enrichir cette classe, merci de faire une classe partielle (il faut créer ContactsExtension.cs) */
    	/* Champs de la table */ 
    	public long IdUser { get; set; }
    	public long IdUserContact { get; set; }
    	public System.DateTime DateAdd { get; set; }
    
        /* Propriétés de navigation */
    	public Users Users { get; set; }
    		public Users Users1 { get; set; }
    	}
}
