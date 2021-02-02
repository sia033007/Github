using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleScreen : MonoBehaviour
{
    public GameObject[] numbers;
    // Start is called before the first frame update
    void Start()
    {
        int index = Random.Range(0,numbers.Length);
        GameObject newplayer = numbers[index];
        newplayer.SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
