using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RifleAmmo : MonoBehaviour
{
    public int maxAmmo = 30;
    public int currentAmmo;
    public float reloadTime = 2f;
    public bool isReloading = false;

    private bool isFiring = false;
    public Text ammoText;
    ThirdPersonShooterController Third;

    private void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoText();
        Third = GetComponent<ThirdPersonShooterController>();
    }

    private void Update()
    {
        if (isReloading)
        {
            return;
        }

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (isFiring)
        {
            StartCoroutine(Fire());
        }
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;
        UpdateAmmoText();
    }

    private IEnumerator Fire()
    {
        isFiring = true;

        // Perform shooting logic here...
        Debug.Log("Shooting!");

        currentAmmo--;
        UpdateAmmoText();

        yield return new WaitForSeconds(0.1f);

        isFiring = false;
    }

    private void UpdateAmmoText()
    {
        ammoText.text = "Ammo: " + currentAmmo.ToString();
    }
}
