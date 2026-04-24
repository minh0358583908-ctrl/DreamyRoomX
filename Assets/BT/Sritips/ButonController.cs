using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButonController : MonoBehaviour
{
    public TMP_InputField input;
    public Button button;
    public TMP_InputField inputA;
    public TMP_InputField inputB;


    public void OnButtonClick()
    {
        string a = inputA.text;
        string b = inputB.text;        
        Debug.Log(a + b);
        
        //Debug.Log(input.text);
    }
    
    void Start()
    {
        button.onClick.AddListener(OnButtonClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
