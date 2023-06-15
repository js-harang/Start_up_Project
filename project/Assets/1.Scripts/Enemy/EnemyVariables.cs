using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class EnemyVariables
{
    public bool feelAlert;
    public bool hearAlert;
    public bool advanceCoverDecision;
    public int watiRounds;
    public bool repeatShot;
    public float waitInCoverTime;
    public float coverTime;
    public float patrolTimer;
    public float shotTimer;
    public float startShootTimer;
    public float currentShots;
    public float shotsInRounds;
    public float blindEngageTimer;
    internal int waitRounds;
}
