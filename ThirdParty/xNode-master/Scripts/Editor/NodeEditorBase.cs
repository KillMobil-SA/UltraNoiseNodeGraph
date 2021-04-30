﻿using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector.Editor;

#endif

namespace XNodeEditor.Internal
{
	/// <summary> Handles caching of custom editor classes and their target types. Accessible with GetEditor(Type type) </summary>
	/// <typeparam name="T">Editor Type. Should be the type of the deriving script itself (eg. NodeEditor) </typeparam>
	/// <typeparam name="A">Attribute Type. The attribute used to connect with the runtime type (eg. CustomNodeEditorAttribute) </typeparam>
	/// <typeparam name="K">Runtime Type. The ScriptableObject this can be an editor for (eg. Node) </typeparam>
	public abstract class NodeEditorBase<T, A, K> where A : Attribute, NodeEditorBase<T, A, K>.INodeEditorAttrib
        where T : NodeEditorBase<T, A, K>
        where K : ScriptableObject
    {
        /// <summary> Custom editors defined with [CustomNodeEditor] </summary>
        private static Dictionary<Type, Type> editorTypes;

        private static readonly Dictionary<K, T> editors = new Dictionary<K, T>();
        public SerializedObject serializedObject;
        public K target;
        public NodeEditorWindow window;

        public static T GetEditor(K target, NodeEditorWindow window)
        {
            if (target == null) return null;
            T editor;
            if (!editors.TryGetValue(target, out editor))
            {
                var type = target.GetType();
                var editorType = GetEditorType(type);
                editor = Activator.CreateInstance(editorType) as T;
                editor.target = target;
                editor.serializedObject = new SerializedObject(target);
                editor.window = window;
                editor.OnCreate();
                editors.Add(target, editor);
            }

            if (editor.target == null) editor.target = target;
            if (editor.window != window) editor.window = window;
            if (editor.serializedObject == null) editor.serializedObject = new SerializedObject(target);
            return editor;
        }

        public static void DestroyEditor(K target)
        {
            if (target == null) return;
            T editor;
            if (editors.TryGetValue(target, out editor)) editors.Remove(target);
        }

        private static Type GetEditorType(Type type)
        {
            if (type == null) return null;
            if (editorTypes == null) CacheCustomEditors();
            Type result;
            if (editorTypes.TryGetValue(type, out result)) return result;
            //If type isn't found, try base type
            return GetEditorType(type.BaseType);
        }

        private static void CacheCustomEditors()
        {
            editorTypes = new Dictionary<Type, Type>();

            //Get all classes deriving from NodeEditor via reflection
            var nodeEditors = typeof(T).GetDerivedTypes();
            for (var i = 0; i < nodeEditors.Length; i++)
            {
                if (nodeEditors[i].IsAbstract) continue;
                var attribs = nodeEditors[i].GetCustomAttributes(typeof(A), false);
                if (attribs == null || attribs.Length == 0) continue;
                var attrib = attribs[0] as A;
                editorTypes.Add(attrib.GetInspectedType(), nodeEditors[i]);
            }
        }

        /// <summary> Called on creation, after references have been set </summary>
        public virtual void OnCreate()
        {
        }

        public interface INodeEditorAttrib
        {
            Type GetInspectedType();
        }
#if ODIN_INSPECTOR
        private PropertyTree _objectTree;
        public PropertyTree objectTree
        {
            get
            {
                if (_objectTree == null)
                    try
                    {
                        var wasInEditor = NodeEditor.inNodeEditor;
                        NodeEditor.inNodeEditor = true;
                        _objectTree = PropertyTree.Create(serializedObject);
                        NodeEditor.inNodeEditor = wasInEditor;
                    }
                    catch (ArgumentException ex)
                    {
                        Debug.Log(ex);
                    }

                return _objectTree;
            }
        }
#endif
    }
}