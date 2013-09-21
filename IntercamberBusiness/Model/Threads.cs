using System;
using System.Collections;
using System.Linq;
using System.Text;

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
    	public ArrayList ThreadMessages { get; set; }
    		public ArrayList ThreadUsers { get; set; }
    	}
}
