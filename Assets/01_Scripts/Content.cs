using System;
using System.Collections.Generic;

[Serializable]
public class Content
{
    public List<Unit> units;

    public Content()
    {
        units = new List<Unit>();
    }
}
