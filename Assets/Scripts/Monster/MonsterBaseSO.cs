using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MonsterBaseSO : ScriptableObject
{
    //���O�A�����A�摜�A�푰�A(�^�C�v)���̃}�X�^�[�f�[�^
    [SerializeField] string monstername;
    [TextArea]
    [SerializeField] string description;

    [SerializeField] Sprite battleimage;

    [SerializeField] MonsterType type;

    //�X�e�[�^�X
    [SerializeField] int maxHP;
    [SerializeField] int maxMP;
    [SerializeField] int attack;
    [SerializeField] int defence;
    [SerializeField] int intel;
    [SerializeField] int res;
    [SerializeField] int speed;

    //���t�@�C������Q�Ƃł���悤�ɂ���
    public int MaxHP { get => maxHP; }
    public int MaxMP { get => maxMP; }
    public int Attack { get => attack;}
    public int Defence { get => defence; }
    public int Intel { get => intel;}
    public int Res { get => res; }
    public int Speed { get => speed; }
}

public enum MonsterType
{
    animal,
    plant,
    machine,
    holy,
    dark
}
