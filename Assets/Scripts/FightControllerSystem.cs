using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightControllerSystem : MonoBehaviour{
	public int startCountUnits = 10;
	[SerializeField] private GameObject prefabUnit;
	[SerializeField] private TerritoryControllerScript territoryController;
	List<UnitControllerScript> units = new List<UnitControllerScript>();
	public int CurrentCountUnits{get => units.Count;}
	private void AddUnit(UnitControllerScript newUnit){
		units.Add(newUnit);
	}
	public void RemoveUnit(UnitControllerScript unitRemoved){
		units.Remove(unitRemoved); 
	}
	public UnitControllerScript GetNearUnit(UnitControllerScript master){
		float minDist = Mathf.Infinity, currentDist;
		UnitControllerScript result = null;
		foreach(UnitControllerScript unit in units){
			if(unit != master){
				if(unit.HaveOpponent == false){
					currentDist = (unit.Position - master.Position).sqrMagnitude;
					if(currentDist < minDist){
						minDist = currentDist;
						result = unit;
					}
				}
			}
		}
		return result;
	}
	void Awake(){
		instance = this;
	}
	void Start(){
		UnitControllerScript unit;
		for(int i = 0; i < startCountUnits; i++){
			unit = Instantiate(prefabUnit, territoryController.GetRandomPosition(), Quaternion.identity).GetComponent<UnitControllerScript>();
			AddUnit(unit);
		}
		for(int i = 0; i < startCountUnits; i++){
			units[i].FindOpponent();
		}
	}
	private static FightControllerSystem instance; 
	public static FightControllerSystem Instance{get => instance;} 
}
