using Assets.Scripts.DropZones;
using Assets.Scripts.Presentation;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Debug.Editor
{
    public class DebugViewer : EditorWindow
    {
        DropZone dz = null;
        Vector3 tablePos = Vector3.zero;

        [MenuItem("Window/DebugView")]
        public static void ShowWindow()
        {
            GetWindow<DebugViewer>("DebugView");
        }


        void OnGUI()
        {
            PrintCoords(tablePos);
            PrintDropZone(dz);
        }

        public void PrintCoords(Vector3 vect)
        {
            GUILayout.Label("Table coordinates:", EditorStyles.boldLabel);
            GUILayout.Label(vect.ToString());
        }

        public void PrintDropZone(DropZone dz)
        {
            GUILayout.Label("DropZone:", EditorStyles.boldLabel);
            if (dz != null)
                GUILayout.Label(dz.name);
            else
                GUILayout.Label("None");
        }

        private void OnEnable()
        {
            //register that OnSceneGUI of this window 
            //should be called when drawing the scene.
            SceneView.duringSceneGui += OnSceneGUI;
        }

        private void OnDisable()
        {
            //cleanup: when the window is gone
            //we don't want to try and call it's function!
            SceneView.duringSceneGui -= OnSceneGUI;
        }

        public void OnInspectorUpdate()
        {
            // This will only get called 10 times per second.
            Repaint();
        }

        private void OnSceneGUI(SceneView sv)
        {

            var tbl = FindObjectOfType<Table>();
            if (tbl == null)
                return;

            dz = null;
            tablePos = Vector3.zero;

            Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);

            Collider tableCollider = tbl.GetComponent<Collider>();

            if (tableCollider.Raycast(ray, out var hit, Mathf.Infinity))
            {
                tablePos = hit.point;
                if (RaycastingHelper.RaycastToTableDropZones(out var hit2, tablePos))
                {
                    dz = hit2.transform.GetComponent<DropZone>();
                }
            }
        }
    }
}