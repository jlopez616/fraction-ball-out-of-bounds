using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using Proyecto26;

public class GameHandler : MonoBehaviour
  
{
    //[DllImport("__Internal")]
    //private static extern string GetID();


    // Start is called before the first frame update
    //public GameObject coachBubble;
    public Text coachText;
    public Text targetText;
    public Text introText_one;
    public Text introText_two;
    public Text introText_three;
    public Text introText_four;
    public Text continueButtonText;

    public static string playerId;
    public static string time;
    public static float timer = 0.0f;
    public static string GameMode;
    public static string representation;
    public static string difficulty;
    public static string lastAction;
    public static float x_pos;
    public static float y_pos;
    public double extraDecimal;
    public static string goalString;
    public int numberOfBalls;
    public static bool unlimitedShots;
    public static int ballsRemaining;
    //Vars added 3/30/2022
    public static int accuracy_correct;
    public static int accuracy_min_shots;
    public static int round_num_of_shots;
    public static int round_num_of_movements;
    public static int total_num_of_shots;
    public static int total_num_of_movements;
    public static int wps_correct;
    public static int wps_min_shots;
    public static int excess_shots;
    public static float movement_time;
    public static float preplan_time;
    public static float total_round_time;
    public static float total_game_time;


    public GameObject continueButton;
    public GameObject easyButton;
    public GameObject mediumButton;
    //public GameObject hardButton;
    public GameObject decimalButton;
    public GameObject fractionButton;
    public GameObject startButton;
    public GameObject shootButton;
    public GameObject fourthsButton;
    public GameObject thirdsButton;
    public GameObject buttonLink;

    public GameObject IntroUI;
    public GameObject IntroPanel;
    public GameObject numberline;
    public GameObject thirds_spaces;
    public GameObject fourths_spaces;
    public GameObject fractionNumberlineCover;
    public GameObject decimalNumberlineCover;
    public GameObject mainCharacter;
    public GameObject ballsLeft;
    public GameObject ballOne;
    public GameObject ballTwo;
    public GameObject ballThree;
    public GameObject ballFour;
    public GameObject ballFive;
    public GameObject fractionCourtLabels;
    public GameObject decimalCourtLabels;
    public GameObject amtText;

    public Image numberLineImage;
    public Sprite[] fractionspriteArray;
    public Sprite[] decimalspriteArray;
    private static Sprite[] spriteArray;
    private Counter UserCount;

    float startCountdown = 3.9F; // 3 seconds
    private static int currentScene;
    public static double goalScore;
    private static double originalGoalScore;
    public static double Score;
    //public static string amtID;
    public static bool shotInProgress = false;
    public static bool gameInProgress = false;
    public static double shotValue;

    public static Dictionary<double, string> fractionPairs = new Dictionary<double, string>() {
        {.25, "1/4"},
        {.5, "2/4"},
        {.75, "3/4"},
        {.33, "1/3"},
        {1, "1" },
        {1.25, "1 1/4"},
        {1.5, "1 2/4"},
        {1.75, "1 3/4"},
        {2, "2" },
        {2.25, "2 1/4"},
        {2.5, "2 2/4"},
        {2.75, "2 3/4"},
        {3, "3" },
        {3.25, "3 1/4"},
        {3.5, "3 2/4"},
        {3.75, "3 3/4"},
        {4, "4"},
        {4.25, "4 1/4"},
        {4.5, "4 2/4"},
        {4.75, "4 3/4"},
        {5, "5" },
        {5.25, "5 1/4"},
        {5.5, "5 2/4"},
        {5.75, "5 3/4"},
        {6, "6" },
        {.66, "2/3"},
        {0, "0"},
        };

    public static Dictionary<double, int> numberLinePairs = new Dictionary<double, int>() {
        {.25, 1},
        {.5, 2},
        {.75, 3},
        {.33, 99999},
        {1, 4},
        {1.25, 5},
        {1.5, 6},
        {1.75, 7},
        {2, 8},
        {2.25, 9},
        {2.5, 10},
        {2.75, 11},
        {3, 12},
        {3.25, 13},
        {3.5, 14},
        {3.75, 15},
        {4, 16},
        {4.25, 17},
        {4.5, 18},
        {4.75, 19},
        {5, 20},
        {5.25, 21},
        {5.5, 22},
        {5.75, 23},
        {6, 24},
        {.66, 9999},
        {0, 0},
        };

    private System.Random random = new System.Random();
    public static Queue<int> scenes = new Queue<int> { };

    public void GetQueryVariable(string id)
    {
        playerId = id;
    }

    /* private void GetNumberOfUsers()
     {
         RestClient.Get<Counter>("https://fractionball-default-rtdb.firebaseio.com/test.json").Then(response =>
         {
              UserCount = new Counter(response.userCount);
              playerId = UserCount.userCount.ToString();
              UserCount.add();
           // RestClient.Put("https://fractionball-default-rtdb.firebaseio.com/test.json", UserCount);
             //Debug.Log(playerId);
         });
       //playerId = "LSLSSL50";
     }*/

    void shuffle (int []scene, int n)
    {
        for (int i = 0; i < n; i++)
        {
            int r = i + random.Next(scene.Length - i);
            int temp = scene[r];
            scene[r] = scene[i];
            scene[i] = temp;
        }
    }

    void Start()
    {

        
        // GetNumberOfUsers();
        lastAction = "None";
        difficulty = "EASY";
        representation = "FOURTHS";
        Score = 0;
        int[] scenesRaw = { 1, 2, 3, 4 };
        shuffle(scenesRaw, 4);
        for (int i=0; i < scenesRaw.Length; i++)
        {
            scenes.Enqueue(scenesRaw[i]);
        }


        /*introText.text = "Time to play Fraction Ball: Exactly! You will play this game four times. Each time you play, we will change the game a little. " +
        "It's very important that you read the instructions before each game, some rules stay the same, but others change.";*/
        try
        {
            introText_one.text = "Ready to play, " + playerId + "?";
        } catch (System.Exception e)
        {
            introText_one.text = "Please enter your name:";
            amtText.SetActive(true);
            InputField iField = amtText.GetComponent<InputField>();
            playerId = iField.text.ToUpper().Replace(" ", "");
            /*introText.text = "In each game, I will call out a target number and you have to score " +
                "that number EXACTLY. Be very careful; if you score past the exact number," +
                "you automatically lose that round.";*/
        }
        continueButtonText.text = "Yes!";
        Button continButton = continueButton.GetComponent<Button>();
        continButton.onClick.AddListener(introOne);

        
        
    }

   void introOne()
    {    
        amtText.SetActive(false);
        introText_one.text = "Time to play FRACTION BALL: EXACTLY!";
        introText_two.text = "Game Rules:";
        introText_three.text = "Challenge 1: Score EXACTLY a number that you get.";
        introText_four.text = "Challenge 2: Don't score over it or you will lose.";
        /*introText.text = "In each game, I will call out a target number and you have to score " +
            "that number EXACTLY. Be very careful; if you score past the exact number," +
            "you automatically lose that round.";*/
        Button continButton = continueButton.GetComponent<Button>();
        continueButtonText.text = "Continue";
        continButton.onClick.RemoveAllListeners();
        continButton.onClick.AddListener(gameConfig);

    }

    void introTwo()
    {

        /*introText.text = "I will also only tell you your target number once at the start of each round, meaning you need to remember it " +
            "as you play.";*/
        introText_one.text = "In each game, I will call out a target number and you have to score " +
    "that number EXACTLY. Be very careful; if you score past the exact number," +
    "you automatically lose that round.";
        Button continButton = continueButton.GetComponent<Button>();
        continButton.onClick.RemoveAllListeners();
        continButton.onClick.AddListener(gameConfig);
    }

    void introThree()
    {

        introText_one.text = "I will also only tell you your target number once at the start of each round, meaning you need to remember it " +
            "as you play.";
        Button continButton = continueButton.GetComponent<Button>();
        continButton.onClick.RemoveAllListeners();
        continButton.onClick.AddListener(gameConfig);
    }

    void gameConfig()
    {
        //startButton.SetActive(true);
        introText_one.text = "How to Play:";
        introText_two.text = "Click on the court to move.";
        introText_three.text = "Click on the SHOOT button to shoot a basketball.";
        introText_four.text = "Use the numberline to keep track of your score.";

        Button continButton = continueButton.GetComponent<Button>();
        continButton.onClick.RemoveAllListeners();
        continButton.onClick.AddListener(scoreConfig);

    }



    int getNumberOfBalls(double score)
    {
        if (score <= 1)
        {
            return 1;
        }
        else if (score <= 2 && score > 1)
        {
            return 2;
        }
        else if (score <= 3 && score > 2)
        {
            return 3;
        }
        else if (score <= 4 && score > 3)
        {
            return 4;
        } else
        {
            return 5;
        }
    }

    void scoreConfig()
    {
        Log log = new Log("PRE-ROUND", "NO SHOT", Score); // double check this
        RestClient.Post("https://fractionball2022-default-rtdb.firebaseio.com/" + GameHandler.playerId + "/fball.json", log);

        ballsLeft.SetActive(false);
        Score = 0;
        shotInProgress = false;

        if (scenes.Count != 0)

        {
            goalScore = Random.Range(1, 5);
            originalGoalScore = goalScore;
            if (representation == "FOURTHS")
            {
                if (goalScore != 5)
                {
                    extraDecimal = Random.Range(0, 4);
                    if (extraDecimal == 0 && goalScore == 1)
                    {
                        goalScore = goalScore + .25;
                        extraDecimal = .25;

                    }
                    if (extraDecimal == 1)
                    {
                        goalScore = goalScore + .25;
                        extraDecimal = .25;

                    }
                    else if (extraDecimal == 2)
                    {
                        goalScore = goalScore + .5;
                        extraDecimal = .5;
                    }
                    else if (extraDecimal == 3)
                    {
                        goalScore = goalScore + .75;
                        extraDecimal = .75;
                    }
                    else
                    {
                        extraDecimal = 0;
                    }
                }

            }

            numberOfBalls = getNumberOfBalls(goalScore);
            currentScene = scenes.Peek();

            //Debug.Log(scenes.Count);




            if (currentScene == 1)
            {
                GameMode = "FRACTIONS";
                numberOfBalls = 100000;
                introText_one.text = "For this round, score EXACTLY " + ScoreToFraction(goalScore);
                introText_two.text = "Try and make " + ScoreToFraction(goalScore) + " with the LEAST number of shots";
                introText_three.text = "Round Boost: You have as many shots as you want!";
                introText_four.text = "";
                unlimitedShots = true;
            }
            else if (currentScene == 2)
            {
                GameMode = "DECIMALS";
                numberOfBalls = 100000;
                introText_one.text = "For this round, score EXACTLY " + goalScore; 
                introText_two.text = "Try and make " + ScoreToFraction(goalScore) + " with the LEAST number of shots";
                introText_three.text = "Round Boost: You have as many shots as you want!";
                introText_four.text = "";
                unlimitedShots = true;
            }
            else if (currentScene == 3)
            {
                GameMode = "FRACTIONS";
                introText_one.text = "For this round, score EXACTLY " + ScoreToFraction(goalScore);
                introText_two.text = "You only have " + numberOfBalls + " shots.";
                introText_three.text = "Round Boost: Your player will never miss a shot!";
                introText_four.text = "";
                unlimitedShots = false;
            }
            else
            {
                GameMode = "DECIMALS";
                introText_one.text = "For this round, score EXACTLY " + goalScore;
                introText_two.text = "You only have " + numberOfBalls + " shots.";
                introText_three.text = "Round Boost: Your player will never miss a shot!";
                introText_four.text = "";

                unlimitedShots = false;
            }


            if (GameMode == "DECIMALS")
            {
                goalString = goalScore.ToString();
                fractionCourtLabels.SetActive(false);
                decimalCourtLabels.SetActive(true);
                spriteArray = decimalspriteArray;
               

            }
            else if (GameMode == "FRACTIONS")
            {
                spriteArray = fractionspriteArray;
                if (extraDecimal != 0)
                {
                    goalString = originalGoalScore.ToString() + " " + fractionPairs[extraDecimal];
                }
                else
                {
                    goalString = originalGoalScore.ToString();
                }

                decimalCourtLabels.SetActive(false) ;
                fractionCourtLabels.SetActive(true) ;
            }
            continueButton.SetActive(false);
            startButton.SetActive(true);
            Button startButt = startButton.GetComponent<Button>();
            startButt.onClick.AddListener(StartGame);

        } else
        {
            loadTest();
        }

    
    }
    void OpenURL()
    {
        Application.OpenURL("https://jlopez616.github.io/fractionball/experiment.html?id=" + playerId);
    }

    void loadTest()
    {
        IntroPanel.SetActive(true);
        startButton.SetActive(false);
        buttonLink.SetActive(true);
        Button linkButton = buttonLink.GetComponent<Button>();
        linkButton.onClick.AddListener(OpenURL);

        introText_one.text = "Nice job! Thank you for playing!";
        introText_two.text = "";
        introText_three.text = "";
        introText_four.text = "";
        continueButton.SetActive(false);
        startButton.SetActive(false);
    }




    /*void configThree(GameObject currentButton = null)
    {
        fractionButton.SetActive(false);
        decimalButton.SetActive(false);
        fourthsButton.SetActive(true);
        thirdsButton.SetActive(true);


        GameMode = currentButton.name;

        introText.text = "Would you like to play in fourths or thirds?";

        Button fourthsButt = fourthsButton.GetComponent<Button>();
        fourthsButt.onClick.AddListener(() => { configFour(fourthsButton); });

        Button thirdsButt = thirdsButton.GetComponent<Button>();
        thirdsButt.onClick.AddListener(() => { configFour(thirdsButton); });

    }
    */


    public static string ScoreToFraction(double translate)
    {
        if (GameHandler.GameMode == "FRACTIONS")
        {
            return GameHandler.fractionPairs[translate];
        }
        else
        {
            return translate.ToString();
        }

    }

    void StartGame()
    {
        //PostToDatabase();
       

        if (GameHandler.unlimitedShots == false)
        {
            ballsLeft.SetActive(true);
            ballOne.SetActive(true);
            ballTwo.SetActive(true);
            ballThree.SetActive(true);
            ballFour.SetActive(true);
            ballFive.SetActive(true);
        }
        timer = 0;
        round_num_of_shots = 0;
        round_num_of_movements = 0;
        preplan_time = 0;
        movement_time = 0;
        shotInProgress = false;
        gameInProgress = true;
        shootButton.SetActive(true);
        startButton.SetActive(false);
        IntroPanel.SetActive(false);
        IntroUI.SetActive(false);
        mainCharacter.SetActive(true);
        targetText.text = "Target: " + goalString;
        coachText.text = "3..2..1..Shoot!";
        if (unlimitedShots == false)
        {
            ballsRemaining = numberOfBalls;
        }
        if (difficulty == "EASY")
        {
            numberline.SetActive(true);
        }

        if (representation == "THIRDS")
        {
            thirds_spaces.SetActive(true);
        } else if (representation == "FOURTHS")
        {
            fourths_spaces.SetActive(true);
        }

       Log log = new Log("START", "NO SHOT", 0);
       RestClient.Post("https://fractionball2022-default-rtdb.firebaseio.com/" + GameHandler.playerId + "/fball.json", log);


    }
    void EndGame()
    {
        Shoot.endTime = Time.time;
        shotInProgress = false;
        gameInProgress = false;
        ballsLeft.SetActive(false);
        ballOne.SetActive(false);
        ballTwo.SetActive(false);
        ballThree.SetActive(false);
        ballFour.SetActive(false);
        ballFive.SetActive(false);
        mainCharacter.SetActive(false);
        total_round_time = movement_time;
        total_game_time += movement_time;
        total_num_of_movements += round_num_of_movements;
        total_num_of_shots += round_num_of_shots;
        if (Score == goalScore)
        {

            IntroUI.SetActive(true);
            IntroPanel.SetActive(true);
            continueButton.SetActive(true);
            shootButton.SetActive(false);
            numberline.SetActive(false);
            coachText.text = "";
            targetText.text = "";
            introText_one.text = "Congratulations! You got “exactly” " + ScoreToFraction(Score) + " points!";
            introText_two.text = "";
            introText_three.text = "";
            introText_four.text = "";

            accuracy_correct = accuracy_correct + 1;
            wps_correct = wps_correct + getNumberOfBalls(goalScore);

            if (round_num_of_shots == getNumberOfBalls(goalScore))
            {
                accuracy_min_shots = accuracy_min_shots + 1;
                wps_min_shots = wps_min_shots + round_num_of_shots;
            }

        } else
        {
            IntroUI.SetActive(true);
            IntroPanel.SetActive(true);
            continueButton.SetActive(true);
            shootButton.SetActive(false);
            numberline.SetActive(false);
            coachText.text = "";
            targetText.text = "";


            introText_one.text = "Oh no, you scored " + ScoreToFraction(Score) + " points. You needed exactly " + goalString + " points instead.";
            introText_two.text = "";
            introText_three.text = "";
            introText_four.text = "";
        }
        if (round_num_of_shots > getNumberOfBalls(Score))
        {
            excess_shots = excess_shots + (round_num_of_shots - getNumberOfBalls(Score));
        }
        Button continueButt = continueButton.GetComponent<Button>();
        continueButt.onClick.RemoveAllListeners();
        continueButt.onClick.AddListener(scoreConfig);
        scenes.Dequeue();
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;
        if (gameInProgress == true){
            movement_time = timer;
            if (unlimitedShots == false)
            {
                if (ballsRemaining < 5)
                {
                    ballFive.SetActive(false);
                }
                if (ballsRemaining < 4)
                {

                    ballFour.SetActive(false);
                }
                if (ballsRemaining < 3)
                {
                    ballThree.SetActive(false);
                }
                if (ballsRemaining < 2)
                {
                    ballTwo.SetActive(false);
                }
                if (ballsRemaining < 1)
                {
                    EndGame();
                  
                }
                if (Score >= goalScore && ballsRemaining > 0)
                {
                    EndGame();
                }

            } else
            {
                if (Score >= goalScore)
                {
                    EndGame();
                }
            }

            //Debug.Log(timer);


            if (Score < goalScore)
            {
                numberLineImage.sprite = spriteArray[numberLinePairs[Score]];
            }

        }

    }   


}
