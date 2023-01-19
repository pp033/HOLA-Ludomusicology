using UnityEngine;
using UnityEngine.UI;

public class PointManager : MonoBehaviour
{
    [SerializeField] private AudioClip audioclip;
    
    [SerializeField] private string scoreWord;
    [SerializeField] private Text scoreUI;

    private int score = 0;

    private GameObject cam;

    private void Start()
    {
        cam = GameObject.Find("Main Camera");
        scoreUI.text = scoreWord + score;
    }

    public void AddPoints(GameObject obj)
    {
        AudioSource.PlayClipAtPoint(audioclip, cam.transform.position);

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
}
