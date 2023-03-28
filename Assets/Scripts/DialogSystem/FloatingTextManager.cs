using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingTextManager : MonoBehaviour
{
    public GameObject textContainer;
    public GameObject textPrefab;

    private List<FloatingText> floatingTexts = new List<FloatingText>();

    private void Update()
    {
        foreach (FloatingText floatingText in floatingTexts)
            floatingText.UpdateFloatingText();
    }

    public void Show(string msg, int fontSize, Color color, Vector3 position, float duration)
    {
        FloatingText floatingText = GetFloatingText();

        floatingText.txtSplited = msg.ToCharArray();
        floatingText.txt.fontSize = fontSize;
        floatingText.txt.color = color;

        floatingText.go.transform.position = Camera.main.WorldToScreenPoint(position);
        floatingText.duration = duration;

        floatingText.Show();
        StartCoroutine(floatingText.PrintText());
    }

    private FloatingText GetFloatingText()
    {
        FloatingText floatingText = floatingTexts.Find(t => !t.active);

        if (floatingText == null)
        {
            floatingText = new FloatingText();
            floatingText.go = Instantiate(textPrefab);
            floatingText.go.transform.SetParent(textContainer.transform);
            floatingText.txt = floatingText.go.GetComponentInChildren<TMP_Text>();

            floatingTexts.Add(floatingText);
        }

        return floatingText;
    }
}