
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEditor.VersionControl;
//using UnityEngine.JSONSerializeModule;


public class UDPReceive : MonoBehaviour
{
    public string IP = "10.162.34.69";
    public int port = 48000; // define > init

    // receiving Thread
    Thread receiveThread;

    // udpclient object
    UdpClient client;

    // infos
    public string lastReceivedUDPPacket = "";


    private Dictionary<string, GameObject> nameObjectDict = new Dictionary<string, GameObject>();
    public float scale = 1.0f;

    
    //public GameObject ToolLink;
    public GameObject BaseLink;
    public GameObject Camera;
    public GameObject YawLink;
    public GameObject PitchEndLink;
    public GameObject MainInsertion;
    public GameObject ToolRollLink;
    public GameObject ToolYawLink;
    public GameObject ToolPitchLink;
    public GameObject ToolGripper1Link;
    public GameObject ToolGripper2Link;
    public GameObject Phantom;

    public GameObject BaseLink2;
    public GameObject YawLink2;
    public GameObject PitchEndLink2;
    public GameObject MainInsertion2;
    public GameObject ToolRollLink2;
    public GameObject ToolYawLink2;
    public GameObject ToolPitchLink2;
    public GameObject ToolGripper1Link2;
    public GameObject ToolGripper2Link2;



    // start from unity3d
    public void Start()
    {
        InitDictionary();


        client = new UdpClient(port);
        IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), port);

        string message = "test";
        byte[] messageData = Encoding.UTF8.GetBytes(message);
        client.Send(messageData, messageData.Length, remoteEndPoint);

        // = new Thread(
        //            new ThreadStart(ReceiveData));
        //receiveThread.IsBackground = true;
        //receiveThread.Start();
    }

    public void Update()
    {
        ReceiveNext();
    }

    // receive thread
    private void ReceiveData()
    {        
        while (true)
        {
            ReceiveNext();
        }
    }

    public string getLatestUDPPacket()
    {
        return lastReceivedUDPPacket;
    }

    void InitDictionary()
    {
        //nameObjectDict.Add("/ambf/env/psm1/toolgripper1link", ToolLink);
        nameObjectDict.Add("/ambf/env/cameras/cameraL", Camera);
        nameObjectDict.Add("/ambf/env/psm1/baselink", BaseLink);
        nameObjectDict.Add("/ambf/env/psm1/yawlink", YawLink);
        nameObjectDict.Add("/ambf/env/psm1/pitchendlink", PitchEndLink);
        nameObjectDict.Add("/ambf/env/psm1/maininsertionlink", MainInsertion);
        nameObjectDict.Add("/ambf/env/psm1/toolrolllink", ToolRollLink);
        nameObjectDict.Add("/ambf/env/psm1/toolpitchlink", ToolPitchLink);
        nameObjectDict.Add("/ambf/env/psm1/toolyawlink", YawLink);
        nameObjectDict.Add("/ambf/env/psm1/toolgripper1link", ToolGripper1Link);
        nameObjectDict.Add("/ambf/env/psm1/toolgripper2link", ToolGripper2Link);
        nameObjectDict.Add("/ambf/env/Phantom", Phantom);

        nameObjectDict.Add("/ambf/env/psm2/baselink", BaseLink2);
        nameObjectDict.Add("/ambf/env/psm2/yawlink", YawLink2);
        nameObjectDict.Add("/ambf/env/psm2/pitchendlink", PitchEndLink2);
        nameObjectDict.Add("/ambf/env/psm2/maininsertionlink", MainInsertion2);
        nameObjectDict.Add("/ambf/env/psm2/toolrolllink", ToolRollLink2);
        nameObjectDict.Add("/ambf/env/psm2/toolpitchlink", ToolPitchLink2);
        nameObjectDict.Add("/ambf/env/psm2/toolyawlink", YawLink2);
        nameObjectDict.Add("/ambf/env/psm2/toolgripper1link", ToolGripper1Link2);
        nameObjectDict.Add("/ambf/env/psm2/toolgripper2link", ToolGripper2Link2);


    }

    void ReceiveNext()
    {
        try
        {
            // IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
            IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
            byte[] data = client.Receive(ref anyIP);
            string text = Encoding.UTF8.GetString(data);
            //Debug.Log("Server: " + text);
            lastReceivedUDPPacket = text;
            ReceivedPose r = JsonUtility.FromJson<ReceivedPose>(text);

            if (nameObjectDict.ContainsKey(r.name))
            {
                Debug.Log(r.name);
                Debug.Log(r.pose.position.x);
                GameObject objectToMove = nameObjectDict[r.name];
                objectToMove.transform.position = new Vector3(r.pose.position.x * scale, r.pose.position.y * scale, r.pose.position.z * scale);
                objectToMove.transform.rotation = new Quaternion(r.pose.orientation.x, r.pose.orientation.y, r.pose.orientation.z, r.pose.orientation.w);

            }
        }
        catch (Exception err)
        {
            print(err.ToString());
        }
    }
}

