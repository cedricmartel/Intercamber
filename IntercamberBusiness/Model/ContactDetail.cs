namespace CML.Intercamber.Business.Model
{
    public class ContactDetail
    {
        public long IdUser { get; set;  }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long NumUnreadMessages { get; set; }

        public string NameFormated
        {
            get { return FirstName + " " + LastName;  }
        }
    }
}
