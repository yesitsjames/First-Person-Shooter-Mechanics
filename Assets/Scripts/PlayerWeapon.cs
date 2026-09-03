using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public Gun equippedWeapon;
    public bool isShootingDisabled = true;

    [SerializeField] Animator handAnimator;
    [SerializeField] Transform firePoint;
    [SerializeField] Camera playerCamera;
    [SerializeField] GameObject armsContainer;
    [SerializeField] GameObject gunContainer;
    [SerializeField] GameObject wallHitDecalPrefab;
    [SerializeField] GameObject projectilePrefab;

    private PlayerController playerController;
    private PlayerStats playerStats;

    private float lastTimeShot = 0f;
    private int currentRecoilIndex = 0;

    // AMMO SYSTEM
    private int currentMagazineAmmo;
    private int currentTotalAmmo;
    private bool isReloading = false;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerStats = GetComponent<PlayerStats>();

        equippedWeapon = new AssaultRifle();

        // Initialize ammo
        currentMagazineAmmo = equippedWeapon.maxMagazineAmmo;
        currentTotalAmmo = equippedWeapon.maxAmmo;

        PullOutGun(() => { });
    }

    void Update()
    {
        // Reload input
        if (Input.GetKeyDown(KeyCode.R))
        {
            TryReload();
        }

        if (isShootingDisabled)
        {
            return;
        }

        bool isTryingToShoot =
            isShootingDisabled == false &&
            isReloading == false &&
            Input.GetKey(KeyCode.Mouse0);

        if (isTryingToShoot)
        {
            HandleShooting();
        }
        else
        {
            // Reset gun recoil rotation
            playerController.SetGunRotation(
                Vector3.Lerp(
                    playerController.gunRotation,
                    Vector3.zero,
                    equippedWeapon.fireRatePerSecond * Time.deltaTime
                )
            );
        }
    }

    public void PullOutGun(Action onFinish)
    {
        gunContainer.SetActive(true);
        StartCoroutine(OnPullOutGun(onFinish));
    }

    public void HideGun()
    {
        gunContainer.SetActive(false);
        isShootingDisabled = true;
    }

    public void SetIsShootingDisabled(bool _isShootingDisabled)
    {
        isShootingDisabled = _isShootingDisabled;
    }

    void HandleShooting()
    {
        // No ammo
        if (currentMagazineAmmo <= 0)
        {
            TryReload();
            return;
        }

        if (Time.time - lastTimeShot >= 1 / equippedWeapon.fireRatePerSecond)
        {
            PlayAttackAnimation();

            HandleGunRecoil();

            Instantiate(projectilePrefab, firePoint.transform.position, firePoint.rotation);

            currentMagazineAmmo--;

            RaycastHit hit;
            if (Physics.Raycast(
                firePoint.transform.position,
                firePoint.transform.TransformDirection(Vector3.forward),
                out hit,
                equippedWeapon.fallOffDistance))
            {
                Instantiate(wallHitDecalPrefab, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));

                Hitbox hitbox = hit.collider.GetComponent<Hitbox>();

                if (hitbox != null)
                {
                    hitbox.ApplyDamage(equippedWeapon.damage);
                }
            }

            lastTimeShot = Time.time;
        }
    }

    void HandleGunRecoil()
    {
        if (Time.time - lastTimeShot >= equippedWeapon.recoilResetTimeSeconds)
        {
            playerController.SetGunRotation(playerController.gunRotation + equippedWeapon.recoilPattern[0]);
            currentRecoilIndex = 1;
        }
        else
        {
            playerController.SetGunRotation(playerController.gunRotation + equippedWeapon.recoilPattern[currentRecoilIndex]);

            if (currentRecoilIndex + 1 <= equippedWeapon.recoilPattern.Length - 1)
            {
                currentRecoilIndex += 1;
            }
            else
            {
                currentRecoilIndex = 0;
            }
        }
    }

    void PlayAttackAnimation()
    {
        handAnimator.Play("Fire");
    }

    void TryReload()
    {
        if (isReloading)
            return;

        if (currentMagazineAmmo == equippedWeapon.maxMagazineAmmo)
            return;

        if (currentTotalAmmo <= 0)
            return;

        StartCoroutine(Reload());
    }

    IEnumerator Reload()
    {
        isReloading = true;
        isShootingDisabled = true;

        handAnimator.Play("Reload");

        yield return new WaitForSeconds(2f); // match animation time

        int bulletsNeeded = equippedWeapon.maxMagazineAmmo - currentMagazineAmmo;

        int bulletsToReload = Mathf.Min(bulletsNeeded, currentTotalAmmo);

        currentMagazineAmmo += bulletsToReload;
        currentTotalAmmo -= bulletsToReload;

        isReloading = false;
        isShootingDisabled = false;
    }

    IEnumerator OnPullOutGun(Action onFinish)
    {
        isShootingDisabled = true;
        handAnimator.Play("Draw");
        yield return new WaitForSeconds(playerStats.pullGunOutDurationSeconds);

        isShootingDisabled = false;
        onFinish();
    }
}