using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float parallax;

    private Vector3 cameraPosition;
    private float unit;

    private void Start()
    {
        cameraPosition = cameraTransform.position;

        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        unit = texture.width / sprite.pixelsPerUnit;
    }

    private void LateUpdate()
    {
        Vector3 delta = cameraTransform.position - cameraPosition;
        transform.position += delta * parallax;
        cameraPosition = cameraTransform.position;

        /* 
         
        // codemonkey video 7:10 für untersch. horizontale und vertikale parallaxen

        if(cameraTransform.position.x - transform.position.x >= unit)
        {
            float offset = (cameraTransform.position.x - transform.position.x) % unit;
            transform.position = new Vector3(cameraTransform.position.x + offset, transform.position.y);
        }

        */
    }
}
