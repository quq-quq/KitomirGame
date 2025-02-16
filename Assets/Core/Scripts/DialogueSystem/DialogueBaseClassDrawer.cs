using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(DialogueBaseClass))]
public class DialogueBaseClassDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // �������� ������ �� ����
        SerializedProperty typeOfDialogue = property.FindPropertyRelative("_typeOfDialogue");
        SerializedProperty simplePhrase = property.FindPropertyRelative("_simplePhrase");
        SerializedProperty answers = property.FindPropertyRelative("_answers");

        float currentY = position.y; // ������� ������� �� ��� Y

        // ������ ���������� ������ ��� ������ ���� �������
        Rect typeRect = new Rect(position.x, currentY, position.width, EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(typeRect, typeOfDialogue);
        currentY += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

        // ����������, ��� ����������
        if (typeOfDialogue.enumValueIndex == (int)TypeOfDialogue.SimplePhrases)
        {
            // ���������� _simplePhrase
            Rect phraseRect = new Rect(position.x, currentY, position.width, EditorGUI.GetPropertyHeight(simplePhrase));
            EditorGUI.PropertyField(phraseRect, simplePhrase, true);
            currentY += EditorGUI.GetPropertyHeight(simplePhrase) + EditorGUIUtility.standardVerticalSpacing;
        }
        else if (typeOfDialogue.enumValueIndex == (int)TypeOfDialogue.Answers)
        {
            // ���������� _answers
            Rect answersRect = new Rect(position.x, currentY, position.width, EditorGUI.GetPropertyHeight(answers));
            EditorGUI.PropertyField(answersRect, answers, true);
            currentY += EditorGUI.GetPropertyHeight(answers) + EditorGUIUtility.standardVerticalSpacing;
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // ��������� ����� ������ � ����������� �� ���������� ���� �������
        SerializedProperty typeOfDialogue = property.FindPropertyRelative("_typeOfDialogue");
        SerializedProperty simplePhrase = property.FindPropertyRelative("_simplePhrase");
        SerializedProperty answers = property.FindPropertyRelative("_answers");

        float height = EditorGUIUtility.singleLineHeight; // ������ ��� ����������� ������ _typeOfDialogue

        if (typeOfDialogue.enumValueIndex == (int)TypeOfDialogue.SimplePhrases)
        {
            height += EditorGUI.GetPropertyHeight(simplePhrase); // ������ ��� _simplePhrase
        }
        else if (typeOfDialogue.enumValueIndex == (int)TypeOfDialogue.Answers)
        {
            height += EditorGUI.GetPropertyHeight(answers); // ������ ��� _answers
        }

        height += EditorGUIUtility.standardVerticalSpacing * 2; // ��������� �������

        return height;
    }
}