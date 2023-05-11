using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObservable
{
    void Subscribe(IObserver obs);

    void NotifyToObservers(string action);

    void Unsubscribe(IObserver obs);
}
