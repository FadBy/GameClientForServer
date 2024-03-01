using System;
using UnityEngine;

[Serializable]
public class PlayerIdentity
{
    public string Nickname;

    public PlayerIdentity(string nickname)
    {
        Nickname = nickname;
    }
}
