using UnityEngine;

public class CameraVertical : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        transform.position = new Vector3(
            transform.position.x,
            Mathf.Clamp(player.transform.position.y, 0, 20),
            transform.position.z);
    }
}
