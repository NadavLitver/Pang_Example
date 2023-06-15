using UnityEngine;
using model;
namespace controller
{

    public class LocoMotion : MonoBehaviour//handles basic movement for the robot and collisions
    {
        [SerializeField] InputHandler inputHandler;
        [SerializeField] Transform robot;
        [SerializeField] PlayerData playerData;
        private float dynamicSpeed;//didnt want to use SO speed because it changes during gameplay but doesnt save we resetting scene

        private void Start()
        {
            dynamicSpeed = playerData.Speed;
        }
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
                robot.Translate(Vector2.right * horInput * dynamicSpeed * Time.deltaTime);
              
                //debugCollisions
                Debug.DrawRay(robot.position, Vector2.left * playerData.width, Color.red);
                Debug.DrawRay(robot.position, Vector2.right * playerData.width, Color.blue);
            }

        }
        internal void SetSpeed(float _speed)
        {
            dynamicSpeed = _speed;
        }
        private bool CheckCollisionOnRight()
        {
            
            return Physics2D.Raycast(robot.position, Vector2.right, playerData.width, playerData.collisionMask);
        }
        private bool CheckCollisionOnLeft()
        {
           
            return Physics2D.Raycast(robot.position, Vector2.left, playerData.width, playerData.collisionMask);
        }
    }
}
