using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knife.Portal
{
    public class PortalControlByKey : MonoBehaviour
    {
     
        [SerializeField] private PortalTransition[] portalTransitions;
        private void Start()
        {
            foreach (var p in portalTransitions)
            {
                p.OpenPortal();
            }
        }
     
    }
}