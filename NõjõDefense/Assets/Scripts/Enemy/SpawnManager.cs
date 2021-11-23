using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    // rounds 
    private int currentRound = 1;
    [HideInInspector] public static int currentActiveTroops = 0;
    [SerializeField] private TextMeshProUGUI roundUI;
    [SerializeField] private GameObject monumentsHolder;


    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(instance);
        spawnPoints = new GameObject[transform.childCount];
    }

    private void Start()
    {
        currentRound = 1;
        roundUI.text = "Round " + currentRound;
        for (int i = 0; i < transform.childCount; i++)
        {
            spawnPoints[i] = transform.GetChild(i).gameObject;
        }

        troopSum = (float)maxMISERAVEL + maxATIRADOR + maxSAMURAI;
        chances = new float[] { maxMISERAVEL / troopSum, maxATIRADOR / troopSum, maxSAMURAI/troopSum};
        maxTroops = multiplyFactor * (maxSAMURAI + maxMISERAVEL + maxATIRADOR);
        activeTroops = 0;
        StartCoroutine(InitialTroops());
    }
    private void NextRound()
    {
        currentRound++;
        monumentsHolder.GetComponent<MonumentsHolder>().ResetMonument();
        roundUI.text = "Round " + currentRound;
        maxTroops += (int)troopSum * multiplyFactor;
        activeTroops = 0;
        StartCoroutine(InitialTroops());

    }

    IEnumerator InitialTroops()
    {

        while (activeTroops < maxTroops / 2)
        {       
            Instantiate(ChooseTroop(), spawnPoints[currentSpawnPoint].transform.position, transform.rotation);
            UpadateSpawnPoint();
            activeTroops++;
            currentActiveTroops++;
            yield return new WaitForSeconds(.1f);
        }

        StartCoroutine(SpawnRemainingTroops());

        yield return null;
    }

    IEnumerator SpawnRemainingTroops()
    {
        while (activeTroops < maxTroops)
        {
            timer = 0f;
            Instantiate(ChooseTroop(), spawnPoints[currentSpawnPoint].transform.position, transform.rotation);
            UpadateSpawnPoint();
            activeTroops++;
            currentActiveTroops++;
            yield return new WaitForSeconds(1f);
        }
        
        yield return null;
    }

    private void LateUpdate()
    {
        timer += Time.deltaTime;
        if (timer > 3f)
        {
            if (currentActiveTroops <= 0)
                NextRound();
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
        return prefabs[0]; // unreacheble
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
