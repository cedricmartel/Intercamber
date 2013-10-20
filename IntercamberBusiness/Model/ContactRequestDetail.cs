namespace CML.Intercamber.Business.Model
{
    public class ContactRequestDetail
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SpokenLanguages { get; set; }

        public string NameFormated
        {
            get { return FirstName + " " + LastName; }
        }
    }
}
