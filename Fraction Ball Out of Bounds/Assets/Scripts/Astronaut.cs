using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astronaut : MonoBehaviour
{
    public GameObject astronaut;
    private Vector3 mousePositionOffset;
    private bool isDragging = false;

    private void OnMouseDown()
    {
        Debug.Log(GameGenerator.gameInProgress+"Game ");
        if(GameGenerator.gameInProgress==false){
            isDragging = true;
            mousePositionOffset = astronaut.transform.position - GetMouseWorldPosition();
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
       if (isDragging)
        {
            Vector3 targetPosition = GetMouseWorldPosition() + mousePositionOffset;
            astronaut.transform.position = targetPosition;
        }
    }
}
