using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] public float speed;

    [SerializeField] private GameObject leftBorder;
    [SerializeField] private GameObject rightBorder;
    [SerializeField] private GameObject recordNeedle;
    [SerializeField] private GameObject finishLine;

    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (Vector3.Distance(rightBorder.transform.position, finishLine.transform.position) > .1f)
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
            leftBorder.transform.Translate(speed * Time.deltaTime, 0, 0);
            rightBorder.transform.Translate(speed * Time.deltaTime, 0, 0);
            recordNeedle.transform.Translate(speed * Time.deltaTime, 0, 0);
        }

        transform.position = new Vector3(
            transform.position.x,
            Mathf.Clamp(player.transform.position.y, 0, 20),
            transform.position.z);
    }
}
