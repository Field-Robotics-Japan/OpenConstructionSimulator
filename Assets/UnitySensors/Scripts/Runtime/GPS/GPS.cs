using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FRJ.Sensor
{
    public class GPS : MonoBehaviour
    {
        [SerializeField] private double _baseLatitude = 35.71020206575301;      // 基地局の緯度 [m]
        [SerializeField] private double _baseLongitude = 139.81070039691542;    // 基地局の経度 [m]
        [SerializeField] private double _baseAltitude = 3.0;                    // 基地局の標高（海抜高さ）[m]
        [SerializeField] private int    _satelliteNum = 8;                      // 使用衛星数
        [SerializeField] private double _HDOP = 1.0;                            // 水平精度低下率
        [SerializeField] private double _geoidHeight = 36.7071;                 // ジオイド高 [m]
        [SerializeField] private float _updateRate = 10f; 

        private double _latitude;   // 緯度 [m]
        private double _longitude;  // 経度 [m]
        private double _altitude;   // 標高 [m]
        // private string _gprmc;      // GPRMC message
        private string _gpgga;      // GPGGA message
        // private string _gpvtg;      // GPVTG message
        // private string _gphdt;      // GPHDT message

        public float updateRate{ get => this._updateRate; }
        // public string gprmc { get => this._gprmc; }
        public string gpgga { get => this._gpgga; }
        // public string gpvtg { get => this._gpvtg; }
        // public string gphdt { get => this._gphdt; }

        private GeoCoordinate _gc;
        private NMEASerializer _serializer;

        public void Init()
        {
            this._gc = new GeoCoordinate(this._baseLatitude, this._baseLongitude);
            this._serializer = new NMEASerializer();
            this._serializer.geoidLevel = (float)this._geoidHeight;
            this._serializer.satelliteNum = this._satelliteNum;
            this._serializer.hdop = (float)this._HDOP;
        }

        public void updateGPS()
        {
            (this._latitude, this._longitude) = this._gc.XZ2LatLon(this.transform.position.x, this.transform.position.z);
            this._altitude = this._baseAltitude + this.transform.position.y;

            this._serializer.latitude = (float)this._latitude;
            this._serializer.longitude = (float)this._longitude;
            this._serializer.altitude = (float)this._altitude;
            
            this._gpgga = this._serializer.GPGGA();
        }
    }
}
