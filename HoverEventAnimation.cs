using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;


public abstract class HoverEventAnimation : MonoBehaviour
{
    [Tooltip("'InteractableHoverEvents' is the source code of SteamVR.")]
    public InteractableHoverEvents interactionHoverEvents;
    protected Coroutine m_Routine;
    protected const float m_AnimationTime = 1f;
    protected const float m_Multiplier = 3f;

    protected abstract bool Initialize();
    protected abstract IEnumerator EHoverBegin();
    protected abstract IEnumerator EHoverEnd();

    protected void Awake()
    {
        if(null == interactionHoverEvents)
        {
            interactionHoverEvents = GetComponent<InteractableHoverEvents>();
        }

        if(null == interactionHoverEvents)
        {
            Debug.LogWarning("InteractableHoverEvents Component is not found.");
            return;
        }

        if(false == Initialize())
        {
            Debug.LogWarning("HoverEvent Initialize Fail.");
            return;
        }

        interactionHoverEvents.onHandHoverBegin.AddListener(OnHoverBegin);
        interactionHoverEvents.onHandHoverEnd.AddListener(OnHoverEnd);
    }

    public void OnHoverBegin()
    {
        StopRoutine();
        m_Routine = StartCoroutine(EHoverBegin());
    }

    public void OnHoverEnd()
    {
        StopRoutine();
        m_Routine = StartCoroutine(EHoverEnd());
    }

    protected virtual void OnDisable()
    {
        StopRoutine();
    }

    protected void StopRoutine()
    {
        if(null != m_Routine)
        {
            StopCoroutine(m_Routine);
            m_Routine = null;
        }
    }
}
