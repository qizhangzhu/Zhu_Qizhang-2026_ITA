// ============================================
// Spawn Within Action
// ============================================
// PURPOSE: Spawns a prefab at random position within an area
// USAGE: Attach to any GameObject, set prefab and area size
// ACTIONS:
//   - Spawn() - spawn prefab in random position
// ============================================

using UnityEngine;

public class SpawnWithinAction : MonoBehaviour
{
    // ===== Target =====
    [Header("Prefab")]
    [Tooltip("The prefab to instantiate")]
    public GameObject prefab;

    // ===== Settings =====
    [Header("Settings")]
    [Tooltip("Size of the random spawn area")]
    public Vector3 areaSize = Vector3.one;

    [Tooltip("Optional parent for the spawned object")]
    public Transform parent;

    [Tooltip("Apply random rotation to spawned object")]
    public bool randomRotation = false;

    // ===== Actions =====
    [ContextMenu("Spawn")]
    public void Spawn()
    {
        if (prefab == null) return;

        Vector3 randomOffset = new Vector3(
            Random.Range(-areaSize.x / 2f, areaSize.x / 2f),
            Random.Range(-areaSize.y / 2f, areaSize.y / 2f),
            Random.Range(-areaSize.z / 2f, areaSize.z / 2f)
        );

        Vector3 position = transform.position + randomOffset;
        Quaternion rotation = randomRotation ? Random.rotation : Quaternion.identity;

        Instantiate(prefab, position, rotation, parent);
    }

    // ===== Gizmos =====
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 1f, 0.3f);
        Gizmos.DrawCube(transform.position, areaSize);
        Gizmos.DrawWireCube(transform.position, areaSize);
    }
}

// ============================================
// IMPLEMENTATION STEPS
// ============================================
// 1. Attach to any GameObject
// 2. Set prefab to spawn
// 3. Set areaSize for spawn bounds
// 4. Optionally set parent for spawned objects
// 5. Call Spawn() from triggers
// ============================================
