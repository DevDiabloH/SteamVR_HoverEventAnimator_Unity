using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HoverTextColorAnimation : HoverEventAnimation
{
    [SerializeField]
    //private Text m_Text = null;
    private TextMeshProUGUI m_Text = null;

    [SerializeField]
    private Color m_TargetColor     = Color.white;

    private Color m_Color           = Color.white;
    private Color m_DefaultColor    = Color.white;

    protected override IEnumerator EHoverBegin()
    {
        float _time = 0f;
        m_Color = m_Text.color;
        do
        {
            _time = Mathf.Clamp(_time + (Time.deltaTime * m_Multiplier), 0f, 1f);
            m_Text.color = Color.Lerp(m_Color, m_TargetColor, _time);
            yield return null;
        } while (_time != m_AnimationTime);
    }

    protected override IEnumerator EHoverEnd()
    {
        float _time = 0f;
        m_Color = m_Text.color;
        do
        {
            _time = Mathf.Clamp(_time + (Time.deltaTime * m_Multiplier), 0f, 1f);
            m_Text.color = Color.Lerp(m_Color, m_DefaultColor, _time);
            yield return null;
        } while (_time != m_AnimationTime);
    }

    protected override bool Initialize()
    {
        if(null == m_Text)
        {
            //m_Text = GetComponentInChildren<Text>();
            m_Text = GetComponentInChildren<TextMeshProUGUI>();

            if(null == m_Text)
            {
                return false;
            }
        }

        m_DefaultColor = m_Text.color;
        return true;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        m_Text.color = m_DefaultColor;
    }
}
