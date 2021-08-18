using System;
using System.Collections.Generic;
using UnityEngine;

public class Dispatcher : MonoBehaviour
{
    static Dispatcher m_Instance;

    public static Dispatcher Instance
    {
        get
        {
            return m_Instance;
        }
    }

    private List<Action> m_Actions = new List<Action>();
    private bool m_Queued = false;

    public void Dispatch(Action action)
    {
        lock (m_Actions)
        {
            m_Actions.Add(action);
            m_Queued = true;
        }
    }

    void Awake()
    {
        m_Instance = this;
    }

    void Update()
    {
        if (m_Queued)
        {
            Action[] actions = null;

            lock (m_Actions)
            {
                actions = m_Actions.ToArray();
                m_Actions.Clear();
                m_Queued = false;
            }

            foreach (Action action in actions)
                action();
        }
    }
}
