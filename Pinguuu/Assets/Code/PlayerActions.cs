using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : ForegroundScroll {

    // Luodaan Animator nimeltä thisAnim, jolla myöhemmin ohjataan Animator-komponenttia.
    private Animator thisAnim;

    // Pingviinin Rigidbody
    private Rigidbody2D rb;

    // Hypyn force
    public float jumpForce = 3f;

    //Nopean laskeutuminen force
    public float fastLandingForce = 10f;

    //Slide force
    public float slideForce = 10f;

    // Montako kertaa on hypitty sitten laskeutumisen. Kertoo myös hahmon tilan. Arvo 0-2.
    public int jumpCount = 0;

    public bool onAir = false;

    static public int playerHealth = 3;

    public AudioSource slapping;

    public GameObject flashRed;

    SpriteRenderer dmg_SpriteRenderer;
    Color dmg_newColor;

    // Startissa päästään käsiksi komponentteihin
    void Start () {
        // Rigidbody2D rb on nyt scriptin peliobjektin Rigidbody2D komponentti
        rb = GetComponent<Rigidbody2D>();
        // Animator thisAnim on nyt scriptin peliobjektin Animator komponentti
        thisAnim = GetComponent<Animator>();
        playerHealth = 3;
        dmg_SpriteRenderer = flashRed.GetComponent<SpriteRenderer>();
        dmg_SpriteRenderer.color = new Color(255,255,255,255);
    }


    // update odottaa näppäinten painamista
    void Update()
    {
        print(playerHealth);

        // kun hyppää välilyönnillä, kutsutaan Jump metodia
        if (Input.GetKeyDown("space") && (isScrolling == true)) {
            Jump();
        }
        print(isScrolling);
        // Animaationvaihtokoodia. Animaattorin int jumpCount määräytyy pelaajan int jumpCountista.
        thisAnim.SetInteger("jumpCount", jumpCount);
        // idletilan määritykseen animatorissa
        if (isScrolling == true) {
            thisAnim.SetBool("raceStarted", true);
        }
        else {
            thisAnim.SetBool("raceStarted", false);
        }

        //Kutsutaan FastLanding metodia, kun pelaaja painaa nuolen alas.
        if (Input.GetKeyDown("down"))
        {
            FastLanding();
        }
        //Kutsutaan Sliding metodia, kun pelaaja painaa nuolen oikealle. *Keskeneräinen*
        if (Input.GetKeyDown("right") && (!onAir) && (isScrolling == true))
        {
            Sliding();
            thisAnim.SetTrigger("Slide");
            StartCoroutine("WaitASec");
        }

        if (Input.GetKeyDown("left") && (!onAir) && (isScrolling == true))
        {
            SlidingBackwards();
        }
        if (jumpCount == 0 && isScrolling == true)
        {
            slapping.UnPause();
        }
        if (jumpCount != 0)
        {
            slapping.Pause();
        }
        if (isScrolling == false)
        {
            slapping.Pause();
        }
        if (playerHealth == 1)
        {
            GameObject.FindGameObjectWithTag("Health 2").transform.position = new Vector3(-10f, 4.5f, transform.position.z);
            GameObject.FindGameObjectWithTag("Health 3").transform.position = new Vector3(-10f, 4.5f, transform.position.z);
        }
        if (playerHealth == 2) {
            GameObject.FindGameObjectWithTag("Health 2").transform.position = new Vector3(5.5f, 4.5f, transform.position.z);
            GameObject.FindGameObjectWithTag("Health 3").transform.position = new Vector3(10f, 4.5f, transform.position.z);
        }
        if (playerHealth == 3)
        {
            GameObject.FindGameObjectWithTag("Health 2").transform.position = new Vector3(5.5f, 4.5f, transform.position.z);
            GameObject.FindGameObjectWithTag("Health 3").transform.position = new Vector3(6f, 4.5f, transform.position.z);
        }
     }


    //Coroutine et saa sliding animaatio triggerin pois
    IEnumerator WaitASec()
    {
        yield return new WaitForSeconds(1f);
        thisAnim.ResetTrigger("Slide");
        
    }

    //Coroutine et saa värifaden aikaiseksi
    IEnumerator Fade()
    {
        for (float f = 1f; f >= 0; f -= 0.1f)
        {
            Color c = dmg_SpriteRenderer.color;
            c.a = f;
            dmg_SpriteRenderer.color = c;
            yield return new WaitForSeconds(.001f);
        }
        for (float f = 0f; f <= 1; f += 0.1f)
        {
            Color c = dmg_SpriteRenderer.color;
            c.a = f;
            dmg_SpriteRenderer.color = c;
            yield return new WaitForSeconds(.001f);
        }
        for (float f = 1f; f >= 0; f -= 0.1f)
        {
            Color c = dmg_SpriteRenderer.color;
            c.a = f;
            dmg_SpriteRenderer.color = c;
            yield return new WaitForSeconds(.001f);
        }
        for (float f = 0f; f <= 1; f += 0.1f)
        {
            Color c = dmg_SpriteRenderer.color;
            c.a = f;
            dmg_SpriteRenderer.color = c;
            yield return new WaitForSeconds(.001f);
        }
        dmg_SpriteRenderer.color = Color.white;
    }

    // Kun peliobjekti collidaa kentän kanssa, asetetaan onLand muuttuja trueen ja palautetaan jumpCount eli hyppyresurssit alkutilaan.
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Kenttä")
        {
            jumpCount = 0;
            onAir = false;
        }
    }

    // Kun törmää Hazardiin menettää healthia.
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hazard")
        {
            dmg_SpriteRenderer.color = Color.red;
            StartCoroutine("Fade");
            playerHealth -= 1;
        }
        if (collision.gameObject.tag == "Fish")
        {
            playerHealth = playerHealth + 1;
            Destroy(collision.gameObject);
        }
    }

    // Hyppymetodi
    public void Jump()
    {
        // Hyppy ja tuplahyppy.
        if ( Input.GetButtonDown("Jump") && (jumpCount <= 1) ) {
            //Nopeuden nollaus.
            rb.velocity = Vector2.zero;
            // saattaa tarvita myös -> rb.angularVelocity = 0f;
            // Itse hyppykoodi
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            jumpCount += 1;
            onAir = true;
        }
    }

    //Nopean laskeutumisen metodi. Kutsutaan Updatessa, kun pelaaja painaa nuolta alas.
    public void FastLanding()
    {
        {
            rb.AddForce(new Vector2(0, -fastLandingForce), ForceMode2D.Impulse);
        }
    }

    //Liukumismetodi. Pingiivini liukuu eteenpäin. KESKENERÄINEN. Tällä hetkellä
    //pingiivini liukuu niin kauan kun pidetään nuolta oikealle pohjassa.
    public void Sliding()
    {
        rb.AddForce(new Vector2(slideForce, 0), ForceMode2D.Impulse);
    }
    public void SlidingBackwards()
    {
        rb.AddForce(new Vector2(-slideForce, 0), ForceMode2D.Impulse);
    }
}   

