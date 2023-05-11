using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IObservable
{
    IObserver _myObserver;

    [SerializeField] KeyCode _keyToDie;
    
    private void Update()
    {
        if (Input.GetKeyDown(_keyToDie))
        {
            NotifyToObservers("EnemyDead");
            Destroy(gameObject);
        }
    }

    #region Observable

    public void NotifyToObservers(string action)
    {
        _myObserver?.Notify(action);
    }

    public void Subscribe(IObserver obs)
    {
        _myObserver = obs;
    }

    public void Unsubscribe(IObserver obs)
    {
        if (obs == _myObserver)
        {
            _myObserver = null;
        }
    }

    #endregion

}
