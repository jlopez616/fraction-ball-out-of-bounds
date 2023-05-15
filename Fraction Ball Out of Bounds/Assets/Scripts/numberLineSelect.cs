using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class numberLineSelect : MonoBehaviour
{
    public double value;
    public GameObject astronaut;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Number line",collider.gameObject);
        if (collider.gameObject.name == "Astronaut")
        {
            GameGenerator.numberlineScore = value;
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
