using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(DialogueBaseClass))]
public class DialogueBaseClassDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // Получаем ссылки на поля
        SerializedProperty typeOfDialogue = property.FindPropertyRelative("_typeOfDialogue");
        SerializedProperty simplePhrase = property.FindPropertyRelative("_simplePhrase");
        SerializedProperty answers = property.FindPropertyRelative("_answers");

        float currentY = position.y; // Текущая позиция по оси Y

        // Рисуем выпадающий список для выбора типа диалога
        Rect typeRect = new Rect(position.x, currentY, position.width, EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(typeRect, typeOfDialogue);
        currentY += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

        // Определяем, что показывать
        if (typeOfDialogue.enumValueIndex == (int)TypeOfDialogue.SimplePhrases)
        {
            // Показываем _simplePhrase
            Rect phraseRect = new Rect(position.x, currentY, position.width, EditorGUI.GetPropertyHeight(simplePhrase));
            EditorGUI.PropertyField(phraseRect, simplePhrase, true);
            currentY += EditorGUI.GetPropertyHeight(simplePhrase) + EditorGUIUtility.standardVerticalSpacing;
        }
        else if (typeOfDialogue.enumValueIndex == (int)TypeOfDialogue.Answers)
        {
            // Показываем _answers
            Rect answersRect = new Rect(position.x, currentY, position.width, EditorGUI.GetPropertyHeight(answers));
            EditorGUI.PropertyField(answersRect, answers, true);
            currentY += EditorGUI.GetPropertyHeight(answers) + EditorGUIUtility.standardVerticalSpacing;
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // Вычисляем общую высоту в зависимости от выбранного типа диалога
        SerializedProperty typeOfDialogue = property.FindPropertyRelative("_typeOfDialogue");
        SerializedProperty simplePhrase = property.FindPropertyRelative("_simplePhrase");
        SerializedProperty answers = property.FindPropertyRelative("_answers");

        float height = EditorGUIUtility.singleLineHeight; // Высота для выпадающего списка _typeOfDialogue

        if (typeOfDialogue.enumValueIndex == (int)TypeOfDialogue.SimplePhrases)
        {
            height += EditorGUI.GetPropertyHeight(simplePhrase); // Высота для _simplePhrase
        }
        else if (typeOfDialogue.enumValueIndex == (int)TypeOfDialogue.Answers)
        {
            height += EditorGUI.GetPropertyHeight(answers); // Высота для _answers
        }

        height += EditorGUIUtility.standardVerticalSpacing * 2; // Добавляем отступы

        return height;
    }
}