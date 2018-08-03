using UnityEngine;
using System.Collections.Generic;
using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class WMG_X_Tutorial_1 : MonoBehaviour
{
  
    //public bool use8;

    // to read thread
    Thread readThread;
    UdpClient _Socket;
    public string allreceivedpackets = "";

    //public int port = -1;
    //public string IPaddress = "";
    public string lastpacket = "";
 /*   public bool startReceiving = false;

    public void StartReceive()
    {   
       // if (port == -1 || IPaddress == "")
        //{
          //  Debug.Log("connection port or IP address is not configured");
        //}
       // print(IPaddress);
        startReceiving = true;

    }

*/
    private void ReceiveData()
    {
        readThread = new Thread(new ThreadStart(delegate
        {
            try
            {
                _Socket = new UdpClient(61557);
                Debug.LogFormat("Receiving on port {0}", 61557);
            }
            catch (Exception err)
            {
                Debug.LogError(err.ToString());
                return;
            }
            //byte[] data;
            //client = new UdpClient(port);
            while (true)
            {
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 61557);

                try
                {
                    print("A");
                    //IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse(IPaddress), port);

                    var data = _Socket.Receive(ref anyIP);
                    print(data);
                    string text;
                    text = Encoding.UTF8.GetString(data);
                    print(">> " + text);
                    lastpacket = text;
                    allreceivedpackets = allreceivedpackets + text;
                }
                catch (Exception err)
                {
                    print(err.ToString());
                }
            }
        }));
        
        readThread.IsBackground = true;
        readThread.Start();
    }

    public string getlatestPacket()
    {
        return lastpacket;
    }
 
    public GameObject emptyGraphPrefab;
	public WMG_Axis_Graph graph;

    public float shanktoknee;
    public float thightoknee;

	public WMG_Series series1;

	public List<Vector2> series1Data;

	// Use this for initialization
	void Start () {
        print("A");
        ReceiveData();
        GameObject graphGO = GameObject.Instantiate(emptyGraphPrefab);
		graphGO.transform.SetParent(this.transform, false);
		graph = graphGO.GetComponent<WMG_Axis_Graph>();
        series1 = graph.addSeries();
		graph.xAxis.AxisMaxValue = 40;
        series1.pointValues.SetList(series1Data);

    }



}
