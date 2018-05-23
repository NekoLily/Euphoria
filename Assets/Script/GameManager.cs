using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int Stars;
    public int ID = 1;
    public int LevelChoisi;

    string CocktailString;
    int ID_Cocktail = 0;

    public System.Random Rnd;

    public static GameState Status { get; set; }
    public static GameManager current;
    public static GameObject MenuPrincipal, LevelSelect, Play, Recettes, Credits, Highscore, Quitter, Reset, Victoire, Defaite, GG;

    Button Jouer, Recette, Credit, Scores, Quit, Réinitialiser;
    Button Level1, Level2, Level3, Level4, Level5;

    DataBase _DataBase;
    Text _TimerText;

    bool IsSpawning = true;

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
        Victoire = GameObject.Find("Victoire");
        Defaite = GameObject.Find("Defaite");
        GG = GameObject.Find("GG");

        //Level1 = GameObject.Find("Level1").GetComponent<Button>;
        //Level2 = GameObject.Find("Level2").GetComponent<Button>;
        //Level3 = GameObject.Find("Level3").GetComponent<Button>;
        //Level4 = GameObject.Find("Level4").GetComponent<Button>;
        //Level5 = GameObject.Find("Level5").GetComponent<Button>;

        /*Level2.interactable = false;
        Level3.interactable = false;
        Level4.interactable = false;
        Level5.interactable = false;*/

        Play.SetActive(false);
        Recettes.SetActive(false);
        Credits.SetActive(false);
        Highscore.SetActive(false);
        //Victoire.SetActive(false);
        //Defaite.SetActive(false);
        //GG.SetActive(false);

        _DataBase = gameObject.GetComponent<DataBase>();
        Status = GameState.MainMenu;
    }

    private void Awake()
    {
        if (current == null)
        {
            current = this;
            DontDestroyOnLoad(this);
        }
        else if (current == this)
            Destroy(gameObject);
    }

    void Update()
    {
        switch (Status)
        {
            case GameState.MainMenu:
                MenuPrincipal.SetActive(true);
                Play.SetActive(false);
                Recettes.SetActive(false);
                Credits.SetActive(false);
                Highscore.SetActive(false);
                break;

            case GameState.Playing:
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
                break;

            case GameState.Quitter:
                Application.Quit();
                break;

            case GameState.Reset:
                break;
        }

        if (Status == GameState.Playing && SceneManager.GetActiveScene().name == "Jeu")
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 CAM_POS = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
                Collider2D Collider = Physics2D.OverlapPoint(CAM_POS);

                if (Collider)
                {
                    Collider.gameObject.GetComponent<Customer>().CheckOrder(ID_Cocktail);
                }
            }
            Rnd = new System.Random();
            if (IsSpawning)
                StartCoroutine("InstantiateMethod");
        }
    }

    void AddCustomer()
    {
        for (int i = 0; i < 4; i++)
        {
            if (_DataBase.Table[i] == 0)
            {
                Instantiate(Resources.Load("Prefab/Customer/Customer"));
                return;
            }
        }
    }

    public void OnClickEvier()
    {
        CocktailString = "";
    }

    public void OnClickBartender()
    {
        ID_Cocktail = _DataBase.Shaker(CocktailString);
        Debug.Log(CocktailString + " " + ID_Cocktail);
        CocktailString = "";
        if (ID_Cocktail != 1)
        {

        }
        else
            return;
    }

    public void OnClickCocktail(int num)
    {
        CocktailString += num;
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
                SceneManager.LoadScene(1);
                Status = GameState.Playing;
                StartCoroutine("LoadLevelData");

                Debug.Log("Level1");
                break;

            case 2:
                LevelChoisi = 2;
                Status = GameState.Playing;
                Debug.Log("Level2");
                break;

            case 3:
                LevelChoisi = 3;
                Status = GameState.Playing;
                Debug.Log("Level3");
                break;

            case 4:
                LevelChoisi = 4;
                Status = GameState.Playing;
                Debug.Log("Level4");
                break;

            case 5:
                LevelChoisi = 5;
                Status = GameState.Playing;
                Debug.Log("Level5");
                break;
        }
    }

    IEnumerator LoadLevelData()
    {
        yield return new WaitForSeconds(0.3f);
        _DataBase = gameObject.GetComponent<DataBase>();
        _TimerText = GameObject.Find("Timer").GetComponent<Text>();
    }

    public void GenerationLevel()
    {
        Instantiate(Resources.Load(LoadLevel()));
    }

    public string LoadLevel()
    {
        switch (LevelChoisi)
        {
            case 0:
                return "Level1";

            case 1:
                return "Level2";

            case 2:

                return "Level3";
            case 3:
                return "Level4";

            case 4:
                return "Level5";

            default:
                return "Error";
        }
    }

    public void UnlockLevels(int UnlockingLevels)
    {
        switch (UnlockingLevels)
        {
            case 10:
                if (Status == GameState.GameClear && Stars >= 1)
                    Level2.interactable = true;
                break;

            case 11:
                if (Status == GameState.GameClear && Stars >= 1)
                    Level3.interactable = true;
                break;

            case 12:
                if (Status == GameState.GameClear && Stars >= 1)
                    Level4.interactable = true;
                break;

            case 13:
                if (Status == GameState.GameClear && Stars >= 1)
                    Level5.interactable = true;
                break;

            case 14:
                if (Status == GameState.GameClear && Stars >= 1)
                    GG.SetActive(true);
                break;
        }
    }

    IEnumerator InstantiateMethod()
    {
        if (Status == GameState.Playing)
        {
            IsSpawning = false;
            AddCustomer();
            yield return new WaitForSeconds(3f);
            IsSpawning = true;

        }
        yield return null;
    }
}