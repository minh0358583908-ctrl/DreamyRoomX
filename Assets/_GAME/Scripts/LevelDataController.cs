using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _GAME.Scripts;
using DG.Tweening;

public class LevelDataController : MonoBehaviour
{
    public List<ItemBaseCtrl> listItem;
    private int _itemSpawnCounter;

    public void Init()
    {
        foreach (var item in listItem)
        {
            //item.transform.SetParent(transform);
            //item.RandomPos();
            //item.PlayAninationActive();
            item.gameObject.SetActive(false);   
        }
        //    item.Init();

        //foreach (var item in listItem)
        //    item.RandomPos();
      
    }
    public bool SpawnItem()
    {
        if (_itemSpawnCounter >= listItem.Count) return false;
        var nextItem = listItem[_itemSpawnCounter];
        nextItem.transform.SetParent(transform);
        nextItem.gameObject.SetActive(true);
        nextItem.transform.position = GameplayController.Instance.boxAnim.transform.position;
        var posX = Random.Range(-5, 5);
        var posY = Random.Range(-5, 5);
        var posZ = nextItem.successPos.z;
        var randPos = new Vector3(posX, posY, posZ);
        nextItem.transform.DOJump(randPos, 2f, 1, 0.5f)
            .SetEase(Ease.OutQuad);
        _itemSpawnCounter++;
        return true;
    }
}
