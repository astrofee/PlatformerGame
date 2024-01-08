using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    private List<Coroutine> coroutines = new List<Coroutine>();

    public void CreateVFX(GameObject vfx, Vector3 position, Quaternion rotation, Transform? parent)
    {
        GameObject vfxObject;
        if(parent != null)
        {
            // Create VFX with parent
            vfxObject = Instantiate(vfx, position, rotation, parent);
        }
        else
        {
            // Create VFX without parent
            vfxObject = Instantiate(vfx, position, rotation);
        }

        // Start the coroutine and add it to the list
        Coroutine coroutine = StartCoroutine(DestroyVFXAfterDelay(vfxObject, 1.115f));
        coroutines.Add(coroutine);
    }

    IEnumerator DestroyVFXAfterDelay(GameObject vfx, float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Destroy the VFX
        Destroy(vfx);
    }

    public void StopAndDestroyVFX(Coroutine coroutine)
    {
        // Stop the coroutine and remove it from the list
        if (coroutines.Contains(coroutine))
        {
            StopCoroutine(coroutine);
            coroutines.Remove(coroutine);
        }
    }
}
