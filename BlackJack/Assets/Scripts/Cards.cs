using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cards : MonoBehaviour
{
    private GameObject prefab;
    private int point;
    public int Point { get { return this.point; } }
    public GameObject Prefab { get { return this.prefab; } }

    public Cards(GameObject prefab)
    {
        this.prefab = prefab;           //takes the prefab of the card
        string name = prefab.name;      //makes this string the name of the prefab
        int point;                      //this is for the value of the cards
        switch (name.Substring(name.Length - 1))        //Takes the string and switches it with the number its value is,
        {                                               //The cases are for face cards and switches the last letter
                                                        //To equal the value corresponding to that face card
            case "e":                                   // ace - last letter is e 
                point = 11;                             //makes the value = 11 ... will make a change in game manager for when ace can = 1
                break;
            case "k":                                   // jacK - last letter is k
            case "g":                                   // kinG - last letter is g
            case "n":                                   // queeN - last letter is n
            case "0":                                   // 10 - last character is 0
                point = 10;                             //makes the value = 10
                break;
            default:
                // other remaining possible cards, 2 - 9
                point = Convert.ToInt16(name.Substring(name.Length - 1);   //This converts the last character in the string to the corresponding value of the card
                break;
        }
        this.point = point;
    }
}
