using UnityEngine;
using System.Collections;
using TMPro;

public class WeaponShoot : MonoBehaviour
{
    public Camera playerCamera;
    public float range = 100f;
    public float damage = 25f;

    [Header("Effects")]
    public ParticleSystem muzzleFlash;

    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip shootSound;
    public AudioClip reloadSound;

    [Header("Ammo Settings")]
    public int magazineSize = 10;
    public float reloadTime = 1.5f;
    public TextMeshProUGUI ammoText;

    private int currentAmmo;
    private bool isReloading = false;

    void Start()
    {
        currentAmmo = magazineSize;
        UpdateAmmoUI();
    }

    void Update()
    {
        if (isReloading)
            return;

        if (Input.GetButtonDown("Fire1"))
        {
            if (currentAmmo > 0)
            {
                Shoot();
            }
            else
            {
                StartCoroutine(Reload());
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < magazineSize)
        {
            StartCoroutine(Reload());
        }
    }

    void Shoot()
    {
        audioSource.PlayOneShot(shootSound);
        muzzleFlash.Play();
        currentAmmo--;
        UpdateAmmoUI();

        RaycastHit hit;

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
        {
            if (hit.transform.CompareTag("Zombie"))
            {
                ZombieBase zombie = hit.transform.GetComponent<ZombieBase>();
                if (zombie != null)
                {
                    zombie.TakeDamage(damage);
                }
            }
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        ammoText.text = "Reloading...";
        audioSource.PlayOneShot(reloadSound);

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = magazineSize;
        isReloading = false;
        UpdateAmmoUI();
    }

    void UpdateAmmoUI()
    {
        ammoText.text = currentAmmo + " / " + magazineSize;
    }
}