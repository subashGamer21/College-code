using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;

public class pickupcontroller : MonoBehaviour
{
    public GunController[] gunControllers; // Array of GunController objects
    public Transform armTransform;
    public Transform aimTransform; // New transform for aiming
    public GameObject pickupMessageText;
    public GameObject handRigObject;
    public GameObject currentGun;
    public GameObject CurrentGun => currentGun;
    public Rig AimRig;
    public Rig handRig;
    public Rig bodyAim;
    ThirdPersonShooterController third;
    public bool isGunEquipped = false;
    public Text firsttext;
    public Text secondtext;
    public GameObject first_particle;

    private void Start()
    {
        handRig = handRigObject.GetComponent<Rig>();
        third = GetComponent<ThirdPersonShooterController>();
        secondtext.enabled = false;
    }

    private void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            PickUpGun();
            third.ammoText.gameObject.SetActive(true);
            
           
        }
        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            DropGun();
            third.ammoText.gameObject.SetActive(false);
        }
    }

    private void PickUpGun()
    {
        if (gunControllers != null && currentGun == null)
        {
            foreach (GunController gunController in gunControllers)
            {
                if (gunController != null && gunController.gameObject.activeSelf)
                {
                    float distanceToGun = Vector3.Distance(transform.position, gunController.transform.position);
                    float pickupRange = 0.6f;

                    if (distanceToGun <= pickupRange)
                    {
                        gunController.transform.parent = armTransform;
                        gunController.transform.localPosition = Vector3.zero;
                        gunController.transform.localRotation = Quaternion.identity;

                        gunController.GetComponent<Collider>().enabled = false;
                        gunController.enabled = false;
                        gunController.GetComponent<MeshRenderer>().enabled = true;

                        currentGun = gunController.gameObject;

                        Destroy(gunController.GetComponent<Rigidbody>());
                        Destroy(gunController.GetComponent<Collider>());

                        currentGun.layer = LayerMask.NameToLayer("Arms");
                        if (handRig != null)
                        {
                            handRig.weight = 1f;
                        }

                        isGunEquipped = true;

                        pickupMessageText.SetActive(false);
                        firsttext.enabled = false;
                        first_particle.SetActive(false);
                        secondtext.enabled = true;
                        break; // Exit the loop after picking up the first available gun
                    }
                }
            }
        }
    }

    public void DropGun()
    {
        if (currentGun != null && isGunEquipped)
        {
            // Enable the gun's scripts if necessary
            currentGun.GetComponent<GunController>().enabled = true;

            // Show the gun's mesh renderer or set its visibility as needed
            currentGun.GetComponent<MeshRenderer>().enabled = true;

            // Remove the parent relationship with the arm transform
            currentGun.transform.parent = null;

            // Reset the gun's position and rotation
            currentGun.transform.position = armTransform.position; // Adjust as needed
            currentGun.transform.rotation = armTransform.rotation; // Adjust as needed

            // Set the weight of the hand rig to 0
            if (handRig != null)
            {
                handRig.weight = 0f;
            }

            isGunEquipped = false;

            // Add a Rigidbody component to the dropped gun
            Rigidbody gunRigidbody = currentGun.AddComponent<Rigidbody>();
            // Adjust the Rigidbody properties as needed
            gunRigidbody.mass = 1f;
            gunRigidbody.drag = 0.5f;
            gunRigidbody.angularDrag = 0.5f;

            // Add a Collider component to the dropped gun if it doesn't have one
            Collider gunCollider = currentGun.GetComponent<Collider>();
            if (gunCollider == null)
            {
                gunCollider = currentGun.AddComponent<BoxCollider>(); // Adjust the collider type as needed
            }
            gunCollider.enabled = true;

            currentGun = null;

            // Show the pickup message text if the player is not colliding with another gun
            if (!IsCollidingWithGun())
            {
                pickupMessageText.SetActive(true);
            }
        }
    }

    private bool IsCollidingWithGun()
    {
        float overlapRadius = 1.0f;
        Collider[] colliders = Physics.OverlapSphere(transform.position, overlapRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Gun") && collider.gameObject != currentGun)
            {
                return true;
            }
        }
        return false;
    }
}
