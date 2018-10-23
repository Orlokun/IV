using UnityEngine;
using TMPro;

public class ObjetoDestruible : MonoBehaviour {

    public int hp;
    private int chp;
    public bool shouldDrop;
    public string drop;

    [SerializeField] TextMeshPro lifeText;

	// Use this for initialization
	void Start ()
    {
        if (hp == 0)
        {
            Debug.LogError("El objeto: " + gameObject.name + "Va a morir altiro");
        }

        chp = hp;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (GameManager.dificultad == 0)
        {
            lifeText.text = chp + "/" + hp;
        }
        else
        {
            lifeText.text = "";
        }
    }

    void Damage(int damage)
    {
        chp = chp - damage;
        
        if(chp < 0)
        {
            chp = 0;
        }

        if(chp == 0)
        {
            ProcessDeath();
        }
    }

    private void ProcessDeath()
    {
        DropManager.Drop(gameObject);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Bullet":
                Damage(1);
                break;
            default:
                break;
        }
    }
}
