using TMPro;
using UnityEngine;

public class ScoreCard : MonoBehaviour
{
    [SerializeField] TMP_Text NameText;
    [SerializeField] TMP_Text ScoreText;

    public void Initialize(ScoreEntry entry)
    {
        NameText.text = entry.GetName();
        ScoreText.text = entry.GetScore().ToString();
    }
}
