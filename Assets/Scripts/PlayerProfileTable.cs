using System;
using UnityEngine;

[Serializable]
public class PlayerProfileTable
{
    public int Id;
    public string Nickname;
    public string RegisterDate;
    public string LastLoginDate;
    public bool BanStatus;
    public float MoneyAmount;
}
