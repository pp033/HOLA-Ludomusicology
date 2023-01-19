using UnityEngine;

public class Point : Collectable
{
    [SerializeField] private Notes note;

    public Notes Note { get; private set; }

    protected override void Start()
    {
        base.Start();
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
