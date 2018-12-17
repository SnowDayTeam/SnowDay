//////// Custom Tree-IGenericMenu Example ////////
/*
        To add your own module, inherit the slot class (HierarchyExtensions.CustomModule_Slot1 / 2 / 3) anywhere in your code.
*/

#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;


/////////////////////////////////////////////////////MENU ITEM TEMPLATE///////////////////////////////////////////////////////////////////////////////
/*
    class MyModule : HierarchyExtensions.CustomModule_Slot1
    {
        public override string NameOfModule { get { return "MyModule"; } }
    
        // In this method, you can display information and buttons
        public override void Draw(Rect drawRect, GameObject o)
        {
            // You can invoke different built-in methods for changing variables
            //        if (GUI.Button(drawRect,"string")) SHOW_StringInput(...
            //        if (GUI.Button(drawRect,"int")) SHOW_IntInput(...
            //        if (GUI.Button(drawRect,"dropdown")) SHOW_DropDownMenu(...
        }
    
        // ToString(...) method is used for the search box
        public override string ToString(GameObject o)
        {
            return null;
        }
    }
*/
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


namespace Hierarchy_Examples
{
    #region MODULE 1 - RotationDirection
    class CustomModule_Example_RotationDirection : HierarchyExtensions.CustomModule_Slot1
    {
        public override string NameOfModule { get { return "Rotation"; } }


        Dictionary<Vector3, string> types = new Dictionary<Vector3, string>() {
            {Vector3.back, "back" },
            {Vector3.forward, "forward" },
            {Vector3.left, "left" },
            {Vector3.right, "right" },
            {Vector3.up, "up" },
            {Vector3.down, "down" }
                                                                             };

        Vector3 GetRoundedRotation(GameObject o)
        {
            //var roundRotation = o.transform.rotation.ToAngleAxis(;
            var roundRotation = o.transform.localRotation * Vector3.forward;
            for (int i = 0 ; i < 3 ; i++) roundRotation[i] = (float)Math.Round( roundRotation[i], 4 );
            return roundRotation;
        }

        public override string ToString(GameObject o)
        {
            var roundRotation = GetRoundedRotation(o);
            return types.ContainsKey( roundRotation ) ? types[roundRotation] : "—";
        }


        public override void Draw(Rect drawRect, GameObject o)
        {
            if (GUI.Button( drawRect, ToString( o ) ))
            {
                var itemsVectors = types.Keys.ToList();
                var itemsStrings = types.Values.ToArray();
                var selectedIndex = itemsVectors.IndexOf(GetRoundedRotation(o));
                SHOW_DropDownMenu( selectedIndex, itemsStrings,
                    newIndex =>
                    {
                        if (!o) return;
                        foreach (var affectsO in HierarchyExtensions.Utilities.GetAffectsGameObjects( o ))
                        {
                            Undo.RecordObject( affectsO.transform, "Change Rotation" );
                            affectsO.transform.localRotation = Quaternion.LookRotation( itemsVectors[newIndex] );
                            EditorUtility.SetDirty( affectsO );
                            EditorSceneManager.MarkSceneDirty( affectsO.scene );
                        }
                    } );
            }
        }

    }
    #endregion // MODULE 1 - RotationDirection








    #region MODULE 2 - StringTypes
    class CustomModule_Example_StringTypes : HierarchyExtensions.CustomModule_Slot2
    {
        public override string NameOfModule { get { return "UI Text"; } }
        static Color green = new Color(0, 1.0f, 0.4f, 0.2f);

        public override string ToString(GameObject o)
        {
            var component = o.GetComponent<Text>();
            return component ? component.text : "";
        }


        public override void Draw(Rect drawRect, GameObject o)
        {
            var component = o.GetComponent<Text>();
            if (component) EditorGUI.DrawRect( drawRect, green );
            else return;

            if (GUI.Button( drawRect, component.text ))
            {
                SHOW_StringInput( component.text,
                    newText =>
                    {
                        if (!o) return;
                        foreach (var affectsO in HierarchyExtensions.Utilities.GetAffectsGameObjects( o ))
                        {
                            component = affectsO.GetComponent<Text>();
                            if (!component) continue;
                            Undo.RecordObject( component, "Change Text" );
                            component.text = newText;
                            EditorUtility.SetDirty( affectsO );
                            EditorSceneManager.MarkSceneDirty( affectsO.scene );
                        }

                    } );
            }
        }

    }
    #endregion // MODULE 2 - StringTypes









    #region MODULE 3 - StaticEditorFlags
    class CustomModule_Example_StaticEditorFlags : HierarchyExtensions.CustomModule_Slot3
    {
        public override string NameOfModule { get { return "Static Editor Flags"; } }
        static Color red = new Color(1.0f, 0.4f, 0, 0.2f);
        static Color yellow = new Color(0.8f, 0.8f, 0, 0.2f);
        static Color blue = new Color(0, 0.4f, 1.0f, 0.2f);
        static StaticEditorFlags[] enabledFlags = { StaticEditorFlags.LightmapStatic, StaticEditorFlags.NavigationStatic };
        static Color[] enabledFlagsColors = { yellow, blue };
        static int? cacheMask;
        static GUIContent content = new GUIContent();

        public override string ToString(GameObject o)
        {
            int result = 0;
            int inverse = (int)GameObjectUtility.GetStaticEditorFlags( o );
            var fl = (int)GameObjectUtility.GetStaticEditorFlags( o );
            for (int i = 0 ; i < enabledFlags.Length ; i++)
            {
                result |= (fl & (int)enabledFlags[i]) != 0 ? (1 << i) : 0;
                inverse &= ~(int)enabledFlags[i];
            }
            if (inverse != 0) result += 1 << 10;
            return result == 0 ? "" : result.ToString();
            /*if (cacheMask == null) for (int i = 0 ; i < enabledFlags.Length ; i++) cacheMask = (cacheMask ?? 0) | (int)enabledFlags[i];
            return ((int)GameObjectUtility.GetStaticEditorFlags( o ) & cacheMask.Value).ToString();*/
        }


        public override void Draw(Rect drawRect, GameObject o)
        {
            EditorGUIUtility.AddCursorRect( drawRect, MouseCursor.Link );

            const int PAD = 1;
            var flags = GameObjectUtility.GetStaticEditorFlags(o);
            const StaticEditorFlags all = (StaticEditorFlags)int.MaxValue;
            drawRect.x += PAD;
            drawRect.y += PAD;
            drawRect.width = drawRect.width / (enabledFlags.Length + 1) - PAD * 2;
            drawRect.height -= PAD * 2;

            var enable = (flags & all) == all;
            content.text = enable ? "*" : "";
            content.tooltip = "Enable All StaticEditorFlags";
            if (GUI.Button( drawRect, content ))
            {
                foreach (var affectsO in HierarchyExtensions.Utilities.GetAffectsGameObjects( o ))
                {
                    Undo.RecordObject( affectsO, "Change StaticEditorFlags" );
                    GameObjectUtility.SetStaticEditorFlags( affectsO, (StaticEditorFlags)SwapBits( (int)flags, (int)all ) );
                };
                UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
            }
            if (enable) EditorGUI.DrawRect( drawRect, red );

            for (int i = 0 ; i < enabledFlags.Length ; i++)
            {
                drawRect.x += drawRect.width + PAD * 2;
                enable = (flags & enabledFlags[i]) == enabledFlags[i];
                content.text = enable ? enabledFlags[i].ToString() : "";
                content.tooltip = enabledFlags[i].ToString();
                if (GUI.Button( drawRect, content ))
                {
                    foreach (var affectsO in HierarchyExtensions.Utilities.GetAffectsGameObjects( o ))
                    {
                        Undo.RecordObject( affectsO, "Change " + enabledFlags[i] );
                        GameObjectUtility.SetStaticEditorFlags( affectsO, (StaticEditorFlags)SwapBits( (int)flags, (int)enabledFlags[i] ) );
                    };
                    UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
                }
                if (enable) EditorGUI.DrawRect( drawRect, enabledFlagsColors[i] );
            }
        }

        int SwapBits(int reference, int mask)
        {
            if ((reference & mask) == mask) reference &= ~mask;
            else reference |= mask;

            return reference;
        }


    }
    #endregion // MODULE 3 - StaticEditorFlags

}

#endif