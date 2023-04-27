using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;

public class Character : MonoBehaviour
{
    public GameObject character;
    public float radius;
    public float value;
    public float first_bound_bottom;
    public float second_bound_bottom;
    public float third_bound_bottom;
    public float fourth_bound_bottom;
    public static float scoreFrom;
    public static float prob;

    public static float x_pos;
    public static float y_pos;


    Collider m_Collider;
    Vector3 m_Center;
    Vector3 m_Size, m_Min, m_Max;

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && GameGenerator.gameInProgress == true)
        {
            if (GameGenerator.shotInProgress == false)
            {
                Vector3 mousePos = Input.mousePosition;

                mousePos = Camera.main.ScreenToWorldPoint(mousePos);
                mousePos = new Vector3(mousePos.x, mousePos.y + 1.35f, mousePos.z);
                Vector2 newPos = Vector2.Lerp(transform.position, mousePos, 1);
                character.transform.position = Vector2.Lerp(transform.position, mousePos, 1.5f);

                GameGenerator.lastAction = "Move";

                GameGenerator.round_num_of_movements = GameGenerator.round_num_of_movements + 1;

                ShotMeter(character.transform.position);

                GameGenerator.time = System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
                Log log = new Log("MOVE", "", GameGenerator.Score);
                // Debug.Log(character.transform.position.y);
                //RestClient.Post("https://fractionball2022-default-rtdb.firebaseio.com/" + GameGenerator.playerId + "/fball.json", log);
            }
        }
    }

    void ShotMeter(Vector3 position)
    {

        if (GameGenerator.unlimitedShots == false)
        {
            prob = 1;
        }

        switch(GameGenerator.shotValue) {
            case .25: 
                scoreFrom = .25F;
                if (GameGenerator.unlimitedShots == true)
                {
                    prob = .85F;
                }
                break;
            case .5:
                scoreFrom = .50F;
                if (GameGenerator.unlimitedShots == true)
                {
                    prob = .70F;
                }
                break;
            case .75:
                scoreFrom = .75F;
                if (GameGenerator.unlimitedShots == true)
                {
                    prob = .55F;
                }
                break;
            case .33:
                scoreFrom = .33F;
                if (GameGenerator.unlimitedShots == true)
                {
                    prob = .80F;
                }
                break;
            case .66:
                scoreFrom = .66F;
                if (GameGenerator.unlimitedShots == true)
                {
                    prob = .60F;
                }
                break;
            case 1:
                scoreFrom = 1;
                if (GameGenerator.unlimitedShots == true)
                {
                    prob = .40F;
                }
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        x_pos = character.transform.position.x;
        y_pos = character.transform.position.y;
        ShotMeter(character.transform.position);

    }
}
