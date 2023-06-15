using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using view;
namespace model
{
    public class ShootController : MonoBehaviour// shootController handles all logic that involves shooting the lasers
    {
        [SerializeField] LeftArrowButton leftArrowButton;
        [SerializeField] RightArrowButton rightArrowButton;
        [SerializeField] RobotAnimatorUpdater robotAnimatorUpdater;
        [SerializeField] BallController ballController;
        [SerializeField] GameManager gameManager;
        [SerializeField] float ShootCD = 0.5f;
        private float lastTimeShot;
        public ObjectPool laserPool;
        public UnityEvent shootEvent;
        private void Start()
        {
            shootEvent.AddListener(robotAnimatorUpdater.PlayShooting);
            foreach (var laser in laserPool.Pool)
            {
                LaserHandler currentLaserHandler = laser.GetComponent<LaserHandler>();

                //on laser hit ball call the "Split ball" method
                currentLaserHandler.onHitBall.AddListener(ballController.SplitBall);

                //on laser hit ball call update score
                currentLaserHandler.onHitBall.AddListener(gameManager.UpdateScoreOnSplitBall);

                // on laser hit ball return laser to object pool
                currentLaserHandler.onHitBall.AddListener(ReturnLaser);
            }
        }
        private void Update()
        {
            if ((Input.GetMouseButtonDown(0) || Input.touchCount > 0) && !IsPointerOverUIObject() && CheckShootCooldown())
            {

                GameObject current = laserPool.GetFromPool();
                current.transform.position = robotAnimatorUpdater.transform.position + Vector3.up;
                shootEvent?.Invoke();
                lastTimeShot = Time.time;
                SoundManager.Play(SoundManager.Sound.playerShoot);

                Debug.Log("Shoot");
            }
        }

        private bool CheckShootCooldown()//Check if shot is on cooldown
        {
            return Time.time - lastTimeShot > ShootCD;
        }

        public void ReturnLaser(LaserHandler laserHandler, Rigidbody2D ballHit)
        {
            laserPool.ReturnToPool(laserHandler.gameObject);
        }

        private bool IsPointerOverUIObject()//check that touch is not on ui object ( so you don't shoot when moving)
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            foreach (var result in results)
            {
                if (result.gameObject == leftArrowButton.gameObject || result.gameObject == rightArrowButton.gameObject)
                {
                    return true; // Pointer is over the left or right arrow button
                }
            }
            return false; // Pointer is not over the left or right arrow button
        }
    }
}