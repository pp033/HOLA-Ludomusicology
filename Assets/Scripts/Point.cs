using UnityEngine;

public class Point : Collectable
{
    [SerializeField] public Notes note;

    protected override void Start()
    {
        base.Start();
    }

    public enum Notes
    {
        ganz,
        halb,
        viertel,
        achtel
    }
}
