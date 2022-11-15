using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Configuraci�n de Spawner")]
    [SerializeField] private GameObject[] enemies;
    //Used to put active enemies in a list
    static public List<GameObject> activeEnemies;
    [SerializeField] private int enemiesAmount = 1;

    [SerializeField] private GameObject[] boosts;
    //[SerializeField] private int boostAmount = 1;

    [SerializeField] private float[] posY;
    [SerializeField] private float timeBetweenWaves = 5.0f;

    [SerializeField] private GameObject player;
    [SerializeField] private Player playerScript;

    [SerializeField] private float minDistance;  // minimum distance from spawnpoint to object


    private void Awake()
    {
        // Create the list
        activeEnemies = new List<GameObject>();
    }
    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<Player>();
        StartCoroutine(EnemySpawnRoutine());
    }

    IEnumerator EnemySpawnRoutine()
    {
        while (true)
        {
            for (int i = 0; i < enemiesAmount; i++)
            {
                Vector3 spawnPoint = RandomPoint();
                GameObject newEnemy = (GameObject)Instantiate(enemies[0], spawnPoint, Quaternion.identity);
                activeEnemies.Add(newEnemy);
            }
            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    public Vector3 RandomPoint()
    {
        bool done = false;
        Vector3 randomPosition = new Vector3();

        while (!done)
        {
            if (Configuracion_General.runner3D == false)
            {

                if (playerScript.carriles)
                {
                    int random = Random.Range(0, 3);
                    GameObject rP = GetChildWithName(this.gameObject, "Spawner" + ((int)random).ToString());
                    randomPosition = new Vector3(rP.transform.position.x, (int)Random.Range(posY[0], posY[1]), 0);
                }
                else
                {
                    GameObject rPosXa = GetChildWithName(this.gameObject, "Spawner0");
                    GameObject rPosXb = GetChildWithName(this.gameObject, "Spawner1");
                    randomPosition.x = (int)Random.Range(rPosXa.transform.position.x, rPosXb.transform.position.x);
                    randomPosition.y = (int)Random.Range(posY[0], posY[1]);
                    randomPosition.z = 0;
                }
            }else
            {
                if (playerScript.carriles)
                {
                    int random = Random.Range(0, 2);
                    GameObject rP = GetChildWithName(this.gameObject, "Spawner" + ((int)random).ToString());
                    randomPosition = new Vector3(rP.transform.position.x, rP.transform.position.y, (int)Random.Range(posY[0], posY[1]));
                }
                else
                {
                    GameObject rPosXa = GetChildWithName(this.gameObject, "Spawner0");
                    GameObject rPosXb = GetChildWithName(this.gameObject, "Spawner2");
                    randomPosition.x = (int)Random.Range(rPosXa.transform.position.x, rPosXb.transform.position.x);
                    randomPosition.y = rPosXa.transform.position.y;
                    randomPosition.z = (int)Random.Range(posY[0], posY[1]);
                }
            }


            done = ((minDistance == 0) || ValidMinimumDistance(randomPosition));
        }
        return randomPosition;
    }
    bool ValidMinimumDistance(Vector3 enemyPosition)
    {
        bool isValid = true;
        minDistance = Mathf.Abs(minDistance);

        if (player != null)
        {
            isValid = (Vector3.Distance(player.transform.position, enemyPosition) > minDistance);
        }

        if (isValid && (activeEnemies.Count > 0))
        {
            for (int i = 0; i < activeEnemies.Count; i++)
            {
                if (Vector3.Distance(activeEnemies[i].transform.position, enemyPosition) < minDistance)
                {
                    isValid = false;
                    break;
                }
            }
        }
        return isValid;
    }

    GameObject GetChildWithName(GameObject obj, string name)
    {
        Transform trans = obj.transform;
        Transform childTrans = trans.Find(name);
        if (childTrans != null)
        {
            return childTrans.gameObject;
        }
        else
        {
            return null;
        }
    }
}
