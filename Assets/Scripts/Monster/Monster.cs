using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;


[CreateAssetMenu(fileName = "Enemy Data", menuName = "Data/Enemy")]
public class Monster : ScriptableObject
{
    [Header("Status")]
    public int id;
    public string monsterName;
    public int Hp;
    public int Atk;
    public int Dex;
    public float knockBack;
    public int DropMoney;

    /*
    if knockBack > 8.0 then knockBack = 8 + (knockBack - 8.0) * 0.9
    if knockBack > 10.0 then knockBack = 10 + (knockBack - 10.0) * 0.8
    if knockBack > 12.0 then knockBack = 12 + (knockBack - 12.0) * 0.7
    if knockBack > 14.0 then knockBack = 14 + (knockBack - 14.0) * 0.6
    if knockBack > 16.0 then knockBack = 16
    if isCrit then knockBack = knockBack * 1.4
    */
}
