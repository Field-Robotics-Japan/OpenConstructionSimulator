using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Ocs.GameSystem
{
    [System.Serializable]
    public class Vehicle
    {
        public bool valid = false;
        public Vector3 position;
        public Vector3 rotation;
    }

    [System.Serializable]
    public class StageSetting
    {
        public enum TimeZone{
            Noon,
            Evening,
            Night,
        }

        public enum Weather{
            Sun,
            Rain,
        }

        public string stage = "DemoEnv";
        [SerializeField]
        public Vehicle WheelLoader;
        public Vehicle BackHoe;
        public Vehicle Truck;

        public TimeZone timeZone = TimeZone.Noon;
        public Weather weather = Weather.Sun;

    }

    public class GameSetting : MonoBehaviour
    {
        public static StageSetting setting = new StageSetting();

        [SerializeField] private string filePath = "/StageSetting.json";

        string dataPath;

        private void Awake()
        {
            //initial setting
            //setting.WheelLoader.valid = true;
            
        

            //初めに保存先を計算する　Application.dataPathで今開いているUnityプロジェクトのAssetsフォルダ直下を指定して、後ろに保存名を書く
            dataPath = Application.dataPath + filePath;
            setting = LoadSetting();

            setting.WheelLoader.rotation.y = 90.0f;
            setting.BackHoe.position.z = -7.5f;
            setting.BackHoe.rotation.y = 90.0f;
            setting.Truck.position.z = -15.0f;
            setting.Truck.rotation.y = 90.0f;
        }

        void Start()
        {

        }

        public void SaveSetting()
        {
            string jsonstr = JsonUtility.ToJson(setting);//受け取ったPlayerDataをJSONに変換
            StreamWriter writer = new StreamWriter(dataPath, false);//初めに指定したデータの保存先を開く
            writer.WriteLine(jsonstr);//JSONデータを書き込み
            writer.Flush();//バッファをクリアする
            writer.Close();//ファイルをクローズする
        }

        public StageSetting LoadSetting()
        {
            string dataStr;
            //error handling
            try{
                StreamReader reader = new StreamReader(dataPath); //受け取ったパスのファイルを読み込む
                dataStr = reader.ReadToEnd();//ファイルの中身をすべて読み込む
                reader.Close();//ファイルを閉じる
            }catch{
                return new StageSetting();
            }
            
            return JsonUtility.FromJson<StageSetting>(dataStr);//読み込んだJSONファイルをclassの型に変換して返す
        }

        public void setField(string fieldName)
        {
            setting.stage = fieldName;
            SaveSetting();
        }
    }
}
