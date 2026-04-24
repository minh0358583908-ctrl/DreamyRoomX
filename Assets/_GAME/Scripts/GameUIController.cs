using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    public TMP_InputField input;
    public Button button;

    public void OnButtonClick()
    {
        string levelInput = input.text;
        Debug.Log(levelInput);
        int  level = int.Parse(levelInput);
        GameplayController.Instance.PlayLevel(level);


    }

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(OnButtonClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
