using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Quocanh.pattern;
using UnityEngine.UI;
using TMPro;
public class GameManager : QuocAnhSingleton<GameManager>
{
    [Header("PLAYER"),Space(10)]
    public TowerBehaviour tower;                //the tower itslef

    [Header("GAME"),Space(10)]
    public int currentWave;                     //current wave that player playing

    [Header("WAVE SPAWNER"), Space(10)]
    public EntitiesSpawner entitySpawner;       //entity spawner

    [Header("GAME UI"), Space(10)]
    public Slider waveSlider;
    public TextMeshProUGUI waveText;
}
