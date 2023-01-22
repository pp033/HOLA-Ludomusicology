using UnityEngine;

public class Finish : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            player.GetComponent<Lives>().Invoke("Finish", 2f);      // NOTE: no player finish animation to call the finish from
        }
    }

}
