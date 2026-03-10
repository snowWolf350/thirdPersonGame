using UnityEngine;

public class GlobalReferences : MonoBehaviour
{
    public static GlobalReferences Instance;

    public Transform PlayerTransform;

    private void Awake()
    {
        Instance = this;
    }
}
