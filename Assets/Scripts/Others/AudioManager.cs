using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    private static AudioSource source;
    public static AudioClip blasterPium;

    public AudioSource _source;
    public AudioClip _blasterPium;

    private void Start()
    {
        source = _source;
        blasterPium = _blasterPium;
    }

    public static void BlasterPium()
    {
        source.PlayOneShot(blasterPium);
    }

}
