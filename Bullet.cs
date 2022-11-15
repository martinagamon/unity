using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 1.5f;
    // Runner cenital: 5.60f - Runner3D: 10f
    [SerializeField] private float distance = 8f;
    public int dmg = 1;


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
            if (transform.position.y <= distance)
            {
                transform.Translate(Vector2.up * speed * Time.deltaTime);
            }
            else
            {
                Destroy(gameObject);
            }
        }else
        {
            if (transform.position.z <= distance)
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        

    }
}
