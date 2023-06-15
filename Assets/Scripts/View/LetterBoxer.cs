using UnityEngine;
namespace view
{
    public class LetterBoxer : MonoBehaviour// created to change size of camera view and viewport to adjust to different aspect ratios and resolutions
    {
        private Camera cam;// main camera
        private enum ReferenceMode { DesignedAspectRatio, OrginalResolution };
     
        [SerializeField] float x = 16;
        [SerializeField] float y = 9;
        [SerializeField] float width = 1920;
        [SerializeField] float height = 1080;
        [SerializeField] bool onAwake = true;
        [SerializeField] bool onUpdate = true;
        [SerializeField] ReferenceMode referenceMode;

        public void Awake()
        {
            // store reference to the camera
            cam = GetComponent<Camera>();

            // perform sizing if onAwake is set
            if (onAwake)
            {
                PerformSizing();
            }
        }

        public void Update()
        {
            // perform sizing if onUpdate is set
            if (onUpdate)
            {
                PerformSizing();
            }
        }

        private void PerformSizing()
        {
            // calc based on aspect ratio
            float targetRatio = x / y;

            // recalc if using resolution as reference
            if (referenceMode == ReferenceMode.OrginalResolution)
            {
                targetRatio = width / height;
            }

            // determine the game window's current aspect ratio
            float windowaspect = (float)Screen.width / Screen.height;

            // current viewport height should be scaled by this amount
            float scaleheight = windowaspect / targetRatio;
            //cache cam rect
            Rect rect = cam.rect;
            // if scaled height is less than current height, add letterbox
            if (scaleheight < 1.0f)
            {

                rect.width = 1.0f;
                rect.height = scaleheight;
                rect.x = 0;
                rect.y = (1.0f - scaleheight) / 2.0f;

                cam.rect = rect;
            }
            else // add pillarbox
            {
                float scalewidth = 1.0f / scaleheight;


                rect.width = scalewidth;
                rect.height = 1.0f;
                rect.x = (1.0f - scalewidth) / 2.0f;
                rect.y = 0;

                cam.rect = rect;
            }
        }

    }
}