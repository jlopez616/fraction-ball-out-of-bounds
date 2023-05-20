using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class numberLineSelect : MonoBehaviour
{
    public double value;
    public Texture2D cursor_Astronaut; 
    private Button button;
    // public GameObject astronaut;
    // Start is called before the first frame update
    void Start()
    {
        // Cursor.SetCursor(cursor_Astronaut,Vector2.zero,CursorMode.ForceSoftware);
        button = GetComponent<Button>();
        button.onClick.AddListener(getNumberLineScore);
    }
    // void OnTriggerEnter2D(Collider2D collider)
    // {
    //     // Debug.Log("Number line",collider.gameObject);
    //     if (collider.gameObject.name == "Astronaut")
    //     {
    //         GameGenerator.numberlineScore = value;
    //     }
        
    // }
    // Update is called once per frame
    public void getNumberLineScore(){
        GameGenerator.numberlineScore = value;
        Debug.Log(GameGenerator.numberlineScore);
        GameGenerator.checkexactlyghost = true;  
             
    }
    void Update()
    {
        
    }
}
