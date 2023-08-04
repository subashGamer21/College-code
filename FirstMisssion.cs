using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstMisssion : MonoBehaviour
{

    class_trigger zombie;
    public Text completionText;
    private int activeZombieCount;

    // Start is called before the first frame update
    void Start()
    {
        zombie = GetComponent<class_trigger>();

        activeZombieCount = zombie.zombies.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DeactivateZombies()
    {
        foreach (GameObject zombie in zombie.zombies)
        {
            if (zombie.activeSelf)
            {
                zombie.SetActive(false);
                activeZombieCount--;
            }
        }

        if (activeZombieCount <= 0)
        {
            // Enable the completion text when all zombies are dead
            if (completionText != null)
            {
                completionText.gameObject.SetActive(true);
            }
        }
    }
}
