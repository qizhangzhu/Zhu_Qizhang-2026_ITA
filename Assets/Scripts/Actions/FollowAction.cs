// ============================================
// Follow Action
// ============================================
// PURPOSE: Makes target smoothly follow another transform
// USAGE: Attach to any GameObject, set target and follow target
// ACTIONS:
//   - StartFollowing() - begin following
//   - StopFollowing() - stop following
// ============================================

using UnityEngine;

public class FollowAction : MonoBehaviour
{
    // ===== Target =====
    [Header("Target")]
    [Tooltip("What moves (defaults to this object)")]
    public Transform target;

    [Tooltip("What to follow")]
    public Transform followTarget;

    // ===== Settings =====
    [Header("Settings")]
    [Tooltip("Offset from the follow target")]
    public Vector3 offset;

    [Tooltip("How fast to follow (higher = snappier)")]
    public float smoothSpeed = 5f;

    [Tooltip("Start following immediately on scene start")]
    public bool followOnStart = false;

    // ===== State =====
    private bool isFollowing = false;

    // ===== Lifecycle =====
    private void Awake()
    {
        if (target == null)
            target = transform;
    }

    private void Start()
    {
        if (followOnStart) StartFollowing();
    }

    private void Update()
    {
        if (!isFollowing || target == null || followTarget == null) return;

        Vector3 desiredPosition = followTarget.position + offset;
        target.position = Vector3.Lerp(target.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }

    // ===== Actions =====
    [ContextMenu("Start Following")]
    public void StartFollowing()
    {
        isFollowing = true;
    }

    [ContextMenu("Stop Following")]
    public void StopFollowing()
    {
        isFollowing = false;
    }

    public void SnapToTarget()
    {
        if (target == null || followTarget == null) return;
        target.position = followTarget.position + offset;
    }

    // ===== Gizmos =====
    private void OnDrawGizmosSelected()
    {
        if (followTarget == null) return;
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(followTarget.position + offset, 0.1f);
        if (target != null)
        {
            Gizmos.DrawLine(target.position, followTarget.position + offset);
        }
    }
}

// ============================================
// IMPLEMENTATION STEPS
// ============================================
// 1. Attach to any GameObject
// 2. Set target (what moves) - defaults to this GameObject
// 3. Set followTarget (what to follow)
// 4. Adjust smoothSpeed and offset
// 5. Call StartFollowing()/StopFollowing() from triggers
// ============================================
