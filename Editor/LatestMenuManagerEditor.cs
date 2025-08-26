using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace CrossingLears
{
    [CustomEditor(typeof(LatestMenuManager))]
    public class LatestMenuManagerEditor : UnityEditor.Editor
    {
        private Dictionary<string, List<AutoLatestMenu>> menuBranches;
        private LatestMenuManager manager;

        private void OnEnable()
        {
            manager = (LatestMenuManager)target;
            RefreshMenuList();
        }

        AutoLatestMenu[] allMenus;
        private void RefreshMenuList()
        {
            menuBranches = new Dictionary<string, List<AutoLatestMenu>>();
            allMenus = FindObjectsByType<AutoLatestMenu>(FindObjectsInactive.Include, FindObjectsSortMode.None);

            foreach (AutoLatestMenu menu in allMenus)
            {
                string branch = menu.branch;
                if (!menuBranches.ContainsKey(branch))
                {
                    menuBranches[branch] = new List<AutoLatestMenu>();
                }
                menuBranches[branch].Add(menu);
            }
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            
            if (GUILayout.Button("Refresh List"))
            {
                RefreshMenuList();
            }

            if (GUILayout.Button("Close All Menu That Hides GUI"))
            {
                manager.CloseAllMenuThatHidesTheGUI();
            }

            foreach (KeyValuePair<string, List<AutoLatestMenu>> branch in menuBranches)
            {
                EditorGUILayout.LabelField(branch.Key, EditorStyles.boldLabel);
                foreach (var menu in branch.Value)
                {
                    GUI.color = menu.gameObject.activeInHierarchy ? Color.green : Color.red;
                    if (GUILayout.Button(menu.gameObject.name))
                    {
                        menu.Open();
                    }
                    GUI.color = Color.white;
                }
            }

            // Ensure the GUI updates every frame during play mode
            if (Application.isPlaying)
            {
                EditorApplication.QueuePlayerLoopUpdate();
                Repaint();
            }
        }
    }
}
