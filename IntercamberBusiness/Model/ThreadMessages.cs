using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace CML.Intercamber.Business.Model
{
    public partial class ThreadMessages
    {
    	/* CODE GENERE, NE PAS MODIFIER */
    	/* pour enrichir cette classe, merci de faire une classe partielle (il faut créer ThreadMessagesExtension.cs) */
    	/* Champs de la table */ 
    	public long IdMessage { get; set; }
    	public long IdThread { get; set; }
    	public long IdUser { get; set; }
    	public System.DateTime DateMessage { get; set; }
    	public string Message { get; set; }
    	public string MessageCorrection { get; set; }
    
        /* Propriétés de navigation */
    	public ArrayList Threads { get; set; }
    	}
}
