// ============================================
// State Trigger
// ============================================
// PURPOSE: Stores a boolean state and fires different events based on Check()
// USAGE: Use as a condition gate between triggers and actions
// EVENTS:
//   - TrueEvent - fires when Check() is called and state is true
//   - FalseEvent - fires when Check() is called and state is false
//   - ChangedEvent - fires whenever state changes
// ============================================

using UnityEngine;
using UnityEngine.Events;

public class StateTrigger : MonoBehaviour
{
    // ===== State =====
    [Header("State")]
    [Tooltip("Current state value. Can be set in Inspector or via methods.")]
    public bool state = false;

    // ===== Events =====
    [Header("Events")]
    [Tooltip("Fires when Check() is called and state is TRUE")]
    public UnityEvent TrueEvent;

    [Tooltip("Fires when Check() is called and state is FALSE")]
    public UnityEvent FalseEvent;

    [Tooltip("Fires whenever the state changes (via SetTrue, SetFalse, or Toggle)")]
    public UnityEvent ChangedEvent;

    // ===== Check =====
    [ContextMenu("Check")]
    public void Check()
    {
        if (state)
        {
            TrueEvent?.Invoke();
        }
        else
        {
            FalseEvent?.Invoke();
        }
    }

    // ===== State Setters =====
    [ContextMenu("Set True")]
    public void SetTrue()
    {
        if (!state)
        {
            state = true;
            ChangedEvent?.Invoke();
        }
    }

    [ContextMenu("Set False")]
    public void SetFalse()
    {
        if (state)
        {
            state = false;
            ChangedEvent?.Invoke();
        }
    }

    [ContextMenu("Toggle")]
    public void Toggle()
    {
        state = !state;
        ChangedEvent?.Invoke();
    }

    // ===== Check And Set (Combined Operations) =====
    /// <summary>
    /// Check the state, then set it to true regardless of outcome.
    /// Useful for "one-time" gates that lock open after passing.
    /// </summary>
    [ContextMenu("Check Then Set True")]
    public void CheckThenSetTrue()
    {
        Check();
        SetTrue();
    }

    /// <summary>
    /// Check the state, then set it to false regardless of outcome.
    /// Useful for "one-time" gates that lock closed after passing.
    /// </summary>
    [ContextMenu("Check Then Set False")]
    public void CheckThenSetFalse()
    {
        Check();
        SetFalse();
    }
}

// ============================================
// IMPLEMENTATION STEPS
// ============================================
// 1. Attach to any GameObject
// 2. Set initial state (true/false)
// 3. Wire a trigger to call Check()
// 4. Connect TrueEvent to "success" actions
// 5. Connect FalseEvent to "fail" actions
// 6. Use SetTrue/SetFalse/Toggle from other triggers to change state
// ============================================

// ============================================
// EXAMPLE: Locked Door
// ============================================
// Setup:
//   - Door has MoveAction (to open)
//   - Key pickup has TriggerZone
//   - Door zone has TriggerZone
//   - StateTrigger starts with state = false
//
// Wiring:
//   Key TriggerZone.EnterEvent    -> StateTrigger.SetTrue()
//   Key TriggerZone.EnterEvent    -> DestroyAction.DestroySelf()
//   Door TriggerZone.EnterEvent   -> StateTrigger.Check()
//   StateTrigger.TrueEvent        -> MoveAction.Move()
//   StateTrigger.FalseEvent       -> (play locked sound)
// ============================================
