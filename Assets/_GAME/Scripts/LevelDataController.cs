using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _GAME.Scripts;

public class LevelDataController : MonoBehaviour
{
    public List<ItemBaseCtrl> listItem;


    public void Init()
    {
        foreach (var item in listItem)
            item.Init();

        foreach (var item in listItem)
            item.RandomPos();
      
    }
}
