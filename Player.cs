using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Configuraci�n de movimiento")]
    public bool carriles = true;

    public bool AutoPilot = true;

    // Este arreglo es para guardar las posiciones en donde se mueve el jugador cuando carriles esta activado.
    [HideInInspector]
    public float[] posCarriles;

    [SerializeField]
    private int cantCarriles = 2;

    [SerializeField]
    private float movementDistance = 16.0f;

    public float playerPosition;

    [SerializeField]
    private float limitY = -3.5f;

    [SerializeField]
    private float limitX = 8.10f;

    [HideInInspector]
    public float speed = 8;

    [SerializeField]
    private bool puedeVolar = false;

    [Header("Configuraci�n de vida")]
    [HideInInspector]
    public int life = 5;

    [HideInInspector]
    public bool inmunity = false;

    [Header("Configuraci�n de municiones")]
    [SerializeField]
    private GameObject[] bullets;

    [SerializeField]
    private int bulletType = 0;

    [SerializeField]
    private float fireRate = 0.5F;

    [SerializeField]
    private bool canShoot = true;

    [Header("Configuraci�n generales")]
    [SerializeField]
    private Configuracion_General config;

    // Start is called before the first frame update
    void Start()
    {
        life = config.vidas;
        speed = config.velocidad;
        if (carriles)
        {
            if (cantCarriles == 2)
            {
                posCarriles =
                    new float[3] { -movementDistance, 0, movementDistance };
            }
            else if (cantCarriles == 3)
            {
                posCarriles =
                    new float[2] { -movementDistance, movementDistance };
                //transform.Translate(this.transform.position.x + movementDistance, transform.position.y, 0);
            }
            else
            {
                Debug
                    .Log("Estas intentando usar" +
                    cantCarriles +
                    ". El permitido es tres o dos. Para otra config hay que programarlo");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        Movement();
        Shoot();
         if ( life == 4 ) 
    {
        config.vidaEstado1 = true;
        
    }
     if ( life == 3 )
    {
        config.vidaEstado2 = true;
        config.vidaEstado1 = false;
    }
    if ( life == 2 )
    {
        config.vidaEstado3 = true;
        config.vidaEstado2 = false;
    }
    if ( life == 1 )
    {
        config.vidaEstado4 = true;
        config.vidaEstado3 = false;
    }
    }

    private void Movement()
    {
        if (carriles)
        {
            float playerPosition = transform.position.x;

            if (Input.GetKeyDown(KeyCode.D))
            {
                if (playerPosition < posCarriles[1])
                {
                    transform.Translate(movementDistance, 0, 0);
                }
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                if (playerPosition > posCarriles[0])
                {
                    transform.Translate(-movementDistance, 0, 0);
                }
            }
        }
        else
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            transform
                .Translate(Vector3.right *
                speed *
                horizontalInput *
                Time.deltaTime);

            if (transform.position.x > limitX)
            {
                transform.position = new Vector3(limitX, transform.position.y);
            }
            else if (transform.position.x < -limitX)
            {
                transform.position = new Vector3(-limitX, transform.position.y);
            }

            if (puedeVolar)
            {
                float verticalInput = Input.GetAxis("Vertical");
                transform
                    .Translate(Vector2.up *
                    speed *
                    verticalInput *
                    Time.deltaTime);

                if (transform.position.y > 0)
                {
                    transform.position = new Vector2(transform.position.x, 0);
                }
                else if (transform.position.y < limitY)
                {
                    transform.position =
                        new Vector2(transform.position.x, limitY);
                }
            }
        }

        if (AutoPilot)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            Debug.Log (speed);
        }
    }

    public void Damage(int _dmg)
    {
        if (inmunity == false)
        {
            if (life > 0)
            {
                life -= _dmg;
                if (life <= 0)
                {
                    config.perdiste = true;
                    Destroy(this.gameObject);
                }
            }
            else if (life <= 0)
            {
                config.perdiste = true;
                Destroy(this.gameObject);
            }
        }
        else
        {
            inmunity = false;
        }

        //Actualizamos la variable de vida de la configuracion general.
        config.vidas = life;
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Return) && canShoot)
        {
            StartCoroutine(ShootDelay());
            if (Configuracion_General.runner3D == false)
            {
                Instantiate(bullets[bulletType],
                new Vector3(transform.position.x, transform.position.y + 1, 0),
                Quaternion.identity);
            }
            else
            {
                Instantiate(bullets[bulletType],
                new Vector3(transform.position.x, -1f, transform.position.z),
                Quaternion.identity);
            }
        }
    }

    public IEnumerator ShootDelay()
    {
        if (Configuracion_General.runner3D == false)
        {
            Instantiate(bullets[bulletType],
            new Vector3(transform.position.x, transform.position.y + 1, 0),
            Quaternion.identity);
        }
        else
        {
            Instantiate(bullets[bulletType],
            new Vector3(transform.position.x, -1f, transform.position.z),
            Quaternion.identity);
        }
        canShoot = false;
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "meta0")
        {
            config.barraEstado1 = true;
        }
        if (other.gameObject.tag == "meta1")
        {
            config.barraEstado1 = false;
            config.barraEstado2 = true;
        }
        if (other.gameObject.tag == "meta2")
        {
            config.barraEstado2 = false;
            config.barraEstado3 = true;
        }
    }

   
}

    
    
