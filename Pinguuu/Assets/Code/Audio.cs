using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour {

    // background musicit
    //Day 6
    public AudioClip startingClip;
    public AudioClip loopClip;

    // Use this for initialization
    void Start () {
        StartCoroutine(playSound());
    }

    IEnumerator playSound()
    {
        GetComponent<AudioSource>().clip = startingClip;
        GetComponent<AudioSource>().loop = false;
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(startingClip.length);
        GetComponent<AudioSource>().clip = loopClip;
        GetComponent<AudioSource>().Play();
        GetComponent<AudioSource>().loop = true;
    }
}
