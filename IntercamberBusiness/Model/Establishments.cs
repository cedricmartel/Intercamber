using System;
using System.Collections.Generic;

namespace CML.Intercamber.Business.Model
{
    public partial class Establishments
    {
    	/* CODE GENERE, NE PAS MODIFIER */
    	/* pour enrichir cette classe, merci de faire une classe partielle (il faut créer EstablishmentsExtension.cs) */
    	/* Champs de la table */ 
    	public int IdEstablishment { get; set; }
    	public string LblEstablishment { get; set; }
    	public int IdCountry { get; set; }
    	public System.DateTime DateCreate { get; set; }
    	public int TimeZone { get; set; }
    
        /* Propriétés de navigation */
    }
}
