using System;
using System.Collections;
using System.Linq;
using System.Text;

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
    	public ArrayList Users { get; set; }
    		public ArrayList Users1 { get; set; }
    	}
}
