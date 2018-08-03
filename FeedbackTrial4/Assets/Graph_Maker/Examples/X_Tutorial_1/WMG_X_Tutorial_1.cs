using UnityEngine;
using System.Collections;
using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;


public class WMG_X_Tutorial_1 : MonoBehaviour 
    {
    public GameObject emptyGraphPrefab;

    public WMG_Axis_Graph graph;

    public WMG_Series series1;

    

    public bool use8;
    // read Thread
    Thread readThread;

    // udpclient object
    UdpClient client;

    // port number
    public int port = -1;
    public string IPaddressConnect = "";

    // UDP packet store
    public string lastReceivedPacket = "";
    public string AllReceivedText;


    public bool startReceving = false;

    // not start from unity3d
    public void StartReceive()
    {
        if (port == -1 || IPaddressConnect == "")
        {
            Debug.Log("Connection port or IP address not configured");
        }
        startReceving = true;
        // create thread for reading UDP messages
        readThread = new Thread(new ThreadStart(ReceiveData));
        readThread.IsBackground = true;
        readThread.Start();
    }

    // Unity Update Function
    void Update()
    {
        // check button "s" to abort the read-thread
        if (Input.GetKeyDown("q") && startReceving)
            stopThread();
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
        startReceving = false;
        lastReceivedPacket = "";
    }

    // receive thread function
    private void ReceiveData()
    {
        client = new UdpClient(port);
        //client.Client.Blocking = false;
        while (true)
        {
            if (!startReceving)
                break;
            try
            {
                // receive bytes
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Parse(IPaddressConnect), port);
                //IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, port);

                byte[] data = new byte[1024];
                data = client.Receive(ref anyIP);

                string text;
                text = Encoding.UTF8.GetString(data);


                // show received message
                print(">>" + text);
                // store new massage as latest message
                lastReceivedPacket = text;



            }
            catch (Exception err)
            {
                print(err.ToString());
            }
        }
    }

    // return the latest message
    public string getLatestPacket()
    {
        return lastReceivedPacket;
    }

      void Start () {
        StartReceive();
        GameObject graphGO = GameObject.Instantiate(emptyGraphPrefab);
	    graphGO.transform.SetParent(this.transform, false);
	    graph = graphGO.GetComponent<WMG_Axis_Graph>();

		series1 = graph.addSeries();
		graph.xAxis.AxisMaxValue = 20;

	   	}



}
