using UnityEngine;

public class InstrumentManager : MonoBehaviour 
{
    private Orchestra orchestra;
    private GameObject cam;

    [SerializeField] private AudioClip audioclipCollect;

    private void Start()
    {
        orchestra = GetComponent<Orchestra>();
        cam = GameObject.Find("Main Camera");

        foreach (InstrumentWrapper i in orchestra.instruments)
        {
            if(i.tilemap != null && i.audiosource != null) { 
                if(i.instrument != Instrument.Instruments.MAIN)
                {
                    i.tilemap.SetActive(false);
                    i.audiosource.mute = true;
                } else
                {
                    i.tilemap.SetActive(true);
                    i.audiosource.mute = false;
                }
            }
        }
    }

    public void AddInstrument(GameObject obj) 
    {
        AudioSource.PlayClipAtPoint(audioclipCollect, cam.transform.position);

        InstrumentWrapper wrap = orchestra.instruments.Find(i => i.instrument == obj.GetComponent<Instrument>().instrument);

        wrap.tilemap.SetActive(true);
        wrap.audiosource.mute = false;
    }
}
