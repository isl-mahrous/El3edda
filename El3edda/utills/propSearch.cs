﻿using System.Linq.Expressions;
using El3edda.Data.Enums;
using El3edda.Models;

namespace El3edda.utills
{
    public class PropSearch
    {
        public PropSearch() { _predicate = PredicateBuilder.True<Mobile>(); }

        protected Expression<Func<Mobile, bool>> _predicate { get; set; }

        public Expression<Func<Mobile, bool>> searchPredicate => _predicate;

        public virtual void andCondition(Expression<Func<Mobile, bool>> new_predicate)
        {
            _predicate = PredicateBuilder.And(_predicate, new_predicate);
        }

        public PropSearch StringSearch(string value)
        {
            andCondition(m => m.Name.ToLower().Contains(value.ToLower()));
            //TODO check the desription and all other fields
            return this;
        }
        public PropSearch PriceLowerSearch(double target)
        {
            andCondition(m => m.Price < target);
            return this;
        }
        public PropSearch PriceHiegherSearch(double target)
        {
            andCondition(m => m.Price > target);
            return this;
        }
        public PropSearch ReleaseBeforeSearch(DateTime target)
        {
            andCondition(m => m.ReleaseDate < target);
            return this;
        }
        public PropSearch ReleaseAfterSearch(DateTime target)
        {
            andCondition(m => m.ReleaseDate > target);
            return this;
        }
        public PropSearch ManufacturerSearch(ICollection<int> target)
        {
            andCondition(m => target.Contains(m.Manufacturer.Id));
            return this;
        }
        public PropSearch InStockSearch(bool isInStock)
        {
            // isInStock              unitsinstock == 0
            // true(instock)       true                  false
            // true(instock)       false                 true
            // false               true                  true
            // false               false                 false
            andCondition(m => isInStock ^ m.UnitsInStock > 0);
            return this;
        }
        public PropSearch CPUSearch(ICollection<string> target)
        {
            andCondition(m => target.Contains(m.Specs.CPU));
            return this;
        }

        public PropSearch ScreenSearch(ICollection<ScreenEnum> target)
        {
            andCondition(m => target.Contains(m.Specs.Screen));
            return this;
        }
        public PropSearch HeightLowerSearch(double target)
        {
            andCondition(m => m.Specs.Height < target);
            return this;
        }
        public PropSearch HeightHigerSearch(double target)
        {
            andCondition(m => m.Specs.Height > target);
            return this;
        }
        public PropSearch WidthLowerSearch(double target)
        {
            andCondition(m => m.Specs.Width < target);
            return this;
        }
        public PropSearch WidthHigerSearch(double target)
        {
            andCondition(m => m.Specs.Width > target);
            return this;
        }
        public PropSearch ThicknessLowerSearch(double target)
        {
            andCondition(m => m.Specs.Thickness < target);
            return this;
        }
        public PropSearch ThicknessHigerSearch(double target)
        {
            andCondition(m => m.Specs.Thickness > target);
            return this;
        }
        public PropSearch CameraMegaPixelsLowerSearch(double target)
        {
            andCondition(m => m.Specs.CameraMegaPixels < target);
            return this;
        }
        public PropSearch CameraMegaPixelsHigerSearch(double target)
        {
            andCondition(m => m.Specs.CameraMegaPixels > target);
            return this;
        }
        public PropSearch ColorSearch(ICollection<Colors> target)
        {
            andCondition(m => target.Contains(m.Specs.Color));
            return this;
        }
        public PropSearch WeightLowerSearch(double target)
        {
            andCondition(m => m.Specs.Weight < target);
            return this;
        }
        public PropSearch WeightHigerSearch(double target)
        {
            andCondition(m => m.Specs.Weight > target);
            return this;
        }
        public PropSearch OSSearch(ICollection<OSEnum> target)
        {
            andCondition(m => target.Contains(m.Specs.OS));
            return this;
        }
        //TODO add ram and rom to specs
        // public PropSearch RAMLowerSearch(double target)
        // {
        //     andCondition(m => m.Specs.RAM < target);
        //     return this;
        // }
        // public PropSearch RAMHigerSearch(double target)
        // {
        //     andCondition(m => m.Specs.RAM > target);
        //     return this;
        // }
        // public PropSearch StorageLowerSearch(double target)
        // {
        //     andCondition(m => m.Specs.ROM < target);
        //     return this;
        // }
        // public PropSearch StorageHigerSearch(double target)
        // {
        //     andCondition(m => m.Specs.ROM > target);
        //     return this;
        // }
        public PropSearch BatteryCapacityLowerSearch(double target)
        {
            andCondition(m => m.Specs.BatteryCapacity < target);
            return this;
        }
        public PropSearch BatteryCapacityHigerSearch(double target)
        {
            andCondition(m => m.Specs.BatteryCapacity > target);
            return this;
        }
        
        bool valid(string s){
            return s != null && s.Length > 0;
        }
        bool valid(double? d){
            return d > 0 && d != null;
        }
        bool valid(int? i){
            return i > 0 && i != null;
        }
        bool valid(DateTime? d){
            return d!=null && d !=DateTime.MinValue;
        }
        bool valid<T>(ICollection<T> c){
            return c != null && c.Count > 0;
        }

        public PropSearch(specSearchParamter searParamters)
        {
            _predicate = PredicateBuilder.True<Mobile>();
            if (valid(searParamters.text_search))
                this.StringSearch(searParamters.text_search);
            if (valid(searParamters.priceLower))
                this.PriceLowerSearch((double)searParamters.priceLower);
            if (valid(searParamters.priceHiegher))
                this.PriceHiegherSearch((double)searParamters.priceHiegher);
            if (valid(searParamters.releaseafter))
                this.ReleaseAfterSearch((DateTime)searParamters.releaseafter);
            if (valid(searParamters.releasebefore))
                this.ReleaseBeforeSearch((DateTime)searParamters.releasebefore);
            if (valid(searParamters.manufacturerids))
                this.ManufacturerSearch(searParamters.manufacturerids);
            if (searParamters.InStock != null)
                this.InStockSearch((bool)searParamters.InStock);
            if (valid(searParamters.CPUs))
                this.CPUSearch(searParamters.CPUs);
            if (valid(searParamters.Screens))
                this.ScreenSearch(searParamters.Screens);
            if (valid(searParamters.HeightLower))
                this.HeightLowerSearch((double)searParamters.HeightLower);
            if (valid(searParamters.HeightHiger))
                this.HeightHigerSearch((double)searParamters.HeightHiger);
            if (valid(searParamters.WidthLower))
                this.WidthLowerSearch((double)searParamters.WidthLower);
            if (valid(searParamters.WidthHiger))
                this.WidthHigerSearch((double)searParamters.WidthHiger);
            if (valid(searParamters.ThicknessLower))
                this.ThicknessLowerSearch((double)searParamters.ThicknessLower);
            if (valid(searParamters.ThicknessHiger))
                this.ThicknessHigerSearch((double)searParamters.ThicknessHiger);
            if (valid(searParamters.CameraMegaPixelsLower))
                this.CameraMegaPixelsLowerSearch((double)searParamters.CameraMegaPixelsLower);
            if (valid(searParamters.CameraMegaPixelsHiger))
                this.CameraMegaPixelsHigerSearch((double)searParamters.CameraMegaPixelsHiger);
            if (valid(searParamters.Colors))
                this.ColorSearch(searParamters.Colors);
            if (valid(searParamters.WeightLower))
                this.WeightLowerSearch((double)searParamters.WeightLower);
            if (valid(searParamters.WeightHiger))
                this.WeightHigerSearch((double)searParamters.WeightHiger);
            if (valid(searParamters.OS))
                this.OSSearch(searParamters.OS);
            // if(validDouble(searParamters.RAMLower))
            //     this.RAMLowerSearch(searParamters.RAMLower);
            // if(validDouble(searParamters.RAMHiger))
            //     this.RAMHigerSearch(searParamters.RAMHiger);
            // if(validDouble(searParamters.StorageLower))
            //     this.StorageLowerSearch(searParamters.StorageLower);
            // if(validDouble(searParamters.StorageHiger))
            //     this.StorageHigerSearch(searParamters.StorageHiger);
            if (valid((double?)searParamters.BatteryCapacityLower))
                this.BatteryCapacityLowerSearch((double)searParamters.BatteryCapacityLower);
            if (valid((double?)searParamters.BatteryCapacityHiger))
                this.BatteryCapacityHigerSearch((double)searParamters.BatteryCapacityHiger);


        }
    }


}
