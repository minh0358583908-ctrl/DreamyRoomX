using DG.Tweening;
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
        public List<Tweener> _animActive = new();
        private Vector3 tempPos;

        private void ClearAnimation()
        {
            foreach(var tweener in _animActive)
            {
                tweener.Complete();
                tweener.Kill();
            }
            transform.DORotateQuaternion(Quaternion.Euler(0, 0, 0),0.1f);
        }
        public void PlayAninationActive()
        {
            ClearAnimation();
            var moveHeight = 0.3f;
            var moveDuration = 1f;
            var tiltAngle = 10;
            var tiltDuration = 3.5f;

            var anim1 = transform.DOMoveY(transform.position.y + moveHeight, moveDuration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);

            var anim2 = transform.DORotate(new Vector3(0,0, tiltAngle),tiltDuration)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo);

            _animActive.Add(anim1);
            _animActive.Add(anim2);

        }


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
            {
                sprt.sortingOrder = 2;
                ClearAnimation();
            }
            else
            {
                sprt.sortingOrder = 1;
                ClearAnimation();
            } 
            
        }
        public void CheckCanSuccess()
        {
            if (parent != null && parent.IsSuccess())
            {
                Debug.Log("da dung vi tri chua!");
                return;
            }

            if (Vector3.Distance(transform.position, successPos) < 0.5f)
            {
                //transform.position = successPos;
                transform.DOMove(successPos, 0.25f);
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
                if (sameItem == this) continue;
                if (Vector3.Distance(transform.position, sameItem.successPos) < 0.5f)
                {
                    Vector3 tempPos = successPos;
                    //transform.position += sameItem.successPos;
                    transform.DOMove(sameItem.successPos, 0.25f);
                    isSuccess = true;
                    sprt.sortingOrder = 0;
                    sameItem.successPos = successPos;
                    sameItem.successPos = tempPos;
                    ClearAnimation();
                    sameItem.ClearAnimation();
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