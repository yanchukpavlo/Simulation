using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineFreeLook))]
public class CinemachineController : MonoBehaviour
{
    
    void Start()
    {
        float zoneLength = AgentsSpawner.instance.ZoneLength;
        if (zoneLength == 0)
        {
            Debug.LogWarning("Zone length is set to zero.");
            return;
        }

        var cinemachine = GetComponent<CinemachineFreeLook>();

        cinemachine.m_Orbits[0].m_Height = zoneLength * 2;
        cinemachine.m_Orbits[0].m_Radius = zoneLength;

        cinemachine.m_Orbits[1].m_Height = 0;
        cinemachine.m_Orbits[1].m_Radius = zoneLength * 2;

        cinemachine.m_Orbits[2].m_Height = -zoneLength * 2;
        cinemachine.m_Orbits[2].m_Radius = zoneLength;
    }
}
