using UnityEngine;

public class Instrument : Collectable
{
    [SerializeField] public Instruments instrument;

    protected override void Start()
    {
        base.Start();
    }

    public enum Instruments
    {
        MAIN,
        guitar,
        trumpet,
        drums,
        keys,
        synths,
        strings, 
        brass,
        sax,
        bass
    }
}
