using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
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

        // TODO: use delta time for camera movement to be independent from framerate?
    }
}
