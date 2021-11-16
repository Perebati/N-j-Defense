using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillUI : MonoBehaviour
{
    [HideInInspector] public static SkillUI instance;
    
    private GameObject[] skillsGO;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(instance);
    }

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            skillsGO[i] = transform.GetChild(i).gameObject;
        }

    }
    public static void UpdateSkillUI(int skillNumber, float skillCD)
    {
        switch(skillNumber)
        {
            case 0:
                instance.StartCoroutine(instance.Skill1(skillCD));
                break;
            case 1:
                instance.StartCoroutine(instance.Skill2(skillCD));
                break;
            case 2:
                instance.StartCoroutine(instance.Skill3(skillCD));
                break;
            case 3:
                instance.StartCoroutine(instance.Skill4(skillCD));
                break;
        }
    }

    IEnumerator Skill1(float cdValue)
    {
        TextMeshPro tmp = instance.skillsGO[0].GetComponentInChildren<TextMeshPro>();
        tmp.text = "" + cdValue;
        instance.skillsGO[0].SetActive(true);
        float timer = cdValue;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            tmp.text = "" + (int)timer;
        }

        yield return null;
    }
    IEnumerator Skill2(float cdValue)
    {
        TextMeshPro tmp = instance.skillsGO[1].GetComponentInChildren<TextMeshPro>();
        tmp.text = "" + cdValue;
        instance.skillsGO[1].SetActive(true);
        float timer = cdValue;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            tmp.text = "" + (int)timer;
        }

        yield return null;
    }
    IEnumerator Skill3(float cdValue)
    {

        TextMeshPro tmp = instance.skillsGO[2].GetComponentInChildren<TextMeshPro>();
        tmp.text = "" + cdValue;
        instance.skillsGO[2].SetActive(true);
        float timer = cdValue;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            tmp.text = "" + (int)timer;
        }
        yield return null;
    }
    IEnumerator Skill4(float cdValue)
    {

        TextMeshPro tmp = instance.skillsGO[3].GetComponentInChildren<TextMeshPro>();
        tmp.text = "" + cdValue;
        instance.skillsGO[3].SetActive(true);
        float timer = cdValue;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            tmp.text = "" + (int)timer;
        }
        yield return null;
    }

}
