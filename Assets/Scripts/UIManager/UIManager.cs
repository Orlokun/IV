using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Image hpBar;
    public TextMeshProUGUI hpIndicator;
    public TextMeshProUGUI bulletsUI;

    [SerializeField] private float hpBarFillAmount;

    [Header("Menu Principal")]
    public Dropdown dificultad;

    public MovieTexture bgVideo;
    public AudioSource bgAudio;
    public RawImage bg;

    [Header("Interfaces")]

    public GameObject mainGUI;              //Qué objeto es?
    public GameObject deathGUI;
    public GameObject reloadImage;

    [Header("Otros")]

    public GameObject fuckCatFace;  

    #region Interfaces Estaticas :(
    private static GameObject _mainGUI;     
    private static GameObject _deathGUI;
    private static GameObject _reloadImage;
    #endregion

    // Use this for initialization
    void Start()
    {
        _mainGUI = mainGUI;     
        _deathGUI = deathGUI;
        _reloadImage = reloadImage;

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
    void FixedUpdate()
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

    public static void ChangeImage(GameObject _object, Sprite toImage)
    {
        _object.GetComponent<Image>().sprite = toImage;
    }

    public static void ChangeStateGameObject(string _gameObject, bool estado)
    {
        switch (_gameObject)
        {
            case "mainGUI":
                _mainGUI.SetActive(estado);
                break;

            case "deathGUI":
                _deathGUI.SetActive(estado);
                break;
            case "reloadImage":
                _reloadImage.SetActive(estado);
                break;

            default:
                Debug.Log("CASO INEXISTENTE REVISAR LA WEA QUE PASASTE");
                break;
        }
    }

    public static GameObject GetGameObject(string _GameObject)
    {
        GameObject toReturn = null;

        switch (_GameObject)
        {
            case "reloadImage":
                toReturn = _reloadImage;
                break;

            default:
                Debug.Log("CASO INEXISTENTE REVISAR LA WEA QUE PASASTE");
                break;
        }

        return toReturn;
    }

}
