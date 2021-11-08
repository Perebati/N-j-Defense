using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [HideInInspector] public static SpawnManager instance;
    private GameObject[] spawnPoints;

    [SerializeField] private GameObject[] prefabs;

    [SerializeField] private int multiplyFactor = 1;
    [SerializeField] [Range(1, 10)] private int maxMISERAVEL = 6;
    [SerializeField] [Range(1, 5)] private int maxATIRADOR = 2;
    [SerializeField] [Range(1, 3)] private int maxSAMURAI = 1;

    private float[] chances;
    private float troopSum;

    private int maxTroops;
    private int currentSpawnPoint = 0;

    [HideInInspector] public int activeTroops = 0;

    private float timer = 0f;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(instance);
        spawnPoints = new GameObject[transform.childCount];
    }

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            spawnPoints[i] = transform.GetChild(i).gameObject;
        }

        troopSum = (float)maxMISERAVEL + maxATIRADOR + maxSAMURAI;
        chances = new float[] { maxMISERAVEL / troopSum, maxATIRADOR / troopSum, maxSAMURAI/troopSum};
        maxTroops = multiplyFactor * (maxSAMURAI + maxMISERAVEL + maxATIRADOR);
        StartCoroutine(InitialTroops());
    }

    IEnumerator InitialTroops()
    {

        while (activeTroops < maxTroops / 2)
        {       
            Instantiate(ChooseTroop(), spawnPoints[currentSpawnPoint].transform.position, transform.rotation);
            UpadateSpawnPoint();
            activeTroops++;
            yield return new WaitForSeconds(.1f);
        }
        yield return null;
    }

    private void LateUpdate()
    {
        timer += Time.deltaTime;
        if (timer > 2f && activeTroops < maxTroops)
        {
            timer = 0f;
            Instantiate(ChooseTroop(), spawnPoints[currentSpawnPoint].transform.position, transform.rotation);
            UpadateSpawnPoint();
            activeTroops++;
        }
    }

    private GameObject ChooseTroop()
    {
        float rng = Random.Range(0f, 1f);
        float currentChance = 0f;
        float minValue;
        float maxValue;

        for (int i = 0; i < chances.Length; i++)
        {
            currentChance += chances[i]; // magic
            minValue = 0.5f - currentChance / 2;
            maxValue = 0.5f + currentChance / 2;

            if (rng >= minValue && rng <= maxValue)
            {
                return prefabs[i];
            }
        }     
        return null; // unreacheble
    }

    private void UpadateSpawnPoint()
    {
        if (currentSpawnPoint < spawnPoints.Length-1)
        {
            currentSpawnPoint++;
        } else
        {
            currentSpawnPoint = 0;
        }
    }


}
