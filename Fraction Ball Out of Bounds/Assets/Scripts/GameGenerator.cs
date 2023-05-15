using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using static System.Math;
public class GameGenerator : MonoBehaviour

{

    public GameObject IntroUI; //Describes the whole Intro UI System
    public Text introText_one; //First line for intro
    public Text introText_two; //Second line for intro
    public Text introText_three; //Third line for intro
    public Text introText_four; //Fourth line for intro
    public GameObject IntroPanel; //Background for intro screen
    public Button introButton;
    public Text introButtonText; //Text for the continue button
    public InputField enterscore; // temp for submitting score
    public Button submitscore; // temp for submitting score

    public static string playerId; //ID of user

    public static string notation; //fractions or decimals
    public static string representation; //thirds, fourths, fifths, sixths
    public static bool timerActive ; // to set timer active for RAPID FIRE MODE
    public static float rapidTimeStart; // to start a time value for RAPID FIRE MODE
    public static float rapidTotalTime; // total timer for RAPID FIRE

    public int number_of_problems = 4; // number of problems to give to player

    //Field Related
    public GameObject fractionCourtLabels;
    public GameObject decimalCourtLabels;
    public GameObject fourths_spaces;
    public GameObject mainCharacter;
    public GameObject blankFourth;
    public GameObject astronaut;

    //Numberline Related
    public Image numberLineImage; //should be blank
    public Sprite[] fractionspriteArray;
    public Sprite[] decimalspriteArray;
    private static Sprite[] spriteArray;

    //UI Related
    public GameObject numberline;
    public GameObject shootButton;
    public Text coachText;
    public Text targetText;
    public Text timerText; // text field for timer

    // Also part of UI; "Balls Left" Related
    public GameObject ballsLeft;
    public GameObject ballOne;
    public GameObject ballTwo;
    public GameObject ballThree;
    public GameObject ballFour;
    public GameObject ballFive;

    //Game Mechanics Related
    public static bool shotInProgress = false;
    public static bool gameInProgress = false;

    //Analytics Vars added 3/30/2022
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

    //this data are not visible to the player but necessary for game mechanics
    public static double Score;
    public static string time;
    public static float timer = 0.0f;
    public static string GameMode;
    public static string difficulty;
    public static string lastAction;
    public static float x_pos;
    public static float y_pos;
    public double extraDecimal;
    public static string goalString;
    public static string goalScoreFraction;
    public int numberOfBalls;
    public static bool unlimitedShots;
    public static int ballsRemaining;
    public static double goalScore;
    private static double originalGoalScore;
    public GameState currentScene;
    public static double shotValue;
    public static double numberlineScore; // to set numberLine value in numberLineSelect script
    public static bool isExactlyGhost;

    //Queue to store scenes

    // Start is called before the first frame update
    void Start()
    {

        //This part of code used to rearanges the scenes into a random order

        introText_one.text = "Ready to play, " + playerId + "?";
        introButtonText.text = "Yes!";
        introButton.onClick.AddListener(introOne);



    }

    //Want better system for coding names:

    void introOne()
    {
        //Suggestion: array of strings
        //New text generator file
        introText_one.text = "Time to play FRACTION BALL: EXACTLY!";
        introText_two.text = "Game Rules:";
        introText_three.text = "Challenge 1: Score EXACTLY a number that you get.";
        introText_four.text = "Challenge 2: Don't score over it or you will lose.";
        introButtonText.text = "Continue";
        introButton.onClick.RemoveAllListeners();
        introButton.onClick.AddListener(gameConfig);

    }


    void gameConfig()
    {
        introText_one.text = "How to Play:";
        introText_two.text = "Click on the court to move.";
        introText_three.text = "Click on the SHOOT button to shoot a basketball.";
        introText_four.text = "Use the numberline to keep track of your score.";
        introButton.onClick.RemoveAllListeners();
        introButton.onClick.AddListener(scoreConfig);

    }

    public static string ScoreToFraction(double translate)
    {
        string score_frac = translate.ToString();
        if (GameMode == "DECIMALS") {
            return score_frac;
        }
        // converting decimal to fraction
        int index = score_frac.IndexOf(".");
        if(index==-1){
            return score_frac;
        }
        string decimal_part = score_frac.Substring(index);
        string num = score_frac.Substring(0,index);
        string final_Score;
        if(num=="0"){
            final_Score = fractionPairs[decimal_part];
        }else{
            final_Score = num + " " + fractionPairs[decimal_part];
        }   
        return final_Score;
        
    }
    
    public static string DisplayGoalScore(){
        return GameMode == "FRACTIONS" ? goalScoreFraction : goalScore.ToString();
    }


    void scoreConfig()
    {
        ballsLeft.SetActive(false);
        Score = 0;
        Log log = new Log("PRE-ROUND", "NO SHOT", Score); // double check this
        //RestClient.Post("https://fractionball2022-default-rtdb.firebaseio.com/" + GameHandler.playerId + "/fball.json", log);
        if(TaskGenerator.scenes.Count == 0) {
            loadTest();
            return;
        }
       
        currentScene = TaskGenerator.scenes.Peek();
        if(currentScene.gameSetting=="EXACTLY" || currentScene.gameSetting=="EXACTLY GHOST"){
            //Goal Score Generator Pt. 1
            //If no goalScore is given, assign it a random value between 1 and 5. Otherwise, give it whatever it says.
            if (currentScene.goalScore == "0") {
                goalScore = Random.Range(1, 6);

                if (goalScore != 5) {
                    int denominator = currentScene.notation == "fourths" ? 4 : currentScene.notation == "thirds" ? 3 : -1; 
                    int numerator = Random.Range(0, denominator);
                    if(goalScore==1 && numerator==0) {
                        numerator = 1;
                    }
                    double fractionScore = System.Math.Round((double)numerator/denominator, 2);

                    if(numerator == 0){
                        goalScoreFraction = goalScore.ToString();
                    } else{
                        goalScoreFraction = goalScore.ToString() + " " + numerator.ToString() + "/" + denominator.ToString();
                    }

                    goalScore+= fractionScore;
                }
            } else {
                goalScoreFraction = currentScene.goalScore;
                if(currentScene.goalScore.Length == 1) {
                    goalScore = currentScene.goalScore[0]-'0';
                } else {
                    int denominator = currentScene.goalScore[4]-'0';
                    int numerator = currentScene.goalScore[2]-'0';
                    goalScore = currentScene.goalScore[0]-'0' + System.Math.Round((double)numerator/denominator, 2);
                }
            }

            originalGoalScore = goalScore; // Analytics, do not touch this for now

            numberOfBalls = getNumberOfBalls(goalScore);
        }else if (currentScene.gameSetting=="RAPID FIRE"){
            // gameSetting = "RAPID FIRE";
            timerActive = true;
            rapidTotalTime = 60.0f;
            goalScore = 10000;
        }
        GameMode = currentScene.representation;
        unlimitedShots = !currentScene.limitedShots;
        //This affects the screen that gives you information about your current round
        // this UI setting is for RAPID FIRE
        if(currentScene.gameSetting == "RAPID FIRE"){
            GameMode = currentScene.representation;
            numberOfBalls = 100000;
            introText_one.text = "For this round, you have 1 minute";
            introText_two.text = "Try and make as much as you can with the LEAST number of shots";
            introText_three.text = "";
            introText_four.text = "";
            unlimitedShots = true;
        } else if ((currentScene.representation == "FRACTIONS") && (currentScene.limitedShots == false)) {
            numberOfBalls = 100000;
            introText_one.text = "For this round, score EXACTLY " + DisplayGoalScore();
            introText_two.text = "Try and make " + DisplayGoalScore() + " with the LEAST number of shots";
            introText_three.text = "Round Boost: You have as many shots as you want!";
            introText_four.text = "";
        } else if ((currentScene.representation == "DECIMALS") && (currentScene.limitedShots == false)) {
            numberOfBalls = 100000;
            introText_one.text = "For this round, score EXACTLY " + goalScore;
            introText_two.text = "Try and make " + DisplayGoalScore() + " with the LEAST number of shots";
            introText_three.text = "Round Boost: You have as many shots as you want!";
            introText_four.text = "";
        } else if ((currentScene.representation == "FRACTIONS") && (currentScene.limitedShots == true)) {
            introText_one.text = "For this round, score EXACTLY " + DisplayGoalScore();
            introText_two.text = "You only have " + numberOfBalls + " shots.";
            introText_three.text = "Round Boost: Your player will never miss a shot!";
            introText_four.text = "";
        } else if ((currentScene.representation == "DECIMALS") && (currentScene.limitedShots == true)) {
            introText_one.text = "For this round, score EXACTLY " + goalScore;
            introText_two.text = "You only have " + numberOfBalls + " shots.";
            introText_three.text = "Round Boost: Your player will never miss a shot!";
            introText_four.text = "";
        }

        //This affects the canvas
        if (currentScene.representation == "DECIMALS")
        {
            fractionCourtLabels.SetActive(false);
            decimalCourtLabels.SetActive(true);
            spriteArray = decimalspriteArray;

        } else if (currentScene.representation == "FRACTIONS"){
            spriteArray = fractionspriteArray;
            decimalCourtLabels.SetActive(false);
            fractionCourtLabels.SetActive(true);
        }
        
        // set exactlyGhost mode to true if gameSetting in EXACTLY GHOST
        if(currentScene.gameSetting == "EXACTLY GHOST"){
            isExactlyGhost = true;
        }
        goalString = DisplayGoalScore();
        introButton.onClick.RemoveAllListeners();
        introButton.onClick.AddListener(StartGame);
        introButtonText.text = "Start";

    }

    void url() {
        Application.OpenURL("https://jlopez616.github.io/fractionball/experiment.html?id=" + playerId);
    }

    void StartGame() {

        fourths_spaces.SetActive(true); //spaces.SetActive(true) TODO: Fix
        // set rapid timer active for RAPID FIRE
        if(timerActive){
            rapidTimeStart = Time.time;
        }
        //UI changes
        if (unlimitedShots == false)
        {
            ballsLeft.SetActive(true);
            ballOne.SetActive(true);
            ballTwo.SetActive(true);
            ballThree.SetActive(true);
            ballFour.SetActive(true);
            ballFive.SetActive(true);

            ballsRemaining = numberOfBalls;
        }

        // numberline.SetActive(true);
          // display target only when in EXACTLY MODE
        if(!timerActive)
            targetText.text = "Target: " + goalString;
        coachText.text = "3..2..1..Shoot!";

        shootButton.SetActive(true);
        IntroPanel.SetActive(false);
        IntroUI.SetActive(false);
        mainCharacter.SetActive(true);

        // set rapid timer active for RAPID FIRE
        
        if(timerActive){
            rapidTimeStart = Time.time;
        } 

        //analytics
        timer = 0;
        round_num_of_shots = 0;
        round_num_of_movements = 0;
        preplan_time = 0;
        movement_time = 0;

        //control
        shotInProgress = false;
        gameInProgress = true;

    }

    void EndGame() {

        //TODO: REWORK, BIG TIME :D
        //Control
        shotInProgress = false;
        gameInProgress = false;


        //analytics
        Shoot.endTime = Time.time;
        total_round_time = movement_time;
        total_game_time += movement_time;
        total_num_of_movements += round_num_of_movements;
        total_num_of_shots += round_num_of_shots;

        //UI
        ballsLeft.SetActive(false);
        ballOne.SetActive(false);
        ballTwo.SetActive(false);
        ballThree.SetActive(false);
        ballFour.SetActive(false);
        ballFive.SetActive(false);
        mainCharacter.SetActive(false);

        if(currentScene.gameSetting == "RAPID FIRE"){
            IntroUI.SetActive(true);
            IntroPanel.SetActive(true);
            shootButton.SetActive(false);
            numberline.SetActive(false);
            coachText.text = "";
            targetText.text = "";
            timerText.text = "";
            introText_one.text = "Congratulations! You scored " + ScoreToFraction(Score) + " points!";
            introText_two.text = "";
            introText_three.text = "";
            introText_four.text = "";
        }else if(currentScene.gameSetting == "EXACTLY GHOST"){
            // blankFourth.SetActive(true);
            // astronaut.SetActive(true);
            enterscore.gameObject.SetActive(true);
            submitscore.gameObject.SetActive(true);
            if(currentScene.representation == "FRACTIONS"){
                fractionCourtLabels.SetActive(false);
            }else{
                decimalCourtLabels.SetActive(false);
            }
            coachText.text = "";
            targetText.text = "";
            submitscore.onClick.AddListener(onScoreSubmit);
            
        }else if (Score == goalScore)
        {

            IntroUI.SetActive(true);
            IntroPanel.SetActive(true);
            shootButton.SetActive(false);
            // numberline.SetActive(false);
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

        } else {
            IntroUI.SetActive(true);
            IntroPanel.SetActive(true);
            shootButton.SetActive(false);
            // numberline.SetActive(false);
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
        introButton.onClick.RemoveAllListeners();
        introButton.onClick.AddListener(scoreConfig);
        TaskGenerator.scenes.Dequeue();
    }




    void loadTest()
    {
        IntroPanel.SetActive(true);
        introButtonText.text = "Click me!";
        introButton.onClick.AddListener(url);
        introText_one.text = "Nice job! Thank you for playing!";
        introText_two.text = "";
        introText_three.text = "";
        introText_four.text = "";
    }


    //THERE HAS TO BE A MORE ELEGANT WAY TO DO THIS
    // made it more readable
    int getNumberOfBalls(double score)
    {
        if(score <= 1){
            return 1;
        }
        return score <= 4 ? (int)Ceiling(score) : 5;
    }

    public static Dictionary<string, string> fractionPairs = new Dictionary<string, string>() {
            //Proposed change: fraction would be default mode, then convert to decimal
        {".25", "1/4"},
        {".5", "2/4"},
        {".75", "3/4"},
        {".33", "1/3"},
        {".67", "2/3"},
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
        {.67, 9999},
        {0, 0},
        };


    // Update is called once per frame
    void Update()
    {
        // Debug.Log(Score);
        timer += Time.deltaTime;
        if (gameInProgress == true) {
            movement_time = timer;
            if(timerActive){
                float elapsedTime = Time.time - rapidTimeStart;
                float remainingtime = rapidTotalTime - elapsedTime;
                // Debug.Log(remainingtime);
                int remainder = (int) remainingtime;
                timerText.text = "Time left: " + remainder.ToString();
                if(remainingtime<=0.0f){
                    timerActive = false;
                    timerText.text = "";
                    rapidTimeStart = 0.0f;
                    EndGame();
                }
            } else if (unlimitedShots == false)
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

            }else {
                if (Score >= goalScore)
                {
                    EndGame();
                }
            }

            //Debug.Log(timer);


            // if (Score < goalScore)
            // {
            //     numberLineImage.sprite = spriteArray[numberLinePairs[Score]];
            // }

        }
       
        
    }
     // temp func to check if score entered matches the goalstring
    void onScoreSubmit(){
        
        string userscore = enterscore.text;
        IntroUI.SetActive(true);
        IntroPanel.SetActive(true);
        introText_two.text = "";
        introText_three.text = "";
        introText_four.text = "";
        // Debug.Log(userscore);
        // Debug.Log("goal"+goalString);
        if(userscore != goalString){ 
            introText_one.text = "Oh no you guessed wrong score, the actual target was " + goalString;
        }else{
            string temp_score = Score.ToString();
            introText_one.text = "Oh no you entered wrong score, the actual score you secured was " + Score;
            if(currentScene.representation == "FRACTIONS"){
                temp_score = ScoreToFraction(Score);
                introText_one.text = "Oh no you entered wrong score, the actual score you secured was " + ScoreToFraction(Score);
            }
            if(userscore == temp_score){
                introText_one.text = "Congratulations! You got “exactly” " + goalString + " points!";
            }
        }
        enterscore.gameObject.SetActive(false);
        submitscore.gameObject.SetActive(false);
    }

}