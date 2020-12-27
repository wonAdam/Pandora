using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "New Key", fileName = "New Key", order = 1)]
public class Key : ScriptableObject
{
    [SerializeField] public KeyCode key;
}
