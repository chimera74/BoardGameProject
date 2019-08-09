using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class RaycastingHelper
    {
        public static RaycastHit? RaycastCursorTo(Collider targetObjectCollider)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits;
            hits = Physics.RaycastAll(ray, 100.0F);

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider == targetObjectCollider)
                {
                    return hits[i];
                }
            }
            return null;
        }

        public static bool RaycastToDropSites(out RaycastHit hit, Vector3 pos)
        {
            int layerMask = 1 << 9; // DropSite layer
            Ray ray = new Ray(new Vector3(pos.x, 10.0f, pos.z), Vector3.down);
            return Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask);
        }
    }
}
