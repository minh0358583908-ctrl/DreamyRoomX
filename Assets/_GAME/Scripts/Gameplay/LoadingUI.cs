using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadingUI : MonoBehaviour
{
    public Slider bar;

    void Start()
    {
        StartCoroutine(Load());
    }

    IEnumerator Load()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync("Level_1");

        while (!op.isDone)
        {
            float progress = Mathf.Clamp01(op.progress / 0.9f);
            bar.value = progress;
            yield return null;
        }
    }
}