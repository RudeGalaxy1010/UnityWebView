using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FactsController : MonoBehaviour
{
    [SerializeField] private TMP_Text _factsText;
    [SerializeField] private Button _switchFactButton;
    [SerializeField] private FactsProvider _factsProvider;

    private void OnEnable()
    {
        _switchFactButton.onClick.AddListener(OnSwitchFactButtonClick);
    }

    private void OnDisable()
    {
        _switchFactButton.onClick.RemoveListener(OnSwitchFactButtonClick);
    }

    private void Start()
    {
        SwitchFact();
    }

    private void OnSwitchFactButtonClick()
    {
        SwitchFact();
    }

    private void SwitchFact()
    {
        _factsText.text = _factsProvider.GetRandomFact();
    }
}
