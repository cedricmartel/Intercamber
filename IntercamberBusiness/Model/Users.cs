using System;
using System.Collections.Generic;

namespace CML.Intercamber.Business.Model
{
    public partial class Users
    {
    	/* CODE GENERE, NE PAS MODIFIER */
    	/* pour enrichir cette classe, merci de faire une classe partielle (il faut créer UsersExtension.cs) */
    	/* Champs de la table */ 
    	public long IdUser { get; set; }
    	public Nullable<int> IdProfil { get; set; }
    	public Nullable<int> IdEstablishment { get; set; }
    	public string Email { get; set; }
    	public string Password { get; set; }
    	public bool MustChangePassword { get; set; }
    	public Nullable<int> IdGender { get; set; }
    	public string FirstName { get; set; }
    	public string LastName { get; set; }
    	public Nullable<System.DateTime> BirthDate { get; set; }
    	public bool Enabled { get; set; }
    	public System.DateTime DateCreate { get; set; }
    
        /* Propriétés de navigation */
    	public ICollection<Contacts> Contacts { get; set; }
    		public ICollection<Contacts> Contacts1 { get; set; }
    		public ICollection<ThreadUsers> ThreadUsers { get; set; }
    	}
}
