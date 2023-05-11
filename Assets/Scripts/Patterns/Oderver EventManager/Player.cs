using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour, IObservable
{
    Rigidbody _rgbd;

    [SerializeField] float _jumpForce = 5;

    List<IObserver> _allObservers;

    void Awake()
    {
        _rgbd = GetComponent<Rigidbody>();
        _allObservers = new List<IObserver>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            EventManager.TriggerEvent(EventManager.Events.Event_PlayerDead, transform.position, Random.Range(3f, 11f));
        }
    }

    void Jump()
    {
        _rgbd.AddForce(Vector3.up * _jumpForce, ForceMode.VelocityChange);

        NotifyToObservers("Jump");
    }

    #region Observable

    public void Subscribe(IObserver obs)
    {
        //Si en mi lista no tengo a ese observer
        if (!_allObservers.Contains(obs))
        {
            //Lo agrego
            _allObservers.Add(obs);
        }
    }

    public void NotifyToObservers(string action)
    {

        //Recorro de atras hacia el principio mis observers registrados y los notifico
        for (int i = _allObservers.Count - 1; i >= 0; i--)
        {
            _allObservers[i].Notify(action);
        }
    }

    public void Unsubscribe(IObserver obs)
    {
        //Remuevo el observer de mi lista
        _allObservers.Remove(obs);
    }

    #endregion
}
