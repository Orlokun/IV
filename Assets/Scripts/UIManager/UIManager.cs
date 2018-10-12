using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Image hpBar;
    public TextMeshProUGUI hpIndicator;
    public TextMeshProUGUI bulletsUI;

    [SerializeField]private float hpBarFillAmount;

    [Header("Menu Principal")]
    public Dropdown dificultad;

    public MovieTexture bgVideo;
    public AudioSource bgAudio;
    public RawImage bg;

    // Use this for initialization
    void Start()
    {
        UpdateHP();
        UpdateBullets();
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            bg.GetComponent<RawImage>().texture = bgVideo as MovieTexture;
            //bgAudio = bg.GetComponent<AudioSource>();
            //bgAudio.clip = bgVideo.audioClip;
            while (!bgVideo.isReadyToPlay)
            {

            }
            bgVideo.loop = true;
            //bgAudio.loop = true;
            bgVideo.Play();
            //bgAudio.Play();
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            switch (dificultad.value)
            {
                case 0:
                    GameManager.dificultad = 0;
                    break;
                case 1:
                    GameManager.dificultad = 1;
                    break;
                case 2:
                    GameManager.dificultad = 2;
                    break;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate ()
    {

        UpdateHP();
        UpdateBullets();
    }

    public void UpdateHP()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            hpBar.fillAmount = ((float)Player.cHP / (float)Player.maxHP);
            hpIndicator.text = Player.cHP + "/" + Player.maxHP;
        }
        
    }

    public void UpdateBullets()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            bulletsUI.text = (Player.cbullets + "/" + Player.bullets * Player.charges);
        }
    }

    public void BotonJugar()
    {     
        SceneManager.LoadScene("PlayerScene");
    }

    public void BotonSalir()
    {
        Application.Quit();
    }


}
