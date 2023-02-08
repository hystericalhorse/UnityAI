using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavAgentController : MonoBehaviour
{
    [System.Serializable]
    public struct NavLocation
    {
        public string name;
		public Node Node;
    }

    [SerializeField] NavAgent navAgent;
    [SerializeField] NavLocation[] navLocations;

    private string currentLocation;

    void Start()
    {
		for (int i = 0; i < navLocations.Length; i++)
		{
            navLocations[i].Node.name = navLocations[i].name;
		}
	}

	private void OnGUI()
	{
        for (int i = 0; i < navLocations.Length; i++)
        {
            if (GUI.Button(new Rect(10, 10 + (i * 25), 100, 20), navLocations[i].name))
            {
                navAgent.SetDestination(navLocations[i].Node);
            }

		}
	}
}
