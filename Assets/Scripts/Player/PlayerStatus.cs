using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PlayerStatus", menuName = "Player status")] 
public class PlayerStatus : ScriptableObject
{
    //���x���A���O�A�X�e�[�^�X
    [Header("Config")]
    public new string name;
    public int Level;

    [Header("Health")]
    public float Health;
    public float MaxHealth;
}
