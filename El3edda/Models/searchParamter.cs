using System.Linq.Expressions;
using El3edda.Data;

namespace El3edda.Models
{
    public class MainSearchParamter
    {
        /// <summary>
        /// search text from the bar 
        /// </summary>
        string text_search { get; set; }
        DateTime releasebefore { get; set; }
        DateTime releaseafter { get; set; }
        ICollection<Manufacturer> manufacturers { get; set; }
        double priceHiegher { get; set; }
        double priceLower { get; set; }
        double warrentyPeriodHiger { get; set; }
        double warrentyPeriodLower { get; set; }
        bool isSold { get; set; }
    }

    public class specSearchParamter
    {
        ICollection<string> CPU { get; set; }
        
    }
}
