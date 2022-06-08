using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ocs.Vehicle.DriveTrain;

namespace Ocs.Vehicle
{
    public class Crawler : Vehicle
    {
        [Header("- DriveTrain Setting -")]
        [SerializeField] private CrawlerUnit _leftCrawler;
        [SerializeField] private CrawlerUnit _rightCrawler;
        
        public float LeftCrawlerInput { get; set; }
        public bool LeftReverse { get; set; }
        public float RightCrawlerInput { get; set; }
        public bool RightReverse { get; set; }

        protected virtual void Update()
        {
            vehicleStateUpdate();

            if(!LeftReverse) this._leftCrawler.Drive(LeftCrawlerInput * 100);
            else this._leftCrawler.Drive(-LeftCrawlerInput * 100);
            if (!RightReverse) this._rightCrawler.Drive(RightCrawlerInput * 100);
            else this._rightCrawler.Drive(-RightCrawlerInput * 100);
        }
    }
}
