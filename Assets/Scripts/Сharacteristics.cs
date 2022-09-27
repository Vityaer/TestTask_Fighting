using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Characteristics{
	[SerializeField] private int damage, hp;
	[SerializeField] private float speed, distanceAttack = 1f, timeCooldownAttack = 1f;
	public int Damage{get => damage;}
	public int HP{get => hp;}
	public float Speed{get => speed;}
	public bool IsAlive{get => (HP > 0);}
	public float DistanceAttack{get => distanceAttack;}
	public float TimeCooldownAttack{get => timeCooldownAttack;}
	public Characteristics(int damage, int hp, float speed){
		this.damage = damage;
		this.hp     = hp;
		this.speed  = speed;
	}
	public void GetDamage(int hit){
		if(hp > hit){
			hp -= hit;
		}else{
			hp = 0;
		}
	}
	public void GetRandomStats(){
		timeCooldownAttack = UnityEngine.Random.Range(1.4f, 1.6f);
		hp = UnityEngine.Random.Range(20, 30);
		damage = UnityEngine.Random.Range(2, 4);
	}
}