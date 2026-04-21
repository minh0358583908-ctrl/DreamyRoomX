using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace _GAME.Scripts
{
    public class ItemBaseCtrl : MonoBehaviour
    {
        public int index;
        public SpriteRenderer sprt;
        public Vector3 successPos;
        public bool isSuccess;
        public ItemBaseCtrl parent;
        public List<ItemBaseCtrl> sameItems;

        public void Init()
        {
            sprt = GetComponent<SpriteRenderer>();
            sprt.sortingOrder = 1;
            successPos = transform.position;
            isSuccess = false;
        }

        public void RandomPos()
        {
            var posX = Random.Range(-5, 5);
            var posY = Random.Range(-5, 5);
            var posZ = successPos.z;
            transform.position = new Vector3(posX, posY, posZ);

        }
        public bool IsSuccess()
        {
            return isSuccess;
        }

        public void SetSelected(bool selected)
        {
            if (selected)
                sprt.sortingOrder = 2;
            else sprt.sortingOrder = 1;
        }
        public void CheckCanSuccess()
        {
            if (parent != null && parent.IsSuccess())
            {
                Debug.Log("da dung vi tri chua!");
                return;
            }

            if (Vector3.Distance(transform.position, successPos) < 0.35f)
            {
                transform.position = successPos;
                isSuccess = true;
                sprt.sortingOrder = 0;

                Debug.Log("Item is success!");
            }
            else CheckCanSuccessInSameItemPos();
        }

        private void CheckCanSuccessInSameItemPos()
        {
            if (sameItems == null || sameItems.Count == 0) return;

            foreach (var sameItem in sameItems)
            {
                if (Vector3.Distance(transform.position, sameItem.successPos) < 0.35f)
                {
                    transform.position += sameItem.successPos;
                    isSuccess = true;
                    sprt.sortingOrder = 0;
                    sameItem.successPos = successPos;
                    sameItem.RemoveSameItem(this.index);
                    Debug.Log("Item is success!");
                    return;

                }
            }
        }

        private void RemoveSameItem(int sameItemIndex)
        {
            for (var i = 0; i < sameItemIndex; i++)
            {
                if (sameItems[i].index == sameItemIndex)
                {
                    sameItems.RemoveAt(i);
                    return;
                }

               
            }
        }
    }
}