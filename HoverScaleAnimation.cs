using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverScaleAnimation : HoverEventAnimation
{
    public float hoveredScaleSize = 1.3f;

    private Vector3 m_Scale         = Vector3.one;
    private Vector3 m_UpScaleSize   = Vector3.one;
    private Vector3 m_DefaultSize   = Vector3.one;

    protected override IEnumerator EHoverBegin()
    {
        float _time = 0f;
        m_Scale = this.transform.localScale;
        do
        {
            _time = Mathf.Clamp(_time + (Time.deltaTime * m_Multiplier), 0f, m_AnimationTime);
            this.transform.localScale = Vector3.Lerp(m_Scale, m_UpScaleSize, _time);
            yield return null;
        } while (_time != m_AnimationTime);
    }

    protected override IEnumerator EHoverEnd()
    {
        float _time = 0f;
        m_Scale = this.transform.localScale;
        do
        {
            _time = Mathf.Clamp(_time + (Time.deltaTime * m_Multiplier), 0f, m_AnimationTime);
            this.transform.localScale = Vector3.Lerp(m_Scale, m_DefaultSize, _time);
            yield return null;
        } while (_time != m_AnimationTime);
    }

    protected override bool Initialize()
    {
        m_DefaultSize = this.transform.localScale;
        m_UpScaleSize = m_DefaultSize * hoveredScaleSize;
        return true;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        this.transform.localScale = m_DefaultSize;
    }
}
