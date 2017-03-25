using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;

public class GetURL : MonoBehaviour {

    private static TestClass responseMessage = new TestClass();

    // Use this for initialization
    void Start () {
        string url = "http://swishgame.com/AppCode/Test.aspx";
        WebRequest request = WebRequest.Create(url);
        request.Method = "GET";
        request.ContentType = "application/x-www-form-urlencoded";

        WebResponse response = request.GetResponse();
        Stream dataStream = response.GetResponseStream();
        
        StreamReader reader = new StreamReader(dataStream);

        // Read the content.  
        string responseFromServer = reader.ReadToEnd();
        Debug.Log(responseFromServer);
        responseMessage = JsonUtility.FromJson<TestClass>(responseFromServer);
        Debug.Log(responseMessage.message);
        // Display the content.  

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    [Serializable]
    public class TestClass
    {
        public string message;
        public string error;
    }

    public static string RetrieveName()
    {
        return responseMessage.message;
    }
}
