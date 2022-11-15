using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement; //Agregamos esto para manejar escenas (pasar de pantalla en pantalla => ganaste-perdiste)
using UnityEngine.UI; //Agregamos esto para manejar las propiedades UI (Canvas, Text, Image, etc). //Agregamos esto para utilizar las tipograf�as m�s avanzadas.

public class Configuracion_General : MonoBehaviour
{
    private Image barraProgreso;

    public bool barraEstado1;

    public bool barraEstado2;

    public bool barraEstado3;
 
   private Image vidaEstado1img;
public bool vidaEstado1;

    private Image vidaEstado2img;
public bool vidaEstado2;

     private Image vidaEstado3img;

    public bool vidaEstado3;

      private Image vidaEstado4img;
 public bool vidaEstado4;


       

    public bool ok = false;

    [Header("Configuraci�n de tipo de juego")]
    public static bool runner3D = false;

    public bool _runner3D = false;

    // [Header("Configuraci�n de variables generales")]
    public enum TiposDeJuego
    {
        Runner,
        Shooter,
        Sincro
    }

    public TiposDeJuego tipodejuego;

    public enum TiposDeDificultad
    {
        facil,
        intermedio,
        dificil
    }

    public float puntos = 0;

    public float tiempo;

    public static int cantPlayers = 1;

    static TiposDeDificultad dificultad; //La pueden utilizar como condición para variar la dificultad.

    public int vidas;

    public float velocidad;

    [Header("Configuracion de elementos UI")]
    [SerializeField]
    private TMP_Text scoreText; //Variable del texto que se visualizara en pantalla en el videojuego. La definimos como publica para poder arrastrar el objeto Text desde el Inspector

    [SerializeField]
    private TMP_Text lifeText;

    [Header("Configuracion de Escenas")]
    public int escenajuego;

    public int escenaperdiste;

    public int escenaganaste;

    public bool perdiste = false;

    public bool ganaste = false;

    void Awake()
    {
        runner3D = _runner3D;
    }

    // Start is called before the first frame update
    void Start()
    {
        barraProgreso =
            GameObject.Find("barraDeProgresoEstado0").GetComponent<Image>();
             
             vidaEstado1img=GameObject.Find("corazonLleno1").GetComponent<Image>();
             vidaEstado2img=GameObject.Find("corazonLleno2").GetComponent<Image>();
              vidaEstado4img=GameObject.Find("corazonLleno3").GetComponent<Image>();
               vidaEstado4img=GameObject.Find("corazonLleno4").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeText != null)
        {
            lifeText.text = "Vidas: " + ((int) vidas).ToString();
        }
        else
        {
            Debug.Log("No hay un TMP text asignado para la vida");
        }
        if (perdiste)
        {
            print("PERDISTE!");
            SceneManager.LoadScene (escenaperdiste);
        }
        else if (ganaste)
        {
            print("GANASTE!");
            SceneManager.LoadScene (escenaganaste);
        }

        //Barra de progreso hud
        if (ok)
        {
            barraProgreso.GetComponent<Image>().sprite =
                Resources.Load<Sprite>("SpritesR/barraVaciaShawarma1");
        }

        if (barraEstado1 == true)
        {
            barraProgreso.GetComponent<Image>().sprite =
                Resources.Load<Sprite>("SpritesR/barra1de4Shawarma");
        }
        if (barraEstado2 == true)
        {
            barraProgreso.GetComponent<Image>().sprite =
                Resources.Load<Sprite>("SpritesR/barra2de4Shawarma");
        }
        if (barraEstado3 == true)
        {
            barraProgreso.GetComponent<Image>().sprite =
                Resources.Load<Sprite>("SpritesR/barra3de4Shawarma");
        }
            
            //corazones

        
            
        if (vidaEstado1)
        {
           vidaEstado1img.GetComponent<Image>().sprite = Resources.Load<Sprite>("SpritesR/corazonLleno01");
        }
        else {
            if (vidaEstado1==false)
            {
                 vidaEstado1img.GetComponent<Image>().sprite = Resources.Load<Sprite>("SpritesR/corazonVacio1");
            }   
                 
        }      
                  
                
        if(vidaEstado2)
        {
        vidaEstado2img.GetComponent<Image>().sprite = Resources.Load<Sprite>("SpritesR/corazonLleno2");
        }
        else if(vidaEstado2==false)
        {
        vidaEstado2img.GetComponent<Image>().sprite = Resources.Load<Sprite>("SpritesR/corazonVacio2");
        }
    
        if(vidaEstado3)
        {
        vidaEstado3img.GetComponent<Image>().sprite = Resources.Load<Sprite>("SpritesR/corazonLleno3");
        }
        else if(vidaEstado3==false)
        {
        vidaEstado3img.GetComponent<Image>().sprite = Resources.Load<Sprite>("SpritesR/corazonVacio3");
        }
        
        if(vidaEstado4)
        {
        vidaEstado4img.GetComponent<Image>().sprite = Resources.Load<Sprite>("SpritesR/corazonLleno4");
        } 
        else if(vidaEstado4==false)
        {
        vidaEstado4img.GetComponent<Image>().sprite = Resources.Load<Sprite>("SpritesR/corazonVacio4");
        }          
         }
          }             
