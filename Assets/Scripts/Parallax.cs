using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float parallax;

    private Vector3 cameraPosition;

    private void Start()
    {
        cameraPosition = cameraTransform.position;
    }

    private void LateUpdate()
    {
        Vector3 delta = cameraTransform.position - cameraPosition;
        transform.position += delta * parallax;
        cameraPosition = cameraTransform.position;
    }
}
