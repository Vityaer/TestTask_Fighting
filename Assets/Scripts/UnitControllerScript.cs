using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitControllerScript : MonoBehaviour{
	[SerializeField] private Rigidbody rb;
	[SerializeField] private Transform tr;
	[SerializeField] private Characteristics characts;
	[SerializeField] private HPViewScript HPController;
	public Vector3 Position{get => tr.position;}
	public bool IsAlive{get => characts.IsAlive;}
	public bool HaveOpponent{get => opponent != null;}
    void Start(){
    	GetComponent<Renderer>().material.color = new Color( Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f) );
    	characts.GetRandomStats();
    	HPController.SetMaxHP(characts.HP);
    }

    public void GetDamage(int hit){
    	characts.GetDamage(hit);
    	HPController.SetCurrentHP(characts.HP);
    	if(IsAlive == false) Death();
    } 
    private void Death(){
    	FightControllerSystem.Instance.RemoveUnit(this);
    	Destroy(gameObject);
    }
    [SerializeField] private UnitControllerScript opponent = null;
    public void SetOpponent(UnitControllerScript opponent){
    	this.opponent = opponent;
    	AttackOpponent();
    }
    Coroutine coroutineFight = null;
    public void FindOpponent(){
    	if(opponent == null){
	    	opponent = FightControllerSystem.Instance.GetNearUnit(this);
	    	if(opponent != null){
	    		opponent.SetOpponent(this);
	    		this.SetOpponent(opponent);
	    	}
    	}
    }
    private void AttackOpponent(){
    	if(coroutineFight != null){
			StopCoroutine(coroutineFight);
			coroutineFight = null;
		}
		coroutineFight = StartCoroutine(Fighting(opponent));
    }
    public bool show = false;
    IEnumerator Fighting(UnitControllerScript opponent){
    	Vector3 dir = (opponent.Position - this.Position);
    	dir.Normalize();
    	while(Distance(this.Position, opponent.Position) > characts.DistanceAttack){
    		rb.velocity = dir * characts.Speed;
    		if(show == true) Debug.Log("move...");
    		yield return null;
    	}
    	while(opponent.IsAlive){
    		if(show == true) Debug.Log("attack");
    		opponent.GetDamage(characts.Damage);
    		if(opponent.IsAlive == false) break;
    		yield return new WaitForSeconds(characts.TimeCooldownAttack);
    	}
		if(show == true) Debug.Log("I killed him");
		opponent = null;
		while(opponent == null && (FightControllerSystem.Instance.CurrentCountUnits > 1) ){
	    	FindOpponent();
	    	yield return new WaitForSeconds(0.05f);
		}


    }
    private float Distance(Vector3 a, Vector3 b){ return (a - b).sqrMagnitude; }
}