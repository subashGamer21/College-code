using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class second_collider : MonoBehaviour
{
    public Text classscene;
    public GameObject second_particle;

    // Start is called before the first frame update
    void Start()
    {
        classscene.enabled = false;
        second_particle.SetActive(false);
    }

    // Update is called once per frame

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            second_particle.SetActive(true);
            classscene.enabled = true;
            Invoke(nameof(DisableCollegeSceneText), Random.Range(3f, 5f));
        }
    }

    private void DisableCollegeSceneText()
    {
        classscene.enabled = false;
    }
}
