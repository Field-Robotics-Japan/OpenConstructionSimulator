using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


namespace FRJ.Sensor
{
    public class NMEASerializer
    {
        private float _latitude;
        private float _longitude;
        private float _altitude;
        private float _geoidLevel;
        private int _satelliteNum;
        private float _hdop;

        public float latitude { set{this._latitude = value;} }
        public float longitude { set{this._longitude = value;} }
        public float altitude { set{this._altitude = value;} }
        public float geoidLevel { set{this._geoidLevel = value;} }
        public int satelliteNum { set{this._satelliteNum = value;} }
        public float hdop { set{this._hdop = value;} }
        
        public string GPGGA()
        {
            string ret = "$GPGGA,";
            // Update UTC Time
            ret += DateTime.UtcNow.Hour.ToString("D02");
            ret += DateTime.UtcNow.Minute.ToString("D02");
            ret += DateTime.UtcNow.Second.ToString("D02");
            ret += ".";
            ret += DateTime.UtcNow.Millisecond.ToString("D3");
            ret += ",";

            // Update Latitude
            float latitude = this._latitude;
            if(latitude < 0)
                latitude = -latitude;
            ret += (latitude*1e2).ToString();
            ret += ",";
            if(this._latitude >= 0)
                ret += "N";
            else
                ret += "S";
            ret += ",";
            
            // Update Longitude
            float longitude = this._longitude;
            if(longitude < 0)
                longitude = -longitude;
            ret += (longitude*1e2).ToString();
            ret += ",";
            if(this._longitude >= 0)
                ret += "E";
            else
                ret += "W";
            ret += ",";
            
            // Update quality
            ret += "1";
            ret += ",";

            // Update number of satellites
            ret += this._satelliteNum.ToString("D02");
            ret += ",";

            // Update HDOP
            ret += this._hdop.ToString();
            ret += ",";

            // Update altitude
            ret += this._altitude.ToString();
            ret += ",";
            ret += "M";
            ret += ",";

            // Update geoid level
            ret += Math.Round(this._geoidLevel,1).ToString();
            ret += ",";
            ret += "M";
            ret += ",";

            // Update DGPS data (in this case, empty)
            ret += ",";
            ret += "0000";
            ret += ",";

            // Update checksum
            byte checksum = 0;
            for(int i=1; i<ret.Length; i++)
                checksum ^= (byte)ret[i];
            ret += "*";
            ret += checksum.ToString("X2");

            // Insert CR LF
            ret += "\r\n";
            
            return ret;
        }
    }
}
