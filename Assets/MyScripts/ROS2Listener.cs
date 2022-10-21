using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ROS2
{
    public class ROS2Listener : MonoBehaviour
    {
        private ROS2UnityComponent ros2Unity;
        private ROS2Node ros2Node;
        private ISubscription<std_msgs.msg.Float32MultiArray> chatter_sub;
        private ISubscription<std_msgs.msg.Int32> chatter_sub_Step;
        public GameObject TargetObject;
        public GameObject StepText;
        public GameObject PositionText;
        private Vector3 TargetPosition;
        private int Step;
        void Start()
        {
            ros2Unity = GetComponent<ROS2UnityComponent>();
            TargetPosition = TargetObject.transform.position;
        }

        void Update()
        {
            if (ros2Node == null && ros2Unity.Ok())
            {
                ros2Node = ros2Unity.CreateNode("ROS2UnityListenerNode_Cube");
                chatter_sub = ros2Node.CreateSubscription<std_msgs.msg.Float32MultiArray>(
                    "CubePosition", msg =>
                    {
                        Debug.Log($"Unity listener heard: [CubePosition:({msg.Data[0]}, {msg.Data[1]}, {msg.Data[2]})]");
                        TargetPosition = new Vector3(msg.Data[0], msg.Data[1], msg.Data[2]);
                    }
                );
                chatter_sub_Step = ros2Node.CreateSubscription<std_msgs.msg.Int32>(
                    "Step", msg =>
                    {
                        Debug.Log($"Unity listener heard: [Step:{msg.Data}]");
                        Step = msg.Data;
                    }
                );
            }

            TargetObject.transform.position = TargetPosition;
            var steptext = StepText.GetComponent<Text>();
            var positiontext = PositionText.GetComponent<Text>();
            steptext.text = $"Step: {Step}";
            positiontext.text = $"Position: {TargetPosition}";
        }
    }
}
