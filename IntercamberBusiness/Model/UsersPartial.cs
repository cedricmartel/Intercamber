namespace CML.Intercamber.Business.Model
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
