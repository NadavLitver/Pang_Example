using UnityEngine;
namespace controller
{

    public class InputHandler : MonoBehaviour// stores the information about player horizontal movement, if there was more complex movement such as jumping the input would also be here
    {
        private int horInput;
        public bool isLeftPressed;
        public bool isRightPressed;

        private void Update()
        {
            SetHorInput();
        }
        public void SetHorInput()
        {
            if (isLeftPressed)
            {
                horInput = -1;
            }
            else if (isRightPressed)
            {
                horInput = 1;
            }
            else
            {
                horInput = 0;
            }
#if UNITY_EDITOR
            horInput = (int)Input.GetAxisRaw("Horizontal");
#endif
        }
        public int GetHorInput() { return horInput; }
    }
}
