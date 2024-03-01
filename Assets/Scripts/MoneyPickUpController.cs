using UnityEngine;

public class MoneyPickUpController : MonoBehaviour
{
    public void OnPickUp(string moneyName)
    {
        ProcessMoneySender.Instance.SendProcess(gameObject, moneyName);
    }
}
