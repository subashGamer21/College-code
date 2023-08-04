using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class findnishan : MonoBehaviour
{

    public Text find_nishan;
    public GameObject finalMission;
    public GameObject fifthparycile;
    public Text kill;

   //ublic GameObject subtitle;
    private void Start()
    {
        find_nishan.enabled = false;
        finalMission.SetActive(false);
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            fifthparycile.SetActive(false);
            find_nishan.enabled = true;
            kill.enabled = false;
            finalMission.SetActive(true);
          //  subtitle.SetActive(true);
        }
    }
}
