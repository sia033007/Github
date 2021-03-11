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
        
        Movie movie1 = new Movie("Marmoulak", "comedy", "IRAN");
        Movie movie2 = new Movie("Interstellar", "sci-fi","USA");

        Debug.Log(movie1.name);
        Debug.Log(movie2.name);

        movie1.film();
        movie2.film();
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
public class Movie
{
    public string genre;
    public string country;
    public string name;

    public Movie(string myname, string mygenre, string mycountry)
    {
        name = myname;
        genre = mygenre;
        country = mycountry;
    }
    public void film()
    {
        Debug.Log(name + $" is a film in genre of {genre} from {country}");
    }
}