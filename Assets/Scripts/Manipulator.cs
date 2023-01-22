using UnityEngine;

public class Manipulator : Collectable
{
    [SerializeField] public Manipulators manipulator;
    [SerializeField] public bool up;

    protected override void Start()
    {
        base.Start();
    }

    public enum Manipulators
    {
        Pitch,
        Volume,
        Speed,
        Rewind,
        Pause
    }
}
