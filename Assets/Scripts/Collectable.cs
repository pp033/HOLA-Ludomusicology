using UnityEngine;

public class Collectable : MonoBehaviour
{
    private GameObject manager;
    private PointManager pointManager;
    private InstrumentManager instrumentManager;
    private ManipulatorManager manipulatorManager;
    private Orchestra orchestra;

    protected virtual void Start()
    {
        manager = GameObject.Find("Manager");
        orchestra = manager.GetComponent<Orchestra>();
        pointManager = manager.GetComponent<PointManager>();
        instrumentManager = manager.GetComponent<InstrumentManager>();
        manipulatorManager = manager.GetComponent<ManipulatorManager>();

        InvokeRepeating("WiggleUp", 0, 60 / orchestra.bpm);
        InvokeRepeating("WiggleDown", 60 / orchestra.bpm / 2, 60 / orchestra.bpm);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // NOTE: in comparison to traps, collectables are just triggers, not physical colliders

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

    private void WiggleUp()
    {
        transform.SetPositionAndRotation(
            new Vector3(
                transform.position.x, 
                transform.position.y + 0.1f, 
                transform.position.z), 
            transform.rotation);
    }

    private void WiggleDown()
    {
        transform.SetPositionAndRotation(
            new Vector3(
                transform.position.x,
                transform.position.y - 0.1f,
                transform.position.z),
            transform.rotation);
    }
}
