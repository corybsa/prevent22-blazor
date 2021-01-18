using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Linq;
using Dapper;
using Raketti.Server.Data;
using Raketti.Shared;
using System.Text;

namespace Raketti.Server
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
				}
				catch (SqlException e)
				{
					// get exec
					string exec = GetStatement(proc, parameters, e);
					response.Success = false;
					response.Info = exec;
					
					// print exec
					Console.WriteLine(exec);
				}
				catch (Exception e)
				{
					response.Success = false;
					response.Info = $"Unknown error: {e.Message}";
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
	}
}
