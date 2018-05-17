using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    public GameState Status { get; set; }
    public System.Random Rnd;
    int seed = Environment.TickCount;
    string CocktailString;
    int ID_Cocktail = 0;

    DataBase _DataBase;
    //Text _TimerText;

    // Use this for initialization
    void Start()
    {
        Status = GameState.Playing;
        _DataBase = gameObject.GetComponent<DataBase>();
        //_TimerText = GameObject.Find("TimerText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Status == GameState.Playing)
        {
            //_TimerText.text = Mathf.Ceil(Time.deltaTime).ToString();
            if (Input.GetMouseButtonDown(0)) // Prend le click
            {
                Vector3 CAM_POS = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
                Collider2D Collider = Physics2D.OverlapPoint(CAM_POS); // Check si il trouve un collider à l'endroit cliquer

                if (Collider)
                    Collider.gameObject.GetComponent<Customer>().CheckOrder(ID_Cocktail); // Lance la méthode pour check si la commande match
            }
            Rnd = new System.Random(seed++);
            AddCustomer(); // Test
        }
    }

    void AddCustomer()
    {
                for (int i = 0; i < 4; i++)
                {
                    if (_DataBase.Table[i] == 0) // Regarde quelle table est inoccupée
                    {
                        Instantiate(Resources.Load("Prefab/Customer/Solo"));
                        return;
                    }
                }
    }

    public void OnClickEvier()
    {
        CocktailString = "";
        //Changement de sprite a 0.
    }

    public void OnClickBartender()
    {
        
        ID_Cocktail = _DataBase.Shaker(CocktailString);
        Debug.Log(CocktailString + " " + ID_Cocktail);
        CocktailString = "";
        if (ID_Cocktail != -1)
        {
            
            //Changement du sprite du curseurs
        }
        else
            return; // Mauvais Cocktail
    }

    public void OnClickCocktail(int num)  //Ajoute au string l'ID de l'élément ajouté.
    {
        CocktailString += num;
    }
}