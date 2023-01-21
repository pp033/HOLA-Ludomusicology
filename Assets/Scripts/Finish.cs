using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    [SerializeField] GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            player.GetComponent<Lives>().Invoke("Finish", 2f);  // no player finish animation to call the finish from
        }
    }

}
