using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;

public class TaskGenerator : MonoBehaviour {

    //Queue to store scenes
    public static Queue<GameState> scenes = new Queue<GameState>();
    public static System.Random random = new System.Random();

    public static string GameSetting;

    //simple shuffle implementation
    void shuffle (GameState[] scene, int n)
    {
        for (int i = 0; i < n; i++)
        {
            int r = i + random.Next(scene.Length - i);
            GameState temp = scene[r];
            scene[r] = scene[i];
            scene[i] = temp;
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        //CHANGE THIS LINE BASED ON GAME SETTING
        // GameSetting = "EXACTLY";
        // GameSetting = "RAPID FIRE"; // to test rapid fire
        GameSetting = "MAY 2023 STUDY";

        //1 is Exactly
        //2 is Make it Count
        //3 is Rapid Fire

        bool fourths_scene = false; // set this to true if notation is fourths otherwise false

        if (fourths_scene) {
            if(SceneManager.GetActiveScene().name == "MainScene_3rd") {
                SceneManager.LoadScene (sceneName:"MainScene");
                return;
            }
        } else {
            if(SceneManager.GetActiveScene().name == "MainScene") {
                SceneManager.LoadScene (sceneName:"MainScene_3rd");
                return;
            }
        }
        

        // every possible level configuration of the intervention or evaluation will be generated here
        // 
        // GameState exactly_decimal_limited = new GameState("DECIMALS", "0", "thirds", 0, true, "day");
        // GameState exactly_decimal_unlimited = new GameState("DECIMALS", "0", "thirds", 0, false, "day");
        // GameState exactly_fraction_limited = new GameState("FRACTIONS", "0", "thirds", 0, true, "day");
        // GameState exactly_fraction_unlimited = new GameState("FRACTIONS", "0", "thirds", 0, false, "day");
        // 

        // To test thirds condition 
        /*
        GameState exactly_decimal_limited = new GameState("DECIMALS", "0", "thirds", 0, true, "day");
        GameState exactly_decimal_unlimited = new GameState("DECIMALS", "0", "thirds", 0, false, "day");
        GameState exactly_fraction_limited = new GameState("FRACTIONS", "0", "thirds", 0, true, "day");
        GameState exactly_fraction_unlimited = new GameState("FRACTIONS", "0", "thirds", 0, false, "day");
        */

        // rapid fire
        GameState rapid_fire_fractions = new GameState("FRACTIONS", "0", "thirds", 0, false, "day", "RAPID FIRE"); 
        GameState rapid_fire_decimals = new GameState("DECIMALS", "0", "thirds", 0, false, "day", "RAPID FIRE"); 

        // To test Exactly flip
        //GameState exactly_decimal_limited = new GameState("FRACTIONS", "0", "thirds", 0, true, "day", "EXACTLY FLIP");
        //GameState exactly_decimal_unlimited = new GameState("FRACTIONS", "0", "thirds", 0, false, "day", "EXACTLY FLIP");
        //GameState exactly_fraction_limited = new GameState("FRACTIONS", "0", "thirds", 0, true, "day", "EXACTLY FLIP");
        //GameState exactly_fraction_unlimited = new GameState("FRACTIONS", "0", "thirds", 0, false, "day", "EXACTLY FLIP"); 

        //May 2023 Study
        GameState exactly_decimal_limited = new GameState("DECIMALS", "0", "thirds", 0, true, "day", "EXACTLY");
        GameState exactly_decimal_unlimited = new GameState("DECIMALS", "0", "thirds", 0, false, "day", "EXACTLY");
        GameState exactly_fraction_limited = new GameState("FRACTIONS", "0", "thirds", 0, true, "day", "EXACTLY");
        GameState exactly_fraction_unlimited = new GameState("FRACTIONS", "0", "thirds", 0, false, "day", "EXACTLY");
        GameState exactly_letters_decimal_limited = new GameState("DECIMALS", "0", "thirds", 0, true, "day", "EXACTLY LETTERS");
        GameState exactly_letters_decimal_unlimited = new GameState("DECIMALS", "0", "thirds", 0, false, "day", "EXACTLY LETTERS");
        GameState exactly_letters_fraction_limited = new GameState("FRACTIONS", "0", "thirds", 0, true, "day", "EXACTLY LETTERS");
        GameState exactly_letters_fraction_unlimited = new GameState("FRACTIONS", "0", "thirds", 0, false, "day", "EXACTLY LETTERS");
        GameState exactly_flip_unlimited = new GameState("FRACTIONS", "0", "thirds", 0, false, "day", "EXACTLY FLIP");
        GameState exactly_flip_limited = new GameState("FRACTIONS", "0", "thirds", 0, true, "day", "EXACTLY FLIP");




        switch (GameSetting) {
            case "MAY 2023 STUDY":
                scenes.Enqueue(rapid_fire_fractions); 
                
                GameState[] orderedScenes = {exactly_decimal_limited,
                exactly_decimal_unlimited,
                exactly_fraction_limited,
                exactly_fraction_unlimited,
                exactly_letters_decimal_limited,
                exactly_letters_decimal_unlimited,
                exactly_letters_fraction_limited,
                exactly_letters_fraction_unlimited,
                exactly_flip_unlimited,
                exactly_flip_limited};

                shuffle(orderedScenes, 10);

                for (int i=0; i < orderedScenes.Length; i++)
                {
                    scenes.Enqueue(orderedScenes[i]);
                }
                break;

            case "EXACTLY":
               // scenes.Enqueue(exactly_decimal_limited);
               // scenes.Enqueue(exactly_decimal_unlimited);
               // scenes.Enqueue(exactly_fraction_limited);
               // scenes.Enqueue(exactly_fraction_unlimited);
                scenes.Enqueue(exactly_flip_unlimited);
                scenes.Enqueue(exactly_flip_limited);
                break;
            case "RAPID FIRE":
                scenes.Enqueue(rapid_fire_fractions);
                // scenes.Enqueue(exactly_decimal_limited);
                break;
            case "EXACTLY FLIP":
                // scenes.Enqueue(exactly_decimal_limited);
                // scenes.Enqueue(exactly_decimal_unlimited);
                // scenes.Enqueue(exactly_fraction_limited);
                // scenes.Enqueue(exactly_fraction_unlimited);
                break;
            case "EXACTLY LETTERS":
                scenes.Enqueue(exactly_decimal_limited);
                //scenes.Enqueue(exactly_fraction_unlimited);
               // scenes.Enqueue(exactly_fraction_limited);
                //scenes.Enqueue(exactly_decimal_unlimited);
               // 
                scenes.Enqueue(exactly_letters_decimal_limited);
                scenes.Enqueue(exactly_letters_fraction_unlimited);
                scenes.Enqueue(exactly_letters_fraction_limited);
                scenes.Enqueue(exactly_letters_decimal_unlimited);
                scenes.Enqueue(exactly_flip_unlimited);
                scenes.Enqueue(exactly_flip_limited);
                break;
        }
        //if goalScoreValue == 0; Game Compiler will randomly assign a score



    }


}