using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    [Header("Configuraci�n de comportamiento")]
    [SerializeField] private float speed = 3.0f;

    [Header("Configuraci�n de estad�sticas")]
    [SerializeField] private int dmg = 1;
    [SerializeField] private int life = 1;
    public float score = 10f;

    [SerializeField] private Configuracion_General config;

    private void Awake()
    {
        GameObject gm = GameObject.FindWithTag("GameController");

        if (gm != null)
        {
            config = gm.GetComponent<Configuracion_General>();

        }

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

     private void Movement()
    {
        if (Configuracion_General.runner3D == false)
        {
            if (transform.position.y >= -6.0f)
            {
                transform.Translate(Vector3.down * speed * Time.deltaTime);
            }
            else
            {
                destroyMe();
            }
        }else
        {
            if (transform.position.z >= -6.0f)
            {
                transform.Translate(Vector3.back * speed * Time.deltaTime);
            }
            else
            {
                destroyMe();
            }
        }

    }

     private void Damage(int _dmg)
    {
        if (life > 0)
        {
            life -= _dmg;
            if (life <= 0)
            {
                destroyMe();
                giveScore(score);
            }
        }
        else if (life <= 0)
        {
            destroyMe();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Si el enemigo choca con el jugador
        if (other.gameObject.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                Debug.Log("Te choque");
                player.Damage(dmg);
                life--;
            }

            if (life <= 0)
            {
                destroyMe();
            }
            
        }
        else if (other.gameObject.tag == "Bullet")
        {

            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet != null)
            {
                Damage(bullet.dmg);
                Destroy(other.gameObject);
            }

        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Si el enemigo choca con el jugador
        if (other.gameObject.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                Debug.Log("Te choque");
                player.Damage(dmg);
                life--;
            }

            if (life <= 0)
            {
                destroyMe();
            }

        }
        else if (other.gameObject.tag == "Bullet")
        {

            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet != null)
            {
                Damage(bullet.dmg);
                Destroy(other.gameObject);
            }

        }
    }

     private void giveScore (float _score)
    {
        if (config != null)
        {
            Debug.Log("SUMA PUNTO");
            config.puntos += _score;
        }else
        {
            Debug.Log("No hay un Script de configuracion general asignado");
        }
        
    }

     void destroyMe ()
    {
        if (SpawnManager.activeEnemies.Count > 0)
        {
            for (int i = 0; i < SpawnManager.activeEnemies.Count; i++)
            {
                if (SpawnManager.activeEnemies[i].transform.position == this.gameObject.transform.position)
                {
                    SpawnManager.activeEnemies.Remove(SpawnManager.activeEnemies[i]);
                    Destroy(this.gameObject);
                    break;
                }
            }
        }
    }
    
    
   
}


