using UnityEngine;

public class Manipulator : Collectable
{
    [SerializeField] private Manipulators manipulator;
    [SerializeField] private bool up;

    public Manipulators Manip { get; private set; }
    public bool Up { get; private set; }

    protected override void Start()
    {
        base.Start();
        Manip = manipulator;
        Up = up;
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
