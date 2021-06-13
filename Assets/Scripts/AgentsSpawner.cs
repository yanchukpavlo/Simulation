using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentsSpawner : MonoBehaviour
{
    public static AgentsSpawner instance;

    [Header("Settings")]
    [SerializeField] int maxAgentCount = 30;
    [SerializeField] float minTameToSpawn = 2f;
    [SerializeField] float maxTameToSpawn = 10f;
    [SerializeField] float zoneLength = 10;

    [Header("Agent")]
    [SerializeField] GameObject agentPref;
    [SerializeField] float agentSize = 1f;

    List<GameObject> agentBufferList;
    int currentAgentCount = 0;
    int index = 0;

    public float ZoneLength { get { return zoneLength; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        agentBufferList = new List<GameObject>();

        for (int i = 0; i < maxAgentCount; i++)
        {
            GameObject agent = Instantiate(agentPref, new Vector3(100, 100, 100), Quaternion.identity);
            agent.SetActive(false);

            agent.name = $"Agent {index}";
            index++;

            agent.transform.localScale = Vector3.one * agentSize;

            agentBufferList.Add(agent);
        }

    }

    private void Start()
    {
        EventsManager.instance.onAgentOffTrigger += AddToBuffer;

        CreateWalls();

        StartCoroutine(Spawn());
    }

    private void OnDestroy()
    {
        agentBufferList.Clear();
        EventsManager.instance.onAgentOffTrigger -= AddToBuffer;
    }

    void CreateWalls()
    {
        BoxCollider box;

        box = gameObject.AddComponent<BoxCollider>();
        box.size = new Vector3(1, zoneLength + 1, zoneLength + 1);
        box.center = new Vector3(zoneLength/2 + box.size.x / 2, 0, 0);

        box = gameObject.AddComponent<BoxCollider>();
        box.size = new Vector3(1, zoneLength + 1, zoneLength + 1);
        box.center = new Vector3(-zoneLength/2 - box.size.x / 2, 0, 0);

        box = gameObject.AddComponent<BoxCollider>();
        box.size = new Vector3(zoneLength + 1, zoneLength + 1, 1);
        box.center = new Vector3(0, 0, zoneLength/2 + box.size.z / 2);

        box = gameObject.AddComponent<BoxCollider>();
        box.size = new Vector3(zoneLength + 1, zoneLength + 1, 1);
        box.center = new Vector3(0, 0, -zoneLength/2 - box.size.z / 2);

        box = gameObject.AddComponent<BoxCollider>();
        box.size = new Vector3(zoneLength + 1, 1, zoneLength + 1);
        box.center = new Vector3(0, zoneLength / 2 + box.size.y / 2, 0);

        box = gameObject.AddComponent<BoxCollider>();
        box.size = new Vector3(zoneLength + 1, 1, zoneLength + 1);
        box.center = new Vector3(0, -zoneLength / 2 - box.size.y / 2, 0);
    }

    void AddToBuffer(GameObject obj)
    {
        agentBufferList.Add(obj);
        currentAgentCount--;

        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(Random.Range(minTameToSpawn, maxTameToSpawn));

        if (currentAgentCount < maxAgentCount)
        {
            GameObject agent;

            if (agentBufferList.Count == 0)
            {
                agent = Instantiate(agentPref,
                    new Vector3(100, 100, 100),
                    Quaternion.identity);

                agent.transform.localScale = Vector3.one * agentSize;

                agent.name = $"Agent {index}";
                index++;

                agent.transform.position = new Vector3(
                    Random.Range(-zoneLength/2, zoneLength/2), 
                    Random.Range(-zoneLength / 2, zoneLength / 2), 
                    Random.Range(-zoneLength/2, zoneLength/2));
            }
            else
            {
                agent = agentBufferList[0];
                agentBufferList.Remove(agent);

                agent.transform.position = new Vector3(
                    Random.Range(-zoneLength/2 + agentSize, zoneLength/2 - agentSize), 
                    Random.Range(-zoneLength / 2 + agentSize, zoneLength / 2 - agentSize), 
                    Random.Range(-zoneLength/2 + agentSize, zoneLength/2) - agentSize);

                agent.transform.localScale = Vector3.one * agentSize;

                agent.SetActive(true);
            }

            currentAgentCount++;

            StartCoroutine(Spawn());
        }
    }
}
