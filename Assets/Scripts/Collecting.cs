using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collecting : MonoBehaviour
{
    [SerializeField] private Text score;

    private int counter = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // not "on collision enter" bc it's set to "is trigger"

        if(collision.gameObject.CompareTag("Collectable"))
        {
            int points = 0;

            switch (collision.gameObject.GetComponent<Collectable>().Note)
            {
                case Collectable.Notes.ganz:
                    points = 8;
                    break;
                case Collectable.Notes.halb:
                    points = 4;
                    break;
                case Collectable.Notes.viertel:
                    points = 2;
                    break;
                case Collectable.Notes.achtel:
                    points = 1;
                    break;
                default:
                    break;
            }

            Destroy(collision.gameObject);
            counter = counter + points;
            score.text = "Score: " + counter;
        }
    }
}
