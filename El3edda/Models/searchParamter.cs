using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using El3edda.Data;
using El3edda.Data.Enums;
using El3edda.utills;
using Microsoft.EntityFrameworkCore;

namespace El3edda.Models
{
    public class MainSearchParamter
    {
        /// <summary>
        /// search text from the bar 
        /// </summary>
        /// 
        // TODO (Low Priority)
        //Naming Convention
        public string text_search { get; set; }
        [DataType(DataType.Date)]
        public DateTime? releasebefore { get; set; } //ReleasedBefore
        [DataType(DataType.Date)]
        public DateTime? releaseafter { get; set; } //ReleasedAfter
        public ICollection<int>? manufacturerids { get; set; }
        [Range(0, double.MaxValue)]
        
        public double? priceHigher { get; set; } //MaxPrice
        [Range(0, double.MaxValue)]
        [CompareTo("priceHigher", ComparisonType.GreaterThanOrEqual)]
        public double? priceLower { get; set; } //MinPrice
        [Range(0, double.MaxValue)]
        
        public double? warrentyPeriodHigher { get; set; } //WarrantyTo
        [Range(0, double.MaxValue)]
        [CompareTo("warrentyPeriodHigher", ComparisonType.GreaterThanOrEqual)]
        public double? warrentyPeriodLower { get; set; } ////WarrantyFrom
        public bool InStock { get; set; }
        
    }

    
    public class specSearchParamter : MainSearchParamter
    {
        public int id { get; set; }
        [NotMapped]
        public ICollection<string> CPUs { get; set; }
        [NotMapped]
        public ICollection<ScreenEnum> Screens { get; set; }
        [Range(0, double.MaxValue)]
        [CompareTo("HeightHigher", ComparisonType.GreaterThanOrEqual)]
        public double? HeightLower { get; set; }
        [Range(0, double.MaxValue)]
        
        public double? HeightHigher { get; set; }
        [Range(0, double.MaxValue)]
        [CompareTo("WidthHigher", ComparisonType.GreaterThanOrEqual)]
        public double? WidthLower { get; set; }
        [Range(0, double.MaxValue)]
        
        public double? WidthHigher { get; set; }
        [Range(0, double.MaxValue)]
        [CompareTo("ThicknessHigher", ComparisonType.GreaterThanOrEqual)]
        public double? ThicknessLower { get; set; }
        [Range(0, double.MaxValue)]
        
        public double? ThicknessHigher { get; set; }
        [Range(0, double.MaxValue)]
        [CompareTo("CameraMegaPixelsHigher", ComparisonType.GreaterThanOrEqual)]
        public double? CameraMegaPixelsLower { get; set; }
        [Range(0, double.MaxValue)]
        
        public double? CameraMegaPixelsHigher { get; set; }
        [NotMapped]
        public ICollection<Colors> Colors { get; set; }
        [Range(0, double.MaxValue)]
        public double? WeightLower { get; set; }
        [Range(0, double.MaxValue)]
        public double? WeightHiger { get; set; }
        [NotMapped]
        public ICollection<OSEnum> OS { get; set; }
        [Range(0, int.MaxValue)]
        [CompareTo("BatteryCapacityHigher", ComparisonType.GreaterThanOrEqual)]
        public int? BatteryCapacityLower { get; set; }
        [Range(0, int.MaxValue)]
        
        public int? BatteryCapacityHigher { get; set; }

        [Range(0, int.MaxValue)]
        [CompareToAttribute("RAMHigher", ComparisonType.GreaterThanOrEqual)]
        public int? RAMLower { get; set; }
        [Range(0, int.MaxValue)]
        public int? RAMHigher { get; set; }
        [Range(0, int.MaxValue)]
        [CompareTo("ROMHigher", ComparisonType.GreaterThanOrEqual)]
        public int? ROMLower { get; set; }
        [Range(0, int.MaxValue)]

        public int? ROMHigher { get; set; }

    }
}
