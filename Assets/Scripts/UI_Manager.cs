using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager instance;

    [Header("UI")]
    [SerializeField] GameObject infoPanel;
    [SerializeField] Text infoText;

    [Header("Raycast")]
    [SerializeField] LayerMask layer;
    [SerializeField] float raycastLength = 3000f;

    Camera _camera;
    Agent agent;

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

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (_camera == null)
            {
                _camera = FindObjectOfType<Camera>();
            }

            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, raycastLength, layer))
            {
                agent = hit.collider.GetComponent<Agent>();
                if (agent == null)
                {
                    Debug.Log("Raycast did not get.");
                }
            }
            else
            {
                agent = null;
                HideInfoText();
            }
        }

        if (agent != null)
        {
            if (agent.gameObject.activeSelf)
            {
                ShowInfoText(agent.name, agent.HP);
            }
            else
            {
                agent = null;
                HideInfoText();
            }
        }
    }

    public void ShowInfoText(string name, int hp)
    {
        infoText.text = $"Object name: {name}\n\nObject HP: {hp}";
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
