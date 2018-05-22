using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    GameManager _GameManager;
    DataBase _DataBase;
    CustomerManager _CustomerManager;
    ScoreManager _ScoreManager;

    //float Sec;
    public float OrderTime = 100;
    public float QTETime = 100;

    int ID_Order; // ID de la commande

    bool IsPressed = false;
    bool WaitKey = false;

    Object Order; // Object commande
    GameObject Homme;

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
        //Sec += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && WaitKey)
            IsPressed = true;
        if (OrderTime <= 0)
            StartCoroutine("_Event");
    }

    void AddOrder()
    {
        ID_Order = _GameManager.Rnd.Next(100,110); // Choisi l'Id de la commande
        Order = Instantiate(Resources.Load("Prefab/Boisson/" + ID_Order), new Vector3(transform.position.x - 0.7f, transform.position.y + 2.5f), transform.rotation); // Instantiate l'affichage de la commande
        OrderTime = 25f;
        Timer();
    }

    void Timer()
    {
        if (OrderTime >= 0f)
        {
            OrderTime -= Time.deltaTime;
        }
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
            Destroy(Order); // supprime la bulle de commande
            _CustomerManager.Leave -= 1; // decrémente pour déclencher la sortie du client
            StartCoroutine("_Leave");
            ID_Cocktail = -1;
        }
        else
        {
            StartCoroutine("_Event");
            ID_Cocktail = -1;
        }
    }

    IEnumerator _Event()
    {
        int rand = _GameManager.Rnd.Next(1, 3);
        rand = 3;
        switch (rand)
        {
            case 1: //Client explose.
                Debug.Log("EXPLOSION"); //anim
                gameObject.GetComponent<Collider2D>().enabled = false;
                _ScoreManager.DecreaseScore();
                Destroy(Order); // supprime la bulle de commande
                transform.parent.gameObject.GetComponent<CustomerManager>().NB_Customer -= 1; // décrémente pour détruire l'objet
                break;

            case 2: //Client frappe le comptoir;
                Debug.Log("Frappe");//anim
                gameObject.GetComponent<Collider2D>().enabled = false;
                _ScoreManager.DecreaseScore();
                Destroy(Order); // supprime la bulle de commande
                _CustomerManager.Leave -= 1; // decrémente pour déclencher la sortie du client
                StartCoroutine("_Leave");
                break;

            case 3: //Bagarre
                Debug.Log("BAGARRE");
                var t = 0f;
                gameObject.GetComponent<Collider2D>().enabled = false;
                _ScoreManager.DecreaseScore();
                Destroy(Order); // supprime la bulle de commande
                Homme = Instantiate(Resources.Load<GameObject>("Prefab/Customer/Client"));
                Vector3 currentPos = Homme.transform.position;
                Vector3 Pos = new Vector3(gameObject.transform.position.x - 1, gameObject.transform.position.y, gameObject.transform.position.z);
                while (t < 1)
                {
                    if (_GameManager.Status == GameState.Playing)
                    {
                        t += Time.deltaTime / 3;
                        Homme.transform.localPosition = Vector3.Lerp(currentPos, Pos, t);
                    }
                    yield return null;
                }
                //anim
                QTETime = 5f;
                StartCoroutine("_QTE");
                break;
        }
    }

    IEnumerator _QTE()
    {
        while (QTETime > 0)
        {
            WaitKey = true;
            if (IsPressed)
                break;
            else
            {
                QTETime--;
                Debug.Log(QTETime);
                _ScoreManager.DecreaseScoreBagarre();
                yield return new WaitForSeconds(1);
            }
        }
        if (QTETime == 0)
        {
            //Fin anim
            _ScoreManager.Score -= 100;
            _CustomerManager.Leave -= 1; // decrémente pour déclencher la sortie du client
            StartCoroutine("_Leave");
            float t = 0f;
            Vector3 currentPos2 = Homme.transform.position;
            Vector3 Pos2 = new Vector3(-10, -4.5f, 0);
            while (t < 1)
            {
                if (_GameManager.Status == GameState.Playing)
                {
                    t += Time.deltaTime / 3;
                    Homme.transform.localPosition = Vector3.Lerp(currentPos2, Pos2, t);
                }
                yield return null;
            }
            DestroyObject(Homme);
        }
        else
        {
            //animBarman
            _CustomerManager.Leave -= 1; // decrémente pour déclencher la sortie du client
            StartCoroutine("_Leave");
            float t = 0f;
            Vector3 currentPos2 = Homme.transform.position;
            Vector3 Pos2 = new Vector3(-10, -4.5f, 0);
            while (t < 1)
            {
                if (_GameManager.Status == GameState.Playing)
                {
                    t += Time.deltaTime / 3;
                    Homme.transform.localPosition = Vector3.Lerp(currentPos2, Pos2, t);
                }
                yield return null;
            }
            DestroyObject(Homme);
        }
        yield return null;
    }

    IEnumerator _Move() // Bouge le client à a coter de la table
    {
        Vector3 currentPos = transform.position;
        Vector3 Pos = _DataBase.FindTable(_CustomerManager.ID_Table); // Poaition de fin
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
        Vector3 currentPos = transform.position; //transform.localPosition;
        Vector3 Pos = new Vector3(-10, -4.5f, 0); // position de sortie
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
