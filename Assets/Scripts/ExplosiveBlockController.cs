using UnityEngine;

public class ExplosiveBlockController : BlockController
{
    bool Exploded = false;

    float Radius = 3f;

    [SerializeField] GameObject Explosion;

    public float GetRadius()
    {
        return Radius;
    }

    public void SetRadius(float radius)
    {
        Radius = radius;
    }

    public override void Break()
    {
        if (Destroyed || Exploded)
            return;

        Exploded = true;

        Instantiate(Explosion, transform.position, Quaternion.identity).GetComponent<ExplosionController>().Explode(Radius);

        Collider[] hits = Physics.OverlapSphere(transform.position, Radius);
        
        foreach (var collider in hits)
        {
            if (collider.gameObject == gameObject)
                continue;

            if (collider.CompareTag("Block"))
            {
                collider.gameObject.GetComponent<BlockController>().Break();
            }
        }

        base.Break();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, Radius);
    }
}
