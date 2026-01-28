// ============================================
// Torque Action
// ============================================
// PURPOSE: Adds rotational force to a Rigidbody
// USAGE: Attach to any GameObject, set target Rigidbody
// ACTIONS:
//   - Spin() - add torque
// ============================================

using UnityEngine;

public class TorqueAction : MonoBehaviour
{
    // ===== Target =====
    [Header("Target")]
    [Tooltip("The Rigidbody to apply torque to")]
    public Rigidbody target;

    // ===== Settings =====
    [Header("Settings")]
    [Tooltip("Rotation axis and magnitude")]
    public Vector3 torque = Vector3.up * 10f;

    [Tooltip("Impulse = instant spin, Force = continuous")]
    public ForceMode forceMode = ForceMode.Impulse;

    [Tooltip("Use object's local axes instead of world")]
    public bool useLocalDirection = false;

    // ===== Actions =====
    [ContextMenu("Spin")]
    public void Spin()
    {
        if (target == null) return;

        Vector3 finalTorque = useLocalDirection ? target.transform.TransformDirection(torque) : torque;
        target.AddTorque(finalTorque, forceMode);
    }

    public void Spin(float multiplier)
    {
        if (target == null) return;

        Vector3 finalTorque = useLocalDirection ? target.transform.TransformDirection(torque) : torque;
        target.AddTorque(finalTorque * multiplier, forceMode);
    }

    // ===== Gizmos =====
    private void OnDrawGizmosSelected()
    {
        if (target == null) return;
        Gizmos.color = Color.red;
        Vector3 dir = useLocalDirection ? target.transform.TransformDirection(torque.normalized) : torque.normalized;

        Gizmos.DrawRay(target.position, dir * 1.5f);
        Gizmos.DrawWireSphere(target.position, 0.3f);
    }
}

// ============================================
// IMPLEMENTATION STEPS
// ============================================
// 1. Attach to any GameObject
// 2. Set target Rigidbody
// 3. Set torque axis and magnitude
// 4. Choose ForceMode (Impulse for instant spin, Force for continuous)
// 5. Call Spin() from triggers
// ============================================
