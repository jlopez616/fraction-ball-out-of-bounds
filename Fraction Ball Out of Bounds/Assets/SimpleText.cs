using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleText : MonoBehaviour
{
    public static Text thisText;

    private Dictionary<string, string> guiPairs = new Dictionary<string, string>() {
        {".25", "1/4"},
        {".5", "1/2"},
        {".75", "3/4"},
        {"1", "1"}
        };

    private Dictionary<string, string> decimalPairs = new Dictionary<string, string>() {
        {"1/4", ".25"},
        {"1/2", ".5"},
        {"3/4", ".75"},
        {"1", "1" }
        };
    // Start is called before the first frame update
    void Start()
    {
        if (GameHandler.GameMode == "FRACTIONS")
        {
            thisText.text = guiPairs[thisText.text];
        }
        else
        {
            thisText.text = decimalPairs[thisText.text];
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(thisText.text);
        if (GameHandler.GameMode == "FRACTIONS")
        {
            thisText.text = guiPairs[thisText.text];
        } else
        {
            thisText.text = decimalPairs[thisText.text];
        }
    }
}
