using controller;
using UnityEngine;
namespace model
{

    public class RobotController : MonoBehaviour//handles basic movement for the robot and collisions
    {
        [SerializeField] InputHandler inputHandler;
        [SerializeField] float speed = 3;
        [SerializeField] Transform robot;
        [SerializeField] float raycastSize = 1;
        [SerializeField] LayerMask collisionLayer;

        private void Update()
        {
            if (inputHandler != null)
            {
                int horInput = inputHandler.GetHorInput();
                //check collisions with walls
                if (CheckCollisionOnLeft() && horInput == -1)
                    return;
                if (CheckCollisionOnRight() && horInput == 1)
                    return;

                //move character
                robot.Translate(Vector2.right * horInput * speed * Time.deltaTime);
              
                //debugCollisions
                Debug.DrawRay(robot.position, Vector2.left * raycastSize, Color.red);
                Debug.DrawRay(robot.position, Vector2.right * raycastSize, Color.blue);
            }

        }
        internal void SetSpeed(float _speed)
        {
            speed = _speed;
        }
        private bool CheckCollisionOnRight()
        {
            
            return Physics2D.Raycast(robot.position, Vector2.right, raycastSize, collisionLayer);
        }
        private bool CheckCollisionOnLeft()
        {
           
            return Physics2D.Raycast(robot.position, Vector2.left, raycastSize, collisionLayer);
        }
    }
}
