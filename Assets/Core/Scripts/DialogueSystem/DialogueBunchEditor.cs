//using Dialogue_system;
//using UnityEditor;
//using UnityEngine;

//[CustomEditor(typeof(DialogueBunch))]
//public class DialogueBunchEditor : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        DialogueBunch dialogueBunch = (DialogueBunch)target;

//        // ���������� ������ ��� ������
//        EditorGUILayout.LabelField("Dialogue Text", dialogueBunch._previousDialogueBunch);

//        // ����� �������� ������ ����, ������� ������ ������� ��������������
//        // EditorGUILayout.PropertyField(serializedObject.FindProperty("otherField"));

//        // ��������� ������
//        serializedObject.ApplyModifiedProperties();
//    }
//}