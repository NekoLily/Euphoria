using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class DataBase : MonoBehaviour
{
    public int[] Table;
    public string cocktail;

    // Position des tables
    public Vector3 Pos_Solo_1;
    public Vector3 Pos_Solo_2;
    public Vector3 Pos_Solo_3;
    public Vector3 Pos_Solo_4;

    public string[] Tab_Score;

    void Start()
    {
        Table = new int[4] { 0, 0, 0, 0 };
        GetSave();
    }

    public void ResetTable()
    {
        Table = new int[4] { 0, 0, 0, 0 };
    }

    public int GetTable()
    {
        int seed = Environment.TickCount;
        System.Random Rnd = new System.Random(seed);
        //for (int i = 0; i < 4; i++)
        //{
        int i = Rnd.Next(0, 4);
            if (Table[i] == 0)
            {
                Table[i] = 1; // Table Occupée
                return (i); // retourne l'ID de la table
            }
        //}
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

    public void GetSave()
    {
        string _Text = "";
        StreamReader _Reader = new StreamReader("Assets/Resources/Save.txt");
        {
            while (!_Reader.EndOfStream)
            {
                _Text += _Reader.ReadLine();
                _Text += "\n";
            }
            _Reader.Close();
            Tab_Score = _Text.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
        }
    }

    public void SaveScore(int Level, int Score)
    {
        if (Score >= int.Parse(Tab_Score[Level - 1]))
        {
            Tab_Score[Level - 1] = "" + Score;
            StreamWriter _Writter = new StreamWriter("Assets/Resources/Save.txt");
            {
                for (int i = 0; i < 5; i++)
                    _Writter.WriteLine(Tab_Score[i]);
                _Writter.Close();
            }
        }
    }

    public void ResetScore()
    {
        StreamWriter _Writter = new StreamWriter("Assets/Resources/Save.txt");
        {
            for (int i = 0; i < 5; i++)
                _Writter.WriteLine("0");
            _Writter.Close();
        }
    }
}
