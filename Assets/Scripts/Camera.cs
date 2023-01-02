using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private GameObject leftBorder;
    [SerializeField] private GameObject rightBorder;
    [SerializeField] private GameObject background;

    private void Update()
    {
        transform.Translate(speed, 0, 0);
        leftBorder.transform.Translate(speed, 0, 0);
        rightBorder.transform.Translate(speed, 0, 0);
        background.transform.Translate(speed, 0, 0);
    }
}
