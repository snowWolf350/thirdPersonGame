using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    public float _DestroyTimer;

    private void Update()
    {
        _DestroyTimer -= Time.deltaTime;
        if (_DestroyTimer < 0)
        {
            Destroy(gameObject);
        }
    }
}
