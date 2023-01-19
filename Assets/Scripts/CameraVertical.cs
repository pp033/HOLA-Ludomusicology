using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraVertical : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private void Update()
    {
        transform.position = new Vector3(
            transform.position.x,
            Mathf.Clamp(player.transform.position.y, 0, 20),
            transform.position.z);
    }
}
