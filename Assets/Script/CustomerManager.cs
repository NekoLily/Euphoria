using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour {

    public int NB_Customer; // Nombre de client doit etre à zéro pour déclencher la destruction et libération de table -> Customer _Leave()

    public int ID_Table; // ID de la table = 0 -> Customer Start()
    public int Leave; // Doit être à zéro pour déclencher la sortie du client -> Customer CheckOrder() 

    void Update ()
    {
        if (NB_Customer == 0)
        {
            DestroyObject(this.gameObject);
            GameObject.Find("GameManager").GetComponent<DataBase>().LeaveTable(ID_Table); // set la table inocupée
        }
    }
}
