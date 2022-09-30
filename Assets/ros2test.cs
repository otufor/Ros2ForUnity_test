using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ROS2;

public class ros2test : MonoBehaviour
{
    private ROS2Node ros2Node;
    // Start is called before the first frame update
    void Start()
    {
        // Example method of getting component, if ROS2UnityComponent lives in different GameObject, just use different get component methods.
        ROS2UnityComponent ros2Unity = GetComponent<ROS2UnityComponent>();
        if (ros2Unity.Ok())
        {
            ros2Node = ros2Unity.CreateNode("ROS2UnityListenerNode");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
