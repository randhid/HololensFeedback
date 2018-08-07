using UnityEngine;
using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;


public class WMG_X_Tutorial_1 : MonoBehaviour 
    {

  //  JavaScriptSerializer serializer;
    public GameObject emptyGraphPrefab;

    public WMG_Axis_Graph graph;

    public WMG_Series series1;

    public float shanklength;
    public float thighlength;

    public float origin_x;
    public float origin_y;

    // read Thread
    Thread readThread;

    // udpclient object
    UdpClient client;

    // port number
    public int port = -1;
    public string IPaddressConnect = "";

    // UDP packet store
    public string lastReceivedPacket = "";

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
        while (true)
        {
            if (!startReceving)
                break;
            try
            {
                // receive bytes
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Parse(IPaddressConnect), port);
                byte[] data = client.Receive(ref anyIP);

                double hipflexion_r = BitConverter.ToDouble(data, 0);
                double hipabduction_r = BitConverter.ToDouble(data, 8);
                double hiprotation_r = BitConverter.ToDouble(data, 16);
                double kneeflexion_r = BitConverter.ToDouble(data, 24);

                double hipflexion_d = hipflexion_r * (180 / Math.PI);
                double hipabduction_d = hipabduction_r * (180 / Math.PI);
                double hiprotation_d = hiprotation_r * (180 / Math.PI);
                double kneeflexion_d = kneeflexion_r * (180 / Math.PI);

                double theta = 180 - hipflexion_d;
                double alpha = kneeflexion_d - theta;

                float xcoord_1 = origin_x + (thighlength * (float)Math.Cos(theta));
                float ycoord_1 = origin_y - (thighlength * (float)Math.Sin(theta));
                float xcoord_2 = xcoord_1 - (shanklength * (float)Math.Cos(alpha));
                float ycoord_2 = ycoord_1 - (shanklength * (float)Math.Sin(alpha));

                Vector2 A = new Vector2(origin_x, origin_y);
                Vector2 B = new Vector2(xcoord_1, ycoord_1);
                Vector2 C = new Vector2(xcoord_2, ycoord_2);

                List<Vector2> Jdata = new List<Vector2>();

                print(hipflexion_d);
                print(hipabduction_d);
                print(kneeflexion_d);

                //Jdata.Add(A);
                //Jdata.Add(B);
                //Jdata.Add(C);
                //series1.pointValues.SetList(Jdata);



            }
            catch (Exception err)
            {
                //print(err.ToString());
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
