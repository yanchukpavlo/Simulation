using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager instance;

    [SerializeField] GameObject infoPanel;
    [SerializeField] Text infoText;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ShowInfoText(string name, int hp)
    {
        infoText.text = $"GameObject name: {name}\n\nObject HP: {hp}";
        infoPanel.SetActive(true);
    }

    public void HideInfoText()
    {
        infoPanel.SetActive(false);
    }

    public void PrintMarkoPolo()
    {

#if UNITY_EDITOR
        for (int i = 1; i <= 100; i++)
        {
            Debug.Log(i);

            if (i % 3 == 0 && i % 5 == 0)
            {
                Debug.Log("MarkoPolo");
            }
            else if (i % 3 == 0)
            {
                Debug.Log("Marko");
            }
            else if (i % 5 == 0)
            {
                Debug.Log("Polo");
            }
        }
#endif

    }
}
