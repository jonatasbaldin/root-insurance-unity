using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PolicyHolders : MonoBehaviour {
	private string type = "id";
	public string number = "";
	public string country = "";
	public string firstName = "";
	public string lastName = "";
	public string email = "";

	[Serializable]
	public class Id
	{
		public string type;
		public string number;
		public string country;
	}

	[Serializable]
	public class CreatedBy
	{
		public string type;
		public string id;
		public string owner_id;
	}

	[Serializable]
	public class PolicyHolder
	{
		public string policyholder_id;
		public string type;
		public string first_name;
		public string last_name;
		public Id id;
		public string email;
		public object cellphone;
		public string date_of_birth;
		public string gender;
		public string created_at;
		public object app_data;
		public List<object> policy_ids;
		public CreatedBy created_by;
	}

	public void GetPolicyHolder()
	{
		if (string.IsNullOrEmpty(number))
		{
			throw new Exception("number cannot be empty");
		}
		if (string.IsNullOrEmpty(country))
		{
			throw new Exception("country cannot be empty");
		}
		if (string.IsNullOrEmpty(firstName))
		{
			throw new Exception("firstName cannot be empty");
		}
		if (string.IsNullOrEmpty(lastName))
		{
			throw new Exception("lastName cannot be empty");
		}
		if (string.IsNullOrEmpty(email))
		{
			throw new Exception("email cannot be empty");
		}

		StartCoroutine(PolicyHolderIEnumerator());
	}

	IEnumerator PolicyHolderIEnumerator()
	{
		Auth Auth = new Auth();
		string URL = Auth.RootURL + "/policyholders";
		string authKey = Auth.GetAuthKey();

		WWWForm form = new WWWForm();
		form.AddField("id", "{\"type\":\"" + type + "\",\"number\":\"" + number + "\",\"country\":\"" + country + "\"}");
		form.AddField("first_name", firstName);
		form.AddField("last_name", lastName);
		form.AddField("email", email);

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
				PolicyHolder policyHolder = JsonUtility.FromJson<PolicyHolder>(www.downloadHandler.text);

				// Accessing the policyHolder data
				Debug.Log(policyHolder.policyholder_id);
			}
		}

	}

}
