using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelListManager : MonoBehaviour
{
    public GameObject levelRowPrefab;
    public Transform parent;
    public Sprite[] levelImages;
    public GameObject levelPanel;

    void Start()
    {
        if (levelPanel != null)
        {
            levelPanel.SetActive(true);
        }
            for (int i = 0; i < levelImages.Length; i++)
        {
            GameObject obj = Instantiate(levelRowPrefab, parent);
            LevelRow row = obj.GetComponent<LevelRow>();

            
            row.Setup(i, levelImages[i]);
        }
    }
}
