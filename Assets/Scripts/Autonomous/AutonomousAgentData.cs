using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AutonomousAgentData", menuName = "AI/AutonomousAgentData")]
public class AutonomousAgentData : ScriptableObject
{
    [Header("Wander")]
    [Range(0, 20)] public float wander_distance = 4;
    [Range(0, 20)] public float wander_radius = 4;
    [Range(0, 20)] public float wander_displacement = 4;
    [Header("Flocking")]
    [Range(0, 20)] public float flock_radius = 1;
    [Header("Weights")]
    [Range(0, 5)] public float seek_weight = 1;
    [Range(0, 5)] public float flee_weight = 1;
    [Range(0, 5)] public float flock_weight = 1;
    [Range(0, 5)] public float separate_weight = 1;
    [Range(0, 5)] public float align_weight = 1;
    [Range(0, 5)] public float obstacle_weight = 1;
}