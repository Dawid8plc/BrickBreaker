using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    [SerializeField] ParticleSystem ps;

    public void Explode(float Radius)
    {
        var module = ps.main;

        module.startSize = new ParticleSystem.MinMaxCurve(Radius * 3);

        ps.Play();
    }
}
