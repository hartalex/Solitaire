using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour {
    
    Dictionary<string, int> data = new Dictionary<string, int>();

    public void Start()
    {
       // load from file
    }

    public int GetStat(string stat)
    {
        int retval = 0;
        if (data.ContainsKey(stat))
        {
            retval = data[stat];
        }
        return retval;
    }

    public void IncrementStat(string stat)
    {
        if (data.ContainsKey(stat))
        {
            data[stat]++;
        }
        else
        {
            data.Add(stat, 1);
        }
    }

    public void DecrementStat(string stat)
    {
        if (data.ContainsKey(stat))
        {
            data[stat]++;
            if (data[stat] < 0)
            {
                data[stat] = 0;
            }
        }
        else
        {
            data.Add(stat, 0);
        }
    }

}
