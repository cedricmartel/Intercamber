using System;
using System.Collections.Generic;

namespace CML.Intercamber.Business.Model
{
    public partial class Threads
    {
    	/* CODE GENERE, NE PAS MODIFIER */
    	/* pour enrichir cette classe, merci de faire une classe partielle (il faut créer ThreadsExtension.cs) */
    	/* Champs de la table */ 
    	public long IdThread { get; set; }
    	public int IdTypeThread { get; set; }
    	public System.DateTime DateCreate { get; set; }
    
        /* Propriétés de navigation */
    	public ICollection<ThreadMessages> ThreadMessages { get; set; }
    		public ICollection<ThreadUsers> ThreadUsers { get; set; }
    	}
}
