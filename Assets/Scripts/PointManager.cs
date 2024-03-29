﻿using UnityEngine;
using System.Collections.Generic;

public class PointManager : MonoBehaviour
{
    [SerializeField] private List<AudioClip> audioclips;
    [SerializeField] private string jsonfile;
    [SerializeField] private int beats;

    private List<List<int>> chords;
    private int score = 0;

    private Orchestra orchestra;
    private GameObject cam;
    private View view;

    private System.Random random = new System.Random();

    private void Start()
    {
        orchestra = GetComponent<Orchestra>();
        cam = GameObject.Find("Main Camera");
        view = GameObject.Find("UI").GetComponent<View>();

        JsonDeserializer deserializer = new JsonDeserializer();
        chords = deserializer.Deserialize(jsonfile);
    }

    public void AddPoints(GameObject obj)
    {
        PlaySound();

        int points = 0;

        switch (obj.GetComponent<Point>().note)
        {
            case Point.Notes.ganz:
                points = 8;
                break;
            case Point.Notes.halb:
                points = 4;
                break;
            case Point.Notes.viertel:
                points = 2;
                break;
            case Point.Notes.achtel:
                points = 1;
                break;
            default:
                break;
        }
        score = score + points;
        view.UpdateScore(score);
    }

    private void PlaySound()
    {
        // float time = orchestra.instruments[0].audiosource.timeSamples / orchestra.instruments[0].audiosource.clip.frequency;
        // TODO: use this wherever .time is used instead?

        float time = this.orchestra.instruments[0].audiosource.time;
        int tact = (int)(time / (60 / orchestra.bpm * beats));

        List<int> poss = new List<int>();
        foreach (int chord in chords[tact])
        {
            poss.Add(chord);
        }

        int r = random.Next(poss.Count);
        AudioSource.PlayClipAtPoint(audioclips[poss[r]], cam.transform.position);
    }
}
