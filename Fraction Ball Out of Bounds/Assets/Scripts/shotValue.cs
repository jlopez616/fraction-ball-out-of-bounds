using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotValue : MonoBehaviour
{

    public double value;
    public GameObject bball;

    // Start is called before the first frame update
    void Start()
    {
    }

    void OnTriggerEnter2D(Collider2D collider)

    {
        if (collider.gameObject.name == "character_feet")
        {
            GameHandler.shotValue = value;
           // Debug.Log(value);
        }

        Physics2D.IgnoreCollision(bball.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    // Update is called once per frame
    void Update()
    {



    }
}
