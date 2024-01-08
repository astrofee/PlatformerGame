using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManager : MonoBehaviour
{
    [SerializeField] VFXManager vfxManager;
    [SerializeField] Move currentMove;
    [SerializeField] Transform currentTarget;

    void FixedUpdate()
    {
        if (currentMove != null && currentTarget != null)
        {
            UpdateActionObjects(currentMove, currentTarget);
        }
    }

    void ExecuteMove(Move move, Transform target)
    {
        currentMove = move;
        currentTarget = target;

        // Loop through all VFX objects
        for (int i = 0; i < move.vfxObjects.Count; i++)
        {
            // Get the current VFX object
            Move.VFXObject vfxObject = move.vfxObjects[i];

            // Create the VFX using VFXManager
            vfxManager.CreateVFX(vfxObject.vfx, vfxObject.position, vfxObject.rotation, vfxObject.parent);
        }
    }

    void UpdateActionObjects(Move move, Transform target)
    {
        // Instantiate action objects according to frames
        float frameTime = Time.deltaTime / 60;
        for (int i = 0; i < move.actions; i++)
        {
            if (move.actionframes[i] * frameTime <= Time.time)
            {
                // Instantiate the action object
                GameObject actionObject = Instantiate(move.actionobjects[i], target.position, Quaternion.identity);
            }
        }
        if (move.actions == move.actionobjects.Count)
        {
            isMoveDone = true;
        }
    }
    public bool IsMoveDone()
    {
        return isMoveDone;
    }
}
