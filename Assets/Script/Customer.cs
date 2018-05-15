using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    GameManager _GameManager;
    DataBase _DataBase;
    CustomerManager _CustomerManager;
    ScoreManager _ScoreManager;

    float Sec;

    int ID_Order; // ID de la commande

    Object Order; // Object commande

    void Start()
    {
        _GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _DataBase = _GameManager.GetComponent<DataBase>();
        _CustomerManager = transform.parent.gameObject.GetComponent<CustomerManager>();
        _ScoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        _CustomerManager.ID_Table = _DataBase.GetTable(); // Attribue table
        StartCoroutine("_Move");
    }

    void Update()
    {
        Sec += Time.deltaTime;
    }

    void AddOrder()
    {
        ID_Order = _GameManager.Rnd.Next(100,110); // Choisi l'Id de la commande

        Order = Instantiate(Resources.Load("Prefab/Boisson/" + ID_Order), new Vector3(transform.position.x - 0.7f, transform.position.y + 2.5f), transform.rotation); // Instantiate l'affichage de la commande
    }

    public void CheckOrder(int ID_Cocktail) // Check si le cocktail est le bon
    {
        if (ID_Cocktail == 0)
        {
            Debug.Log("Nothing");
        }
        if (ID_Order == ID_Cocktail)
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            _ScoreManager.IncreaseScore();
            Destroy(Order); // supprime la bulle de commande
            _CustomerManager.Leave -= 1; // decrémente pour déclencher la sortie du client
            StartCoroutine("_Leave");
        }
        else
            _ScoreManager.DecreaseScore();
    }

    IEnumerator _Move() // Bouge le client à a coter de la table
    {
        Vector3 currentPos = transform.position;
        Vector3 Pos = _DataBase.FindTable(_CustomerManager.ID_Table); // Poaition de fin
        Debug.Log(Pos);
        var t = 0f;
        while (t < 1)
        {
            if (_GameManager.Status == GameState.Playing)
            {
                t += Time.deltaTime / 3;
                transform.localPosition = Vector3.Lerp(currentPos, Pos, t);
            }
            yield return null;
        }
        gameObject.GetComponent<Collider2D>().enabled = true;
        AddOrder(); // ajoute une commande
    }

    IEnumerator _Leave() // Fais sortir le client
    {
        Vector3 currentPos = transform.localPosition;
        Vector3 Pos = new Vector3(-8, 0, 0); // position de sortie
        var t = 0f;
        while (t < 1)
        {
            if (_GameManager.Status == GameState.Playing && _CustomerManager.Leave == 0)
            {
                t += Time.deltaTime / 3;
                transform.position = Vector3.Lerp(currentPos, Pos, t);
            }
            yield return null;
        }
        transform.parent.gameObject.GetComponent<CustomerManager>().NB_Customer -= 1; // décrémente pour détruire l'objet
    }
}
