using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;


public class Test : MonoBehaviour
{
    public Text text;
    public Button sendData;

    [DllImport("__Internal")]
    public static extern void GetJSON(string path, string objectName, string callback);

    [DllImport("__Internal")]
    public static extern void PushJSON(string path, string value);

    private void Start()
    {
        Button btn = sendData.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);

        GetJSON("example", gameObject.name, "OnRequestSuccess");
    }

    private void OnRequestSuccess(string data)
    {
        text.color = Color.blue;
        text.text = data;
    }

    private void OnClick()
    {
        PushJSON("example", "WOOP DE FREAKING DO");
        Debug.Log("Success");

    }

}
