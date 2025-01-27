using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    [SerializeField] float GlowCycleSpeed = 1f;
    [SerializeField] float GlowIntensity = 2f;

    private float hue;

    static EffectsManager Instance;

    [SerializeField] Material SpecialGlowMat;

    public static EffectsManager GetInstance()
    {
        return Instance;
    }

    private void Awake()
    {
        Instance = this;

        SpecialGlowMat = new Material(SpecialGlowMat);
    }

    public Material GetSpecialGlowMat()
    {
        return SpecialGlowMat;
    }

    private void OnDestroy()
    {
        Destroy(SpecialGlowMat);
    }

    // Update is called once per frame
    void Update()
    {
        // Increment hue based on cycleSpeed and time
        hue += GlowCycleSpeed * Time.deltaTime;
        hue %= 1f; // Keep hue in the range [0, 1]

        // Convert hue to RGB color
        Color baseColor = Color.HSVToRGB(hue, 1f, 1f);

        // Multiply the base color by the intensity
        Color emissionColor = baseColor * GlowIntensity;

        // Set the emission color on the material
        if (SpecialGlowMat != null)
        {
            SpecialGlowMat.SetColor("_EmissionColor", emissionColor);
        }
    }
}
