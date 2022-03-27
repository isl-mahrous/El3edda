using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using El3edda.Data;
using El3edda.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace El3edda.Models
{
    public class MainSearchParamter
    {
        /// <summary>
        /// search text from the bar 
        /// </summary>
        public string text_search { get; set; }
        public DateTime? releasebefore { get; set; }
        public DateTime? releaseafter { get; set; }
        public ICollection<int>? manufacturerids { get; set; }
        [Range(0, double.MaxValue)]
        public double? priceHiegher { get; set; }
        [Range(0, double.MaxValue)]
        public double? priceLower { get; set; }
        [Range(0, double.MaxValue)]
        public double? warrentyPeriodHiger { get; set; }
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
        public double? HeightHiger { get; set; }
        [Range(0, double.MaxValue)]
        public double? WidthLower { get; set; }
        [Range(0, double.MaxValue)]
        public double? WidthHiger { get; set; }
        [Range(0, double.MaxValue)]
        public double? ThicknessLower { get; set; }
        [Range(0, double.MaxValue)]
        public double? ThicknessHiger { get; set; }
        [Range(0, double.MaxValue)]
        public double? CameraMegaPixelsLower { get; set; }
        [Range(0, double.MaxValue)]
        public double? CameraMegaPixelsHiger { get; set; }
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
        public int? BatteryCapacityHiger { get; set; }

        [Range(0, int.MaxValue)]
        public int? RAMLower { get; set; }
        [Range(0, int.MaxValue)]
        public int? RAMHiger { get; set; }
        [Range(0, int.MaxValue)]
        public int? ROMLower { get; set; }
        [Range(0, int.MaxValue)]
        public int? ROMHiger { get; set; }


    }
}
