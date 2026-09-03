using System;
using System.Collections.Generic;
using UnityEngine;

class AssaultRifle : Gun
{
    public string name { get; private set; } = "AssaultRifle";

    // Magazine size
    public int maxMagazineAmmo { get; private set; } = 25;

    // Total reserve ammo
    public int maxAmmo { get; private set; } = 100;

    public float fireRatePerSecond { get; private set; } = 8f;

    public float fallOffDistance { get; private set; } = 150f;

    public float recoilResetTimeSeconds { get; private set; } = 0.5f;

    //reload speed per weapon
    public float reloadTimeSeconds { get; private set; } = 2f;

    // recoil strength multiplier
    public float recoilMultiplier { get; private set; } = 0.3f;

    public float damage { get; private set; } = 20f;

    private Vector3[] baseRecoilPattern = new Vector3[25]
    {
        new Vector3(-0.5f, 0, 0),
        new Vector3(-0.5f, 0, 0),
        new Vector3(-0.8f, 0, 0),
        new Vector3(-0.8f, 0, 0),
        new Vector3(-0.8f, 0, 0),
        new Vector3(-0.8f, 0, 0),
        new Vector3(-0.8f, 0, 0),
        new Vector3(-0.8f, 0, 0),
        new Vector3(-0.8f, 0, 0),
        new Vector3(-0.8f, 0, 0),
        new Vector3(-0.8f, 0, 0),
        new Vector3(-0.8f, 0, 0),

        new Vector3(0, 0.8f, 0),
        new Vector3(0, 0.8f, 0),
        new Vector3(0, 0.8f, 0),
        new Vector3(0, 0.8f, 0),

        new Vector3(0, -0.8f, 0),
        new Vector3(0, -0.8f, 0),
        new Vector3(0, -0.8f, 0),
        new Vector3(0, -0.8f, 0),
        new Vector3(0, -0.8f, 0),
        new Vector3(0, -0.8f, 0),
        new Vector3(0, -0.8f, 0),
        new Vector3(0, -0.8f, 0),
        new Vector3(0, -0.8f, 0)
    };

    public Vector3[] recoilPattern
    {
        get
        {
            Vector3[] scaled = new Vector3[baseRecoilPattern.Length];

            for (int i = 0; i < baseRecoilPattern.Length; i++)
            {
                scaled[i] = baseRecoilPattern[i] * recoilMultiplier;
            }

            return scaled;
        }
    }
}