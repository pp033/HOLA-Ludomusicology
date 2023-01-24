using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;

public class PointManager : MonoBehaviour
{
    [SerializeField] private List<AudioClip> audioclips;
    [SerializeField] private string jsonfile;

    private List<List<int>> chords { get; set; }
    private int score = 0;

    private Orchestra orchestra;
    private GameObject cam;
    private View view;

    private System.Random random = new System.Random();

    private void Start()
    {
        Deserialize();
        cam = GameObject.Find("Main Camera");
        view = GameObject.Find("UI").GetComponent<View>();
        orchestra = GetComponent<Orchestra>();
    }

    public void Deserialize()
    {
        TextAsset json = Resources.Load<TextAsset>(jsonfile);
        chords = JsonConvert.DeserializeObject<List<List<int>>>(json.text);
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
        // TODO: Könnte auf anderen Systemen wichtig werden

        float time = orchestra.instruments[0].audiosource.time;
        int tact = (int)(time / (60 / orchestra.bpm * 4));

        List<int> poss = new List<int>();
        foreach (int chord in chords[tact])
        {
            poss.Add(chord);
        }

        int r = random.Next(poss.Count);
        AudioSource.PlayClipAtPoint(audioclips[poss[r]], cam.transform.position);
        Debug.Log("Takt " + tact + ", Chord " + poss[r]);
    }
}
