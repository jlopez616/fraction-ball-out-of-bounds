using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class TaskGenerator : MonoBehaviour {

    //Queue to store scenes
    public static Queue<GameState> scenes = new Queue<GameState>();
    public static System.Random random = new System.Random();

    public static string GameSetting;

    //simple shuffle implementation
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
        //CHANGE THIS LINE BASED ON GAME SETTING
    // GameSetting = "EXACTLY";
    GameSetting = "RAPID FIRE"; // to test rapid fire
        //1 is Exactly
        //2 is Make it Count
        //3 is Rapid Fire
    
    //every possible level configuration of the intervention or evaluation will be generated here
    GameState exactly_decimal_limited = new GameState("DECIMALS", 0, "fourths", 0, true, "day");
    GameState exactly_decimal_unlimited = new GameState("DECIMALS", 0, "fourths", 0, false, "day");
    GameState exactly_fraction_limited = new GameState("FRACTIONS", 0, "fourths", 0, true, "day");
    GameState exactly_fraction_unlimited = new GameState("FRACTIONS", 0, "fourths", 0, false, "day");
    GameState rapid_fire_fractions = new GameState("FRACTIONS", 0, "fourths", 0, false, "day","RAPID FIRE");   


        switch (GameSetting) {
            case "EXACTLY":
                scenes.Enqueue(exactly_decimal_limited);
                scenes.Enqueue(exactly_decimal_unlimited);
                scenes.Enqueue(exactly_fraction_limited);
                scenes.Enqueue(exactly_fraction_unlimited);
                break;
            case "RAPID FIRE":
                scenes.Enqueue(rapid_fire_fractions);
                break;

        }
    //if goalScoreValue == 0; Game Compiler will randomly assign a score



    }


}