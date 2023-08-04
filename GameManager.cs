using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text collegescene;

    // Start is called before the first frame update
    void Start()
    {
        collegescene.enabled = false;
    }

    // Update is called once per frame
   

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            collegescene.enabled = true;
            Invoke(nameof(DisableCollegeSceneText), Random.Range(3f, 5f));
        }
    }

    private void DisableCollegeSceneText()
    {
        collegescene.enabled = false;
    }
}
