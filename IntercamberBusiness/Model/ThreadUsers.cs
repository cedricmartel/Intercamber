using System;
using System.Collections.Generic;

namespace CML.Intercamber.Business.Model
{
    public partial class ThreadUsers
    {
    	/* CODE GENERE, NE PAS MODIFIER */
    	/* pour enrichir cette classe, merci de faire une classe partielle (il faut créer ThreadUsersExtension.cs) */
    	/* Champs de la table */ 
    	public long IdThread { get; set; }
    	public long IdUser { get; set; }
    	public System.DateTime DateCreate { get; set; }
    	public Nullable<System.DateTime> DateLastSeen { get; set; }
    
        /* Propriétés de navigation */
    	public Threads Threads { get; set; }
    		public Users Users { get; set; }
    	}
}
