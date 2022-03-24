using System.Linq.Expressions;
using El3edda.Models;

namespace El3edda.utills
{
    public interface IPropSearch
    {
        Expression<Func<Mobile, bool>>  searchPredicate {get;}
    }

    public class PropSearch : IPropSearch
    {
        public PropSearch() { _predicate = PredicateBuilder.True<Mobile>(); }
        public PropSearch(IPropSearch propsearch)
        {
            _propsearch = propsearch;
            _predicate = _propsearch.searchPredicate;
        }
        protected Expression<Func<Mobile, bool>> _predicate { get; set; }

        Expression<Func<Mobile, bool>>  IPropSearch.searchPredicate => _predicate;

        protected IPropSearch _propsearch;
        public virtual void addStringSearch(Expression<Func<Mobile, bool>> new_predicate)
        {
            _predicate = PredicateBuilder.And(_predicate, new_predicate);
        }
        public virtual void addDoubleLowerFilter(Expression<Func<Mobile, bool>> new_predicate)
        {
            _predicate = PredicateBuilder.And(_predicate, new_predicate);
        }
    }

    public class StringSearch : PropSearch
    {
        public StringSearch(IPropSearch propSearch, string value) : base(propSearch)
        {
            addStringSearch(m => m.Name.ToLower().Contains(value.ToLower()));
        }
    }
    public class PriceLowerSearch : PropSearch
    {   
        public PriceLowerSearch(IPropSearch propSearch, double target) : base(propSearch)
        {
            addDoubleLowerFilter(m => m.Price < target);
        }
    }
    public class PriceHiegherSearch : PropSearch
    {
        public PriceHiegherSearch(IPropSearch propSearch, double target) : base(propSearch)
        {
            addDoubleLowerFilter(m => m.Price > target);
        }
    }
}
