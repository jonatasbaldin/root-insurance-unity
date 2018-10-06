using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Applications : MonoBehaviour {
	public string policyHolderId = "";
	public string quotePackageId = "";
	public string serialNumber = "";

	[Serializable]
	public class Module
	{
		public string type;
		public string make;
		public string model;
		public string serial_number;
	}

	[Serializable]
	public class Application
	{
		public string application_id;
		public string policyholder_id;
		public string package_name;
		public int sum_assured;
		public int monthly_premium;
		public int base_premium;
		public Module module;
		public string created_at;
	}

	public void GetApplication()
	{
		if (string.IsNullOrEmpty(policyHolderId))
		{
			throw new Exception("policyHolderId cannot be empty");
		}
		if (string.IsNullOrEmpty(quotePackageId))
		{
			throw new Exception("quotePackageId cannot be empty");
		}
		if (string.IsNullOrEmpty(serialNumber))
		{
			throw new Exception("serialNumber cannot be empty");
		}

		StartCoroutine(ApplicationIEnumerator());
	}

	IEnumerator ApplicationIEnumerator()
	{
		Auth Auth = new Auth();
		string URL = Auth.RootURL + "/applications";
		string authKey = Auth.GetAuthKey();

		WWWForm form = new WWWForm();
		form.AddField("policyholder_id", policyHolderId);
		form.AddField("quote_package_id", quotePackageId);
		form.AddField("serial_number", serialNumber);

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
				Application application = JsonUtility.FromJson<Application>(www.downloadHandler.text);

				// Accessing the application data
				Debug.Log(application.application_id);
			}
		}
	}
}
