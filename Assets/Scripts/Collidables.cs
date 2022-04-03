using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidables : MonoBehaviour
{
    private DogManager dm;
    private TextSetter ts;
    private AudioSource good;
    private AudioSource bad;

    void Start()
    {
        dm = GameObject.Find("Dog").GetComponent<DogManager>();
        ts = GameObject.FindGameObjectWithTag("TextSetter").GetComponent<TextSetter>();
        good = GameObject.Find("GoodSound").GetComponent<AudioSource>();
        bad = GameObject.Find("BadSound").GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (this.gameObject.name.Contains("Stick")
            || this.gameObject.name.Contains("Bone")
            || this.gameObject.name.Contains("Sleep")
            || this.gameObject.name.Contains("Heart"))
        {
            // Heart popup
            good.Play();
            dm.heartScore++;
            ts.UpdateScores();
            dm.ShowHeart();
        }
        else if (this.gameObject.name.Contains("Mite")
            || this.gameObject.name.Contains("Pill")
            || this.gameObject.name.Contains("Worm"))
        {
            // Skull popup
            bad.Play();
            dm.skullScore++;
            ts.UpdateScores();
            dm.ShowSkull();
        }

        Destroy(this.gameObject);
    }
}
