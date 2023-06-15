using System.Collections.Generic;
using UnityEngine;
namespace model
{
    public class BallPoolHandler : MonoBehaviour
    {
        [SerializeField] GameObject ballPrefabRef;
        [SerializeField] int ballPoolSize = 20;
        List<GameObject> ballPool;
        private void Awake()
        {
            ballPool = new List<GameObject>();
            PopulatePool();
        }
        private void PopulatePool()
        {
            //Populate the ball pool list by the ball pool size 
            for (int i = 0; i < ballPoolSize; i++)
            {
                GameObject currentBall = Instantiate(ballPrefabRef);
                currentBall.name = "Ball" + i;
                ballPool.Add(currentBall);
                currentBall.SetActive(false);
            }
        }
        public GameObject GetBallFromPool()
        {
            // Find an inactive ball in the pool and return it
            for (int i = 0; i < ballPool.Count; i++)
            {
                if (!ballPool[i].activeInHierarchy)
                {
                    ballPool[i].SetActive(true);
                    return ballPool[i];
                }
            }

            // If no inactive ball is available, create a new one and return it
            GameObject currentBall = Instantiate(ballPrefabRef);
            currentBall.name = "ExtraBall";
            ballPool.Add(currentBall);
            currentBall.SetActive(false);
            return currentBall;
        }
        public void ReturnBallToPool(GameObject ball)
        {
            // Deactivate the ball and return it to the pool
            ball.SetActive(false);
        }
       
    }
}