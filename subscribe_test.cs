using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

[System.Serializable]
public class RosData
{
	public string op;
	public string topic;
}

public class subscribe_test : MonoBehaviour {
	WebSocket ws;

	// Use this for initialization
	void Start () {
		ws = new WebSocket("ws://192.168.22.173:9090/");
		//接続完了
		ws.OnOpen += (sender, e) =>
		{
			Debug.Log("WebSocket Open");

			RosData data = new RosData ();
			data.op = "subscribe";
			data.topic = "/camera/rgb/image_raw";
			string json = JsonUtility.ToJson(data);
			ws.Send(json);
		};

		ws.OnError += (sender, e) =>
		{
			Debug.Log("WebSocket Error Message: " + e.Message);
		};

		ws.OnClose += (sender, e) =>
		{
			Debug.Log("WebSocket Close");
			RosData data = new RosData ();
			data.op = "unsubscribe";
			data.topic = "/camera/rgb/image_raw";
			string json = JsonUtility.ToJson(data);
			ws.Send(json);
		};

		ws.Connect();
		
		
	}
	
	// Update is called once per frame
	void Update () {
		//受信したとき
		ws.OnMessage += (sender, e) => {
			string message = e.Data;
		};
		ws.OnClose += (sender, e) =>
		{
			Debug.Log("WebSocket Close");
			RosData data = new RosData ();
			data.op = "unsubscribe";
			data.topic = "/camera/rgb/image_raw";
			string json = JsonUtility.ToJson(data);
			ws.Send(json);
		};
	}
}
