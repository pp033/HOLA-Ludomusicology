using UnityEngine;

public class CameraHorizontal : MonoBehaviour
{
    [SerializeField] public float speed;

    [SerializeField] private GameObject leftBorder;
    [SerializeField] private GameObject rightBorder;
    [SerializeField] private GameObject recordNeedle;
    [SerializeField] private GameObject finishLine;

    private void Update()
    {
        if (Vector3.Distance(rightBorder.transform.position, finishLine.transform.position) > .1f)
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
            leftBorder.transform.Translate(speed * Time.deltaTime, 0, 0);
            rightBorder.transform.Translate(speed * Time.deltaTime, 0, 0);
            recordNeedle.transform.Translate(speed * Time.deltaTime, 0, 0);
        }
    }
}
