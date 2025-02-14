//using UnityEditor;
//using UnityEngine;

//[CustomPropertyDrawer(typeof(DialogueBaseClass), true)]
//public class DialogueBaseEditor : PropertyDrawer
//{
//    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//    {
//        // Получаем имя типа элемента
//        var targetObject = property.serializedObject.targetObject;
//        var type = targetObject.GetType();

//        // Отображаем базовые свойства
//        var nameProperty = property.FindPropertyRelative("name");
//        if (nameProperty != null)
//        {
//            EditorGUI.PropertyField(position, nameProperty, new GUIContent("Name"));
//            position.y += EditorGUIUtility.singleLineHeight;
//        }

//        // Проверяем тип и отображаем соответствующие свойства
//        if (type.IsSubclassOf(typeof(SimplePhrases)))
//        {
//            var phraseProperty = property.FindPropertyRelative("phrase");
//            if (phraseProperty != null)
//            {
//                EditorGUI.PropertyField(position, phraseProperty, new GUIContent("Phrase"));
//            }
//        }
//        else if (type.IsSubclassOf(typeof(Answers)))
//        {
//            var answerProperty = property.FindPropertyRelative("answer");
//            if (answerProperty != null)
//            {
//                EditorGUI.PropertyField(position, answerProperty, new GUIContent("Answer"));
//            }
//        }
//    }

//    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
//    {
//        // Возвращаем высоту в зависимости от количества полей
//        int extraLines = IsChildClass(property) ? 1 : 0;
//        return EditorGUIUtility.singleLineHeight * (2 + extraLines);
//    }

//    private bool IsChildClass(SerializedProperty property)
//    {
//        var targetObject = property.serializedObject.targetObject;
//        var type = targetObject.GetType();
//        return type.IsSubclassOf(typeof(SimplePhrases)) || type.IsSubclassOf(typeof(Answers));
//    }
//}
