using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class CutSceneTrigger : MonoBehaviour
{
    public bool _OnCollisionTrigger;

    BoxCollider _boxCollider;
    PlayableDirector _playableDirector;
    [SerializeField] GameObject _timelineGameObject;

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _playableDirector = _timelineGameObject.GetComponent<PlayableDirector>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<ThirdPersonShooterController>() == null) return;
        
        //plaayer hit the collider 
        if (_OnCollisionTrigger)
        {
            StartCoroutine(PlayCutscene());
        }
    }

    IEnumerator PlayCutscene()
    {
        _playableDirector.Play();
        _boxCollider.enabled = false;   
        yield return new WaitForSeconds((float)_playableDirector.duration);
        Destroy(_playableDirector.gameObject);
        Destroy(gameObject);
    }

}
