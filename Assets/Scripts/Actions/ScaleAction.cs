// ============================================
// Scale Action
// ============================================
// PURPOSE: Scales target to a size
// USAGE: Attach to any GameObject, set target and scale
// ACTIONS:
//   - Scale() - scale instantly or animated
// ============================================

using UnityEngine;
using DG.Tweening;

public class ScaleAction : MonoBehaviour
{
    // ===== Target =====
    [Header("Target")]
    [Tooltip("What to scale (defaults to this object)")]
    public Transform target;

    // ===== Settings =====
    [Header("Settings")]
    [Tooltip("Target scale (used when uniform is false)")]
    public Vector3 targetScale = Vector3.one;

    [Tooltip("Animation time in seconds (0 = instant)")]
    public float duration = 0f;

    [Tooltip("Use uniform scale instead of per-axis")]
    public bool uniform = true;

    [Tooltip("Uniform scale value (used when uniform is true)")]
    public float uniformScale = 1f;

    // ===== Lifecycle =====
    private void Awake()
    {
        if (target == null)
            target = transform;
    }

    // ===== Actions =====
    [ContextMenu("Scale")]
    public void Scale()
    {
        if (target == null) return;

        Vector3 finalScale = uniform ? Vector3.one * uniformScale : targetScale;

        if (duration > 0)
        {
            target.DOScale(finalScale, duration);
        }
        else
        {
            target.localScale = finalScale;
        }
    }

    public void Scale(float customDuration)
    {
        if (target == null) return;

        Vector3 finalScale = uniform ? Vector3.one * uniformScale : targetScale;
        target.DOScale(finalScale, customDuration);
    }

    public void SetUniformScale(float scale)
    {
        if (target == null) return;
        target.localScale = Vector3.one * scale;
    }
}

// ============================================
// IMPLEMENTATION STEPS
// ============================================
// 1. Attach to any GameObject
// 2. Set target (what scales) - defaults to this GameObject
// 3. Set targetScale or uniformScale
// 4. Set duration (0 = instant, >0 = animated)
// 5. Call Scale() from triggers
// ============================================
