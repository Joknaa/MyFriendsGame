using System;
using Reality;
using UnityEngine;

namespace DefaultNamespace {
    public class LevelController : MonoBehaviour {
        public GameObject level0Part1;
        public GameObject level0Part2;
        public GameObject level6Part1;
        public GameObject level6Part2;
        
        public void Start() {
            level6Part1.SetActive(true);
            level0Part1.SetActive(true);
            
            level6Part2.SetActive(false);
            level0Part2.SetActive(false);
        }


        public void StartSecondHalfLevels() {
            level6Part1.SetActive(false);
            level0Part1.SetActive(false);
            
            level6Part2.SetActive(true);
            level0Part2.SetActive(true);
        }
    }
}