using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour
{
    Vector3 StartPos;
    Vector3 screenPoint;

    Vector3 StartRotation;

    public Transform target;

    float Drink_Value;
    bool IsPouring = false;
   
    void Start()
    {
        StartPos = transform.position;
        StartRotation = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    void Update()
    {
        Vector3 targetDir = target.position - transform.position;
        float angle = Vector3.Angle(transform.position, targetDir);

        if (transform.position.y >= 0)
            transform.eulerAngles = new Vector3(0, 0, angle);
        else
            transform.eulerAngles = new Vector3();

        if (transform.position.y >= 2 && IsPouring == false)
            StartCoroutine("IncreaseDrink");
        
    }

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
        if (curPosition.y <= 2 && curPosition.y > -1.8)
            transform.position = new Vector3(StartPos.x, curPosition.y, transform.position.z);
        else if (curPosition.y > 2)
            transform.position = new Vector3(StartPos.x, 2, transform.position.z);
    }

    void OnMouseUp()
    {
        Debug.Log(Drink_Value);
        transform.position = StartPos;
        transform.eulerAngles = StartRotation;
        Drink_Value = 0;
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
