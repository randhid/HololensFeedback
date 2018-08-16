using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UDPResponse : MonoBehaviour
{


    public TextMesh tm = null;

    public void ResponseToUDPPacket(string incomingIP, string incomingPort, byte[] data)
    {

        if (tm != null)
        {
            tm.text = System.Text.Encoding.UTF8.GetString(data);
            print(tm.text);
        }

#if !UNITY_EDITOR

        //ECHO 
        UDPCommunication comm = UDPCommunication.Instance;
        comm.SendUDPMessage(incomingIP, comm.externalPort, data);

#endif
    }
}