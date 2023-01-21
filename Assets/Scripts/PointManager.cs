using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Newtonsoft.Json;

public class PointManager : MonoBehaviour
{
    [SerializeField] private List<AudioClip> audioclips;
    [SerializeField] private string jsonfile;

    [SerializeField] private string scoreWord;
    [SerializeField] private Text scoreUI;

    private System.Random random = new System.Random();

    private List<List<int>> chords { get; set; }
    private int tact = 0;  

    private int score = 0;

    private GameObject cam;

    private void Awake()
    {
        InvokeRepeating("ChooseAudio", 0, 60 / GameObject.Find("Manager").GetComponent<Orchestra>().bpm * 4);
    }
    private void Start()
    {
        Deserialize();
        cam = GameObject.Find("Main Camera");
        scoreUI.text = scoreWord + score;       
    }

    public void Deserialize()
    {
        TextAsset json = Resources.Load<TextAsset>(jsonfile);
        chords = JsonConvert.DeserializeObject<List<List<int>>>(json.text);
    }

    public void AddPoints(GameObject obj)
    {
        int points = 0;

        switch (obj.GetComponent<Point>().Note)
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
        scoreUI.text = scoreWord + score;
    }

    private void ChooseAudio()
    {
        List<int> poss = new List<int>();
        foreach(int chord in chords[tact])
        {
            poss.Add(chord);
            Debug.Log("Takt " + tact + ", Chord Möglichkeit " + chord);
        }

        int r = random.Next(poss.Count);
        Debug.Log(poss[r]);
        // AudioSource.PlayClipAtPoint(audioclips[poss[r]], cam.transform.position); // todo: don't play here ofc, but in add points

        tact++;
    }
}
