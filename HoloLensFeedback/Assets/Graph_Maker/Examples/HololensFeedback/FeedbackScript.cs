using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class FeedbackScript : MonoBehaviour {
    // graphing variables
    public GameObject emptyGraphPrefab;

    public WMG_Axis_Graph graph;

    public float ShanktoKnee;
    public float ThightoKnee;

    public WMG_Series Series;

    public List<Vector2> SeriesPoints;

    // UDP code
    // to read the thread
    Thread readThread;

    // UDP object
    UdpClient client;

    // port number
    public int port = -1;
    public string IPaddress = "";

    //UDP storage
    public string latestPacket = "";
    public bool Receiving = false;

    public void Receive()
    {
        if(port==-1 || IPaddress=="")
        {
            Debug.Log("Port or IP address is wrong");
        }
        else
        {
            Receiving = true;
            readThread = new Thread(new ThreadStart(Receiver));
            readThread.IsBackground = true;
            readThread.Start();
        }
    }

	// Use this for initialization
	void Start () {


        GameObject feedbackgraph = GameObject.Instantiate(emptyGraphPrefab);
        feedbackgraph.transform.SetParent(this.transform, false);
        graph = feedbackgraph.GetComponent<WMG_Axis_Graph>();



	}

    void OnDisable()
    {
        if (readThread != null)
            readThread.Abort();

        client.Close();
    }

    // Unity Application Quit Function
    void OnApplicationQuit()
    {
        stopThread();
    }

    // Stop reading UDP messages
    public void stopThread()
    {
        if (readThread.IsAlive)
        {
            readThread.Abort();
        }
        client.Close();
        Receiving = false;
        latestPacket = "";
    }

    private void Receiver()
    {
        client = new UdpClient(port);
        while(true)
        {
            if (!Receiving)
                break;
            try
            {
                IPEndPoint IP = new IPEndPoint(IPAddress.Parse(IPaddress), port);
                byte[] data = client.Receive(ref IP);
                string text;

                text = Encoding.UTF8.GetString(data);
                latestPacket = text;
                print(latestPacket);
            }
            catch(Exception err)
            {
                print(err.ToString());
            }
        }
    }

    // Update is called once per frame
    void Update () {

        Series = graph.addSeries();
        Series.pointValues.SetList(SeriesPoints);

    }
}
