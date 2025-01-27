using UnityEngine;

public class PowerupBlockController : BlockController
{
    [SerializeField] GameObject Powerup;

    public GameObject GetPowerup()
    {
        return Powerup;
    }

    public void SetPowerup(GameObject powerup)
    {
        Powerup = powerup;
    }

    public override void Break()
    {
        if (Destroyed)
            return;

        Instantiate(Powerup, transform.position, Quaternion.identity);

        base.Break();
    }
}
