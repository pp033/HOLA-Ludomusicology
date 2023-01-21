﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lives : MonoBehaviour 
{
    private Animator anim;
    private Rigidbody2D rb;
    private GameObject cam;

    [SerializeField] private Orchestra orchestra;
    [SerializeField] private List<AudioClip> damageSounds;
    [SerializeField] private AudioClip deathSound;

    [SerializeField] private int lives = 3;

    private void Start()
    {
        cam = GameObject.Find("Main Camera");
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // in comparison to collectables, traps are physical, not just triggers

        if (collision.gameObject.CompareTag("Trap"))
        {
            LoseLife();
        }
    }

    private void LoseLife()
    {
        // anim.SetTrigger("Hit");

        LoseInstrument();
        lives--;
        Debug.Log("Übrige Leben:" + lives);

        var random = new System.Random();
        int r = random.Next(damageSounds.Count);
        AudioSource.PlayClipAtPoint(damageSounds[r], cam.transform.position);

        if (lives <= 0)
        {
            Die();
        }
    }

    private void LoseInstrument()   // TODO: if you didn't lose an instrument, don't show the collectible again
    {
        List<InstrumentWrapper> available = new List<InstrumentWrapper>();

        foreach (InstrumentWrapper i in orchestra.instruments)
        {
            if(i.tilemap && i.tilemap.activeInHierarchy)
            {
                available.Add(i);
            }
        }

        if(available.Count > 1) {

            var random = new System.Random();
            int r = random.Next(orchestra.instruments.Count);

            if (orchestra.instruments[r].instrument != Instrument.Instruments.MAIN) 
            {
                orchestra.instruments[r].tilemap.SetActive(false);
                orchestra.instruments[r].audiosource.mute = true;
            }
            else
            {
                LoseInstrument();
            }
        }
    }

    public void Die()
    {
        AudioSource.PlayClipAtPoint(deathSound, cam.transform.position);

        anim.SetTrigger("Death");
        rb.bodyType = RigidbodyType2D.Static;
        cam.GetComponent<CameraHorizontal>().enabled = false;
    }

    public void Finish()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void Restart()                                                      // called from player death animation
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);             // TODO: go back to menu instead?
    }
}