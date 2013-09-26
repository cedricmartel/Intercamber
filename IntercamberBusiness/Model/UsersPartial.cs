namespace CML.Intercamber.Business
{
    public partial class Users
    {
        public string UserNameComplete
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }
}
