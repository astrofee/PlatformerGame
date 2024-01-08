using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Move", menuName = "Move")]
public class Move : ScriptableObject
{
    [System.Serializable]
    public struct VFXObject
    {
        public GameObject vfx;
        public Vector3 position;
        public Quaternion rotation;
        public Transform parent;
        public float duration;
    }
    public List<VFXObject> vfxObjects;
    public List<int> vfxFrames;
    public string name;
    public bool forcedmovement;
    [Header("Move Fields")]
    public int actions;
    public List<GameObject> actionobjects;
    public List<int> actionframes;
    [Header("Forced Movement Fields")]
    public Vector3 relativeendposition;
    public int frames;

}
