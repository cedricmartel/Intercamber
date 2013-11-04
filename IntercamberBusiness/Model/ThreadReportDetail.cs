using System;

namespace CML.Intercamber.Business.Model
{
    public class ThreadReportDetail
    {
        public bool Connected;
        public long IdThread;
        public long IdUser;
        public string NameUser;
        public DateTime? DateStart;
        public DateTime? DateLastMessageSent;
        public DateTime? DateLastMessageReceived;
        public long NumberMessagesReceived;
        public long NumberMessagesSent;

        public string StrDateStart
        {
            get
            {
                if (DateStart == null || DateStart == DateTime.MinValue)
                    return null;
                return DateStart.Value.ToString("dd/MM/yyyy HH:mm:ss").Replace(" ", "<br/>");
            }
        }
        public string StrDateLastMessageSent
        {
            get
            {
                if (DateLastMessageSent == null || DateLastMessageSent == DateTime.MinValue)
                    return null;
                return DateLastMessageSent.Value.ToString("dd/MM/yyyy HH:mm:ss").Replace(" ", "<br/>");
            }
        }
        public string StrDateLastMessageReceived
        {
            get
            {
                if (DateLastMessageReceived == null || DateLastMessageReceived == DateTime.MinValue)
                    return null;
                return DateLastMessageReceived.Value.ToString("dd/MM/yyyy HH:mm:ss").Replace(" ", "<br/>");
            }
        }
        
    }
}
