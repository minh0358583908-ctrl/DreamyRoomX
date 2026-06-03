using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : Singleton<GameUIController>
{
    public CanvasGroup uiHome;
    public List<Button> listLevel;
    public CanvasGroup uiGameplay;
    public Button btnBackHome;
    public CanvasGroup uiPopup;
    [Header("Popup")]
    public GameObject popupPauseGame;
    public GameObject popupSetting;
   

    public void Start()
    {
        uiHome.gameObject.SetActive(true);
        uiHome.alpha = 1f;
        uiGameplay.gameObject.SetActive(false);
        uiGameplay.alpha = 0f;
        uiPopup.gameObject.SetActive(true);
        uiPopup.alpha = 1f;
        if (popupPauseGame != null)
        {
            popupPauseGame.SetActive(false);
        }

        if (popupSetting != null)
        {
            popupSetting.SetActive(false);
        }
        for (var i = 0; i < listLevel.Count; i++)
        {
            var level = i;
            listLevel[i].onClick.AddListener(
                () => { OnClickButtonPlayLevel(level); });

        }
        btnBackHome.onClick.AddListener(OnClickButtonBackHome);
    }
    public void ShowPopupSetting()
    {
        popupSetting.SetActive(true);
    }

    public void HidePopupSetting()
    {
        popupSetting.SetActive(false);
    }
    public void ShowPopupPause()
    {
        popupPauseGame.SetActive(true);
        Time.timeScale = 0f;
    }
    public void HidePopupPause()
    {
        popupPauseGame.SetActive(false);
        Time.timeScale = 1f;
    }
    public void OnClickLevel(int levelIndex)
    {
        VibrationController.Instance.Vibrate();

        Debug.Log("Click Level " + levelIndex);
    }
    public void OpenSetting()
    {
        VibrationController.Instance.Vibrate();

        uiPopup.alpha = 1;
    }

    private void OnClickButtonPlayLevel(int level)
    {
        GameplayController.Instance.PlayLevel(level);
        uiHome.DOFade(0f, 0.25f).OnComplete(
            () => { uiHome.gameObject.SetActive(false); });
        uiGameplay.gameObject.SetActive(true);
        uiGameplay.DOFade(1f, 0.25f);
    }
    private void OnClickButtonBackHome()
    {
        GameplayController.Instance.CleanLevel();
        uiHome.gameObject.SetActive(true);
        uiHome.DOFade(1f, .025f);
        uiGameplay.DOFade(0f, 0.25f).OnComplete(
            () => { uiGameplay.gameObject.SetActive(false); });
    }
}


