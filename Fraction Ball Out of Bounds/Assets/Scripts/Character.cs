using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Proyecto26;

public class Character : MonoBehaviour
{
    //public GameObject field;
    public GameObject character;
    //public GameObject first_bound;
    // public GameObject second_bound;
    // public GameObject third_bound;
    // public GameObject fourth_bound;
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

    // Start is called before the first frame update
    void Start()
    {
        /* if (GameHandler.representation == "FOURTHS")
         {
             scoreFrom = .25F; //rework for more than thirds
             prob = .65F;

         } else
         {
             scoreFrom = .33F; //rework for more than thirds
             prob = .5F;
         }*/


        //Fetch the Collider from the GameObject

        /* For Debugging Purposes:
        Fetch the center of the Collider volume
        m_Center = m_Collider.bounds.center;
        Fetch the size of the Collider volume
        m_Size = m_Collider.bounds.size;
        Fetch the minimum and maximum bounds of the Collider volume
        m_Min = m_Collider.bounds.min;
        m_Max = m_Collider.bounds.max;

        Output this data into the console
        OutputData(); */
    }

    void OutputData()
    {
        //Output to the console the center and size of the Collider volume
       // Debug.Log("Collider Center : " + m_Center);
       // Debug.Log("Collider Size : " + m_Size);
        //Debug.Log("Collider bound Minimum : " + m_Min);
       // Debug.Log("Collider bound Maximum : " + m_Max);
    }

    /*void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == character)
        {

            Debug.Log(collision.gameObject.name);
        }

        //Debug.Log(collision.gameObject.name);
        /*{
            Debug.Log(".25");
        }
        if (collision.gameObject == second_bound)
        {
            Debug.Log(".5");
        }
        if (collision.gameObject == third_bound)
        {
            Debug.Log(".75");
        }
        if (collision.gameObject == fourth_bound)
        {
            Debug.Log("1");
        }
    }*/

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && GameHandler.gameInProgress == true)
        {
            if (GameHandler.shotInProgress == false)
            {
                Vector3 mousePos = Input.mousePosition;

                mousePos = Camera.main.ScreenToWorldPoint(mousePos);
                mousePos = new Vector3(mousePos.x, mousePos.y + 1.35f, mousePos.z);
                Vector2 newPos = Vector2.Lerp(transform.position, mousePos, 1);
                character.transform.position = Vector2.Lerp(transform.position, mousePos, 1.5f);

                GameHandler.lastAction = "Move";

                GameHandler.round_num_of_movements = GameHandler.round_num_of_movements + 1;

                ShotMeter(character.transform.position);

                GameHandler.time = System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
                Log log = new Log("MOVE", "", GameHandler.Score);
                // Debug.Log(character.transform.position.y);
               // RestClient.Post("https://fractionball2022-default-rtdb.firebaseio.com/" + GameHandler.playerId + "/fball.json", log);
            }

            


        }
    }

    void ShotMeter(Vector3 position)
    {
        //Debug.Log(GameHandler.shotValue);
        if (GameHandler.unlimitedShots == false)
        {
            prob = 1;
        }
        if (GameHandler.shotValue == .25)
        {
            scoreFrom = .25F;
            if (GameHandler.unlimitedShots == true)
            {
                prob = .85F;
            }
        } else if (GameHandler.shotValue == .5)
        {
            scoreFrom = .50F;
            if (GameHandler.unlimitedShots == true)
            {
                prob = .70F;
            }
        } else if (GameHandler.shotValue == .75)
        {
            scoreFrom = .75F;
            if (GameHandler.unlimitedShots == true)
            {
                prob = .55F;
            }

        } else
        {
            scoreFrom = 1;
            if (GameHandler.unlimitedShots == true)
            {
                prob = .40F;
            }
        }
    }
        /*if (Mathf.Pow(position.x, 2) + (Mathf.Pow(position.y, 2) - 1) <= Mathf.Pow(radius, 2)) {
            Debug.Log(value);
        }
        
        /if (GameHandler.representation  == "FOURTHS")
        {
            if (character.transform.position.x >= 0)
            {
                if (character.transform.position.x < first_bound && character.transform.position.y > first_bound_bottom)
                {
                    scoreFrom = .25F;
                    if (GameHandler. unlimitedShots == true) {
                        prob = .75F;
                    }
                   
                }
                else if (character.transform.position.x < second_bound && character.transform.position.y > second_bound_bottom)
                {
                    scoreFrom = .50F;
                    if (GameHandler.unlimitedShots == true)
                    {
                        prob = .60F;
                    }
                }
                else if (character.transform.position.x < third_bound && character.transform.position.y > third_bound_bottom)
                {
                    scoreFrom = .75F;
                    if (GameHandler.unlimitedShots == true)
                    {
                        prob = .45F;
                    }
                }
                else if (character.transform.position.x < fourth_bound && character.transform.position.y > fourth_bound_bottom)
                {
                    scoreFrom = 1;
                    if (GameHandler.unlimitedShots == true)
                    {
                        prob = .30F;
                    }
                }
            }
            else
            {
                if (character.transform.position.x > -first_bound && character.transform.position.y > first_bound_bottom)
                {
                    scoreFrom = .25F;
                    if (GameHandler.unlimitedShots == true)
                    {
                        prob = .75F;
                    }
                }
                else if (character.transform.position.x > -second_bound && character.transform.position.y > second_bound_bottom)
                {
                    scoreFrom = .50F;
                    if (GameHandler.unlimitedShots == true)
                    {
                        prob = .60F;
                    }
                }
                else if (character.transform.position.x > -third_bound && character.transform.position.y > third_bound_bottom)
                {
                    scoreFrom = .75F;
                    if (GameHandler.unlimitedShots == true)
                    {
                        prob = .45F;
                    }
                }
                else if (character.transform.position.x > -fourth_bound && character.transform.position.y > fourth_bound_bottom)
                {
                    scoreFrom = 1;
                    if (GameHandler.unlimitedShots == true)
                    {
                        prob = .30F;
                    }
                }
            }
        } else
        {
            if (character.transform.position.x >= 0)
            {
                if (character.transform.position.x < first_bound)
                {
                    scoreFrom = .33F;
                    if (GameHandler.unlimitedShots == true)
                    {
                        prob = .60F;
                    }
                }
                else if (character.transform.position.x < second_bound)
                {
                    scoreFrom = .66F;
                    {
                        prob = .45F;
                    }
                }
                else if (character.transform.position.x < third_bound)
                {
                    scoreFrom = 1;
                    if (GameHandler.unlimitedShots == true)
                    {
                        prob = .30F;
                    }
                }
            }
            else
            {
                if (character.transform.position.x > -first_bound)
                {
                    scoreFrom = .33F;
                    if (GameHandler.unlimitedShots == true)
                    {
                        prob = .60F;
                    }
                }
                else if (character.transform.position.x > -second_bound)
                {
                    scoreFrom = .66F;
                    {
                        prob = .45F;
                    }
                }
                else if (character.transform.position.x > -third_bound)
                {
                    scoreFrom = 1;
                    if (GameHandler.unlimitedShots == true)
                    {
                        prob = .30F;
                    }
                }
            }
        }*/


    // Update is called once per frame
    void Update()
    {


        x_pos = character.transform.position.x;
        y_pos = character.transform.position.y;
        ShotMeter(character.transform.position);

    }
}
