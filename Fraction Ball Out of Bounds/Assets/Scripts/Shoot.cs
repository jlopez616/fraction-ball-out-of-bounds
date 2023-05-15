using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Proyecto26;


public class Shoot : MonoBehaviour
{
    public GameObject ball;
    public GameObject character;
    public static Vector3 endPos;
    public Button shootBall;
    public GameObject net;
    public Text coachText;
    public GameObject fourth_ring;
    public GameObject hitShot;
    public GameObject missShot;
    private AudioSource hitShotAudio;
    private AudioSource missShotAudio;
    private bool wasShotHit;

    private float currentTime;
    public static float endTime;


    // Start is called before the first frame update
    void Start()
    {
        
        hitShotAudio = hitShot.GetComponent<AudioSource>();
        missShotAudio = missShot.GetComponent<AudioSource>();
        endPos = net.transform.position;
        shootBall.onClick.AddListener(OnClick);


    }

    // Update is called once per frame

    void OnClick()
    {
        
        if (GameGenerator.shotInProgress == false)
        {
            GameGenerator.lastAction = "Shoot";
            ShootBall(Character.prob);
        }
        
      

    }


    void ShootBall(float prob)
    {

        endTime = Time.time + 3; //3 seconds

        float chance = Random.Range(0, 100);
        string shotHit;
        double oldScore = GameGenerator.Score;
        
        double newScore = GameGenerator.Score;
        ball.transform.position = character.transform.position;
        if (chance <= (prob * 100)) {
            double scoreFrom = System.Math.Round(Character.scoreFrom, 2);
            if (scoreFrom.ToString()=="0.33" && GameGenerator.Score.ToString().EndsWith(".33")) {
                newScore = System.Math.Round(GameGenerator.Score + 0.34, 2);
            } else if (scoreFrom.ToString()=="0.67" && GameGenerator.Score.ToString().EndsWith(".67")) {
                newScore = System.Math.Round(GameGenerator.Score + 0.66, 2);
            }   else {
                newScore = System.Math.Round(GameGenerator.Score + scoreFrom, 2);
            }
            
            if(!GameGenerator.isExactlyGhost)
                coachText.text = "Shot hit! " + GameGenerator.ScoreToFraction(GameGenerator.Score) + "+" + GameGenerator.ScoreToFraction(scoreFrom) + "=" + GameGenerator.ScoreToFraction(newScore);
            missShotAudio.Stop();
            hitShotAudio.Play();
            
            shotHit = "TRUE";
            wasShotHit = true;

            GameGenerator.round_num_of_shots = GameGenerator.round_num_of_shots + 1;


            if (GameGenerator.round_num_of_shots == 1)
            {
                GameGenerator.preplan_time = GameGenerator.timer;
            }

        } else {
            coachText.text = "Shot missed! " + GameGenerator.ScoreToFraction(GameGenerator.Score) + "+0=" + GameGenerator.ScoreToFraction(GameGenerator.Score);
            hitShotAudio.Stop(); 
            missShotAudio.Play();
            shotHit = "FALSE";
            wasShotHit = false;
        }
        GameGenerator.time = System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
        Log log = new Log("SHOT", shotHit, oldScore);
       // RestClient.Post("https://fractionball2022-default-rtdb.firebaseio.com/" + GameGenerator.playerId + "/fball.json", log);
        if (shotHit == "TRUE")
        {
            GameGenerator.Score = newScore;
        }

        if (GameGenerator.unlimitedShots == false)
        {
            GameGenerator.ballsRemaining--;
        }


    }

    void Update()
    {

        if (ball.transform.position != endPos)
        {
            if (GameGenerator.shotInProgress == true)
            {
                if (wasShotHit == false)
                {
                    endPos = new Vector3(net.transform.position.x, net.transform.position.y - 3f, net.transform.position.z);
                }
                else
                {
                    endPos = new Vector3(net.transform.position.x, net.transform.position.y, net.transform.position.z);
                }
                ball.transform.position = Vector3.Lerp(ball.transform.position, endPos, Time.deltaTime * 2);
            }
            else
            {
                ball.transform.position = new Vector3(character.transform.position.x - 1.25f, character.transform.position.y + 1, character.transform.position.z);
            }


                // Debug.Log(ball.transform.position)

        }
       
        if (Time.time < endTime)
        {
            
            GameGenerator.shotInProgress = true;
        } else
        {
            GameGenerator.shotInProgress = false;
           
        }
    }


}
