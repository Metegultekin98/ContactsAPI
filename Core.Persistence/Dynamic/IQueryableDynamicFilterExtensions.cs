using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Core.Persistence.Dynamic;

public static class IQueryableDynamicFilterExtensions
{
	private static readonly string[] _orders = { "asc", "desc" };
	private static readonly string[] _logics = { "and", "or" };

	private static readonly IDictionary<string, string> _operators = new Dictionary<string, string>
	{
		{ "eq", "=" },
		{ "neq", "!=" },
		{ "lt", "<" },
		{ "lte", "<=" },
		{ "gt", ">" },
		{ "gte", ">=" },
		{ "isnull", "== null" },
		{ "isnotnull", "!= null" },
		{ "startswith", "StartsWith" },
		{ "endswith", "EndsWith" },
		{ "contains", "Contains" },
		{ "doesnotcontain", "Contains" }
	};

	public static IQueryable<T> ToDynamic<T>(this IQueryable<T> query, DynamicQuery dynamicQuery)
	{
		if (dynamicQuery.Filter is not null)
			query = Filter(query, dynamicQuery.Filter);
		if (dynamicQuery.Sort is not null && dynamicQuery.Sort.Any())
			query = Sort(query, dynamicQuery.Sort);
		return query;
	}

	private static IQueryable<T> Filter<T>(IQueryable<T> queryable, Filter filter)
	{
		IList<Filter> filters = GetAllFilters(filter);
		string?[] values = filters.Select(f => f.Value).ToArray();
		string where = Transform(filter, filters);
		if (!string.IsNullOrEmpty(where) && values != null)
			queryable = queryable.Where(where, values);

		return queryable;
	}

	private static IQueryable<T> Sort<T>(IQueryable<T> queryable, IEnumerable<Sort> sort)
	{
		foreach (Sort item in sort)
		{
			if (string.IsNullOrEmpty(item.Field))
				throw new ArgumentException("Invalid Field");
			if (string.IsNullOrEmpty(item.Dir) || !_orders.Contains(item.Dir))
				throw new ArgumentException("Invalid Order Type");
		}

		if (sort.Any())
		{
			string ordering = string.Join(separator: ",", values: sort.Select(s => $"{s.Field} {s.Dir}"));
			return queryable.OrderBy(ordering);
		}

		return queryable;
	}

	public static IList<Filter> GetAllFilters(Filter filter)
	{
		List<Filter> filters = [];
		GetFilters(filter, filters);
		return filters;
	}

	private static void GetFilters(Filter filter, IList<Filter> filters)
	{
		filters.Add(filter);
		if (filter.Filters is not null && filter.Filters.Any())
			foreach (Filter item in filter.Filters)
				GetFilters(item, filters);
	}

	public static string Transform(Filter filter, IList<Filter> filters)
	{
		if (filter == null)
			throw new ArgumentNullException(nameof(filter));

		StringBuilder where = new();

		if (!string.IsNullOrEmpty(filter.Field))
		{
			if (string.IsNullOrEmpty(filter.Operator) || !_operators.ContainsKey(filter.Operator))
				throw new ArgumentException("Invalid Operator");

			int index = filters.IndexOf(filter);
			string comparison = _operators[filter.Operator];

			if (!string.IsNullOrEmpty(filter.Value))
			{
				string valueExpression = $"@{index}.ToLower()";
				string fieldExpression = $"np({filter.Field}).ToLower()";

				if (comparison is "StartsWith" or "EndsWith" or "Contains")
					where.Append($"({fieldExpression}.{comparison}({valueExpression}))");
				else if (filter.Operator == "doesnotcontain")
					where.Append($"(!{fieldExpression}.{comparison}({valueExpression}))");
				else
					where.Append($"np({filter.Field}) {comparison} @{index}");
			}
			else if (filter.Operator is "isnull" or "isnotnull")
			{
				where.Append($"np({filter.Field}) {comparison}");
			}
		}

		if (filter.Filters != null && filter.Filters.Any())
		{
			if (string.IsNullOrEmpty(filter.Logic) || !_logics.Contains(filter.Logic))
				throw new ArgumentException("Invalid Logic");

			var subFilters = filter.Filters.Select(f => Transform(f, filters)).Where(sub => !string.IsNullOrEmpty(sub)).ToArray();
			string joinedSubFilters = string.Join($" {filter.Logic} ", subFilters);

			if (where.Length > 0)
			{
				where.Append($" {filter.Logic} ({joinedSubFilters})");
			}
			else
			{
				where.Append($"({joinedSubFilters})");
			}
		}

		return where.ToString();
	}
}