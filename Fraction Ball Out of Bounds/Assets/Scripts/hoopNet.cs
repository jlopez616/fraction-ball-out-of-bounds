using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class hoopNet : MonoBehaviour
{

    public GameObject basketball;
    public Sprite emptyHoop;
    public Sprite hoopWithBall;
    public SpriteRenderer hoopSprite;
    //public Text coachText;
    // Start is called before the first frame update
    void Start()
    {
        hoopSprite.sprite = emptyHoop;
    }



    void OnTriggerEnter2D(Collider2D collider)

    {
        if (collider.gameObject.name == "bball")
        {
            collider.gameObject.SetActive(false);
            hoopSprite.sprite = hoopWithBall;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (GameHandler.shotInProgress == false) {
            basketball.SetActive(true);
            hoopSprite.sprite = emptyHoop;
        }


    }
}
