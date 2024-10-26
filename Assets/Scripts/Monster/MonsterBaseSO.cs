using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MonsterBaseSO : ScriptableObject
{
    //名前、説明、画像、種族、(タイプ)等のマスターデータ
    [SerializeField] string monstername;
    [TextArea]
    [SerializeField] string description;

    [SerializeField] Sprite battleimage;

    [SerializeField] MonsterType type;

    //ステータス
    [SerializeField] int maxHP;
    [SerializeField] int maxMP;
    [SerializeField] int attack;
    [SerializeField] int defence;
    [SerializeField] int intel;
    [SerializeField] int res;
    [SerializeField] int speed;

    //他ファイルから参照できるようにする
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
