using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentManager : MonoBehaviour 
{
    [SerializeField] private Orchestra orchestra;
    [SerializeField] private AudioClip audioclip;

    private GameObject cam;

    private void Start()
    {
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
        AudioSource.PlayClipAtPoint(audioclip, cam.transform.position);

        InstrumentWrapper wrap = orchestra.instruments.Find(i => i.instrument == obj.GetComponent<Instrument>().Inst);

        wrap.tilemap.SetActive(true);
        wrap.audiosource.mute = false;
    }
}
