using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;


public class UDPReceiver : MonoBehaviour {
    Thread receiveThread;
    UdpClient client;

    public int port;

    public string lastUDPPacket = "";
    public string allUDPPackets = "";

    private static void Main()
    {
        UDPReceiver receiveObj = new UDPReceiver();
        receiveObj.init();

        string text = "";
        do
        {
            text = Console.ReadLine();
        } while (!text.Equals("exit"));
    }

    private void init()
    {
        throw new NotImplementedException();
    }

    // Use this for initialization
    public void Start () {
        init();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
