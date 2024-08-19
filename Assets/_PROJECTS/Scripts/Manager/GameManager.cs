using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Quocanh.pattern;
public class GameManager : QuocAnhSingleton<GameManager>
{
    [Header("PLAYER"),Space(10)]
    public TowerBehaviour tower;    //the tower itslef

    [Header("GAME"),Space(10)]
    public int currentWave;         //current wave that player playing
}
