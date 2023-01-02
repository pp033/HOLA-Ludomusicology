using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private Notes note;

    public Notes Note { get; private set; }

    private void Start()
    {
        Note = note;
    }
    public enum Notes
    {
        ganz,
        halb,
        viertel,
        achtel
    }
}
