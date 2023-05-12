using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    //Creo el enum con todos los eventos a utilizar
    public enum Events
    {
        Event_PlayerDead,
        Event_ChangeInteractiveBarValues
    }

    //Creo un delegado donde voy a obligar a las funciones que se quieran suscribir
    //que devuelvan void y ademas tomen por parametro params object[]
    public delegate void Listener(params object[] parameters);

    //Creo el diccionario que va a guardar cada evento
    //con sus respectivas funciones registradas
    static Dictionary<Events, Listener> _events;

    public static void SubscribeToEvent(Events desiredEvent, Listener listenerToSubscribe)
    {
        //Si no tengo el diccionario inicializado, lo hago
        if (_events == null) _events = new Dictionary<Events, Listener>();

        //Si no tengo el evento pre-creado, lo hago
        if (!_events.ContainsKey(desiredEvent))
        {
            _events.Add(desiredEvent, null);
        }

        //Sumo a ese evento, la funcion que vino por parametro
        _events[desiredEvent] += listenerToSubscribe;
    }

    public static void UnsubscribeToEvent(Events desiredEvent, Listener listenerToUnsubscribe)
    {
        //Si no existe el diccionario entonces aviso y termino la funcion
        if (_events == null)
        {
            Debug.LogWarning("No events subscribed");
            return;
        }

        //Si tengo el evento pre-creado en el diccionario
        if (_events.ContainsKey(desiredEvent))
        {
            //Desuscribo la funcion de ese evento
            _events[desiredEvent] -= listenerToUnsubscribe;
        }
    }

    public static void TriggerEvent(Events desiredEvent, params object[] parameters)
    {
        //Si mi diccionario existe
        if (_events != null)
        {
            //Y el evento tambien
            if (_events.ContainsKey(desiredEvent))
            {
                //Entonces accedo a ese evento y si tengo alguien suscripto,
                //ejecuto esas funciones pasando por parametro lo que me llego 
                _events[desiredEvent]?.Invoke(parameters);
            }
        }
    }
}