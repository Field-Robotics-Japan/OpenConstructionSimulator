using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayer : MonoBehaviour
{
    [System.Serializable]
    public class VehicleInfo
    {
        public GameObject vehicle;
        public bool isUsed;
    }

    [SerializeField] private GameObject _camera;
    [SerializeField] private GameObject _viewPoint;
    [SerializeField] private GameObject _selectObj;
    [SerializeField] private List<VehicleInfo> _vehicleList;

    private int _vehicleNum = 0;
    private bool _isMenu = true;
    private Ocs.Input.InputActions _input;
    public Ocs.Input.InputActions Input { get => _input; }
    public GameObject Camera { set => _camera = value; }

    private void Awake() => this._input = new Ocs.Input.InputActions();
    private void OnEnable() => this._input.Enable();
    private void OnDisable() => this._input.Disable();
    private void OnDestroy() => this._input.Dispose();

    // Start is called before the first frame update
    void Start()
    {
        this._camera.transform.parent = this._viewPoint.transform;
        SetAsChild(this._selectObj, this._vehicleList[this._vehicleNum].vehicle.gameObject.transform);
        var tmp = this._vehicleList[this._vehicleNum].vehicle.transform.Find("Driver").transform;
        this._selectObj.transform.localPosition = tmp.localPosition + new Vector3(0, 4, 0);
        this._selectObj.transform.rotation = Quaternion.Euler(90.0f, 0.0f, 90.0f);

        // Callback
        this._input.Player.DPad.started += context =>
        {
            if (!this._isMenu) return;

            //this._camera.transform.parent = transform;

            this._vehicleNum += (int)this._input.Player.DPad.ReadValue<Vector2>()[0];
            this._vehicleNum = System.Math.Min(this._vehicleNum, this._vehicleList.Count - 1);
            this._vehicleNum = System.Math.Max(this._vehicleNum, 0);

            SetAsChild(this._selectObj, this._vehicleList[this._vehicleNum].vehicle.gameObject.transform);
            var tmp = this._vehicleList[this._vehicleNum].vehicle.transform.Find("Driver").transform;
            this._selectObj.transform.localPosition = tmp.localPosition + new Vector3(0, 4, 0);
            this._selectObj.transform.rotation = Quaternion.Euler(90.0f, 0.0f, 90.0f);
        };

        this._input.Player.Menu.started += context =>
        {
            if (this._isMenu) return;

            this._selectObj.SetActive(true);
            
            SetAsChild(this._selectObj, this._vehicleList[this._vehicleNum].vehicle.gameObject.transform);
            var tmp = this._vehicleList[this._vehicleNum].vehicle.transform.Find("Driver").transform;
            this._selectObj.transform.localPosition = tmp.localPosition + new Vector3(0, 4, 0);
            this._selectObj.transform.rotation = Quaternion.Euler(90.0f, 0.0f, 90.0f);

            this._camera.transform.parent.gameObject.SetActive(false);
            this._camera.transform.parent = this._viewPoint.transform;

            this._isMenu = true;
        };

        this._input.Player.Select.started += context =>
        {
            this._selectObj.SetActive(false);
            this._vehicleList[this._vehicleNum].vehicle.transform.Find("Driver").gameObject.SetActive(true);

            this._camera.transform.parent = this._vehicleList[this._vehicleNum].vehicle.transform.Find("Driver").transform;
            
            this._isMenu = false;
        };
    }

    void SetAsChild(GameObject obj, Transform transform, bool lerp = false)
    {
        obj.transform.parent = transform;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;
    }
}
