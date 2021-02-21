using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Transports.UNET;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public InputField inputField;
    public GameObject menuPanel;
    private void Start() {
        NetworkingManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
        Game game1 = new Game("Strategy",1000,"Iran");
        Game game2 = new Game("Casual",2000,"Switzerland");

        Debug.Log(game1.country);
        Debug.Log(game2.country);
        
        game1.mygame();
    }
    private void ApprovalCheck(byte[] connectionData, ulong clientID, NetworkingManager.ConnectionApprovedDelegate callback)
    {
        bool approve = false;
        //if the connection is correct then we approve
        string password = System.Text.Encoding.ASCII.GetString(connectionData);
        if(password == "mygame")
        {
            approve = true;
        }
        Debug.Log($"Approval : {approve}");
        callback(true, null, approve, new Vector3(0,10,0), Quaternion.identity);


    }

    public void Host()
    {
        NetworkingManager.Singleton.StartHost();
        menuPanel.SetActive(false);
    }
    public void Join()
    {
        //client join
        if(inputField.text.Length <=0)
        {
            NetworkingManager.Singleton.GetComponent<UnetTransport>().ConnectAddress = "127.0.0.1";
        }
        else
        {
            NetworkingManager.Singleton.GetComponent<UnetTransport>().ConnectAddress = inputField.text;

        }
        NetworkingManager.Singleton.NetworkConfig.ConnectionData = System.Text.Encoding.ASCII.GetBytes("mygame");
        NetworkingManager.Singleton.StartClient();
        menuPanel.SetActive(false);
    }
   
}
public class Game
{
    public string genre;
    public int price;
    public string country;

    public Game(string myGenre, int myPrice, string myCountry)
    {
        genre = myGenre;
        price = myPrice;
        country = myCountry;
    }
    public void mygame()
    {
        Debug.Log("This game genre is " + genre + " from " + country);
    }
}
