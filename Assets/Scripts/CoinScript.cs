using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinScript : MonoBehaviour
{
    int sceneIndex;
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player")
        {
            Coin.point +=1;
            Destroy(this.gameObject);

        }
    }
    private void Start() {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    private void Update() {
        if(Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(sceneIndex -1);
        }
    }
}
