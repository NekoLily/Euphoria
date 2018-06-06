using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour
{
    public Vector3 StartPos;
    public Vector3 StartRotation;

    Vector3 screenPoint;

    public Transform target;
    public Transform ItemObject;

    float Drink_Value;
    bool IsPouring = false;

    void Update()
    {
        if (ItemObject != null)
        {
            Vector3 targetDir = target.position - ItemObject.position;
            float angle = Vector3.Angle(ItemObject.position, targetDir);

            if (ItemObject.position.y >= 0)
                ItemObject.eulerAngles = new Vector3(0, 0, angle);
            else
                ItemObject.eulerAngles = new Vector3();

            if (ItemObject.position.y >= 2 && IsPouring == false)
                StartCoroutine("IncreaseDrink");
        }
    }

    void OnMouseDown()
    {
        if (ItemObject != null)
            screenPoint = Camera.main.WorldToScreenPoint(ItemObject.position);
    }

    void OnMouseDrag()
    {
        if (ItemObject != null)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
            if (curPosition.y <= 2 && curPosition.y > -1.8)
                ItemObject.position = new Vector3(StartPos.x, curPosition.y, ItemObject.position.z);
            else if (curPosition.y > 2)
            {
                ItemObject.position = new Vector3(StartPos.x, 2, ItemObject.position.z);
                ItemObject.gameObject.GetComponent<Item>().IsPoured = true;
            }
        }
    }

    void OnMouseUp()
    {
        if (ItemObject != null)
        {
            ItemObject.eulerAngles = StartRotation;
            Drink_Value = 0;

            switch (ItemObject.gameObject.GetComponent<Item>().IndexItemID)
            {
                case 0:
                    ItemObject.transform.position = new Vector3(-8, -1.5f, 0);
                    break;
                case 1:
                    ItemObject.transform.position = new Vector3(-7, -1.5f, 0);
                    break;
                case 2:
                    ItemObject.transform.position = new Vector3(-6, -1.5f, 0);
                    break;
            }
        }
        ItemObject = null;
    }

    IEnumerator IncreaseDrink()
    {
        IsPouring = true;
        Drink_Value += 1;
        Debug.Log(Drink_Value);
        yield return new WaitForSeconds(1f);
        IsPouring = false;
    }
}
