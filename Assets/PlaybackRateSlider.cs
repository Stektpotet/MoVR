using UnityEngine;

[RequireComponent(typeof(UnityEngine.UI.Text))]
public class PlaybackRateSlider : MonoBehaviour {
    UnityEngine.UI.Text m_pbRateText;

    private void Awake()
    {
        m_pbRateText = GetComponent<UnityEngine.UI.Text>();
    }

    public void SetText(float value)
    {
        m_pbRateText.text = value.ToString("F3");
    }
}
