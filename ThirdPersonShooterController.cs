using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;

public class ThirdPersonShooterController : MonoBehaviour
{

    public AudioClip m416;
    public AudioSource auidosource;
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform debugTransform;
    [SerializeField] private Transform vfxHitGreen;
    [SerializeField] private Transform vfxHitRed;
    [SerializeField] private Animator animator;
    public Animator layerAim;
    public string LayerName = "Aimlayer";
    private int layerIndex;
    [SerializeField] private Transform pfBulletProjectile;
    [SerializeField] private Transform spawnBulletPosition;
    [SerializeField] public Text ammoText;

    private Vector3 initialAimDirection;

    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetsInputs;
    private pickupcontroller pickupController;

    private bool isAiming = false;
    private bool isShooting = false;

    public int maxAmmo = 30;
    public int currentAmmo;



   

    private void Awake()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        pickupController = GetComponent<pickupcontroller>();
        layerAim = GetComponent<Animator>();
        layerIndex = animator.GetLayerIndex(LayerName);
    }

    private void Start()
    {
        // Store the original position of the camera
        
        currentAmmo = maxAmmo;
        UpdateAmmoText();
        ammoText.gameObject.SetActive(false);

        auidosource = GetComponent<AudioSource>();
        auidosource.clip = m416;
    }

    private void Update()
    {
        Vector3 mouseWorldPosition = Vector3.zero;

        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        Transform hitTransform = null;
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
            hitTransform = raycastHit.transform;
        }

        if (Keyboard.current.uKey.wasPressedThisFrame && pickupController.isGunEquipped)
        {
            if (!isAiming)
            {
                animator.SetBool("IsAiming", true);
                isAiming = true;
                layerAim.SetLayerWeight(layerIndex, 1f);

                TransferGunToAimTransform();
                if (pickupController.AimRig != null)
                {
                    pickupController.AimRig.weight = 1f;
                    pickupController.handRig.weight = 0f;
                    pickupController.bodyAim.weight = 1f;
                }

                // Store initial aim direction
                initialAimDirection = (mouseWorldPosition - transform.position).normalized;
            }
        }
        else if (Mouse.current.rightButton.wasReleasedThisFrame && isAiming)
        {
            // Stop aiming animation
            animator.SetBool("IsAiming", false);
            isAiming = false;
            layerAim.SetLayerWeight(layerIndex, 0f);

            TransferGunToArmTransform();
            if (pickupController.AimRig != null)
            {
                pickupController.AimRig.weight = 0f;
                pickupController.handRig.weight = 1f;
                pickupController.bodyAim.weight = 0f;
            }

           
        }

        if (isAiming)
        {
            aimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensitivity);
            thirdPersonController.SetRotateOnMove(false);

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
            if (Vector3.Dot(aimDirection, initialAimDirection) > 0.99f)
            {
                transform.forward = aimDirection;
            }
            else
            {
                transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
            }

            if (Input.GetMouseButtonUp(0) && currentAmmo > 0 && !isShooting)
            {
                currentAmmo--;
                isShooting = true;
                Shoot();
                starterAssetsInputs.shoot = false;
                UpdateAmmoText();
            }

            if (!starterAssetsInputs.shoot && isShooting)
            {
                isShooting = false;
            }
        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensitivity);
            thirdPersonController.SetRotateOnMove(true);
        }
    }

    private void Shoot()
    {
        // Shooting logic, instantiate bullets, play effects, etc.
        Vector3 aimDir = (debugTransform.position - spawnBulletPosition.position).normalized;
        Instantiate(pfBulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
        // Apply recoil
        auidosource.Play();

    }

    private void TransferGunToAimTransform()
    {
        if (pickupController.CurrentGun != null)
        {
            pickupController.CurrentGun.transform.SetParent(pickupController.aimTransform, false);
            pickupController.CurrentGun.transform.localPosition = Vector3.zero;
            pickupController.CurrentGun.transform.localRotation = Quaternion.identity;
        }
    }

    private void TransferGunToArmTransform()
    {
        if (pickupController.CurrentGun != null)
        {
            pickupController.CurrentGun.transform.SetParent(pickupController.armTransform, false);
            pickupController.CurrentGun.transform.localPosition = Vector3.zero;
            pickupController.CurrentGun.transform.localRotation = Quaternion.identity;
        }
    }

    private void UpdateAmmoText()
    {
        ammoText.text = "Ammo: " + currentAmmo.ToString();
    }
    public void AddAmmo(int amount)
    {
        currentAmmo += amount;

        // Clamp ammo to the maximum capacity
        currentAmmo = Mathf.Clamp(currentAmmo, 0, maxAmmo);

        UpdateAmmoText();
    }
    

    
}
