//using Dialogue_system;
//using UnityEditor;
//using UnityEngine;

//[CustomEditor(typeof(DialogueBunch))]
//public class DialogueBunchEditor : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        DialogueBunch dialogueBunch = (DialogueBunch)target;

//        // Отображаем только для чтения
//        EditorGUILayout.LabelField("Dialogue Text", dialogueBunch._previousDialogueBunch);

//        // Можно добавить другие поля, которые хотите сделать редактируемыми
//        // EditorGUILayout.PropertyField(serializedObject.FindProperty("otherField"));

//        // Обновляем объект
//        serializedObject.ApplyModifiedProperties();
//    }
//}