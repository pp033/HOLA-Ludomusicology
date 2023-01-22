using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class View : MonoBehaviour
{
    [SerializeField] private Text scoreUI;

    [SerializeField] private HorizontalLayoutGroup livesUI;
    [SerializeField] private Sprite livesOn;
    [SerializeField] private Sprite livesOff;

    private void Start()
    {
        for (int i = 0; i < livesUI.GetComponentsInChildren<Image>().Length; i++)
        {
            livesUI.GetComponentsInChildren<Image>()[i].sprite = livesOn;
        }

        scoreUI.text = "0";
    }

    public void UpdateScore(int score)
    {
        scoreUI.text = score.ToString();
    }

    public void UpdateLives(int lives)  // NOTE: currently, you can only lose lives
    {
        Image[] images = livesUI.GetComponentsInChildren<Image>();
        images[lives].sprite = livesOff; 
    }
}
