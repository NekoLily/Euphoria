using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject itemDrag;

    Vector3 StartPosition;

    void Update()
    {
        transform.rotation = 
    }
    
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        itemDrag = gameObject;
        StartPosition = transform.position;       
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        itemDrag = null;
        transform.position = StartPosition;
    }
}
/* private Color originalColor = Color.white;
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