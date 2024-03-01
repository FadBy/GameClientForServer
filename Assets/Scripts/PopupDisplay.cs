using TMPro;
using UnityEngine;

public class PopupDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void ChangeMessage(string newMessage)
    {
        _textMeshPro.text = newMessage;
    }
}
