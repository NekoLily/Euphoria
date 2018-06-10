using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving : MonoBehaviour {

    // Use this for initialization
    public bool isMoving;
    Vector3 targetPosition;
    public float speed;
    public float posbouteille;
    void Start()
    {

    }

    void Update()
    {
        /*float mouveHorizontal = Input.GetAxis("Horizontal");
        Vector3 mouvement = new Vector3(mouveHorizontal, 0);*/
        targetPosition = new Vector3(posbouteille, -0.14f, 0);
        /*if (Input.GetMouseButtonDown(0))
        {
            isMoving = true;
            //SetTargetPosition();
        }*/
        if (isMoving)
            MovePlayer();
        //transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
    private void MovePlayer()
    {
        Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (transform.position == targetPosition)
            isMoving = false;
        
    }
}
