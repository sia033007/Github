using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.NetworkedVar;
using MLAPI.Messaging;

public class PlayerHealth : NetworkedBehaviour
{
    CharacterController cc;
    MeshRenderer [] renderers;
    public NetworkedVarFloat health = new NetworkedVarFloat(100);
    private void Start() {
        cc = GetComponent<CharacterController>();
        renderers = GetComponentsInChildren<MeshRenderer>();
    }
    //Running on the server
    public void TakeDamage (float damage)
    {
        health.Value -= damage;
        //check health
        if(health.Value <=0)
        {
            //Respawn
            health.Value =100;  
            Vector3 pos = new Vector3(Random.Range(-10,10),4,Random.Range(-10,10));
            InvokeClientRpcOnEveryone(ClientRespawn,pos);
        }
    }
    [ClientRPC]
    void ClientRespawn(Vector3 position)
    {
        StartCoroutine(Respawn(position));
      
    }
    IEnumerator Respawn(Vector3 position)
    {
        foreach (var rendere in renderers)
        {
            rendere.enabled = false;       
        }
        yield return new WaitForSeconds(1f);
        cc.enabled = false;
        transform.position = position;
        cc.enabled = true;
        foreach (var rendere in renderers)
        {
            rendere.enabled = true;       
        }

    }
}
