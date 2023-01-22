using UnityEngine;

public class ManipulatorManager : MonoBehaviour
{
    private Orchestra orchestra;
    private GameObject player;
    private GameObject cam;

    [SerializeField] private AudioClip audioclipCollect;
    [SerializeField] private AudioClip audioclipCountdown;

    [Header("Pitch Manipulation")]
    [SerializeField] private float pitchCountdown = 10f;
    [SerializeField] private float pitchMusicChange = 0.5f;
    [SerializeField] private int pitchPlatformChange = 2;

    [Header("Volume Manipulation")]
    [SerializeField] private float volumeCountdown = 10f;
    [SerializeField] private float volumeMusicChange = 0.2f;
    [SerializeField] private float volumeSizeChange = 0.5f;
    [SerializeField] private float volumeJumpChange = 2;

    [Header("Rewind Manipulation")]
    [SerializeField] private float rewindCountdown = 10f;

    [Header("Pause Manipulation")]
    [SerializeField] private float pauseCountdown = 10f;
    [SerializeField] private AudioClip pauseAudio;

    private void Start()
    {
        player = GameObject.Find("Player");
        cam = GameObject.Find("Main Camera");
        orchestra = GetComponent<Orchestra>();
    }

    public void AddManipulation(GameObject obj)
    {
        if(obj.GetComponent<Manipulator>().manipulator != Manipulator.Manipulators.Pause)
        {
            AudioSource.PlayClipAtPoint(audioclipCollect, cam.transform.position);
        }

        bool up = obj.GetComponent<Manipulator>().up;

        switch (obj.GetComponent<Manipulator>().manipulator)
        {
            case Manipulator.Manipulators.Pitch:
                ChangePitch(up);
                gameObject.AddComponent<Countdown>().Construct(pitchCountdown, audioclipCountdown, ()=> ChangePitch(!up));
             // gameObject.AddComponent<Countdown>().Construct(pitchCountdown, this.Helper);
                break;
            case Manipulator.Manipulators.Volume:
                ChangeVolume(up);
                gameObject.AddComponent<Countdown>().Construct(volumeCountdown, audioclipCountdown, () => ChangeVolume(!up));
                break;
            case Manipulator.Manipulators.Rewind:
                Rewind(true);
                gameObject.AddComponent<Countdown>().Construct(rewindCountdown, audioclipCountdown, () => Rewind(false));
                break;
            case Manipulator.Manipulators.Pause:
                Pause(true);
                gameObject.AddComponent<Countdown>().Construct(pauseCountdown, audioclipCountdown, () => Pause(false));
                break;
            default:
                break;
        }
    }

    // private void Helper() { ChangePitch(false); } // little reminder what the lambda above actually kinda does

    private void ChangePitch(bool up) 
    {
        int platformChange = (up) ? pitchPlatformChange : pitchPlatformChange * -1;
        float musicChange = (up) ? pitchMusicChange : pitchMusicChange * -1;

        foreach (InstrumentWrapper i in orchestra.instruments)
        {
            if (i.tilemap != null && i.audiosource != null)
            {
                if (i.instrument != Instrument.Instruments.MAIN)
                {
                    i.tilemap.transform.SetPositionAndRotation(new Vector3(
                        i.tilemap.transform.position.x, 
                        i.tilemap.transform.position.y + platformChange,  
                        i.tilemap.transform.position.z), 
                        i.tilemap.transform.rotation);

                    i.audiosource.pitch += musicChange;
                }
                else
                {
                    i.audiosource.pitch += musicChange;
                }
            }
        }
        if (up && player.GetComponent<Moveable>().floor != null)    // sucks, but i don't know how to do it better
        {
            player.transform.SetPositionAndRotation(new Vector3(     
                player.transform.position.x,
                player.transform.position.y + platformChange,
                player.transform.position.z),
                player.transform.rotation);
        }
    }

    private void ChangeVolume(bool up)
    {
        float charChange = (up) ? volumeSizeChange : volumeSizeChange * -1;
        float musicChange = (up) ? volumeMusicChange : volumeMusicChange * -1;
        float jumpChange = (up) ? volumeJumpChange : volumeJumpChange * -1;

        foreach (InstrumentWrapper i in orchestra.instruments)
        {
            if (i.tilemap != null && i.audiosource != null)
            {
                i.audiosource.volume += musicChange;
            }
        }
        player.transform.localScale = new Vector3(     
            player.transform.localScale.x + charChange,
            player.transform.localScale.y + charChange,
            player.transform.localScale.z + charChange); 

        player.GetComponent<Moveable>().jump += jumpChange;
    }

    private void Rewind(bool rewind)
    {
        foreach (InstrumentWrapper i in orchestra.instruments)
        {
            if (i.audiosource != null)
            {
                i.audiosource.pitch = i.audiosource.pitch * -1;
            }
        }
 
        cam.GetComponent<Camera>().speed *= -1;
    }

    private void Pause(bool pause)
    {
        if (pause)
        {
            AudioSource.PlayClipAtPoint(pauseAudio, cam.transform.position);
        }

        cam.GetComponent<Camera>().enabled = !pause;
    }
}

public class Countdown : MonoBehaviour
{
    public delegate void Callback();
    protected Callback callback;

    private GameObject cam;

    private float countdown;
    private AudioClip countdownSound;
    private float countdownSoundLength;

    private bool played = false;
    private bool invoked = false;

    public void Construct(float countdown, AudioClip countdownSound, Callback callback)
    {
        cam = GameObject.Find("Main Camera");

        this.countdown = countdown;
        this.callback = callback;
        this.countdownSound = countdownSound;

        countdownSoundLength = this.countdownSound.length;
    }

    private void Update()
    {
        if (countdown > 0f)
        {
            if(countdown <= countdownSoundLength && !played)
            {
                AudioSource.PlayClipAtPoint(countdownSound, cam.transform.position);
                played = true;
            }
            countdown = countdown - (Time.deltaTime);
        }
        else
        {
            if(!invoked)
            {
                callback.Invoke();      // TODO: only keep counting when effect is still active (complementary item wasn't collected)
                invoked = true;
                Destroy(this);
            }
        }
    }
}
