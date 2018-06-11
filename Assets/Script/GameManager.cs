using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameState Status { get; set; }
    public static GameManager current;
    public static GameObject MenuPrincipal, LevelSelect, Play, Recettes, Carte, Bar1, Bar2, Credits, Highscore, Quitter, Reset, Loading;

    public static int Stars = 0;
    public static int ID = 1;
    public static int LevelChoisi;
    public static int Score;

    public System.Random Rnd, Rnd2;
    int seed = Environment.TickCount;

    public static int[] Items = new int[3];
    static int ID_Cocktail = -1;

    GameObject client;

    Button Jouer, Recette, Credit, Scores, Quit, Réinitialiser;
    Button Level1, Level2, Level3, Level4, Level5;

    DataBase _DataBase;

    int[,] OffsetScoreRequired = { { 500, 1000, 1500 }, { 500, 1000, 1500 }, { 500, 1000, 1500 }, { 500, 1000, 1500 }, { 500, 1000, 1500 } }; // {0 , 1, 2} = {1er*, 2eme*, 3eme*}

    public float SpawnTimerSecs = 1;

    static GameObject ItemBar_0;
    static GameObject ItemBar_1;
    static GameObject ItemBar_2;

    public AudioSource SE;
    public AudioClip MainMenu;

    void Start()
    {
        MenuPrincipal = GameObject.Find("MenuPrincipal");
        LevelSelect = GameObject.Find("LevelSelect");
        Play = GameObject.Find("Jouer");
        Recettes = GameObject.Find("Recettes");
        Credits = GameObject.Find("Credits");
        Highscore = GameObject.Find("Highscore");
        Quitter = GameObject.Find("Quitter");
        Reset = GameObject.Find("Reset");
        Loading = GameObject.Find("Loading");

        Level2 = LevelSelect.transform.Find("Level2").GetComponent<Button>();
        Level3 = LevelSelect.transform.Find("Level3").GetComponent<Button>();
        Level4 = LevelSelect.transform.Find("Level4").GetComponent<Button>();
        Level5 = LevelSelect.transform.Find("Level5").GetComponent<Button>();

        Level2.interactable = false;
        Level3.interactable = false;
        Level4.interactable = false;
        Level5.interactable = false;

        Play.SetActive(false);
        Recettes.SetActive(false);
        Credits.SetActive(false);
        Highscore.SetActive(false);
        Loading.SetActive(false);

        _DataBase = gameObject.GetComponent<DataBase>();
        CheckSave();
        Status = GameState.MainMenu;
        StartCoroutine("SpawnTimer");
    }

    private void Awake()
    {
        if (current == null)
        {
            current = this;
            DontDestroyOnLoad(this);
        }
        else if (current != this)
            Destroy(gameObject);
    }

    void Update()
    {
        StartCoroutine("BGM");
        Debug.Log(Status);
        if (SceneManager.GetActiveScene().name != "MenuPrincipal")
            MenuPrincipal.SetActive(false);
        switch (Status)
        {
            case GameState.MainMenu:
                MenuPrincipal.SetActive(true);
                Play.SetActive(false);
                Recettes.SetActive(false);
                Credits.SetActive(false);
                Highscore.SetActive(false);
                break;
            case GameState.SelectLevel:
                Play.SetActive(true);
                break;

            case GameState.Recettes:
                Recettes.SetActive(true);
                break;

            case GameState.Credits:
                Credits.SetActive(true);
                break;

            case GameState.Highscore:
                Highscore.SetActive(true);
                Highscore.transform.Find("HighscoreText").GetComponent<Text>().text = String.Format("Votre Highscore est : \n\n Level 1 : {0} \n Level 2 : {1} \n Level 3 : {2} \n Level 4 : {3} \n Level 5 : {4}",
                    _DataBase.Tab_Score[0], _DataBase.Tab_Score[1], _DataBase.Tab_Score[2], _DataBase.Tab_Score[3], _DataBase.Tab_Score[4]);
                break;

            case GameState.Quitter:
                Application.Quit();
                break;

            case GameState.Reset:
                _DataBase.ResetScore();
                _DataBase.GetSave();
                CheckSave();
                Status = GameState.MainMenu;
                break;

            case GameState.Start:
                Loading.SetActive(true);
                MenuPrincipal.SetActive(true);
                Play.SetActive(false);
                Recettes.SetActive(false);
                Credits.SetActive(false);
                Highscore.SetActive(false);
                Loading.GetComponent<LoadingScreen>().Loading(0);
                GameManager.Status = GameState.Loading;
                break;

            case GameState.Playing:
                if (SceneManager.GetActiveScene().name == "Jeu")
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        Vector3 CAM_POS = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
                        Collider2D Collider = Physics2D.OverlapPoint(CAM_POS);

                        if (Collider)
                        {
                            switch (Collider.tag)
                            {
                                case "Customer":
                                    Collider.gameObject.GetComponent<Customer>().CheckOrder(ID_Cocktail);
                                    ID_Cocktail = -1;
                                    Items = new int[3];
                                    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                                    break;
                                case "Items":
                                    if (Bar1.activeInHierarchy == true)
                                    {
                                        Items[Collider.gameObject.GetComponent<Item>().IndexItemID] = 0;
                                        DestroyObject(Collider.gameObject);
                                    }
                                    else if (Bar2.activeInHierarchy == true)
                                        if (GameObject.Find("DragManager").GetComponent<DragHandler>().ItemObject == null)
                                        {
                                            GameObject.Find("DragManager").GetComponent<DragHandler>().ItemObject = Collider.transform;
                                            Collider.transform.position = new Vector3(0f, -1.5f, 0);
                                            GameObject.Find("DragManager").GetComponent<DragHandler>().StartPos = Collider.transform.position;
                                            GameObject.Find("DragManager").GetComponent<DragHandler>().StartRotation = new Vector3(Collider.transform.eulerAngles.x, Collider.transform.eulerAngles.y, Collider.transform.eulerAngles.z);
                                        }
                                    break;
                                case "Shaker":
                                    if (Items[2] == 0)
                                    {
                                        if (ItemBar_0.GetComponent<Item>().IsPoured && ItemBar_1.GetComponent<Item>().IsPoured)
                                            ID_Cocktail = Shaker();
                                    }
                                    else if (Items[2] > 0)
                                    {
                                        if (ItemBar_0.gameObject.GetComponent<Item>().IsPoured && ItemBar_1.gameObject.GetComponent<Item>().IsPoured && ItemBar_2.GetComponent<Item>().IsPoured)
                                            ID_Cocktail = Shaker();
                                    }
                                    Debug.Log(ID_Cocktail);
                                    Items = new int[3];

                                    DestroyObject(ItemBar_0);
                                    DestroyObject(ItemBar_1);
                                    DestroyObject(ItemBar_2);

                                    Bar2.SetActive(false);
                                    Bar1.SetActive(true);
                                    Sprite Cursor_Sprite = Resources.Load<Sprite>("Prefab/Boisson/1");
                                    Cursor.SetCursor(Cursor_Sprite.texture, new Vector2(Cursor_Sprite.texture.width / 2, Cursor_Sprite.texture.height / 2), CursorMode.ForceSoftware);
                                    break;

                            }
                            if (Collider.gameObject.name == "BorderCollider")
                            {
                                Bar2.SetActive(false);
                                ItemBar_0.transform.position = new Vector3(3.5f, -1.5f, 0);
                                ItemBar_1.transform.position = new Vector3(2.5f, -1.5f, 0);
                                ItemBar_2.transform.position = new Vector3(1.5f, -1.5f, 0);
                                Bar1.SetActive(true);
                            }
                        }
                    }
                    Rnd = new System.Random(seed++);
                    AddCustomer();
                }
                break;

            case GameState.GameClear:
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                SpawnTimerSecs = 1;
                if (Score >= OffsetScoreRequired[LevelChoisi - 1, 0] && Score < OffsetScoreRequired[LevelChoisi - 1, 1])
                    Stars = 1;
                else if (Score >= OffsetScoreRequired[LevelChoisi - 1, 1] && Score < OffsetScoreRequired[LevelChoisi, 2])
                    Stars = 2;
                else if (Score >= OffsetScoreRequired[LevelChoisi, 2])
                    Stars = 3;
                Loading.SetActive(true);
                Loading.GetComponent<LoadingScreen>().Loading(0);
                GameManager.Status = GameState.Loading;
                break;

            case GameState.Tuto:
                Loading.SetActive(true);
                MenuPrincipal.SetActive(true);
                Play.SetActive(false);
                Recettes.SetActive(false);
                Credits.SetActive(false);
                Highscore.SetActive(false);
                Loading.GetComponent<LoadingScreen>().Loading(1);
                GameManager.Status = GameState.Loading;
                break;

        }
    }

    IEnumerator BGM()
    {
        if ((SceneManager.GetActiveScene().name == "MenuPrincipal") && (SE.clip != MainMenu))
        {
            SE.Stop();
            SE.clip = MainMenu;
            yield return new WaitForSeconds(0.2f);
            SE.Play();
        }
        else
            SE.Stop();
    }

    void AddCustomer()
    {
        Debug.Log("Spawn");
        if (SpawnTimerSecs <= 0)
        {
            SpawnTimerSecs = 1;
            //for (int i = 0; i < 4; i++)
            //{
            int Table_ID;
            if ((Table_ID = _DataBase.GetTable()) != -1)
            {
                //if (_DataBase.Table[ID] == 0)
                //{
                Rnd2 = new System.Random(seed++);
                switch (Rnd2.Next(1, 5))
                {
                    case 1:
                        if (LevelChoisi == 1 || LevelChoisi == 2 || LevelChoisi == 4)
                        {
                            client = Instantiate(Resources.Load<GameObject>("Prefab/Customer/Customer1"));
                            client.GetComponent<Customer>().ID = 1;
                            client.GetComponent<Customer>().ID_Table = Table_ID;
                            return;
                        }
                        else
                        {
                            client = Instantiate(Resources.Load<GameObject>("Prefab/Customer/Customer1.2"));
                            client.GetComponent<Customer>().ID = 1;
                            client.GetComponent<Customer>().ID_Table = Table_ID;
                            return;
                        }

                    case 2:
                        if (LevelChoisi == 1 || LevelChoisi == 2 || LevelChoisi == 4)
                        {
                            client = Instantiate(Resources.Load<GameObject>("Prefab/Customer/Customer2"));
                            client.GetComponent<Customer>().ID = 2;
                            client.GetComponent<Customer>().ID_Table = Table_ID;
                            return;
                        }
                        else
                        {
                            client = Instantiate(Resources.Load<GameObject>("Prefab/Customer/Customer2.2"));
                            client.GetComponent<Customer>().ID = 2;
                            client.GetComponent<Customer>().ID_Table = Table_ID;
                            return;
                        }

                    case 3:
                        if (LevelChoisi == 1 || LevelChoisi == 2 || LevelChoisi == 4)
                        {
                            client = Instantiate(Resources.Load<GameObject>("Prefab/Customer/Customer3"));
                            client.GetComponent<Customer>().ID = 3;
                            client.GetComponent<Customer>().ID_Table = Table_ID;
                            return;
                        }
                        else
                        {
                            client = Instantiate(Resources.Load<GameObject>("Prefab/Customer/Customer3.2"));
                            client.GetComponent<Customer>().ID = 3;
                            client.GetComponent<Customer>().ID_Table = Table_ID;
                            return;
                        }

                    case 4:
                        if (LevelChoisi == 1 || LevelChoisi == 2 || LevelChoisi == 4)
                        {
                            client = Instantiate(Resources.Load<GameObject>("Prefab/Customer/Customer4"));
                            client.GetComponent<Customer>().ID = 4;
                            client.GetComponent<Customer>().ID_Table = Table_ID;
                            return;
                        }
                        else
                        {
                            client = Instantiate(Resources.Load<GameObject>("Prefab/Customer/Customer4.2"));
                            client.GetComponent<Customer>().ID = 4;
                            client.GetComponent<Customer>().ID_Table = Table_ID;
                            return;
                        }
                }
                //}
            }
        }
    }

    public void OnClickEvier()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        ID_Cocktail = -1;
        Items = new int[3];
        DestroyObject(ItemBar_0);
        DestroyObject(ItemBar_1);
        DestroyObject(ItemBar_2);
        //Changement de sprite a 0.
    }

    public void OnClickRecette()
    {
        if (Carte.activeInHierarchy == false)
            Carte.SetActive(true);
        else
            Carte.SetActive(false);
    }

    public void OnClickBartender()
    {
        Debug.Log("Item : " + Items[0] + " " + Items[1] + " " + Items[2]);
        Debug.Log(LevelChoisi);
        if (LevelChoisi >= 4)
        {
            if (Items[0] > 0 && Items[1] > 0)
            {
                Bar2.SetActive(true);
                Bar1.SetActive(false);

                ItemBar_0.transform.position = new Vector3(-8, -1.5f, 0);
                ItemBar_0.GetComponent<SpriteRenderer>().sortingLayerName = "Bouteilles2";
                ItemBar_1.transform.position = new Vector3(-7, -1.5f, 0);
                ItemBar_1.GetComponent<SpriteRenderer>().sortingLayerName = "Bouteilles2";
                if (Items[2] > 0)
                {
                    ItemBar_2.transform.position = new Vector3(-6, -1.5f, 0);
                    ItemBar_2.GetComponent<SpriteRenderer>().sortingLayerName = "Bouteilles2";
                }
            }  
            else
                Debug.Log("Pas assez d'ingrédients");
        }
        else
        {
            ID_Cocktail = Shaker();
            DestroyObject(ItemBar_0);
            DestroyObject(ItemBar_1);
            DestroyObject(ItemBar_2);
            Debug.Log("Cocktail ID : " + ID_Cocktail);
        }
        if (ID_Cocktail > 0)
        {
            Sprite Cursor_Sprite = Resources.Load<Sprite>("Prefab/Boissons/" + ID_Cocktail);
            Cursor.SetCursor(Cursor_Sprite.texture, new Vector2(Cursor_Sprite.texture.width / 2, Cursor_Sprite.texture.height / 2), CursorMode.ForceSoftware);
        }
    }

    public void OnClickCocktail(int ID)
    {
        for (int IndexItems = 0; IndexItems < 3; IndexItems++)
        {
            if (Items[IndexItems] == 0)
            {
                Items[IndexItems] = ID;
                switch (IndexItems)
                {
                    case 0:
                        ItemBar_0 = Instantiate(Resources.Load<GameObject>("Prefab/Items/" + ID), new Vector3(3.5f, -1.5f, 0), Quaternion.identity);
                        ItemBar_0.GetComponent<Item>().IndexItemID = IndexItems;
                        break;
                    case 1:
                        ItemBar_1 = Instantiate(Resources.Load<GameObject>("Prefab/Items/" + ID), new Vector3(2.5f, -1.5f, 0), Quaternion.identity);
                        ItemBar_1.GetComponent<Item>().IndexItemID = IndexItems;
                        break;
                    case 2:
                        ItemBar_2 = Instantiate(Resources.Load<GameObject>("Prefab/Items/" + ID), new Vector3(1.5f, -1.5f, 0), Quaternion.identity);
                        ItemBar_2.GetComponent<Item>().IndexItemID = IndexItems;
                        break;
                }
                return;
            }
        }
        // Plein enlever d'abord la bouteille
    }

    public int Shaker()    //Validation du cocktail créé.
    {
        if (Items[0] > 0 && Items[1] > 0)
        {
            //{Item1, item2, item3, ID_Cocktail}
            int[,] Info_Cocktail = { { 1, 9, 0, 100 }, { 3, 11, 0, 101 }, { 4, 5, 0, 102 }, { 2, 10, 11, 103 }, { 3, 7, 0, 104 }, { 6, 12, 0, 105 }, { 1, 4, 5, 106 }, { 8, 11, 0, 107 }, { 8, 10, 12, 108 }, { 2, 13, 0, 109 } };

            for (int Index_IC_1 = 0; Index_IC_1 < Info_Cocktail.GetLength(0); Index_IC_1++)
            {
                int[] Check_Value = { 0, 0, 0 };
                for (int Index_IC_2 = 0; Index_IC_2 < 3; Index_IC_2++)
                {
                    if (Items[0] == Info_Cocktail[Index_IC_1, Index_IC_2])
                        Check_Value[0] += 1;
                    if (Items[1] == Info_Cocktail[Index_IC_1, Index_IC_2])
                        Check_Value[1] += 1;
                    if (Items[2] == Info_Cocktail[Index_IC_1, Index_IC_2])
                        Check_Value[2] += 1;
                }
                if (Check_Value[0] == 1 && Check_Value[1] == 1 && Check_Value[2] == 1)
                    return Info_Cocktail[Index_IC_1, 3];
            }
        }
        else
            return -1;
        return 0;
    }

    public void OnClickButton(int Number)
    {
        switch (Number)
        {
            case 0:
                MenuPrincipal.SetActive(false);
                Status = GameState.SelectLevel;
                break;

            case 1:
                MenuPrincipal.SetActive(false);
                Status = GameState.Recettes;
                break;

            case 2:
                MenuPrincipal.SetActive(false);
                Status = GameState.Credits;
                break;

            case 3:
                MenuPrincipal.SetActive(false);
                Status = GameState.Highscore;
                break;

            case 4:
                MenuPrincipal.SetActive(false);
                Status = GameState.Quitter;
                break;

            case 5:
                MenuPrincipal.SetActive(false);
                Status = GameState.Reset;
                break;

            case 6:
                Status = GameState.MainMenu;
                break;
        }
    }

    public void OnClickLevel(int number)
    {
        switch (number)
        {
            case 1:
                LevelChoisi = 1;
                Status = GameState.Start;
                Debug.Log("Level1");
                break;

            case 2:
                LevelChoisi = 2;
                Status = GameState.Start;
                Debug.Log("Level2");
                break;

            case 3:
                LevelChoisi = 3;
                Status = GameState.Start;
                Debug.Log("Level3");
                break;

            case 4:
                LevelChoisi = 4;
                Status = GameState.Start;
                Debug.Log("Level4");
                break;

            case 5:
                LevelChoisi = 5;
                Status = GameState.Start;
                Debug.Log("Level5");
                break;

            case 6:
                Status = GameState.Tuto;
                break;
        }
    }

    public void CheckSave()
    {
        if (int.Parse(_DataBase.Tab_Score[0]) >= OffsetScoreRequired[0, 0])
            Level2.interactable = true;
        else
            Level2.interactable = false;

        if (int.Parse(_DataBase.Tab_Score[1]) >= OffsetScoreRequired[1, 0])
            Level3.interactable = true;
        else
            Level3.interactable = false;

        if (int.Parse(_DataBase.Tab_Score[2]) >= OffsetScoreRequired[2, 0])
            Level4.interactable = true;
        else
            Level4.interactable = false;

        if (int.Parse(_DataBase.Tab_Score[3]) >= OffsetScoreRequired[3, 0])
            Level5.interactable = true;
        else
            Level5.interactable = false;
    }

    IEnumerator LoadLevelData()  //Spécificité des niveaux.
    {
        _DataBase.ResetTable();
        Carte = GameObject.Find("Recettes");
        Bar1 = GameObject.Find("Bar1");
        Bar2 = GameObject.Find("Bar2");
        Carte.SetActive(false);
        switch (LevelChoisi)
        {
            case 1:
                GameObject.Find("Timer").GetComponent<Countdown>().timeLeft = 180;
                break;
            case 2:
                GameObject.Find("Timer").GetComponent<Countdown>().timeLeft = 90;
                break;
            case 3:
                GameObject.Find("Timer").GetComponent<Countdown>().timeLeft = 90;
                break;
            case 4:
                GameObject.Find("Timer").GetComponent<Countdown>().timeLeft = 180;
                break;
            case 5:
                GameObject.Find("Timer").GetComponent<Countdown>().timeLeft = 180;
                break;
        }
        Bar2.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        GameManager.Loading.SetActive(false);
        GameManager.Status = GameState.Playing;
    }

    IEnumerator SpawnTimer()
    {
        while (true)
        {
            if (Status == GameState.Playing && SceneManager.GetActiveScene().name == "Jeu")
                SpawnTimerSecs -= 1;
            yield return new WaitForSeconds(1f);
        }
    }
}