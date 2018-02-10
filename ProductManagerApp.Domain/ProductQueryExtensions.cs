﻿using System.Linq;

namespace ProductManagerApp.Domain
{
    public static class ProductQueryExtensions
    {
        public static IQueryable<Product> WhereFilterIsSatisfied(this IQueryable<Product> query,
                                                                 string name, 
                                                                 double priceFrom, 
                                                                 double priceTo)
        {
            return query.Where(p => (name == "return_all" || p.Name == name)
                                    && p.Price >= priceFrom
                                    && p.Price <= priceTo);
        }
    }
}