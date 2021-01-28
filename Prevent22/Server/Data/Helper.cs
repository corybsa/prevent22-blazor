using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Linq;
using Dapper;
using Prevent22.Server.Data;
using Prevent22.Shared;
using System.Text;
using Telerik.Blazor.Components;
using Telerik.DataSource;

namespace Prevent22.Server
{
	public class Helper
	{
		private readonly SqlConfiguration _sql;

		// default empty contructor
		public Helper() { }

		public Helper(SqlConfiguration sql)
		{
			_sql = sql;
		}

		public async Task<DbResponse<T>> ExecStoredProcedure<T>(string proc, DynamicParameters parameters = null)
		{
			var response = new DbResponse<T>();


			if (_sql == null)
			{
				response.Success = false;
				response.Info = "SqlConfiguration not found. Did you forget to pass SqlConfiguration to Helper?";
				return response;
			}

			using (var conn = new SqlConnection(_sql.Value))
			{
				if (conn.State == ConnectionState.Closed)
				{
					// open connection
					conn.Open();
				}

				try
				{
					// execute stored procedure
					response.Success = true;
					response.Data = (await conn.QueryAsync<T>(proc, parameters, commandType: CommandType.StoredProcedure)).ToList();
					Console.WriteLine(GetStatement(proc, parameters, null));
				}
				catch (SqlException e)
				{
					// get exec
					string exec = GetStatement(proc, parameters, e);
					response.Success = false;
					response.Info = exec;

					throw new Exception(e.Message);
				}
				catch (Exception e)
				{
					response.Success = false;
					response.Info = $"Unknown error: {e.Message}";

					throw new Exception(response.Info);
				}
				finally
				{
					if (conn.State == ConnectionState.Open)
					{
						// close connection
						conn.Close();
					}
				}
			}

			return response;
		}

		private string GetStatement(string proc, DynamicParameters parameters, SqlException e = null)
		{
			var sb = new StringBuilder();

			if (e != null)
			{
				sb.AppendLine(e.Message);
			}

			sb.Append($"EXEC {proc}");

			if (parameters != null)
			{
				foreach (var name in parameters.ParameterNames)
				{
					if (parameters.Get<dynamic>(name) == null)
					{
						sb.Append(" null,");
					}
					else
					{
						sb.Append(" '").Append(parameters.Get<dynamic>(name)).Append("',");
					}
				}
			}

			return sb.Remove(sb.Length - 1, 1).ToString();
		}

		public DbResponse<T> SortGridData<T>(DbResponse<T> response, GridReadEventArgs args)
		{
			try
			{
				var sortedData = new List<T>(response.Data);

				foreach (SortDescriptor sorts in args.Request.Sorts)
				{
					if (sorts.SortDirection == ListSortDirection.Ascending)
					{
						response.Data = response.Data.OrderBy(x => x.GetType().GetProperty(sorts.Member).GetValue(x)).ToList();
					}
					else
					{
						response.Data = response.Data.OrderByDescending(x => x.GetType().GetProperty(sorts.Member).GetValue(x)).ToList();
					}
				}

				return response;
			}
			catch
			{
				return response;
			}
		}

		public DbResponse<T> FilterGridData<T>(DbResponse<T> response, GridReadEventArgs args)
		{
			Console.WriteLine(args.Request.Filters.Count);
			try
			{
				bool filteredOnce = false;
				List<T> filteredData = new List<T>(response.Data);

				foreach (CompositeFilterDescriptor filters in args.Request.Filters)
				{
					filters.FilterDescriptors.AsList().ForEach(x =>
					{
						FilterDescriptor filter = (FilterDescriptor)x;
						Console.WriteLine(filter.ConvertedValue);

						if (!filteredOnce) // first iteration
						{
							filteredData = response.Data.FindAll(u => FilterObject(u, filter));
						}
						else // second iteration
						{
							if (filters.LogicalOperator == FilterCompositionLogicalOperator.And)
							{
								filteredData = filteredData.FindAll(u => FilterObject(u, filter));
							}
							else
							{
								filteredData.AddRange(response.Data.FindAll(u => FilterObject(u, filter)));
							}
						}

						filteredOnce = true;
					});
				}

				response.DataTotalCount = filteredData.Count;
				response.Data = filteredData
					.Skip(args.Request.PageSize * (args.Request.Page - 1))
					.Take(args.Request.PageSize)
					.ToList();

				return response;
			}
			catch
			{
				return response;
			}
		}

		private bool FilterObject<T>(T obj, FilterDescriptor filter)
		{
			try
			{
				var val = obj.GetType().GetProperty(filter.Member).GetValue(obj);

				switch (val)
				{
					case string s:
						return FilterString(s.ToLower(), filter);
					case int i:
						return FilterInt(i, filter);
					case bool b:
						return FilterBool(b, filter);
					default:
						return false;
				}
			}
			catch
			{
				return false;
			}
		}

		private bool FilterString(string s, FilterDescriptor filter)
		{
			try
			{
				string filterVal = filter.Value.ToString().ToLower();

				switch (filter.Operator)
				{
					case FilterOperator.IsEqualTo:
						return s.Equals(filterVal);
					case FilterOperator.IsNotEqualTo:
						return !(s.Equals(filterVal));
					case FilterOperator.StartsWith:
						return s.StartsWith(filterVal);
					case FilterOperator.Contains:
						return s.Contains(filterVal);
					case FilterOperator.DoesNotContain:
						return !(s.Contains(filterVal));
					case FilterOperator.EndsWith:
						return s.EndsWith(filterVal);
					case FilterOperator.IsNull:
						return s == null;
					case FilterOperator.IsNotNull:
						return s != null;
					case FilterOperator.IsEmpty:
						return string.IsNullOrEmpty(s);
					case FilterOperator.IsNotEmpty:
						return !(string.IsNullOrEmpty(s));
					default:
						return false;
				}
			}
			catch
			{
				return false;
			}
		}

		private bool FilterInt(int? i, FilterDescriptor filter)
		{
			try
			{
				int? filterVal = (int?)filter.Value;

				switch (filter.Operator)
				{
					case FilterOperator.IsLessThan:
						return i < filterVal;
					case FilterOperator.IsLessThanOrEqualTo:
						return i <= filterVal;
					case FilterOperator.IsEqualTo:
						return i == filterVal;
					case FilterOperator.IsNotEqualTo:
						return i != filterVal;
					case FilterOperator.IsGreaterThanOrEqualTo:
						return i >= filterVal;
					case FilterOperator.IsGreaterThan:
						return i > filterVal;
					case FilterOperator.IsNull:
						return i == null;
					case FilterOperator.IsNotNull:
						return i != null;
					default:
						return false;
				}
			}
			catch
			{
				return false;
			}
		}

		private bool FilterBool(bool b, FilterDescriptor filter)
		{
			try
			{
				bool filterVal = (bool)filter.Value;
				return b == filterVal; ;
			}
			catch
			{
				return false;
			}
		}
	}
}
