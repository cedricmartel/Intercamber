using System;
using CML.Intercamber.Business.Helper;
using CML.Intercamber.Web;
using CML.Intercamber.Web.Helpers;

namespace CML.Intercamber.Business.Model
{
    /// <summary>
    /// exchange class for contact search
    /// </summary>
    public class UsersDetail
    {
        public long IdUser { get; set; }
        public int? IdGender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string IdCountry { get; set; }
        public string City { get; set; }
        public string SpokenLanguages { get; set; }
        public string RequestMessage { get; set; }
        public long NumUnreadMessages { get; set; }

        public string Name
        {
            get { return FirstName + " " + LastName; }
        }

        public string Age
        {
            get
            {
                if (BirthDate == null)
                    return "?";
                return DateTimeHelper.Age(BirthDate.Value).ToString();
            }
        }

        public bool Connected
        {
            get
            {
                // TODO virer la dependance vers chathub & signalr
                return ChatHub.ListUserConnected.Contains(IdUser);
            }
        }

        public string PresentationText
        {
            get
            {
                return (string.IsNullOrEmpty(RequestMessage) ? "" : "Contact request: " + RequestMessage + "<br/>") + 
                    "Presentation text writen by " + Name + "<br>His photo will also be visible here";
            }
        }

        public string Location
        {
            get
            {
                // TODO virer le dependance vers les resources 
                if (string.IsNullOrEmpty(City))
                    return ResourcesHelper.GetString("Countries_" + IdCountry);
                return City + " (" + ResourcesHelper.GetString("Countries_" + IdCountry) + ")";
            }
        }
    }
}
