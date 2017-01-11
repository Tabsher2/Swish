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

public partial class JSONServices_HomePage : System.Web.UI.Page
{

	public struct LoginRequest
	{
		public int id;
		public int score;
	}
	
	public struct LoginResponse
	{
		public int id;
		public int score;
		public string error;
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		LoginRequest request;
		LoginResponse response = new LoginResponse();
		response.error = String.Empty;

		// Need passed in store id and number of requested results.
		// 1. Deserialize the incoming Json.
		try
		{
			request = GetRequestInfo();
//request.uname = "Rick";
//request.pw = "test";
		}
		catch (Exception ex)
		{
			response.error = ex.Message.ToString();

			// Return the results as Json.
			SendInfoAsJson(response);

			return;
		}
		
		// Done deserializing...

		// Do stuff here.
		SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
		private DataTable leaderboard = new DataTable();
		try
		{
			

			string sql = String.Format("SELECT * FROM currentWinStreakBoard" );
			SqlDataAdapter adapter = new SqlDataAdapter(new SqlCommand(sql, connection));
			connection.Open();
			adapter.Fill(leaderboard);
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

	LoginRequest GetRequestInfo()
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
		LoginRequest request = JsonConvert.DeserializeObject<LoginRequest>(strJson);

		return (request);
	}

	void SendInfoAsJson(LoginResponse response)
	{
		string strJson = JsonConvert.SerializeObject(response);
		Response.ContentType = "application/json; charset=utf-8";
		Response.Write(strJson);
		Response.End();
	}

}