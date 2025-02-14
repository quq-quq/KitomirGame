//using UnityEditor;
//using UnityEngine;

//[CustomEditor(typeof(DialogueBunch))]
//public class DialogueBunchEditor : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        DialogueBunch dialogueBunch = (DialogueBunch)target;

//        // Отображаем список диалогов
//        for (int i = 0; i < dialogueBunch.dialogues.Count; i++)
//        {
//            EditorGUILayout.BeginVertical("box");
//            EditorGUILayout.LabelField("Dialogue " + (i + 1));

//            // Отображаем свойства каждого диалога
//            SerializedProperty dialogueProperty = serializedObject.FindProperty("dialogues").GetArrayElementAtIndex(i);
//            EditorGUILayout.PropertyField(dialogueProperty, new GUIContent("Dialogue"), true);

//            EditorGUILayout.EndVertical();
//        }

//        // Сохраняем изменения
//        if (GUI.changed)
//        {
//            EditorUtility.SetDirty(dialogueBunch);
//            serializedObject.ApplyModifiedProperties();
//        }
//    }
//}

