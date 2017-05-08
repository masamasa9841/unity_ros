using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using WebSocketSharp.Net;

public class websocket : MonoBehaviour {

    // Use this for 
    WebSocket ws;
    double linear = 0.0;
    double angular = 0.0;

    void Start () {
        
        ws = new WebSocket("ws://192.168.22.210:9090/");

        ws.OnOpen += (sender, e) =>
        {
            Debug.Log("WebSocket Open");

            string msg = "{\"op\": \"advertise\"," +
             "\"topic\": \"/mobile_base/commands/velocity\"," +
             "\"type\": \"geometry_msgs/Twist\"}";

            ws.Send(msg);
        };

        ws.OnError += (sender, e) =>
        {
            Debug.Log("WebSocket Error Message: " + e.Message);
        };

        ws.OnClose += (sender, e) =>
        {
            Debug.Log("WebSocket Close");
        };

        ws.Connect();

    }
	
	// Update is called once per frame
	void Update () {
        string msg2 = "{\"op\": \"publish\"," +
                       "\"topic\": \"/mobile_base/commands/velocity\"," +
                       "\"msg\": {" +
                                  "\"linear\"  : { \"x\" : " + linear + ", \"y\" : 0, \"z\" : 0 }," +
                                  "\"angular\" : { \"x\" : 0, \"y\" : 0, \"z\" : " + angular + " } " +
                                 "}" +
                       "}";
        ws.Send(msg2);
    }
    public void Foward()
    { // MUST public
        linear = 0.2;
        angular = 0;
    }

    public void Back()
    { // MUST public
        linear = -0.2;
        angular = 0;
    }

    public void Left()
    { // MUST public
        angular = 0.1;
    }

    public void Right()
    { // MUST public
        angular = -2;
    }


}
