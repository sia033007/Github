using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject coinPrefab;
    public static int point;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InstanciateCoin());
        
    }
    private void OnGUI() {
        GUI.Box(new Rect(430,20,100,25),"Score : " + point);
        GUI.color = Color.yellow;
    }
    IEnumerator InstanciateCoin()
    {
        GameObject myCoin = Instantiate(coinPrefab,new Vector3(Random.Range(-4,4.37f),0.41f,Random.Range(-3.85f,4)),Quaternion.identity) as GameObject;
        yield return new WaitForSeconds(2f);
        Destroy(myCoin.gameObject);
        StartCoroutine(InstanciateCoin());
    }
}
