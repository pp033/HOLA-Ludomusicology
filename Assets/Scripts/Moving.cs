using UnityEngine;

public class Moving : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float speed = 2f;

    private int current = 0;

    private void Update()
    {
        if(Vector2.Distance(waypoints[current].transform.position, transform.position) < .1f)
        {
            current++;
            if(current >= waypoints.Length)
            {
                current = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[current].transform.position, Time.deltaTime * speed);
    }
}
