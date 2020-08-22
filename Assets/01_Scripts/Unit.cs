﻿using System;
using System.Collections.Generic;

[Serializable]
public class Unit
{
    public List<Lesson> lessons;
    public string unitName;
    public Unit(string unitName)
    {
        this.unitName = unitName;
        lessons = new List<Lesson>();
    }
}