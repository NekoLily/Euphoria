using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour
{
    Vector3 StartPos;
    Vector3 screenPoint;
    Vector3 offset;

    Vector3 StartRotation;

    public Transform target;

    // prints "close" if the z-axis of this transform looks
    // almost towards the target

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
    }

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        if (transform.position.y < 2 || curPosition.y < 2)
            transform.position = new Vector3(StartPos.x, curPosition.y, transform.position.z);
    }

    void OnMouseUp()
    {
        transform.position = StartPos;
        transform.eulerAngles = StartRotation;
    }
}



/*     private Color originalColor = Color.white;
     private Color mouseOverColor = Color.yellow;
     private bool dragging = false;
     private float dragDistance;

     // Use this for initialization
     void Start ()
     {

     }

     // Update is called once per frame
     void Update ()
     {
         if (dragging)
         {
             Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
             Vector3 rayPoint = ray.GetPoint(dragDistance);
             transform.position = rayPoint;
         }
     }

     public void OnMouseEnter()
     {
         GetComponent<Renderer>().material.color = mouseOverColor;
     }

     public void OnMouseExit()
     {
         GetComponent<Renderer>().material.color = originalColor;
     }

     public void OnMouseDown()
     {
         dragDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
         dragging = true;
     }

     public void OnMouseUp()
     {
         dragging = false;
     }*/
