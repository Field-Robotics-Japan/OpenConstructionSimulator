using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ocs.GameSystem
{

public class VehicleSelect : MonoBehaviour
{
    const int maxVehicle = 10;
    private List<Ocs.Vehicle.Vehicle> vehicles = new List<Ocs.Vehicle.Vehicle>(maxVehicle);
    //private Ocs.Vehicle.Vehicle[] vehicles = new Ocs.Vehicle.Vehicle[10];
    private int vehicle_num;
    private int changeCount = 0;
    private bool initialized = false;

    //input
    private Ocs.Input.InputActions _input;
    public Ocs.Input.InputActions Input { get => _input; }

    private void Awake() => this._input = new Ocs.Input.InputActions();
    private void OnEnable() => this._input.Enable();
    private void OnDisable() => this._input.Disable();
    private void OnDestroy() => this._input.Dispose();

    // Start is called before the first frame update
    void Start()
    {
        //button control
        this._input.Player.Select.started += context =>
        {
            onVehicleChange();
        };
    }

    // Update is called once per frame
    void Update()
    {
        getVehicle();
        if(!initialized){
            vehicles[0].setOwner(true);
        }
        initialized = true;

    }

    private void getVehicle()
    {
        vehicles.Clear();

        var vehicle_array = (Ocs.Vehicle.Vehicle[])UnityEngine.Object.FindObjectsOfType(typeof(Ocs.Vehicle.Vehicle));
        vehicle_num = vehicle_array.Length;
        for(int i = 0; i < vehicle_array.Length; i++){
            vehicles.Add(vehicle_array[i]);
        }
        
        Debug.Log("number of vehicle is " + vehicle_num);

        /*int count = 0;
        //get vehicle from hierarchy
        foreach(Ocs.Vehicle.Vehicle vehicle in UnityEngine.Object.FindObjectsOfType(typeof(Ocs.Vehicle.Vehicle)))
        {
            Debug.Log(vehicle);
            vehicles[count] = vehicle;
            count++;
        }
        vehicle_num = count;*/
    }

    public void onVehicleChange()
    {
        if(!(vehicle_num==0)){
            vehicles[changeCount%vehicle_num].setOwner(false);
            changeCount++;
            vehicles[changeCount%vehicle_num].setOwner(true);
        }
        
    }
}

}//end of namespace Ocs.GameSystem


