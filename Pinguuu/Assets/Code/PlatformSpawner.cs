using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour {

    // viittaus blockkiin.
    public GameObject[] blockRandom;

    // Startissa lähdetään kutsumaan spawnblockia nimistä metodia tietyn sekuntimäärän
    // jälkeen tietyn aikavälin välein.
    void Start () {
        InvokeRepeating("SpawnBlock", 2, 6.2f);
    }

    private void SpawnBlock()
    {
        Instantiate(blockRandom[Random.Range(0,25)]);
    }
}