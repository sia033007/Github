using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.NetworkedVar;
using MLAPI.Messaging;

public class PlayerShooting : NetworkedBehaviour
{
    float fireRate =10f;
    float shootTimer =0f;
    public ParticleSystem bulletParticleSysytem;
    private ParticleSystem.EmissionModule em;
    NetworkedVarBool shooting = new NetworkedVarBool(new NetworkedVarSettings{WritePermission = NetworkedVarPermission.OwnerOnly},false);
    //bool shooting = false;
    // Start is called before the first frame update
    void Start()
    {
        em = bulletParticleSysytem.emission;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(IsLocalPlayer)
        {
            shooting.Value = Input.GetMouseButton(0);
            shootTimer += Time.deltaTime;
            if(shooting.Value && shootTimer >= 1/fireRate)
            {
                shootTimer =0;
                //call our method;
                InvokeServerRpc(Shoot);
            }
        }
        
        em.rateOverTime = shooting.Value? fireRate:0f;
        
    }
    [ServerRPC]
    void Shoot()
    {
        Ray ray = new Ray(bulletParticleSysytem.transform.position,bulletParticleSysytem.transform.forward);
        if(Physics.Raycast(ray,out RaycastHit hit, 100))
        {
            var player = hit.collider.GetComponent<PlayerHealth>();
            if(player != null)
            {
                player.TakeDamage(10f);
            }
        }
    }
}
