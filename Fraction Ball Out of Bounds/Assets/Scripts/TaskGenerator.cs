using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class TaskGenerator : MonoBehaviour

{

    public Text introText_one; //First line for intro
    public Text introText_two; //Second line for intro
    public Text introText_three; //Third line for intro
    public Text introText_four; //Fourth line for intro
    public Button continueButton;
    public Text continueButtonText; //Text for the continue button

    public static string playerId; //ID of user

    public static string notation; //fractions or decimals
    public static string representation; //thirds, fourths, fifths, sixths

    public int number_of_problems = 4; // number of problems to give to player

    //A simple shuffle implementation for selecting the scenes
    private System.Random random = new System.Random();

    //Queue to store scenes
    public static Queue<int> scenes = new Queue<int> { };

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

    // Start is called before the first frame update
    void Start()
    {

        //This part of code rearanges the scenes into a random order
        int[] scenesRaw = {1, 2, 3, 4}; //TODO: Create function for scenes of any n length
        shuffle(scenesRaw, number_of_problems);
        for (int i=0; i < scenesRaw.Length; i++)
        {
            scenes.Enqueue(scenesRaw[i]);
        }

        Debug.Log(scenes);


    }

    //From here to line 113: Needs to be reworked majorly!
    //Want better system for coding names :

       void introOne()
    {    
        introText_one.text = "Time to play FRACTION BALL: EXACTLY!";
        introText_two.text = "Game Rules:";
        introText_three.text = "Challenge 1: Score EXACTLY a number that you get.";
        introText_four.text = "Challenge 2: Don't score over it or you will lose.";
        //Button continButton = continueButton.GetComponent<Button>();
        continueButtonText.text = "Continue";
        continueButton.onClick.RemoveAllListeners();
        continueButton.onClick.AddListener(gameConfig);

    }

    void introTwo()
    {

        introText_one.text = "In each game, I will call out a target number and you have to score " +
    "that number EXACTLY. Be very careful; if you score past the exact number," +
    "you automatically lose that round.";
        //Button continButton = continueButton.GetComponent<Button>();
        continueButton.onClick.RemoveAllListeners();
        continueButton.onClick.AddListener(gameConfig);
    }

    void introThree()
    {

        introText_one.text = "I will also only tell you your target number once at the start of each round, meaning you need to remember it " +
            "as you play.";
        Button continButton = continueButton.GetComponent<Button>();
        continueButton.onClick.RemoveAllListeners();
        continueButton.onClick.AddListener(gameConfig);
    }

    void gameConfig()
    {
        //startButton.SetActive(true);
        introText_one.text = "How to Play:";
        introText_two.text = "Click on the court to move.";
        introText_three.text = "Click on the SHOOT button to shoot a basketball.";
        introText_four.text = "Use the numberline to keep track of your score.";

        //Button continButton = continueButton.GetComponent<Button>();
        continueButton.onClick.RemoveAllListeners();
        //continButton.onClick.AddListener(scoreConfig);

    }

    // Update is called once per frame
    void Update()
    {}


}