using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    Stack<IScreen> _stack;

    public static ScreenManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;

        _stack = new Stack<IScreen>();
    }

    public void Push(IScreen newScreen)
    {
        if (_stack.Count > 0)
        {
            _stack.Peek().Deactivate();
        }

        _stack.Push(newScreen);

        newScreen.Activate();
    }

    public void Push(string resourceName)
    {
        var go = Instantiate(Resources.Load<GameObject>(resourceName));

        if (go.TryGetComponent(out IScreen newScreen))
        {
            Push(newScreen);
        }
    }

    public void Pop()
    {
        if (_stack.Count <= 1) return;

        _stack.Pop().Free();

        if (_stack.Count == 0) return;
        
        _stack.Peek().Activate();
    }
}
