using UnityEngine;

namespace YourNamespace
{
    [ExecuteInEditMode]
    public class scalepotrait : MonoBehaviour
    {
        [SerializeField] private float baseRatio = 0.75f;
        [SerializeField] private float additionalScale = 1;
        [Range(0, 1)][SerializeField] private float heightWidthRatio = 1;

        private float currRatio;

        void Start()
        {
            SetScale();
        }

        void Update()
        {
            SetScale();
        }

        void SetScale()
        {
            // Determine the actual screen width and height based on orientation
            float screenWidth = Screen.width;
            float screenHeight = Screen.height;

            // Check if the current orientation is portrait
            bool isPortrait = screenHeight > screenWidth;

            // Calculate the aspect ratio based on the current orientation
            if (isPortrait)
            {
                currRatio = screenWidth / screenHeight; // Portrait
            }
            else
            {
                currRatio = screenHeight / screenWidth; // Landscape
            }

            // Clamp values to ensure they are within valid ranges
            heightWidthRatio = Mathf.Clamp01(heightWidthRatio);
            additionalScale = Mathf.Clamp01(additionalScale);
            baseRatio = Mathf.Clamp(baseRatio, 0.3f, 2.5f);

            // Calculate scale based on current and base aspect ratios
            float scale = currRatio / baseRatio;

            // Apply additional scale and lerp based on heightWidthRatio
            scale = Mathf.Lerp(1f, scale, heightWidthRatio) * additionalScale;

            // Set the local scale of the gameObject
            transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
