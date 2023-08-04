using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.UI;

public class showZOmbie : MonoBehaviour
{
    public GameObject showzombie_scene;
    private ThirdPersonController thirdPersonController;
    private bool isPlayerInside;
    public GameObject zombie;
    public Text KILL;
    public GameObject findNishan;
    public GameObject firth_particle;
    public GameObject forthparticle;
    // Start is called before the first frame update
    void Start()
    {
        showzombie_scene.SetActive(false);
        isPlayerInside = false;
        zombie.SetActive(false);
        KILL.enabled = false;
        findNishan.SetActive(false);
        firth_particle.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            firth_particle.SetActive(true);
            forthparticle.SetActive(false);
            KILL.enabled = true;
            thirdPersonController = other.GetComponent<ThirdPersonController>();
            if (thirdPersonController != null)
            {
                thirdPersonController.enabled = false;
                isPlayerInside = true;
                StartCoroutine(EnableControllerAfterDelay());
            }

            // Show the zombie scene
            showzombie_scene.SetActive(true);
            zombie.SetActive(true);
            findNishan.SetActive(true);
        }
    }

    private IEnumerator EnableControllerAfterDelay()
    {
        yield return new WaitForSeconds(5f); // Wait for 5 seconds
        if (thirdPersonController != null && isPlayerInside)
        {
            thirdPersonController.enabled = true;
            isPlayerInside = false;
            showzombie_scene.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
}
