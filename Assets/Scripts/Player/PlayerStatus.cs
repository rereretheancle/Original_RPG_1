using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PlayerStatus", menuName = "Player status")] 
public class PlayerStatus : ScriptableObject
{
    //レベル、名前、ステータス
    [Header("Config")]
    public new string name;
    public int Level;

    [Header("Health")]
    public float Health;
    public float MaxHealth;
}
