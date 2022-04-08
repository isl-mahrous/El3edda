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
        [CompareTo("priceLower",ComparisonType.GreaterThanOrEqual)]
        public double? priceHigher { get; set; } //MaxPrice
        [Range(0, double.MaxValue)]
        public double? priceLower { get; set; } //MinPrice
        [Range(0, double.MaxValue)]
        [CompareTo("warrentyPeriodLower",ComparisonType.GreaterThanOrEqual)]
        public double? warrentyPeriodHigher { get; set; } //WarrantyTo
        [Range(0, double.MaxValue)]
        public double? warrentyPeriodLower { get; set; } ////WarrantyFrom
        public bool? InStock { get; set; }
    }

    
    public class specSearchParamter : MainSearchParamter
    {
        public int id { get; set; }
        [NotMapped]
        public ICollection<string> CPUs { get; set; }
        [NotMapped]
        public ICollection<ScreenEnum> Screens { get; set; }
        [Range(0, double.MaxValue)]
        public double? HeightLower { get; set; }
        [Range(0, double.MaxValue)]
        [CompareTo("HeightLower",ComparisonType.GreaterThanOrEqual)]
        public double? HeightHigher { get; set; }
        [Range(0, double.MaxValue)]
        public double? WidthLower { get; set; }
        [Range(0, double.MaxValue)]
        [CompareTo("WidthLower",ComparisonType.GreaterThanOrEqual)]
        public double? WidthHigher { get; set; }
        [Range(0, double.MaxValue)]
        public double? ThicknessLower { get; set; }
        [Range(0, double.MaxValue)]
        [CompareTo("ThicknessLower",ComparisonType.GreaterThanOrEqual)]
        public double? ThicknessHigher { get; set; }
        [Range(0, double.MaxValue)]
        public double? CameraMegaPixelsLower { get; set; }
        [Range(0, double.MaxValue)]
        [CompareTo("CameraMegaPixelsLower",ComparisonType.GreaterThanOrEqual)]
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
        public int? BatteryCapacityLower { get; set; }
        [Range(0, int.MaxValue)]
        [CompareTo("BatteryCapacityLower",ComparisonType.GreaterThanOrEqual)]
        public int? BatteryCapacityHigher { get; set; }

        [Range(0, int.MaxValue)]
        public int? RAMLower { get; set; }
        [Range(0, int.MaxValue)]
        [CompareToAttribute("ROMLower",ComparisonType.GreaterThanOrEqual)]
        public int? RAMHigher { get; set; }
        [Range(0, int.MaxValue)]
        public int? ROMLower { get; set; }
        [Range(0, int.MaxValue)]
        [CompareTo("ROMLower",ComparisonType.GreaterThanOrEqual)]
        public int? ROMHigher { get; set; }


    }
}
