using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _GAME.Scripts;
using DG.Tweening;

public class LevelDataController : MonoBehaviour
{
    public List<ItemBaseCtrl> listItem;
    private int _itemSpawnCounter = 0;
    public List<GameObject> allItems; 
    private int currentItemIndex = 0;
    public void Init()
    {
        _itemSpawnCounter = 0;
        foreach (var item in listItem)
        {
            //item.transform.SetParent(transform);
            //item.RandomPos();
            //item.PlayAninationActive();
            item.Init();
            item.gameObject.SetActive(false);   
        }

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
            .SetEase(Ease.OutQuad)
            .OnComplete(() => nextItem.PlayAnimationActive());
        _itemSpawnCounter++;
        return true;
    }
    [ContextMenu("AutoReference")]
    public void AutoReference()
    {
        AutoReferenceParent();
        AutoReferenceSameItem();
        AutoReferenceOther();
        UnityEditor.EditorUtility.SetDirty(this);
    }

    private void AutoReferenceParent()
    {
     
        listItem = new List<ItemBaseCtrl>();

       
        for (var i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i).GetComponent<ItemBaseCtrl>();
            if (child == null) continue;
              child.index = listItem.Count;
            listItem.Add(child);
            
            child.parent = null;
           
            AutoReferenceChild(child);
          
            //var oldPos = child.transform.localPosition;
            //child.transform.localPosition = new Vector3(oldPos.x, oldPos.y, child.index * -0.001f);
        }
    }

    private void AutoReferenceChild(ItemBaseCtrl item)
    {
       
        for (var i = 0; i < item.transform.childCount; i++)
        {
            var child = item.transform.GetChild(i).GetComponent<ItemBaseCtrl>();
            if (child == null) continue;
           
            child.index = listItem.Count;
            listItem.Add(child);
          
            child.parent = item;
           
            AutoReferenceChild(child);
           
            //var oldPos = child.transform.localPosition;
            //child.transform.localPosition = new Vector3(oldPos.x, oldPos.y, child.index * -0.001f);
        }
    }

    private void AutoReferenceSameItem()
    {
      
        var dictSameItems = new Dictionary<string, List<ItemBaseCtrl>>();
        foreach (var item in listItem)
        {
            dictSameItems.TryAdd(item.gameObject.name, new List<ItemBaseCtrl>());
            dictSameItems[item.gameObject.name].Add(item);
        }
     
        foreach (var sameItems in dictSameItems.Values)
        {
            foreach (var sameItem in sameItems)
            {
                sameItem.sameItems = new List<ItemBaseCtrl>();
                foreach (var sameItemOther in sameItems)
                {
                   
                    if (sameItem.index == sameItemOther.index) continue;
                    sameItem.sameItems.Add(sameItemOther);
                }
            }
        }
    }
    private void AutoReferenceOther()
    {
        foreach (var item in listItem)
        {
           
            item.sprt = item.GetComponent<SpriteRenderer>();
            item.sprt.sortingOrder = 1;
            item.successPos = item.transform.position;
            item.isSuccess = false;
        }
    }


    public void SpawnNextItem()
    {
        if (currentItemIndex < allItems.Count)
        {
            
            allItems[currentItemIndex].SetActive(true);

          
            // allItems[currentItemIndex].transform.position = boxPosition.position;

            currentItemIndex++;
        }
        else
        {
            Debug.Log("da het do!");
        }
    }

}
