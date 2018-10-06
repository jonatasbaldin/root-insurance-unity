using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Policies : MonoBehaviour {
	public string applicationId = "";

	[Serializable]
	public class CreatedBy
	{
		public string type;
		public string id { get; set; }
		public string owner_id { get; set; }
	}

	[Serializable]
	public class Module
	{
		public string make;
		public string type;
		public string model;
		public string serial_number;
	}

	[Serializable]
	public class Policy
	{
		public string policy_id;
		public string scheme_type;
		public string created_at;
		public CreatedBy created_by;
		public string policy_number;
		public string policyholder_id;
		public string package_name;
		public int sum_assured;
		public int base_premium;
		public int monthly_premium;
		public int billing_amount;
		public string billing_frequency;
		public int billing_day;
		public string start_date;
		public string end_date;
		public object reason_cancelled;
		public object app_data;
		public Module module;
		public List<object> beneficiaries;
		public List<object> schedule_versions;
		public object current_version;
		public string terms_uri;
		public string policy_schedule_uri;
		public List<object> claim_ids;
		public List<object> complaint_ids;
		public string status;
	}

	public void GetPolicy()
	{
		if (string.IsNullOrEmpty(applicationId))
		{
			throw new Exception("applicationId cannot be empty");
		}

		StartCoroutine(PolicyIEnumerator());
	}

	IEnumerator PolicyIEnumerator()
	{
		Auth Auth = new Auth();
		string URL = Auth.RootURL + "/policies";
		string authKey = Auth.GetAuthKey();

		WWWForm form = new WWWForm();
		form.AddField("application_id", applicationId);

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
				Policy policy = JsonUtility.FromJson<Policy>(www.downloadHandler.text);

				// Accessing the policy data
				Debug.Log(policy.policy_id);
			}
		}
	}

}
