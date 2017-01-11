using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

public partial class JSONServices_AddToLeaderboard : System.Web.UI.Page
{

	public struct AddWinStreak
	{
		public int userID;
		public int numWins;
	}
	
	public struct WinStreakResponse
	{
		public int ranking;
		public string error;
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		AddWinStreak request;
		WinStreakResponse response = new WinStreakResponse();
		response.error = String.Empty;

		// Need passed in store id and number of requested results.
		// 1. Deserialize the incoming Json.
		try
		{
			request = GetRequestInfo();
		}
		catch (Exception ex)
		{
			response.error = ex.Message.ToString();

			// Return the results as Json.
			SendInfoAsJson(response);

			return;
		}

		// Do stuff here.
		SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
		try
		{
			connection.Open();

			string sql = String.Format("insert into currentWinStreakBoard (userID,score) VALUES (@val1, @val2)" );
			SqlCommand command = new SqlCommand( sql, connection );
			command.Parameters.AddWithValue("@val1", request.id);
			command.Parameters.AddWithValue("@val2", request.wins);

			command.ExecuteNonQuery();
			

		}
		catch (Exception ex)
		{
			response.error = ex.Message.ToString();
		}
		finally
		{
			if (connection.State == ConnectionState.Open)
			{
				connection.Close();
			}
		}

		SendInfoAsJson(response);
	}

	AddWinStreak GetRequestInfo()
	{
		// Get the Json from the POST.
		string strJson = String.Empty;
		HttpContext context = HttpContext.Current;
		context.Request.InputStream.Position = 0;
		using (StreamReader inputStream = new StreamReader(context.Request.InputStream))
		{
			strJson = inputStream.ReadToEnd();
		}

		// Deserialize the Json.
		AddWinStreak request = JsonConvert.DeserializeObject<AddWinStreak>(strJson);

		return (request);
	}

	void SendInfoAsJson(WinStreakResponse response)
	{
		string strJson = JsonConvert.SerializeObject(response);
		Response.ContentType = "application/json; charset=utf-8";
		Response.Write(strJson);
		Response.End();
	}

}