using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine.Networking;
using UnityEngine.UI;

public class RadioManager : MonoBehaviour
{
    public AudioSource source;
    public List<AudioClip> radioSynth;
    public AudioClip currentAudio;
    public int selectedRadio = 0;
    public GameObject mp3Object;


    [Header("Artista")]
    public TMPro.TextMeshProUGUI NombreArtista;
    public TMPro.TextMeshProUGUI NombreCancion;
    public Image DiskImage;

    public void NextRadio()
    {
        selectedRadio = selectedRadio + 1;
        ChangeRadio(selectedRadio);

    }

    public void BackRadio()
    {
        selectedRadio = selectedRadio - 1;
    }

    public void ChangeRadio(int radio)
    {
        switch (radio)
        {
            case 0:
                currentAudio = radioSynth[Random.Range(0, radioSynth.Count)];
                ChangeMusic(currentAudio);
                break;

            default:
                currentAudio = radioSynth[Random.Range(0, radioSynth.Count)];
                ChangeMusic(currentAudio);
                break;
        }
        
    }

    public void ChangeMusic(AudioClip clip)
    {
        source.clip = clip;
        var names = source.clip.name.Split('-');
        NombreArtista.text = names[0];
        NombreCancion.text = names[1];
        DiskImage.sprite = Resources.Load<Sprite>("Sprites/Discos/" + clip.name);
        Debug.Log("Sprites/Discos/" + clip.name);
        source.Play();
    }

    public void ShowHideMP3()
    {
        switch (mp3Object.activeInHierarchy)
        {
            case true:
                mp3Object.SetActive(false);
                break;

            case false:
                mp3Object.SetActive(true);
                break;
        }
    }
}