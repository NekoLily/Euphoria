using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        StartCoroutine("_Destroy");
	}
	

    IEnumerator _Destroy()
    {
        yield return new WaitForSeconds(3f);
        DestroyObject(transform);
    }
}
