using UnityEngine;
using model;
using Zenject;

namespace controller
{

    public class LocoMotion : ILocomotion, ITickable//handles basic movement for the robot and collisions
    {
        //controllers
        [Inject] IInputHandler inputHandler;
        //data
        [Inject] IPlayerConfig playerData;
        [Inject] Transform robot;
        private float dynamicSpeed;//didnt want to use SO speed because it changes during gameplay but doesnt save we resetting scene

        public LocoMotion(IInputHandler inputHandler, IPlayerConfig playerData, Transform robot)
        {
            this.inputHandler = inputHandler;
            this.playerData = playerData;
            this.robot = robot;
            this.dynamicSpeed = playerData.MoveSpeed;
        }
        public void SetSpeed(float _speed)//get new speed
        {
            dynamicSpeed = _speed;
        }
        bool CheckCollisionOnRight()
        {
            return Physics2D.Raycast(robot.position, Vector2.right, playerData.Width, playerData.CollisionLayer);
        }
        bool CheckCollisionOnLeft()
        {
           
            return Physics2D.Raycast(robot.position, Vector2.left, playerData.Width, playerData.CollisionLayer);
        }

        public void Tick()
        {

            if (inputHandler != null)
            {
                int horInput = inputHandler.GetHorInput();//get input from manager

                //check collisions with walls
                if (CheckCollisionOnLeft() && horInput == -1)
                    return;
                if (CheckCollisionOnRight() && horInput == 1)
                    return;

                //move character
                robot.Translate(Vector2.right * horInput * dynamicSpeed * Time.deltaTime);

                //debugCollisions
                Debug.DrawRay(robot.position, Vector2.left * playerData.Width, Color.red);
                Debug.DrawRay(robot.position, Vector2.right * playerData.Width, Color.blue);
            }
        }
    }
}
