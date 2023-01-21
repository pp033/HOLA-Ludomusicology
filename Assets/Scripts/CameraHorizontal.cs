using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHorizontal : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private GameObject leftBorder;
    [SerializeField] private GameObject rightBorder;
    [SerializeField] private GameObject plattenspielernadel;
    [SerializeField] private GameObject finishLine;

    public float publicSpeed;

    private void Start()
    {
        publicSpeed = speed;
    }

    private void Update()
    {

        if (Vector3.Distance(rightBorder.transform.position, finishLine.transform.position) > .1f)
        {
            transform.Translate(publicSpeed * Time.deltaTime, 0, 0);
            leftBorder.transform.Translate(publicSpeed * Time.deltaTime, 0, 0);
            rightBorder.transform.Translate(publicSpeed * Time.deltaTime, 0, 0);
            plattenspielernadel.transform.Translate(publicSpeed * Time.deltaTime, 0, 0);
        }
    }
}
