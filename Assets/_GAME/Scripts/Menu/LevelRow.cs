using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class LevelRow : MonoBehaviour
{
    public Image levelImage;
    public Text levelText; 
    public Button openButton;

    private int levelIndex;

    public void Setup(int index, Sprite sprite)
    {
        
        levelIndex = index;

        if (levelImage != null) levelImage.sprite = sprite;

     
        if (levelText != null) levelText.text = "Level " + (index + 1);

        openButton.onClick.RemoveAllListeners();
        openButton.onClick.AddListener(OpenLevel);
    }

    void OpenLevel()
    {
        GameplayController.Instance.StartLevel(levelIndex);
        
        transform.root.gameObject.SetActive(false);
    }





}
