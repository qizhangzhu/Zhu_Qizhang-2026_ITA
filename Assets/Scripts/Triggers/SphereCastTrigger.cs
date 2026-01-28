// ============================================
// Sphere Cast Trigger
// ============================================
// PURPOSE: Fires events when sphere cast hits or stops hitting something
// USAGE: Attach to any GameObject, sphere casts from transform forward
// EVENTS:
//   - HitEvent - fires when sphere hits something
//   - LostEvent - fires when sphere stops hitting
// ============================================

using UnityEngine;
using UnityEngine.Events;

public class SphereCastTrigger : MonoBehaviour
{
    // ===== Settings =====
    [Header("Settings")]
    [Tooltip("Radius of the sphere cast")]
    public float radius = 0.5f;

    [Tooltip("How far the sphere travels")]
    public float distance = 10f;

    [Tooltip("Which layers the sphere can hit")]
    public LayerMask layerMask = ~0;

    [Tooltip("Delay in seconds before firing events (0 = instant)")]
    public float delay = 0f;

    // ===== Events =====
    [Header("Events")]
    public UnityEvent HitEvent;
    public UnityEvent LostEvent;

    // ===== State =====
    private bool wasHitting = false;

    // ===== SphereCast Logic =====
    private void Update()
    {
        bool isHitting = Physics.SphereCast(transform.position, radius, transform.forward, out _, distance, layerMask);

        if (isHitting && !wasHitting)
        {
            if (delay > 0)
                Invoke(nameof(FireHit), delay);
            else
                HitEvent?.Invoke();
        }
        else if (!isHitting && wasHitting)
        {
            if (delay > 0)
                Invoke(nameof(FireLost), delay);
            else
                LostEvent?.Invoke();
        }

        wasHitting = isHitting;
    }

    private void FireHit() => HitEvent?.Invoke();
    private void FireLost() => LostEvent?.Invoke();

    // ===== Gizmos =====
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * distance);
        Gizmos.DrawWireSphere(transform.position + transform.forward * distance, radius);
    }
}

// ============================================
// IMPLEMENTATION STEPS
// ============================================
// 1. Attach to any GameObject
// 2. Set radius, distance, and layerMask
// 3. Sphere shoots from object's position in forward direction
// 4. Connect events to Actions in Inspector
// ============================================
