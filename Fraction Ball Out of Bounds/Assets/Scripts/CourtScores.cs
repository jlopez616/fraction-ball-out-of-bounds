using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourtScores : MonoBehaviour
{
    public GameObject character;
    public float score;


    void OnTriggerEnter2D(Collider2D collision)
    {
       /* if (collision.gameObject == character)
        {
            Debug.Log("First method");
            Debug.Log(score);
        }*/
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
