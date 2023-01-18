using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Agent[] agents;
    public LayerMask layer_mask;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && agents.Length >= 1)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 100, layer_mask))
            {
                Instantiate(agents[0], hitInfo.point, Quaternion.identity);
            }
        }

        if (Input.GetMouseButtonDown(1) && agents.Length >= 2)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 100, layer_mask))
            {
                Instantiate(agents[1], hitInfo.point, Quaternion.identity);
            }
        }
    }
}
