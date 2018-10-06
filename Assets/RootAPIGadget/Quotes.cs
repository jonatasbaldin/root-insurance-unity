using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Quotes : MonoBehaviour {
	public string gadget = "";

	[Serializable]
	public class Result
	{
		public Quote[] quotes;
	}

	[Serializable]
	public class Quote
	{
		public string quote_package_id;
		public string package_name;
		public int sum_assured;
		public int base_premium;
		public int suggested_premium;
		public string created_at;
		public QuoteModule module;
	}

	[Serializable]
	public class QuoteModule
	{
		public string type;
		public string make;
		public string model;
	}

	public void GetQuote()
	{
		if (string.IsNullOrEmpty(gadget))
		{
			throw new Exception("gadget cannot be empty");
		}

		StartCoroutine(QuoteIEnumerator());
	}

	IEnumerator QuoteIEnumerator()
	{
		Auth Auth = new Auth();
		string URL = Auth.RootURL + "/quotes";
		string authKey = Auth.GetAuthKey();

		WWWForm form = new WWWForm();
		form.AddField("type", "root_gadgets");
		form.AddField("model_name", gadget);

		UnityWebRequest www = UnityWebRequest.Post(URL, form);
		www.SetRequestHeader("AUTHORIZATION", authKey);
		yield return www.SendWebRequest();

		if (www.isNetworkError || www.isHttpError)
		{
			Debug.Log("API request failed");
			Debug.Log(www.error);
			Debug.Log(www.downloadHandler.text);
		} else
		{
			if (www.isDone)
			{
				Debug.Log("API request successful");
				Result result = JsonUtility.FromJson<Result>("{\"quotes\":" + www.downloadHandler.text + "}");

				// Reading each Quote
				foreach (var quote in result.quotes)
				{
					// You may access each quote property, e.g:
					// quote.quote_package_id
					Debug.Log(JsonUtility.ToJson(quote));
				}
			}
		}
	}
}