using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForegroundScroll : Player {

    // isScrolling = true -> kenttä liikkuu vasemmalle.
    public static bool isScrolling = false;

    // Kuinka nopeasti kenttä liikkuu. Arvo annetaan unityn puolella.
    [Range(+10, -10)]
    public float scrollSpeed = -5f;


    private void Start()
    {
        // suorittaa metodin x float y sekunnin päästä
        // käytetääm aloittamaan ruudun scrollaus y sekunnin päästä
        Invoke("StartingLineWait", 2f);
    }

    void StartingLineWait()
    {
        isScrolling = true;
    }

    void Update() {
        // Scrollaa ruutua
        if (isScrolling == true) {
            // liikuta skriptin alaisia objekteja vasemmalle negatiivisella scrollSpeed nopeudella
            transform.localPosition += transform.right * Time.deltaTime *  scrollSpeed; 
        }
    }

}