using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBase : MonoBehaviour
{
    public int[] Table;
    public string cocktail;

    // Position des tables
    public Vector3 Pos_Solo_1;
    public Vector3 Pos_Solo_2;
    public Vector3 Pos_Solo_3;
    public Vector3 Pos_Solo_4;

    void Start()
    {
        Table = new int[4];
    }

    public int GetTable()
    {
        for (int i = 0; i < 4; i++)
        {
            if (Table[i] == 0)
            {
                Table[i] = 1; // Table Occupée
                return (i); // retourne l'ID de la table
            }
        }
        return -1;
                
    }

    public Vector3 FindTable(int ID_Table) // Trouve la position de la table avec L'ID_Table
    {
        switch (ID_Table)
        {
            case 0:
                return Pos_Solo_1;
            case 1:
                return Pos_Solo_2;
            case 2:
                return Pos_Solo_3;
            case 3:
                return Pos_Solo_4;
        }
        return new Vector3();
    }


    public void LeaveTable(int ID_Table) // Met la table inocupée
    {
        Table[ID_Table] = 0;
    }

    public int Shaker(string Cocktail)    //Validation du cocktail créé.
    {
        switch (Cocktail) //retourne en fonction du string le cocktail créé.
        {
            case "113":  //LastCall
                return 100;
            case "311":  //Bière
                return 101;
            case "45":  //KirRoyal
                return 102;
            case "21011":  //Tropical
                return 103;
            case "73":  //SakéBomb
                return 104;
            case "612":  //Whisky
                return 105;
            case "154":  //PinkLove
                return 106;
            case "812":  //CubaLibre
                return 107;
            case "81011":  //Daïgoro
                return 108;
            case "213":  //BloodyMary
                return 109;
            default:
                return 0;
        }
    }
}
