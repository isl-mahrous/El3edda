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
        public string text_search { get; set; }
        [DataType(DataType.Date)]
        public DateTime? releasebefore { get; set; }
        [DataType(DataType.Date)]
        public DateTime? releaseafter { get; set; }
        public ICollection<int>? manufacturerids { get; set; }
        [Range(0, double.MaxValue)]
        public double? priceHigher { get; set; }
        [Range(0, double.MaxValue)]
        public double? priceLower { get; set; }
        [Range(0, double.MaxValue)]
        public double? warrentyPeriodHigher { get; set; }
        [Range(0, double.MaxValue)]
        public double? warrentyPeriodLower { get; set; }
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
        public double? HeightHigher { get; set; }
        [Range(0, double.MaxValue)]
        public double? WidthLower { get; set; }
        [Range(0, double.MaxValue)]
        public double? WidthHigher { get; set; }
        [Range(0, double.MaxValue)]
        public double? ThicknessLower { get; set; }
        [Range(0, double.MaxValue)]
        public double? ThicknessHigher { get; set; }
        [Range(0, double.MaxValue)]
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
        public int? BatteryCapacityLower { get; set; }
        [Range(0, int.MaxValue)]
        [CompareTo("BatteryCapacityLower",ComparisonType.GreaterThanOrEqual)]
        public int? BatteryCapacityHigher { get; set; }

        [Compare("RAMHiger")]
        [Range(0, int.MaxValue)]
        public int? RAMLower { get; set; }
        [Range(0, int.MaxValue)]
        [CompareToAttribute("ROMLower",ComparisonType.GreaterThanOrEqual)]
        public int? RAMHigher { get; set; }
        [Range(0, int.MaxValue)]
        public int? ROMLower { get; set; }
        [Range(0, int.MaxValue)]
        public int? ROMHigher { get; set; }


    }
}
