using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Copycat : MonoBehaviour, IObserver
{
    IObservable _objectObserved;

    Rigidbody _rgbd;

    [SerializeField] float _jumpForce = 5;

    Dictionary<string, System.Action> _notifications;

    void Awake()
    {
        _rgbd = GetComponent<Rigidbody>();

        FillNotifiesDictionary();
    }

    private void OnEnable()
    {
        EventManager.SubscribeToEvent(EventManager.Events.Event_PlayerDead, PlayerDead);
    }

    private void OnDisable()
    {
        EventManager.UnsubscribeToEvent(EventManager.Events.Event_PlayerDead, PlayerDead);
    }

    private void Start()
    {
        _objectObserved = FindObjectOfType<Player>();
        _objectObserved.Subscribe(this);
    }

    void FillNotifiesDictionary()
    {
        _notifications = new Dictionary<string, System.Action>();

        _notifications.Add("Jump", Jump);
    }

    void Jump()
    {
        _rgbd.AddForce(Vector3.up * _jumpForce, ForceMode.VelocityChange);
    }

    void PlayerDead(params object[] p)
    {
        Vector3 _lastKnownPosition = (Vector3)p[0];
        float _aliveDuration = (float)p[1];

        Debug.Log($"Player died at {_lastKnownPosition}, took {_aliveDuration} seconds");

        _objectObserved.Unsubscribe(this);

        EventManager.UnsubscribeToEvent(EventManager.Events.Event_PlayerDead, PlayerDead);

    }

    #region Observer

    public void Notify(string action)
    {
        //Chequeo si en mi diccionario de notificaciones existe esa notificacion
        if (_notifications.ContainsKey(action))
        {
            //Ejecuto la funcion que representa a la notificacion
            _notifications[action]();
        }
    }

    #endregion
}
