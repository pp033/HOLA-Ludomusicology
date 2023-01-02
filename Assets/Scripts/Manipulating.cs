using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manipulating : MonoBehaviour // Todo: auslagern mit bool param, null check
{
    [SerializeField] private GameObject tiles_beat;
    [SerializeField] private GameObject tiles_drums;
    [SerializeField] private GameObject tiles_trumpet;
    [SerializeField] private GameObject tiles_guitar;

    [SerializeField] private AudioSource audio_beat;
    [SerializeField] private AudioSource audio_drums;
    [SerializeField] private AudioSource audio_trumpet;
    [SerializeField] private AudioSource audio_guitar;

    private void Start()
    {
        tiles_beat.SetActive(true);
        audio_beat.mute = false;

        tiles_drums.SetActive(false);
        audio_drums.mute = true;

        tiles_guitar.SetActive(false);
        audio_guitar.mute = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // not "on collision enter" bc it's set to "is trigger"

        if (collision.gameObject.CompareTag("Manipulatable"))
        {
            switch (collision.gameObject.GetComponent<Manipulatable>().Type)
            {
                case Manipulatable.Manipulators.guitar:
                    tiles_guitar.SetActive(true);
                    audio_guitar.mute = false;
                    break;
                case Manipulatable.Manipulators.drums:
                    tiles_drums.SetActive(true);
                    audio_drums.mute = false;
                    break;
                case Manipulatable.Manipulators.trumpet:
                    //
                    break;
                default:
                    break;
            }

            Destroy(collision.gameObject);
        }
    }
}
