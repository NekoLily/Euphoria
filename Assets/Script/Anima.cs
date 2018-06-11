using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Anima : MonoBehaviour {

    public GameObject barman;
    //public float posBouteille;
    //public float speed = 10;
    //public Animator anim;


    void Start()
    {
        
    }

    // Update is called once per frame
    /*void Update()
    {
      float mouveHorizontal = Input.GetAxis("Horizontal");     
      Vector3 mouvement = new Vector3(mouveHorizontal, 0);

        if (Input.GetMouseButtonDown(0))
        {
            isMoving = true;
            //SetTargetPosition();
        }
        if (isMoving)
            MovePlayer();
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }*/

    public void onClick()
    {
        barman.GetComponent<moving>().posbouteille = this.gameObject.transform.position.x + 2;
        barman.GetComponent<moving>().isMoving = true;
    }
    /*private void MovePlayer()
    {
        Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);

     
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (transform.position == targetPosition)
            isMoving = false;
    }*/

    /*private void SetTargetPosition()
    {
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitdist = 0.0f;

        if (playerPlane.Raycast(ray, out hitdist))
            targetPosition = ray.GetPoint(hitdist);
    }*/
}
