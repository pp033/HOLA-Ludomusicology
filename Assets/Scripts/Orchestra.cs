﻿using System.Collections.Generic;
using UnityEngine;

public class Orchestra : MonoBehaviour
{
    public List<InstrumentWrapper> instruments;

    [SerializeField] public float bpm;
}

[System.Serializable]
public class InstrumentWrapper
{
    [SerializeField] public Instrument.Instruments instrument;
    [SerializeField] public GameObject tilemap;
    [SerializeField] public AudioSource audiosource;
    [SerializeField] public AudioClip audio;
    [SerializeField] public AudioClip audioPitchLow;
    [SerializeField] public AudioClip audioPitchHigh;
    [SerializeField] public GameObject inventory;
}
