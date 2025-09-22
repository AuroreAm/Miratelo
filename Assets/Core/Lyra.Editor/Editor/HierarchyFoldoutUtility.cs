using UnityEditor;
using UnityEngine;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

public static class HierarchyFoldoutUtility
{
    public static bool IsExpanded ( GameObject gameObject )
    {
        EditorApplication.ExecuteMenuItem ("Window/General/Hierarchy");
        object sceneHierarchy =  typeof(EditorWindow).Assembly.GetType("UnityEditor.SceneHierarchyWindow").GetProperty("sceneHierarchy").GetValue( EditorWindow.focusedWindow );
        var GetExpandedGameObjectMethod = sceneHierarchy.GetType ().GetMethod("GetExpandedGameObjects", BindingFlags.Public | BindingFlags.Instance);
        List <GameObject> ExpandedGameObjects = (List <GameObject>) GetExpandedGameObjectMethod.Invoke( sceneHierarchy, null );
        return ExpandedGameObjects.Contains ( gameObject );
    }
    // NOTE: this class relies on reflection, might break for future Unity versions
}