using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    GameManager _GameManager;
    DataBase _DataBase;
    ScoreManager _ScoreManager;

    int ID_Order; // ID de la commande
    public int ID_Table;
    public int ID;

    
    GameObject Order; // bulle + Info
    

    GameObject Homme;
    Animator Anim;

    public float Timer_Order = 30;

    void Start()
    {
        _GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();                  //Lancement du client.
        _DataBase = _GameManager.GetComponent<DataBase>();
        Anim = GetComponent<Animator>();
        _ScoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        StartCoroutine("_Move");
        //Anim.SetTrigger("move");
    }

    public void AddOrder()
    {
        ID_Order = _GameManager.Rnd.Next(100, 110); // Choisi l'Id de la commande
        GameObject Info; // Text ou sprite du cocktail
        if (GameManager.LevelChoisi == 3 || GameManager.LevelChoisi == 5)
        {
            Order = Instantiate(Resources.Load<GameObject>("Prefab/Cloud/Bulle"), new Vector3(transform.position.x - 0.7f, transform.position.y + 2.5f, transform.position.z - 0.1f), transform.rotation);
            Info = Instantiate(Resources.Load<GameObject>("Prefab/Cloud/Image/" + ID_Order), new Vector3(transform.position.x - 0.7f, transform.position.y + 2.5f), transform.rotation); // Instantiate l'affichage de la commande
        }
        else
        {
            Order = Instantiate(Resources.Load<GameObject>("Prefab/Cloud/Bulle"), new Vector3(transform.position.x - 0.7f, transform.position.y + 2.5f, transform.position.z - 0.1f), transform.rotation);
            Info = Instantiate(Resources.Load<GameObject>("Prefab/Cloud/Text/" + ID_Order), new Vector3(transform.position.x - 0.7f, transform.position.y + 2.5f), transform.rotation);
        }
        Order.transform.parent = gameObject.transform;
        Info.transform.parent = Order.transform;
        StartCoroutine("_Timer");
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
            //Anim.SetTrigger("move");
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
                        GameObject.Find("Rage").GetComponent<RectTransform>().position = new Vector2(transform.position.x-0.5f, -2);
                        GameObject.Find("Rage").GetComponent<Animator>().SetTrigger("E11");
                        StartCoroutine("rage");
                        break;

                    case 2:
                        gameObject.GetComponent<Collider2D>().enabled = false;
                        _ScoreManager.DecreaseScore(2);
                        Destroy(Order); // supprime la bulle de commande
                        GameObject.Find("Eclair").GetComponent<RectTransform>().position = new Vector2(transform.position.x, 0);
                        GameObject.Find("Eclair").GetComponent<Animator>().SetTrigger("E12");
                        StartCoroutine("eclair");
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
                        //Anim.SetTrigger("E21");
                        Debug.Log("Event3");
                        _DataBase.LeaveTable(ID_Table);
                        DestroyObject(this.gameObject);
                        break;

                    case 2:
                        gameObject.GetComponent<Collider2D>().enabled = false;
                        _ScoreManager.DecreaseScore(4);
                        Destroy(Order); // supprime la bulle de commande
                        GameObject.Find("Flamme").GetComponent<Animator>().SetTrigger("E22");
                        Debug.Log("Event4");
                        StartCoroutine("_Leave");
                        //Anim.SetTrigger("move");
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
                        GameObject.Find("Lumiere").GetComponent<RectTransform>().position = new Vector2(transform.position.x, 0);
                        GameObject.Find("Lumiere").GetComponent<Animator>().SetTrigger("E31");
                        StartCoroutine("lumiere");
                        
                        break;

                    case 2:
                        gameObject.GetComponent<Collider2D>().enabled = false;
                        _ScoreManager.DecreaseScore(6);
                        Destroy(Order); // supprime la bulle de commande
                        //Anim.SetTrigger("E32");
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
                        GameObject.Find("Fumée").GetComponent<RectTransform>().position = new Vector2(transform.position.x, 0);
                        GameObject.Find("Fumée").GetComponent<Animator>().SetTrigger("E41");
                        StartCoroutine("fumee");
                        break;

                    case 2:
                        gameObject.GetComponent<Collider2D>().enabled = false;
                        _ScoreManager.DecreaseScore(8);
                        Destroy(Order); // supprime la bulle de commande
                        GameObject.Find("Cri").GetComponent<RectTransform>().position = new Vector2(transform.position.x + 1, -1);
                        GameObject.Find("Cri").GetComponent<Animator>().SetTrigger("E42");
                        StartCoroutine("cri");
                        break;
                }
                break;
        }
        return null;
    }

    IEnumerator rage()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("Event1");
        _DataBase.LeaveTable(ID_Table);
        DestroyObject(this.gameObject);
    }

    IEnumerator eclair()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("Event2");
        _DataBase.LeaveTable(ID_Table);
        DestroyObject(this.gameObject);
    }

    IEnumerator lumiere()
    {
        Vector3 currentPos = transform.position;
        Vector3 Pos = new Vector3(transform.position.x, transform.position.y + 1000, transform.position.z);
        yield return new WaitForSeconds(1f);
        var t = 0f;
        /*while (t < 1)
        {
            t += Time.deltaTime / 3;
            transform.localPosition = Vector3.Lerp(currentPos, Pos, t);
        }*/
        transform.localPosition = Vector3.MoveTowards(currentPos, Pos, 20* Time.deltaTime);
        yield return new WaitForSeconds(2f);
        Debug.Log("Event5");
        _DataBase.LeaveTable(ID_Table);
        DestroyObject(this.gameObject);
    }

    IEnumerator fumee()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("Event7");
        StartCoroutine("_Leave");
        //Anim.SetTrigger("move");
    }

    IEnumerator cri()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("Event8");
        _DataBase.LeaveTable(ID_Table);
        DestroyObject(this.gameObject);
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

    IEnumerator _Timer()
    {
        while (Timer_Order > 0)
        {
            Timer_Order -= Time.deltaTime;
            yield return null;
        }
        StartCoroutine("_Event");
    }
}