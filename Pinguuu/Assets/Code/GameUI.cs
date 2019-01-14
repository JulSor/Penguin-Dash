using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : ForegroundScroll
{

    public float distanceAmount;

    Text distanceText;

    void Start()
    {
        distanceText = GetComponent<Text>();
        distanceAmount = 0;
    }

    void FixedUpdate()
    {
        if (isScrolling == true)
        {
            // distancea tulee hitusen per frame kerrottuna pelin nopeudella
            // tällöin distancea ei tule pausella ja on mahdollista tehdä nopeutus tai hidastusjuttuja cuckaamatta scorea
            distanceAmount += 0.1f * Time.timeScale;
            // muuttaa placeholdertekstin ja pyöristää lukeman
            distanceText.text = "Distance: " + Mathf.RoundToInt(distanceAmount) + " m";
        }
    }
}