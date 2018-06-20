using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item: MonoBehaviour {

    public int IndexItemID;
    public int ItemID;
    public bool IsPoured = false;
    public bool AlreadyPoured = false;

    public Vector3 DefaultScale;

    private void Awake()
    {
        DefaultScale = transform.localScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Shaker")
        {

            if (AlreadyPoured)
                IsPoured = false;
            else if (IsPoured == false)
                IsPoured = true;
            if (IsPoured)
                AlreadyPoured = true;
            transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            GameObject.Find("DragManager").GetComponent<DragHandler>().MoveDefaultPos();
        }
    }

}
