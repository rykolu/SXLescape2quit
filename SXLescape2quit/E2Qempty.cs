using UnityEngine;

namespace SXLescape2quit
{
    public class E2Qempty : MonoBehaviour
    {
        void FixedUpdate()  //using FixedUpdate just to make exiting the game a bit cleaner
        {
            if (Input.GetKey("escape"))
            {
                Application.Quit();
            }
        }
    }
}
