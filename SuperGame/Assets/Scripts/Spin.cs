using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Spin : MonoBehaviour {
    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update () {
        // rotate at 90 degrees per second
        transform.Rotate(Vector3.up * Time.deltaTime*90);
        var Client = new UdpClient();
        var RequestData = Encoding.ASCII.GetBytes("Hello there");

        Client.EnableBroadcast = true;
        Client.Send(RequestData, RequestData.Length, new IPEndPoint(IPAddress.Broadcast, 8888));

        Client.Close();
    }
}