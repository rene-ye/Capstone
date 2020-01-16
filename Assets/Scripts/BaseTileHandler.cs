﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface BaseTileHandler
{
    GameObject getGameObject();
    Vector2Int getCoordinate();
    Unit getCurrentUnit();
    int getNodeWeight();
}
