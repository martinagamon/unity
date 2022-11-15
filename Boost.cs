using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    //private Player playerScript;
    [SerializeField]
    private float BoostTimer = 9.1f;

    [SerializeField]
    private float speedUp = 15f;

    [SerializeField]
    private int lifeUp = 1;

    [SerializeField]
    public Player playerScript;

    [SerializeField]
    private Configuracion_General config;

    //Estructura de datos para definir tipo de boost
    public enum
    boostType // your custom enumeration
    {
        Defensa,
        Velocidad,
        Vida
    }

    public boostType bs;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Si el boost choca con el jugador
        if (other.gameObject.tag == "Player")
        {
            switch (bs)
            {
                case boostType.Defensa:
                    StartCoroutine(DefenseBoost());
                    break;
                case boostType.Velocidad:
                    StartCoroutine(SpeedBoost());
                    break;
                case boostType.Vida:
                    if (playerScript != null)
                    {
                        LifeBoost();
                    }
                    break;
            }
        }
    }

    IEnumerator DefenseBoost()
    {
        if (playerScript != null)
        {
            playerScript.inmunity = true;
        }
        yield return new WaitForSeconds(BoostTimer);
        playerScript.inmunity = false;
    }

    IEnumerator SpeedBoost()
    {
        if (playerScript != null)
        {
            playerScript.speed += speedUp;
        }
        yield return new WaitForSeconds(BoostTimer);
        playerScript.speed -= speedUp;
    }

    private void LifeBoost()
    {
        if (playerScript.life < config.vidas)
        {
            playerScript.life += lifeUp;
        }
    }
}
