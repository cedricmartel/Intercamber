using System;

namespace CML.Intercamber.Business.Model
{
    /// <summary>
    /// exchange class for contact search
    /// </summary>
    public class UsersDetail
    {
        public long IdUser { get; set; }
        public Nullable<int> IdGender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string IdCountry { get; set; }
        public string City { get; set; }
    }
}
