using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace activate
{

    public class SetActiveGameLengthPanel : MonoBehaviour
    {

        public GameObject objectToActivate;
        // Use this for initialization
        public void setActivePanel(bool active)
        {
            objectToActivate.SetActive(active);
        }

    }
}
