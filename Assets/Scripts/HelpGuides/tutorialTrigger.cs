using System;
using UnityEngine;


[RequireComponent(typeof(BoxCollider))]
public class tutorialTrigger : MonoBehaviour
{
    [Header("Scriptable obj")]
    [SerializeField] TutorialSO _tutorialSO;

    BoxCollider _boxCollider;

    public static event EventHandler<TutorialEventArgs> OnTutorialEnter;
    public static event EventHandler OnTutorialExit;

    public class TutorialEventArgs :EventArgs
    {
        public TutorialSO tutorialSO;
    }

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnTutorialEnter?.Invoke(this, new TutorialEventArgs
            {
                tutorialSO = _tutorialSO,
            });
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnTutorialExit?.Invoke(this, EventArgs.Empty);
        }
    }


}
