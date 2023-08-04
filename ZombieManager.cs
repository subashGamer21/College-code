using UnityEngine;
using UnityEngine.UI;

public class ZombieManager : MonoBehaviour
{
    public GameObject completeGameObject;
    public GameObject[] zombies;

    private int zombiesRemaining;

    private void Start()
    {
        zombiesRemaining = zombies.Length;
    }

    public void ZombieDied()
    {
        zombiesRemaining--;
        if (zombiesRemaining <= 0)
        {
            if (completeGameObject != null) // Check if the reference is not null before activating it.
            {
                completeGameObject.SetActive(true);
            }
        }
    }
}
