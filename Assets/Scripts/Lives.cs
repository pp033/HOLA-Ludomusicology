using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lives : MonoBehaviour 
{
    private View view;
    private LevelManager leveling;

    private GameObject manager;
    private Orchestra orchestra;
    private Animator anim;
    private Rigidbody2D rb;
    private GameObject cam;

    private int lives = 3;

    [SerializeField] private List<AudioClip> damageSounds;
    [SerializeField] private AudioClip deathSound;

    private void Start()
    {
        manager = GameObject.Find("Manager");
        leveling = manager.GetComponent<LevelManager>();
        orchestra = manager.GetComponent<Orchestra>();
        view = GameObject.Find("UI").GetComponent<View>();
        cam = GameObject.Find("Main Camera");
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

  

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // NOTE: in comparison to collectables, traps are physical colliders, not just triggers

        if (collision.collider.gameObject.CompareTag("Trap"))
        {
            LoseLife();
        }
    }

    private void LoseLife()
    {
        anim.SetTrigger("Damage");

        lives--;
        view.UpdateLives(lives);

        // LoseInstrument();        // NOTE: feature not wanted anymore

        var random = new System.Random();
        int r = random.Next(damageSounds.Count);
        AudioSource.PlayClipAtPoint(damageSounds[r], cam.transform.position);

        if (lives <= 0)
        {
            Die();
        }
    }

    private void LoseInstrument()   // TODO: if you didn't lose an instrument, don't show the collectible again
    {
        List<InstrumentWrapper> available = new List<InstrumentWrapper>();

        foreach (InstrumentWrapper i in orchestra.instruments)
        {
            if(i.tilemap && i.tilemap.activeInHierarchy)
            {
                available.Add(i);
            }
        }

        if(available.Count > 1) {

            var random = new System.Random();
            int r = random.Next(orchestra.instruments.Count);

            if (orchestra.instruments[r].instrument != Instrument.Instruments.MAIN) 
            {
                orchestra.instruments[r].tilemap.SetActive(false);
                orchestra.instruments[r].audiosource.mute = true;
            }
            else
            {
                LoseInstrument();
            }
        }

        view.UpdateInventory();
    }

    public void Die()
    {
        AudioSource.PlayClipAtPoint(deathSound, cam.transform.position);

        anim.SetTrigger("Death");
        rb.bodyType = RigidbodyType2D.Static;
        cam.GetComponent<CameraHorizontal>().enabled = false;
    }

    public void Finish()
    {
        leveling.LoadNextScene();
    }

    private void Restart()  // NOTE: called from player death animation
    {
        leveling.ReloadScene();
    }
}
