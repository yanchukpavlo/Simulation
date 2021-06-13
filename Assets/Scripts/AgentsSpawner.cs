using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentsSpawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] int maxAgentCount = 30;
    [SerializeField] float minTameToSpawn = 2f;
    [SerializeField] float maxTameToSpawn = 10f;
    [SerializeField] float zoneLength = 10;

    [Header("Agent")]
    [SerializeField] GameObject agentPref;


    List<GameObject> agentBufferList;
    int currentAgentCount = 0;
    int index = 0;

    private void Awake()
    {
        agentBufferList = new List<GameObject>();

        for (int i = 0; i < maxAgentCount; i++)
        {
            GameObject agent = Instantiate(agentPref, new Vector3(100, 100, 100), Quaternion.identity);
            agent.SetActive(false);

            agent.name = $"Agent {index}";
            index++;

            agentBufferList.Add(agent);
        }

    }

    private void Start()
    {
        StartCoroutine(Spawn());
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
                agent.name = $"Agent {index}";
                index++;

                agent.transform.position = new Vector3(
                    Random.Range(-zoneLength, zoneLength), 0, Random.Range(-zoneLength, zoneLength));
            }
            else
            {
                agent = agentBufferList[0];
                agentBufferList.Remove(agent);

                agent.SetActive(true);
            }

            currentAgentCount++;
        }
    }
}
