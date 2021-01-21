using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringElement : MonoBehaviour
{
    public delegate void ScoreHandler(object content);
    public static event ScoreHandler ScoreHandlerEvent;

    public virtual void ScorePoint(object o) {
        ScoreHandlerEvent?.Invoke(o);
    }
}
