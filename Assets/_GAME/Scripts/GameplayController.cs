using Spine.Unity;
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
        PlayLevel(0);
    }

    private void PlayLevel(int level)
    {
        if (curLevel != null) Destroy(curLevel.gameObject);
        curLevel = Instantiate(listLevel[level],transform);
        curLevel.Init();
    }
}
