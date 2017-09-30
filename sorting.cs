 public class TableSimpleFilter
    {
        public string Field { get; set; }
        public FilterType CompareType { get; set; }
        public object Value1 { get; set; }
        public object Value2 { get; set; }
    }




public enum FilterType
    {
        DateFrom = 0,
        DateFromIncl = 1,
        DateFor = 2,
        DateForIncl = 3,
        DateIn = 4,
        DateInIncl = 5,
        Contains = 6,
        SameString = 7,
        SameInt = 8,
        TriState = 9,
        Lookup = 10,
        DateTimeFrom = 11,
        DateTimeFromIncl = 12,
        DateTimeFor = 13,
        DateTimeForIncl = 14,
        DateTimeIn = 15,
        DateTimeInIncl = 16
    }


///// Filtering method

private IQueryable<T> FilterQuery(IQueryable<T> query, TableSimpleFilter filter)
        {
            var filterType = filter.CompareType;
            switch (filterType)
            {
                case FilterType.Contains:
                    {
                        query = query.Where(e => e.GetType()
                            .GetProperty(filter.Field, BindingFlags.IgnoreCase |
                                                        BindingFlags.Public |
                                                        BindingFlags.Instance)
                            .GetValue(e, null) != null && e.GetType()
                            .GetProperty(filter.Field, BindingFlags.IgnoreCase |
                                                        BindingFlags.Public |
                                                        BindingFlags.Instance)
                            .GetValue(e, null)
                            .ToString()
                            .Contains(filter.Value1.ToString()));

                        break;
                    }
                case FilterType.DateFor:
                    {
                        if (filter.Value1 != null)
                        {
                            query = query.Where(e => ((DateTime)e.GetType().GetProperty(filter.Field).GetValue(e, null)).Date <
                                                     filter.Value1.ToDate());

                        }
                        break;
                    }
                case FilterType.DateForIncl:
                    {
                        if (filter.Value1 != null)
                        {
                            query = query.Where(e => ((DateTime)e.GetType().GetProperty(filter.Field).GetValue(e, null)).Date <=
                                                     filter.Value1.ToDate());

                        }
                        break;
                    }
                case FilterType.DateFrom:
                    {
                        if (filter.Value1 != null)
                        {
                            query = query.Where(e => ((DateTime)e.GetType().GetProperty(filter.Field).GetValue(e, null)).Date >
                                                     filter.Value1.ToDate());

                        }
                        break;
                    }
                case FilterType.DateFromIncl:
                    {
                        if (filter.Value1 != null)
                        {
                            query = query.Where(e => ((DateTime)e.GetType().GetProperty(filter.Field).GetValue(e, null)).Date >=
                                                     filter.Value1.ToDate());

                        }
                        break;
                    }
                case FilterType.DateIn:
                    {
                        if (filter.Value1 != null)
                        {
                            query = query.Where(
                                e => ((DateTime)e.GetType().GetProperty(filter.Field).GetValue(e, null)).Date >
                                     filter.Value1.ToDate());
                        }
                        if (filter.Value2 != null)
                        {
                            query = query.Where(
                                e => ((DateTime)e.GetType().GetProperty(filter.Field).GetValue(e, null)).Date <
                                     filter.Value2.ToDate());
                        }

                        break;
                    }
                case FilterType.DateInIncl:
                    {
                        if (filter.Value1 != null)
                        {
                            query = query.Where(
                                e => ((DateTime)e.GetType().GetProperty(filter.Field).GetValue(e, null)).Date >=
                                     filter.Value1.ToDate());
                        }
                        if (filter.Value2 != null)
                        {
                            query = query.Where(
                                e => ((DateTime)e.GetType().GetProperty(filter.Field).GetValue(e, null)).Date <=
                                     filter.Value2.ToDate());
                        }
                        break;
                    }
                case FilterType.SameString:
                    {
                        query = query.Where(e => (string)e.GetType().GetProperty(filter.Field).GetValue(e, null) ==
                                                 filter.Value1 as string);
                        break;
                    }
                case FilterType.SameInt:
                    {
                        if (filter.Value1 != null)
                        {
                            var converted = Convert.ToInt32(filter.Value1);
                            query = query.Where(e => (int)e.GetType().GetProperty(filter.Field).GetValue(e, null) ==
                                                     converted);
                        }
                        break;
                    }
                case FilterType.TriState:
                    {
                        if (filter.Value1 != null)
                        {
                            var converted = Convert.ToBoolean(filter.Value1);
                            query = query.Where(e => (bool)e.GetType().GetProperty(filter.Field).GetValue(e, null) ==
                                                     converted);
                        }
                        break;
                    }
                case FilterType.Lookup:
                    {
                        if (filter.Value1 != null)
                        {
                            var converted = Convert.ToInt32(filter.Value1);
                            query = query.Where(e => (int)e.GetType().GetProperty(filter.Field).GetValue(e, null) ==
                                                     converted);
                        }

                        break;
                    }
                default:
                    {
                        query = query.Where(e => e.GetType()
                            .GetProperty(filter.Field)
                            .GetValue(e, null)
                            .ToString()
                            .Contains(filter.Value1.ToString()));
                        // calls Contains
                        break;
                    }
            }
            return query;
        }


///// Example 

public IEnumerable<T> Get(TableSearchSettings settings, params Expression<Func<T, object>>[] includes)
        {
            if (settings != null)
            {
                if (includes == null)
                    includes = new Expression<Func<T, object>>[0];
                var query = includes.Aggregate(_entities.AsNoTracking(), (current, include) => current.Include(include));

                //var query = _entities.AsNoTracking().AsQueryable();

                // I. Filter

                var filters = settings.Filter;
                if (filters != null)
                {
                    if (filters.Any())
                    {
                        query = filters.Aggregate(query, FilterQuery);
                    }
                }

                // II. Sort

                var sort = settings.Sort;
                if (sort != null)
                {
                    if (sort.Any())
                    {
                        foreach (var sorting in sort)
                        {
                            if (sorting.SortDirection == "ASC")
                            {
                                query = query.OrderBy(sorting.Field);
                                //query = query.OrderBy(e => e.GetType().GetProperty(sorting.Field));
                            }
                            if (sorting.SortDirection == "DESC")
                            {
                                query = query.OrderByDescending(sorting.Field);
                                //query = query.OrderByDescending(e => e.GetType().GetProperty(sorting.Field));
                            }
                        }
                    }
                }


                // III. Pagination

                var pagination = settings.Pagination;
                if (pagination != null)
                {
                    if (pagination.Page != -1)
                        query = query.Skip((pagination.Page - 1) * pagination.PerPage);

                    if (pagination.PerPage != -1)
                        query = query.Take(pagination.PerPage);
                }

                return query.ToList();
            }
            else
            {
                return GetAll();
            }
        }