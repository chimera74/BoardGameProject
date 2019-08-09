﻿using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Debug.Editor
{
    public class DebugViewer : EditorWindow
    {

        DropSite ds = null;
        Vector3 tablePos = Vector3.zero;

        [MenuItem("Window/DebugView")]
        public static void ShowWindow()
        {
            GetWindow<DebugViewer>("DebugView");
        }



        void OnGUI()
        { 

            PrintCoords(tablePos);
            PrintDropSite(ds);

        }

        public void PrintCoords(Vector3 vect)
        {
            GUILayout.Label("Table coordinates:", EditorStyles.boldLabel);
            GUILayout.Label(vect.ToString());
        }

        public void PrintDropSite(DropSite ds)
        {
            GUILayout.Label("DropSite:", EditorStyles.boldLabel);
            if (ds != null)
                GUILayout.Label(ds.name);
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

            ds = null;
            tablePos = Vector3.zero;


                //                Ray ray = Camera.current.ScreenPointToRay(Event.current.mousePosition);


                //            Vector3 mousePos = Event.current.mousePosition;
                //            mousePos.y = SceneView.lastActiveSceneView.camera.pixelHeight - mousePos.y;
                //            Ray ray = SceneView.lastActiveSceneView.camera.ScreenPointToRay(mousePos);

                Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);

                Collider tableCollider = FindObjectOfType<Table>().GetComponent<Collider>();
                if (tableCollider.Raycast(ray, out var hit, Mathf.Infinity))
                {
                    tablePos = hit.point;
                    if (RaycastingHelper.RaycastToDropSites(out var hit2, tablePos))
                    {
                        ds = hit2.transform.GetComponent<DropSite>();

                    }
                }

           
        }
    }
}