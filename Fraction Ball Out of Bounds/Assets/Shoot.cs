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
        
        if (TaskGenerator.shotInProgress == false)
        {
            TaskGenerator.lastAction = "Shoot";
            ShootBall(Character.prob);
        }
        
      

    }


    void ShootBall(float prob)
    {

        endTime = Time.time + 3; //3 seconds

        float chance = Random.Range(0, 100);
        string shotHit;
        double oldScore = TaskGenerator.Score;
        
        double newScore = TaskGenerator.Score;
        ball.transform.position = character.transform.position;
        if (chance <= (prob * 100))
        {
            double scoreFrom = System.Math.Round(Character.scoreFrom, 2);
            newScore = System.Math.Round(TaskGenerator.Score + scoreFrom, 2);

            coachText.text = "Shot hit! " + TaskGenerator.ScoreToFraction(TaskGenerator.Score) + "+" + TaskGenerator.ScoreToFraction(scoreFrom) + "=" + TaskGenerator.ScoreToFraction(newScore);
            missShotAudio.Stop();
            hitShotAudio.Play();
            
            shotHit = "TRUE";
            wasShotHit = true;

            TaskGenerator.round_num_of_shots = TaskGenerator.round_num_of_shots + 1;


            if (TaskGenerator.round_num_of_shots == 1)
            {
                TaskGenerator.preplan_time = TaskGenerator.timer;
            }
           

            

        } else {
            coachText.text = "Shot missed! " + TaskGenerator.ScoreToFraction(TaskGenerator.Score) + "+0=" + TaskGenerator.ScoreToFraction(TaskGenerator.Score);
            hitShotAudio.Stop(); 
            missShotAudio.Play();
            shotHit = "FALSE";
            wasShotHit = false;
        }
        TaskGenerator.time = System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
        Log log = new Log("SHOT", shotHit, oldScore);
       // RestClient.Post("https://fractionball2022-default-rtdb.firebaseio.com/" + TaskGenerator.playerId + "/fball.json", log);
        if (shotHit == "TRUE")
        {
            TaskGenerator.Score = newScore;
        }

        if (TaskGenerator.unlimitedShots == false)
        {
            TaskGenerator.ballsRemaining--;
        }


    }

    void Update()
    {

        if (ball.transform.position != endPos)
        {
            if (TaskGenerator.shotInProgress == true)
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
            
            TaskGenerator.shotInProgress = true;
        } else
        {
            TaskGenerator.shotInProgress = false;
           
        }
    }


}
