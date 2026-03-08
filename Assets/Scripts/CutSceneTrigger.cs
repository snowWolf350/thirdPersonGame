using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class CutSceneTrigger : MonoBehaviour
{
    public bool _OnCollisionTrigger;

    BoxCollider _boxCollider;
    PlayableDirector _playableDirector;

    [Header("The timeline GameObject")]
    [SerializeField] GameObject _timelineGameObject;

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _playableDirector = _timelineGameObject.GetComponent<PlayableDirector>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<ThirdPersonShooterController>() == null) return;
        
        //player hit the collider 
        if (_OnCollisionTrigger)
        {
            _playableDirector.Play();
            _boxCollider.enabled = false;
        }
    }

    public void DestroSelf()
    {
        Destroy(gameObject);
    }
}
