using System.Collections.Generic;

namespace CML.Intercamber.Business.Model
{
    public class GridData<T>
    {

        /// <summary>
        /// total pages of query 
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// number of requested page
        /// </summary>
        public int page { get; set; }
        
        /// <summary>
        /// total records from query
        /// </summary>
        public int records { get; set; }
        
        public List<T> rows { get; set; }
    }
}
