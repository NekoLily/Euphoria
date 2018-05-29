using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    GameManager _GameManager;
    DataBase _DataBase;
    ScoreManager _ScoreManager;

    //float Sec;
    public float QTETime = 100;

    int ID_Order; // ID de la commande
    int ID_Table;
    public int ID;

    bool IsPressed = false;
    bool WaitKey = false;

    Object Order; // Object commande
    GameObject Homme;
    Animator Anim;

    void Start()
    {
        Anim = GetComponent<Animator>();
        _GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();                  //Lancement du client.
        _DataBase = _GameManager.GetComponent<DataBase>();
        _ScoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        ID_Table = _DataBase.GetTable(); // Attribue table
        StartCoroutine("_Move");
        Anim.SetTrigger("move");
    }

    void Update()
    {

    }

    public void AddOrder()
    {
        ID_Order = _GameManager.Rnd.Next(100,110); // Choisi l'Id de la commande
        Order = Instantiate(Resources.Load("Prefab/Boisson/" + ID_Order), new Vector3(transform.position.x - 0.7f, transform.position.y + 2.5f), transform.rotation); // Instantiate l'affichage de la commande
    }

    void Timer()
    {
        StartCoroutine("_Event");       //Fonction de fin d'anim client.
    }

    public void CheckOrder(int ID_Cocktail) // Check si le cocktail est le bon
    {
        if (ID_Cocktail == -1)
        {
            Debug.Log("Nothing");
        }
        else if (ID_Order == ID_Cocktail)
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            _ScoreManager.IncreaseScore();
            Destroy(Order); // supprime la bulle de commande                //Cocktail OK.
            StartCoroutine("_Leave");
            Anim.SetTrigger("move");
            ID_Cocktail = -1;
        }
        else
        {
            Debug.Log("Wrong cocktail");
            StartCoroutine("_Event");                               //Ccocktail Pas OK.
            ID_Cocktail = -1;
        }
    }

    IEnumerator _Event()                        //Tous les Events possibles selon les clients.
    {
        int rand = _GameManager.Rnd.Next(1, 3);
        switch (ID)
        {
            case 1:
                switch (rand)
                {
                    case 1:
                        gameObject.GetComponent<Collider2D>().enabled = false;
                        _ScoreManager.DecreaseScore(1);
                        Destroy(Order); // supprime la bulle de commande
                        //anim Explose de rage
                        Debug.Log("Event1");
                        _DataBase.LeaveTable(ID_Table);
                        DestroyObject(this.gameObject);
                        break;

                    case 2:
                        gameObject.GetComponent<Collider2D>().enabled = false;
                        _ScoreManager.DecreaseScore(2);
                        Destroy(Order); // supprime la bulle de commande
                        //anim foudre + explosion bouteille
                        Debug.Log("Event2");
                        _DataBase.LeaveTable(ID_Table);
                        DestroyObject(this.gameObject);
                        break;
                }
                break;

            case 2:
                switch (rand)
                {
                    case 1:
                        gameObject.GetComponent<Collider2D>().enabled = false;
                        _ScoreManager.DecreaseScore(3);
                        Destroy(Order); // supprime la bulle de commande
                        //Anime flamme + bouteille brule
                        Debug.Log("Event3");
                        _DataBase.LeaveTable(ID_Table);
                        DestroyObject(this.gameObject);
                        break;

                    case 2:
                        gameObject.GetComponent<Collider2D>().enabled = false;
                        _ScoreManager.DecreaseScore(4);
                        Destroy(Order); // supprime la bulle de commande
                        //Flamme devant poubelle durant 5 sec.
                        Debug.Log("Event4");
                        _DataBase.LeaveTable(ID_Table);
                        DestroyObject(this.gameObject);
                        break;
                }
                break;

            case 3:
                switch (rand)
                {
                    case 1:
                        gameObject.GetComponent<Collider2D>().enabled = false;
                        _ScoreManager.DecreaseScore(5);
                        Destroy(Order); // supprime la bulle de commande
                        //Enlevement Alien
                        Debug.Log("Event5");
                        _DataBase.LeaveTable(ID_Table);
                        DestroyObject(this.gameObject);
                        break;

                    case 2:
                        gameObject.GetComponent<Collider2D>().enabled = false;
                        _ScoreManager.DecreaseScore(6);
                        Destroy(Order); // supprime la bulle de commande
                        //Anim explosion de tete
                        Debug.Log("Event6");
                        _DataBase.LeaveTable(ID_Table);
                        DestroyObject(this.gameObject);
                        break;
                }
                break;

            case 4:
                switch (rand)
                {
                    case 1:
                        gameObject.GetComponent<Collider2D>().enabled = false;
                        _ScoreManager.DecreaseScore(7);
                        Destroy(Order); // supprime la bulle de commande
                        //fume, cache les bouteilles.
                        Debug.Log("Event7");
                        StartCoroutine("_Leave");
                        Anim.SetTrigger("move");
                        break;

                    case 2:
                        gameObject.GetComponent<Collider2D>().enabled = false;
                        _ScoreManager.DecreaseScore(8);
                        Destroy(Order); // supprime la bulle de commande
                        //Anim Crie + casse bouteille
                        Debug.Log("Event8");
                        _DataBase.LeaveTable(ID_Table);
                        DestroyObject(this.gameObject);
                        break;
                }
                break;
                yield return null;
        }
    }

    IEnumerator _Move() // Bouge le client à a coter de la table
    {
        Vector3 currentPos = transform.position;
        Vector3 Pos = _DataBase.FindTable(ID_Table); // Poaition de fin
        var t = 0f;
        while (t < 1)
        {
            if (GameManager.Status == GameState.Playing)
            {
                t += Time.deltaTime / 3;
                transform.localPosition = Vector3.Lerp(currentPos, Pos, t);
            }
            yield return null;
        }
        gameObject.GetComponent<Collider2D>().enabled = true;
        AddOrder(); // ajoute une commande
        Anim.SetTrigger("Att");
    }

    IEnumerator _Leave() // Fais sortir le client
    {
        Vector3 currentPos = transform.localPosition;
        Vector3 Pos = new Vector3(-10, -5, 0); // position de sortie
        var t = 0f;
        while (t < 1)
        {
            if (GameManager.Status == GameState.Playing)
            {
                t += Time.deltaTime / 3;
                transform.position = Vector3.Lerp(currentPos, Pos, t);
            }
            yield return null;
        }
        _DataBase.LeaveTable(ID_Table);
        DestroyObject(this.gameObject);
    }
}