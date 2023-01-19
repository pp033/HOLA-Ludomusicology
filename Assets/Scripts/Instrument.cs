using UnityEngine;

public class Instrument : Collectable
{
    [SerializeField] private Instruments instrument;

    public Instruments Inst { get; private set; }

    protected override void Start()
    {
        base.Start();
        Inst = instrument;
    }

    public enum Instruments
    {
        MAIN,
        guitar,
        trumpet,
        drums,
        keys
    }
}
