using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;


public class Server : MonoBehaviour
{
    private bool isServerStarted;
    private byte reliableChannel;
    private int hostID;
    private int WebHostID;
    private const int maxUser=100;
    private const int port= 26000;
    private const int webPort= 26001;
    private const int Byte_Size =1024;
    private byte error;
    // Start is called before the first frame update
    #region Monobehaviour
    void Start()

    {
        DontDestroyOnLoad(gameObject);
        Init();
        
    }
    #endregion
    public void Init()
    {
        NetworkTransport.Init();
        ConnectionConfig cc = new ConnectionConfig();
        reliableChannel =  cc.AddChannel(QosType.Reliable);
        HostTopology topo = new HostTopology(cc,maxUser);

        //server only
        hostID = NetworkTransport.AddHost(topo,port,null);
        WebHostID = NetworkTransport.AddHost(topo,webPort,null);
        Debug.Log(string.Format("Openning connection on port {0} and webport{1}",port,webPort));

        isServerStarted =true;
    }
    public void ShutDown()
    {
        isServerStarted =false;
        NetworkTransport.Shutdown();
    }
    private void Update() {
        UpdateMessagePump();
    }
    public void UpdateMessagePump()
    {
        if(!isServerStarted)
        {
            return;
        }
        else
        {
            int recoHostID; //is this from web or standalone?
            int connectionID; //which user is sending me this?
            int channelID; //which lane is sending that message from;

            byte[] recBuffer = new byte[Byte_Size];
            int datasize;

            NetworkEventType type = NetworkTransport.Receive(out recoHostID, out connectionID, out channelID, recBuffer,Byte_Size,out datasize,out error);
            switch(type)
            {
                case NetworkEventType.Nothing:
                break;

                case NetworkEventType.ConnectEvent:
                Debug.Log(string.Format("User {0} has connected!",connectionID));
                break;

                case NetworkEventType.DisconnectEvent:
                Debug.Log(string.Format("User {0} has disconnected!",connectionID));
                break;

                case NetworkEventType.DataEvent:
                BinaryFormatter formatter = new BinaryFormatter();             
                MemoryStream ms = new MemoryStream(recBuffer);
                break;

                default:
                case NetworkEventType.BroadcastEvent:
                Debug.Log("Unexpected network event type!");
                break;
            }
        }
    }
}


