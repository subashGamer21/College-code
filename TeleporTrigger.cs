using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleporTrigger : MonoBehaviour
{

    public GameObject clz_trigger;
    public Text exitClass;
    public Text firstmission;
    public GameObject forth_particle;
    public GameObject third_particle;

    private void Start()
    {
        clz_trigger.SetActive(false);
        exitClass.enabled = false;
        forth_particle.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            forth_particle.SetActive(true);
            clz_trigger.SetActive(true);
            exitClass.enabled=(true);
            firstmission.enabled = false;
            third_particle.SetActive(false);
        }
    }

}
