using _GAME.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserActionController : MonoBehaviour
{
    private GameObject _curObjSelected;
    private float _oldPosZOfObjSelected;
    private ItemBaseCtrl _curItemSelected;

    private Vector3 _deltaPos;
   

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            HandlePutItem();
        }

        if (Input.GetMouseButtonDown(0))
        {
            HandlePickItem();
        }

        if (_curObjSelected != null)
        {
            HandleMoveItem();
        }
        
    }

    private void HandlePickItem()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //_curObjSelected.transform.position = new Vector3(mousePos.x , mousePos.y , -3f);
        var rayCastPos = new Vector3(mousePos.x, mousePos.y, 0);
        var rayCastHits = Physics2D.RaycastAll(rayCastPos, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("GamePlay"));
        foreach (var hit in rayCastHits)
        {
            if (hit.collider != null && hit.collider.tag.Equals("Item"))
            {
                _curObjSelected = hit.collider.gameObject;
                _oldPosZOfObjSelected = _curObjSelected.transform.position.z;
                //var itemSelected = hit.collider.gameObject.GetComponent<ItemBaseCtrl>();
                var itemSelected = hit.collider.GetComponentInParent<ItemBaseCtrl>();
                if (itemSelected.IsSuccess()) continue;
                _curItemSelected = itemSelected;
                _deltaPos = _curItemSelected.transform.position - rayCastPos;
                _curItemSelected.SetSelected(true);
                break;
            }
        }
    }

    private void HandlePutItem()
    {
        if (_curItemSelected == null) return;

        var curPos = _curItemSelected.transform.position;
        var posZ = _curItemSelected.successPos.z;

        _curItemSelected.transform.position = new Vector3(curPos.x, curPos.y, posZ);
        _curItemSelected.SetSelected(false);
        _curItemSelected.CheckCanSuccess();
        _curItemSelected = null;
    }

    private void HandleMoveItem()
    {
        if (_curItemSelected == null) return;

        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var posZ = _curItemSelected.successPos.z;

        var newObjPos = new Vector3(mousePos.x, mousePos.y, -3f);
        _curItemSelected.transform.position = newObjPos + _deltaPos;
    }
}
