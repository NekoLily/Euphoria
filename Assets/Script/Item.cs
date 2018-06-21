using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

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
            GameObject _DragManager = GameObject.Find("DragManager");
            if (AlreadyPoured)
            {
                _DragManager.GetComponent<AudioSource>().clip = _DragManager.GetComponent<DragHandler>().Bad_Clip;
                _DragManager.GetComponent<AudioSource>().Play();
                Instantiate(Resources.Load<GameObject>("Prefab/FxParticle/ParticleBad"), new Vector3(-0.54f, -1.38f, -1), transform.rotation);
                IsPoured = false;
            }
            else if (IsPoured == false)
            {
                _DragManager.GetComponent<AudioSource>().clip = _DragManager.GetComponent<DragHandler>().Good_Clip;
                _DragManager.GetComponent<AudioSource>().Play();
                Instantiate(Resources.Load<GameObject>("Prefab/FxParticle/ParticleGood"), new Vector3(-0.54f, -1.38f, -1), transform.rotation);
                IsPoured = true;
            }
            if (IsPoured)
                AlreadyPoured = true;
            transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            GameObject.Find("DragManager").GetComponent<DragHandler>().MoveDefaultPos();
        }
    }

}
