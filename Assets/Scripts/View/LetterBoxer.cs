using UnityEngine;
namespace view
{
    public class LetterBoxer : ILetterBoxer // created to change size of camera view and viewport to adjust to different aspect ratios and resolutions
    {
        private Camera cam;// main camera
        private enum ReferenceMode { DesignedAspectRatio, OrginalResolution };

        float x = 20;
        float y = 9;
        float width = 2400;
        float height = 1080;

        ReferenceMode referenceMode;
        public LetterBoxer()
        {
            // store reference to the camera
            cam = Camera.main;
            //set reference mode
            referenceMode = ReferenceMode.OrginalResolution;
            // perform sizing
            PerformSizing();

        }



        public void PerformSizing()
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