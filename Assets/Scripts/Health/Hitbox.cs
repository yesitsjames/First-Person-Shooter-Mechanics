using UnityEngine;

public enum HitboxType
{
    Head,
    Body,
    Legs
}

public class Hitbox : MonoBehaviour
{
    public HitboxType hitboxType;
    public PlayerHealth playerHealth;

    public void ApplyDamage(float baseDamage)
    {
        float damage = baseDamage;

        switch (hitboxType)
        {
            case HitboxType.Head:
                damage *= 2f;
                break;

            case HitboxType.Body:
                damage *= 1f;
                break;

            case HitboxType.Legs:
                damage *= 0.6f;
                break;
        }

        playerHealth.TakeDamage(damage);
    }
}