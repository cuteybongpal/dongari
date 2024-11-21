using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[System.Serializable]
//public class EventEntry
//{
//    public EVENT_TYPE eventType;
//    public List<ILinerEvnet> eventListeners;
//}
public class EvnetManager : MonoBehaviour
{
    public static EvnetManager Instance { get { return instance; }}
    private static EvnetManager instance = null;
    private Dictionary<EVENT_TYPE, List<ILinerEvnet>> listeners = new Dictionary<EVENT_TYPE, List<ILinerEvnet>>();
    //public List<EventEntry> eventEntries = new List<EventEntry>();
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(this);
        }

        //foreach (var entry in eventEntries)
        //{
        //    listeners.Add(entry.eventType, entry.eventListeners);
        //}
    }
    private void OnLevelWasLoaded()
    {
        RemoveLoadScene();
    }

    //private void UpdateEventEntries(EVENT_TYPE type, List<ILinerEvnet> list)
    //{
    //    var entry = eventEntries.Find(e => e.eventType == type);
    //    if (entry != null)
    //    {
    //        entry.eventListeners = list;
    //    }
    //    else
    //    {
    //        eventEntries.Add(new EventEntry { eventType = type, eventListeners = list });
    //    }
    //}
    public void AddListener(EVENT_TYPE type, ILinerEvnet evnet)
    {
        List<ILinerEvnet> list = null;
        if(listeners.TryGetValue(type, out list))
        {
            list.Add(evnet);
            return;
        }

        list = new List<ILinerEvnet>
        {
            evnet
        };
        listeners.Add(type, list);

        //UpdateEventEntries(type, list);
    }

    public void PostListener(EVENT_TYPE type, Component sender,object param1 = null, object param2 = null)
    {
        Debug.Log("넘어간다니라");
        List <ILinerEvnet> list = null;
        if (!listeners.TryGetValue(type, out list)) return;
        Debug.Log(list.Count);
        for(int i = 0; i < listeners.Count; i++)
        {
            if (!list[i].Equals(null))
            {
                list[i].OnEvnet(type, sender, param1, param2);
            }
      
        }
    }

    public void RemoveListener(EVENT_TYPE type)
    {
        listeners.Remove(type);
    }

    public void RemoveLoadScene()
    {
        Dictionary<EVENT_TYPE, List<ILinerEvnet>> TMPlist = new Dictionary<EVENT_TYPE, List<ILinerEvnet>>();
        
        foreach(KeyValuePair<EVENT_TYPE,List<ILinerEvnet>> item in listeners)
        {
            for(int i = listeners.Count - 1; i >= 0; i--)
            {
                if (item.Value[i].Equals(null))
                {
                    item.Value.RemoveAt(i);
                }
            }

            if(item.Value.Count > 0)
            {
                TMPlist.Add(item.Key, item.Value);
            }
        }

        listeners = TMPlist;
    }
}
