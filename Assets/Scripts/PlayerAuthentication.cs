using System;

[Serializable]
public class PlayerAuthentication
{
    public string nickname;

    public PlayerAuthentication(string nickname)
    {
        this.nickname = nickname;
    }
}
