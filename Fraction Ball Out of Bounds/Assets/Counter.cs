using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter
{
    public long userCount;
    // Start is called before the first frame update
    public Counter(long count)
    {
        userCount = count;
    }

    public void add()
    {
        userCount++;
    }
}
