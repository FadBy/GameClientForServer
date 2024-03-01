using GameCreator.Editor.Common;
using GameCreator.Runtime.Inventory;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Inventory
{
    [CustomPropertyDrawer(typeof(BagShapeList), true)]
    public class BagShapeListDrawer : TBagShapeWithWeightDrawer
    {
        protected override void EditorContent(SerializedProperty property, VisualElement root)
        {
            SerializedProperty hasMaxHeight = property.FindPropertyRelative("m_HasMaxHeight");
            SerializedProperty height = property.FindPropertyRelative("m_Height");

            PropertyField fieldHasMaxHeight = new PropertyField(hasMaxHeight);
            VisualElement contentHeight = new VisualElement();
            PropertyField fieldHeight = new PropertyField(height);
            
            root.Add(fieldHasMaxHeight);
            root.Add(contentHeight);

            if (hasMaxHeight.boolValue)
            {
                contentHeight.Add(fieldHeight);
                contentHeight.Add(new SpaceSmall());
            }
            fieldHasMaxHeight.RegisterValueChangeCallback(changeEvent =>
            {
                contentHeight.Clear();
                if (changeEvent.changedProperty.boolValue)
                {
                    contentHeight.Add(fieldHeight);
                    contentHeight.Add(new SpaceSmall());
                    fieldHeight.Bind(changeEvent.changedProperty.serializedObject);
                }
            });
            
            base.EditorContent(property, root);
        }

        protected override void RuntimeContent(IBagShape shape, VisualElement root)
        {
            base.RuntimeContent(shape, root);
            
            string height = shape.HasInfiniteHeight ? "Infinite" : shape.MaxHeight.ToString();
            TextField fieldHeight = new TextField("Max Height") { value = height };
            
            string weight = shape.MaxWeight == int.MaxValue ? "Infinite" : shape.MaxWeight.ToString();
            TextField fieldWeight = new TextField("Max Weight") { value = weight };
            
            fieldHeight.SetEnabled(false);
            fieldWeight.SetEnabled(false);
            
            root.Add(fieldHeight);
            root.Add(fieldWeight);
        }
    }
}