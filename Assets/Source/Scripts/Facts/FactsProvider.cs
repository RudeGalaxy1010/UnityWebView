using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FactsProvider : MonoBehaviour
{
    private const string FactsFilePath = "Facts";

    private List<string> _facts = new List<string>();

    private void Awake()
    {
        _facts = new List<string>();
        TextAsset dictionaryTextFile = Resources.Load<TextAsset>(FactsFilePath);
        _facts = dictionaryTextFile.text.Split(".\r\n").Select(w => w).ToList();
    }

    public string GetRandomFact()
    {
        return _facts[Random.Range(0, _facts.Count)];
    }
}
