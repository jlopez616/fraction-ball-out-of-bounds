using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using Proyecto26;
using static System.Math;
using TMPro;
public class GameGenerator : MonoBehaviour

{
    public static string language;
    public GameObject IntroUI; //Describes the whole Intro UI System
    public Text introText_one; //First line for intro
    public Text introText_two; //Second line for intro
    public Text introText_three; //Third line for intro
    public Text introText_four; //Fourth line for intro
    public GameObject IntroPanel; //Background for intro screen
    public GameObject languageButtons;
    public GameObject navigationButtons;
    public Button introButton;
    public Button englishButton;
    public Button spanishButton;
    public Text introButtonText; //Text for the continue button

    public static string playerId; //ID of user

    public static string notation; //fractions or decimals
    public static string representation; //thirds, fourths, fifths, sixths
    public static bool timerActive ; // to set timer active for RAPID FIRE MODE
    public static float rapidTimeStart; // to start a time value for RAPID FIRE MODE
    public static float rapidTotalTime; // total timer for RAPID FIRE
    public static string user_input; // DATA COLLECTED FROM USER;

    public int number_of_problems = 4; // number of problems to give to player

    //Field Related
    public GameObject fractionCourtLabels;
    public GameObject decimalCourtLabels;
    public GameObject fourths_spaces;
    public GameObject mainCharacter;

    //Numberline Related
    public Image numberLineImage; //should be blank
    public Sprite[] fractionspriteArray;
    public Sprite[] decimalspriteArray;
    private static Sprite[] spriteArray;

    //UI Related
    public GameObject numberline;
    public GameObject shootButton;
    public GameObject InputSequence;
    public TMP_InputField InputSeq;
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
    public static bool actualFractionCourt;
    public static bool flipTermination;
    public static int shotcount;
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
    public static string GameSetting;
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
    public static bool isFractionCourt;


    //Queue to store scenes
    public void GetQueryVariable(string id)
    {
        playerId = id;
    }
    // Start is called before the first frame update
    void Start()
    {
        //This part of code used to rearanges the scenes into a random order
        IntroPanel.SetActive(true);
        navigationButtons.SetActive(false);
        introText_one.text = "Please pick your preferred language, " + playerId + "!";
        introText_three.text = "Por favor elige tu idioma preferido, " + playerId + "!";
        englishButton.onClick.AddListener(setEnglish);
        spanishButton.onClick.AddListener(setSpanish);



    }
    void setEnglish() {
        language = "ENGLISH";
        introOne();
    }

    void setSpanish() {
        language = "SPANISH";
        introOne();
    }

    //Want better system for coding names:

    void introOne()
    {
        //Suggestion: array of strings
        //New text generator file
        englishButton.onClick.RemoveAllListeners();
        spanishButton.onClick.RemoveAllListeners();
        languageButtons.SetActive(false);
        navigationButtons.SetActive(true);
        introText_one.text = (language == "ENGLISH") ? "Challenge 1: Score EXACTLY a number that you get.": "Desafío 1: Marque EXACTAMENTE un número que obtenga.";
        introText_two.text = (language == "ENGLISH") ? "Challenge 2: Don't score over it or you will lose.": "Desafío 2: No lo superes o perderás.";
        introText_three.text = "";
        introText_four.text = "";
        introButtonText.text = (language == "ENGLISH") ? "Continue": "Continuar";
        introButton.onClick.RemoveAllListeners();
        introButton.onClick.AddListener(gameConfigOne);

    }


    void gameConfigOne()
    {
        introText_one.text = (language == "ENGLISH") ? "Click on the court to move." : "Haz clic en la cancha para moverte";
        introText_two.text = (language == "ENGLISH") ? "Click on the SHOOT button to shoot a basketball." : "Haz clic en el botón SHOOT para disparar una pelota de baloncesto.";
        introText_three.text = "";
        introText_four.text = "";
        introButton.onClick.RemoveAllListeners();
        introButton.onClick.AddListener(gameConfigTwo);

    }


    void gameConfigTwo()
    {
        introText_one.text = (language == "ENGLISH") ? "Pay attention!" : "¡Ponga atención!";
        introText_two.text = (language == "ENGLISH") ? "At the start of each round, one of the game rules will change." : "Al comienzo de cada ronda, una de las reglas del juego cambiará.";
        introText_three.text = "";
        introText_four.text = "";
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
        InputSequence.SetActive(false);
        Score = 0;
        Log log = new Log("ROUND START", "NO SHOT", Score); // double check this
        RestClient.Post("https://fraction-ball-2023-test-default-rtdb.firebaseio.com/" + GameGenerator.playerId + "/fball.json", log);
        if(TaskGenerator.scenes.Count == 0) {
            loadTest();
            return;
        }
        Debug.Log(TaskGenerator.scenes.Count);
        currentScene = TaskGenerator.scenes.Peek();
        if(currentScene.gameSetting != "RAPID FIRE"){
            //Goal Score Generator Pt. 1
            //If no goalScore is given, assign it a random value between 1 and 5. Otherwise, give it whatever it says.
            if (currentScene.goalScore == "0") {
                goalScore = Random.Range(2, 6);

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
                } else {
                    goalScoreFraction = goalScore.ToString();
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
        } else {
            // gameSetting = "RAPID FIRE";
            timerActive = true;
            rapidTotalTime = 60.0f;
            goalScore = 10000;
        }
        GameMode = currentScene.representation;
        unlimitedShots = !currentScene.limitedShots;
        GameSetting = currentScene.gameSetting;
        user_input = "";
        //This affects the screen that gives you information about your current round
        // this UI setting is for RAPID FIRE
        if(currentScene.gameSetting == "RAPID FIRE"){
            numberOfBalls = 100000;
            introText_one.text = (language == "ENGLISH") ? "For this round, practice shooting!": "¡Para esta ronda, practica tiro!";
            introText_two.text = "";
            introText_three.text = (language == "ENGLISH") ? "Score as high as you can, with the fewest number of shots." : "¡Anota lo más alto que puedas, con el menor número de tiros.";
            introText_four.text = "";
        } else if(currentScene.gameSetting == "EXACTLY" || currentScene.gameSetting == "EXACTLY LETTERS") {
            introText_one.text = (language == "ENGLISH") ? "For this round, score EXACTLY " + DisplayGoalScore() + " with the LEAST number of shots." :  "Para esta ronda, marque EXACTAMENTE " + DisplayGoalScore() + " con la MENOR cantidad de tiros.";
            introText_three.text = "";
            if(unlimitedShots == true){
                numberOfBalls = 100000;
                introText_two.text = (language == "ENGLISH") ? "Special Rule: You have as many shots as you want!" :  "Regla Especial: ¡Tienes tantos tiros como quieras!";
                introText_four.text = "";
            } else {
                introText_two.text = (language == "ENGLISH") ? "Special Rule: You only have " + numberOfBalls + " shots, but your player will never miss a shot!" : "Regla especial: solo tienes " + numberOfBalls + " tiros, ¡pero tu jugador nunca fallará un tiro!";
                introText_four.text = "";
            }
        } else if(currentScene.gameSetting == "EXACTLY FLIP") {
            introText_one.text = (language == "ENGLISH") ? "For this round, score EXACTLY " + DisplayGoalScore() + " with the LEAST number of shots." : "Para esta ronda, marque EXACTAMENTE " + DisplayGoalScore() + " con la MENOR cantidad de tiros.";
            introText_three.text = "";
            if(unlimitedShots == true){
                numberOfBalls = 100000;
                introText_two.text = (language == "ENGLISH") ? "Special Rule: Only shoot from the side of the court we tell you to!" : "Regla Especial: ¡¡Dispara solo desde el lado de la cancha que te indiquemos!";
                introText_four.text = "";
            } else {
                introText_two.text = (language == "ENGLISH") ? "Special Rule: Only shoot from the side of the court we tell you to, with " + numberOfBalls + " shots!" : "Regla Especial: ¡¡Dispara solo desde el lado de la cancha que te indiquemos, con " + numberOfBalls + " tiros!";
                introText_four.text = "";
            }
        }

        //This affects the canvas
        if(currentScene.gameSetting == "EXACTLY FLIP") {
            fractionCourtLabels.SetActive(true);
            decimalCourtLabels.SetActive(true);
        } else if (currentScene.representation == "DECIMALS") {
            fractionCourtLabels.SetActive(false);
            decimalCourtLabels.SetActive(true);
            spriteArray = decimalspriteArray;
        } else if (currentScene.representation == "FRACTIONS") {
            spriteArray = fractionspriteArray;
            decimalCourtLabels.SetActive(false);
            fractionCourtLabels.SetActive(true);
        }

        if(currentScene.gameSetting == "EXACTLY LETTERS") {
            introButton.onClick.RemoveAllListeners();
            introButton.onClick.AddListener(exactlyLetters);
        } else {
            goalString = DisplayGoalScore();
            introButton.onClick.RemoveAllListeners();
            introButton.onClick.AddListener(startGame);
            introButtonText.text = (language == "ENGLISH") ? "Start" : "Comenzar";
        }
    }

    void exactlyLetters() {

        string sequence = "";
        if (currentScene.notation == "thirds") {
            if (currentScene.representation == "DECIMALS") {
                if (currentScene.limitedShots){
                    sequence = "N X C E";
                } else {
                    sequence = "G X W S";
                }
            } else {
                if (currentScene.limitedShots){
                    sequence = "P X V R";
                } else {
                    sequence = "A T S W";
                }
            }
        } else if(currentScene.notation == "fourths") {
            if (currentScene.representation == "DECIMALS") {
                if (currentScene.limitedShots){
                    sequence = "W E J L";
                } else {
                    sequence = "P L H W";
                }
            } else {
                if (currentScene.limitedShots){
                    sequence = "R B W P";
                } else {
                    sequence = "X W C N";
                }
            }
        }
        introText_one.text = (language == "ENGLISH") ?  "Bonus Challenge: Remember as many letters as you can, in the right order!" : "Desafío de bonificación: recuerda tantas letras como puedas, ¡en el orden correcto!";
        introText_two.text = sequence;
        introText_three.text = "";
        introText_four.text = "";
        goalString = DisplayGoalScore();
        introButton.onClick.RemoveAllListeners();
        introButton.onClick.AddListener(startGame);
        introButtonText.text = (language == "ENGLISH") ? "Start" : "Comenzar";
    }

    void url() {
        Application.OpenURL("https://stem-lab.vercel.app/tol.html?id=" + playerId);
    }

    void startGame() {

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

        if (GameSetting == "EXACTLY FLIP") {
            Debug.Log("DONE");
            actualFractionCourt = true;
            flipTermination = false;
            shotcount = 0;
        }

        // numberline.SetActive(true);
          // display target only when in EXACTLY MODE
        if(!timerActive)
            targetText.text = (language == "ENGLISH") ?  "Goal \n" + DisplayGoalScore():  "Meta: \n" + DisplayGoalScore();
        if(GameSetting == "EXACTLY FLIP") {
            coachText.text = (language == "ENGLISH") ?  "Fraction side!" : "¡El lado de fracción!";
        } else 
            coachText.text = (language == "ENGLISH") ? "3..2..1..Shoot!" : "3..2..1..¡Disparar!";

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

        if(flipTermination == true) {
            IntroUI.SetActive(true);
            IntroPanel.SetActive(true);
            shootButton.SetActive(false);
            coachText.text = "";
            targetText.text = "";
            introText_one.text = (language == "ENGLISH") ? "Oh no, you scored from the wrong side of the court!" : "¡Oh no, anotaste desde el lado equivocado de la cancha!";
            introText_two.text = "";
            introText_three.text = "";
            introText_four.text = "";
            flipTermination = false;
        } else {
            if(currentScene.gameSetting == "RAPID FIRE") {
                IntroUI.SetActive(true);
                IntroPanel.SetActive(true);
                shootButton.SetActive(false);
                numberline.SetActive(false);
                coachText.text = "";
                targetText.text = "";
                timerText.text = "";
                introText_one.text = (language == "ENGLISH") ? "Congratulations! You got " + ScoreToFraction(Score) + " points!" : "¡Felicitaciones! ¡Obtuviste " + ScoreToFraction(Score) + " puntos!";
                introText_two.text = "";
                introText_three.text = "";
                introText_four.text = "";
            } else if (Score == goalScore) {
                IntroUI.SetActive(true);
                IntroPanel.SetActive(true);
                shootButton.SetActive(false);
                // numberline.SetActive(false);
                coachText.text = "";
                targetText.text = "";
                introText_one.text = (language == "ENGLISH") ? "Congratulations! You got “exactly” " + ScoreToFraction(Score) + " points!" : "¡Felicitaciones! ¡Obtuviste ”exactamente” " + ScoreToFraction(Score) + " puntos!";
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
                introText_one.text = (language == "ENGLISH") ? "Oh no, you scored " + ScoreToFraction(Score) + " points. You needed exactly " + goalString + " points instead." :  "Oh, no, anotó " + ScoreToFraction(Score) + " puntos. Necesitaba exactamente " + goalString + " puntos en su lugar.";
                introText_two.text = "";
                introText_three.text = "";
                introText_four.text = "";
            }
        }
        if (round_num_of_shots > getNumberOfBalls(Score))
        {
            excess_shots = excess_shots + (round_num_of_shots - getNumberOfBalls(Score));
        }
        
        if (currentScene.gameSetting == "EXACTLY LETTERS") {
            InputSequence.SetActive(true);
            introText_two.text = (language == "ENGLISH") ?  "Enter as many letters as you remember, in the right order, and press continue. " : "Ingrese tantas letras como recuerde, en el orden correcto, y presione continuar. ";
            introText_four.text = "";
            introButtonText.text = (language == "ENGLISH") ? "Continue" : "Continuar";
            introButton.onClick.RemoveAllListeners();
            introButton.onClick.AddListener(tryMe);
        } else{
            introButton.onClick.RemoveAllListeners();
            introButton.onClick.AddListener(scoreConfig);
            Log log = new Log("ROUND END", "NO SHOT", Score); // double check this
            RestClient.Post("https://fraction-ball-2023-test-default-rtdb.firebaseio.com/" + GameGenerator.playerId + "/fball.json", log);
            TaskGenerator.scenes.Dequeue();
        }
    }

    void tryMe() {
        user_input = InputSeq.text;
        InputSeq.text = "";
         Log log = new Log("ROUND END", "NO SHOT", Score); // double check this
        RestClient.Post("https://fraction-ball-2023-test-default-rtdb.firebaseio.com/" + GameGenerator.playerId + "/fball.json", log);
        TaskGenerator.scenes.Dequeue();
        scoreConfig();
    }


    void loadTest()
    {
        introText_one.text = (language == "ENGLISH") ?  "Nice job! Thank you for playing!" : "¡Buen trabajo! ¡Gracias por jugar!";
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
        timer += Time.deltaTime;
        if (gameInProgress == true) {
            movement_time = timer;
            if(timerActive){
                float elapsedTime = Time.time - rapidTimeStart;
                float remainingtime = rapidTotalTime - elapsedTime;
                // Debug.Log(remainingtime);
                int remainder = (int) remainingtime;
                timerText.text = (language == "ENGLISH") ? "Clock: \n " + remainder.ToString() : "Reloj: \n " + remainder.ToString();
                if(remainingtime<=0.0f){
                    timerActive = false;
                    timerText.text = "";
                    rapidTimeStart = 0.0f;
                    EndGame();
                }
            } else if (unlimitedShots == false) {
                if(flipTermination == true) {
                    EndGame();
                    return;
                }
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

            } else {
                if(flipTermination == true || Score >= goalScore) {
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


}