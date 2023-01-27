using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ManipulatorManager : MonoBehaviour
{
    private Orchestra orchestra;
    private GameObject player;
    private GameObject cam;

    private float timestampPauseEffect;
    private float timestampPitchEffect;

    private bool platformMoving = false;
    private bool platformMovingUp = false;
    private float platformMovingInterpolation = 0.0f;
    private List<float> platformOriginalHeights = new List<float>();    

    [SerializeField] private AudioClip audioclipCollect;
    [SerializeField] private AudioClip audioclipCountdown;

    [Header("Pitch Manipulation")]
    [SerializeField] private float pitchCountdown = 10f;
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

        foreach(InstrumentWrapper i in orchestra.instruments)
        {
            platformOriginalHeights.Add(i.tilemap.transform.position.y);
        }
    }

    private void Update()
    {
        if (platformMoving)
        {
            platformMovingInterpolation += Time.deltaTime * 1;
            float translate = Mathf.Lerp(0.0f, platformMovingUp ? pitchPlatformChange : pitchPlatformChange * -1, platformMovingInterpolation);

            for (int i = 0; i < orchestra.instruments.Count; ++i)
            {
                if (orchestra.instruments[i].instrument != Instrument.Instruments.MAIN)
                {
                    orchestra.instruments[i].tilemap.transform.SetPositionAndRotation(new Vector3(
                        orchestra.instruments[i].tilemap.transform.position.x,
                        platformOriginalHeights[i] + translate,
                        orchestra.instruments[i].tilemap.transform.position.z),
                        orchestra.instruments[i].tilemap.transform.rotation);
                }
            }
            if (platformMovingInterpolation > 1.0f)
            {
                platformMoving = false;
                platformMovingInterpolation = 0.0f;

                for (int j = 0; j < platformOriginalHeights.Count; ++j)
                {
                    platformOriginalHeights[j] += platformMovingUp ? pitchPlatformChange : pitchPlatformChange * -1;
                }
            }
        }
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

    // private void Helper() { ChangePitch(false); } 
    // NOTE: little reminder what the lambda above actually kinda does

    private void ChangePitch(bool up)
    {
        platformMovingUp = (up) ? true : false;
        platformMoving = true;

        foreach (InstrumentWrapper i in orchestra.instruments)
        {
            if (i.tilemap != null && i.audiosource != null)
            {
                if (up && i.audiosource.clip == i.audio)
                {
                    timestampPitchEffect = i.audiosource.time;
                    i.audiosource.clip = i.audioPitchHigh;
                    i.audiosource.time = timestampPitchEffect;
                    i.audiosource.Play();
                }
                else if (!up && i.audiosource.clip == i.audio)
                {
                    timestampPitchEffect = i.audiosource.time;
                    i.audiosource.clip = i.audioPitchLow;
                    i.audiosource.time = timestampPitchEffect;
                    i.audiosource.Play();
                }
                else
                {
                    timestampPitchEffect = i.audiosource.time;
                    i.audiosource.clip = i.audio;
                    i.audiosource.time = timestampPitchEffect;
                    i.audiosource.Play();
                }
            }
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
 
        cam.GetComponent<CameraHorizontal>().speed *= -1;
    }

    private void Pause(bool pause)
    {
        if (pause) 
        {
            AudioSource.PlayClipAtPoint(pauseAudio, cam.transform.position);

            foreach (InstrumentWrapper i in orchestra.instruments)
            {
                if (i.tilemap != null && i.audiosource != null)
                {
                    timestampPauseEffect = i.audiosource.time;
                    i.audiosource.Stop();       // TODO: also deactivate tilemaps?
                }
            }
        }
        else
        {
            foreach (InstrumentWrapper i in orchestra.instruments)
            {
                if (i.tilemap != null && i.audiosource != null)
                {
                    i.audiosource.time = timestampPauseEffect;
                    i.audiosource.Play();
                }
            }
        }
        cam.GetComponent<CameraHorizontal>().enabled = !pause;
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
                callback.Invoke();      // TODO: only keep counting when complementary item wasn't collected
                invoked = true;
                Destroy(this);
            }
        }
    }
}
