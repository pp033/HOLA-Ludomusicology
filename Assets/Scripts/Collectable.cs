using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    GameObject manager;
    PointManager pointManager;
    InstrumentManager instrumentManager;
    ManipulatorManager manipulatorManager;

    protected virtual void Start()
    {
        manager = GameObject.Find("Manager");
        pointManager = manager.GetComponent<PointManager>();
        instrumentManager = manager.GetComponent<InstrumentManager>();
        manipulatorManager = manager.GetComponent<ManipulatorManager>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // not "on collision enter" bc it's set to "is trigger" only

        if (this is Point)
        {
            pointManager.AddPoints(gameObject);
        }
        if(this is Instrument)
        {
            instrumentManager.AddInstrument(gameObject);
        }
        if (this is Manipulator)
        {
            manipulatorManager.AddManipulation(gameObject);
        }

        Destroy(gameObject);
    }
}
