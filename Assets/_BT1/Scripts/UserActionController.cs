using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserActionController : MonoBehaviour
{
    private GameObject _curObjSelected;
    private float _oldPosZOfObjSelected;

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
                break;
            }
        }
    }

    private void HandlePutItem()
    {
        if (_curObjSelected == null) return;
        var curPos = _curObjSelected.transform.position;
        _curObjSelected.transform.position = new Vector3(curPos.x, curPos.y, _oldPosZOfObjSelected);
        _curObjSelected = null;
    }

    private void HandleMoveItem()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _curObjSelected.transform.position = new Vector3(mousePos.x, mousePos.y, -3);
    }
}
