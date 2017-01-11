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

public partial class JSONServices_Login : System.Web.UI.Page
{

	public struct LoginRequest
	{
		public string uname, pw;
	}
	
	public struct LoginResponse
	{
		public int id;
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
		try
		{
			connection.Open();

			string sql = String.Format("SELECT * FROM userInfo WHERE username=@un and password=@pw", request.uname, request.pw );
			SqlCommand command = new SqlCommand( sql, connection );
			command.Parameters.Add(new SqlParameter("@un", request.uname));
			command.Parameters.Add(new SqlParameter("@pw", request.pw));
			SqlDataReader reader = command.ExecuteReader();
			if( reader.Read() )
			{
				response.id = Convert.ToInt32( reader["userID"] );
			}
			else
			{
				response.error = "No matching record";
			}
			reader.Close();
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