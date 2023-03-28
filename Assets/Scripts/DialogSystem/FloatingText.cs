using UnityEngine;
using TMPro;
using System.Collections;

public class FloatingText
{
    public bool active;
    public GameObject go;
    public TMP_Text txt;
    public char[] txtSplited;
    private char[] txtToPrint;
    public float duration;
    public float lastShown;

    public void Show()
    {
        active = true;
        lastShown = Time.time;
        txtToPrint = new char[txtSplited.Length];
        go.SetActive(active);
    }

    public void Hide()
    {
        active = false;
        go.SetActive(active);
    }

    public IEnumerator PrintText()
    {
        for (int i = 0; i < txtSplited.Length; i++)
        {
            txtToPrint[i] = txtSplited[i];
            txt.text = new string(txtToPrint);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void UpdateFloatingText()
    {
        if (!active) return;

        if (Time.time - lastShown > duration) Hide();
    }
}