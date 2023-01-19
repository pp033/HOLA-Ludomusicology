﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orchestra : MonoBehaviour
{
    public List<InstrumentWrapper> instruments;
}

[System.Serializable]
public class InstrumentWrapper
{
    [SerializeField] public Instrument.Instruments instrument;
    [SerializeField] public GameObject tilemap;
    [SerializeField] public AudioSource audiosource;
}
