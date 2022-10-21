using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROS2
{
    public class ROS2Talker : MonoBehaviour
    {
        private ROS2UnityComponent ros2Unity;
        private ROS2Node ros2Node;
        private IPublisher<std_msgs.msg.Float32MultiArray> chatter_pub;
        private int i;
        private IPublisher<std_msgs.msg.Int32> chatter_pub_Step;

        void Start()
        {
            ros2Unity = GetComponent<ROS2UnityComponent>();
        }

        void Update()
        {
            if (ros2Unity.Ok())
            {
                if (ros2Node == null)
                {
                    ros2Node = ros2Unity.CreateNode("ROS2UnityTalkerNode_Cube");
                    chatter_pub = ros2Node.CreatePublisher<std_msgs.msg.Float32MultiArray>("CubePosition");
                    chatter_pub_Step = ros2Node.CreatePublisher<std_msgs.msg.Int32>("Step");
                }

                i++;

                std_msgs.msg.Float32MultiArray CubePosition = new std_msgs.msg.Float32MultiArray();
                CubePosition.Data = new float[] { i * 0.001f, i * 0.001f, i * 0.001f };

                chatter_pub.Publish(CubePosition);

                std_msgs.msg.Int32 step = new std_msgs.msg.Int32();
                step.Data = i;
                chatter_pub_Step.Publish(step);
            }
        }
    }
}