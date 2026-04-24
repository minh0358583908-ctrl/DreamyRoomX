using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public static GameplayController Instance;
    private void Awake()
    {
        Instance = this;
    }

    public List<LevelDataController> listLevel;
    public LevelDataController curLevel;

    public SkeletonAnimation boxAnim;

    // Start is called before the first frame update
    void Start()
    {
        //PlayLevel(1);
    }

    public void PlayLevel(int level)
    {
        if (curLevel != null) Destroy(curLevel.gameObject);
        curLevel = Instantiate(listLevel[level],transform);
        curLevel.Init();
        curLevel.transform.position = Vector3.zero;

        boxAnim.gameObject.SetActive(true);

    }
    public void StartLevel(int index)
    {
        

        GameObject newLevel = Instantiate(listLevel[index].gameObject);

       
        newLevel.transform.position = Vector3.zero;

        curLevel = newLevel.GetComponent<LevelDataController>();
    }
    public void OnClickBox()
    {
       
        if (curLevel != null)
        {

            curLevel.SpawnNextItem();
        }
    }


}
