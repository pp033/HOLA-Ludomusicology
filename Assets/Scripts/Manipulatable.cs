using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manipulatable : MonoBehaviour
{
    [SerializeField] private Manipulators type;

    public Manipulators Type { get; private set; }

    private void Start()
    {
        Type = type;
    }

    public enum Manipulators
    {
        MAIN,
        guitar,
        trumpet,
        drums
    }
}
