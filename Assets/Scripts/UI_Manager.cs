using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager instance;

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
