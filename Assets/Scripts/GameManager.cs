using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if(!introduction)
        {
            introduction = true;
            StartCoroutine(PrintIntroduction());
        }
    }

    public FloatingTextManager floatingTextManager;
    public GameObject player;

    private bool introduction = false;

    public void ShowText(string msg, int fontSize, Color color, Vector3 position, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, duration);
    }

    public IEnumerator PrintIntroduction()
    {
        yield return new WaitForSeconds(1f);
        instance.ShowText("Here Ultimate Mecha Penguin IX.", 25, Color.black, CalcNewPosition(), 4.0f);
        yield return new WaitForSeconds(4f);
        instance.ShowText("We arrived to defend the King and the eggs.", 25, Color.black, CalcNewPosition(), 6.0f);
        yield return new WaitForSeconds(6f);
        instance.ShowText("They all are safe for now...", 25, Color.black, CalcNewPosition(), 4.0f);
        yield return new WaitForSeconds(4f);
        instance.ShowText("Wait! The Cyber Killer Whales are coming!", 25, Color.black, CalcNewPosition(), 5.0f);
        yield return new WaitForSeconds(5f);
    }

    /*
     * Use that function to print a message: duration is time in seconds, msg is the text to display.
     * To display another line (next message), re-call the function a 2nd time.
     * */
    public IEnumerator PrintCustomText(float duration, string msg)
    {
        instance.ShowText(msg, 25, Color.black, CalcNewPosition(), duration);
        yield return new WaitForSeconds(duration);
    }

    private Vector3 CalcNewPosition()
    {
        Vector3 v = new(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        return v;
    }
}