using Project.Scripts.Game.WheelGame.Data.Item;
using UnityEditor;
using UnityEngine;

namespace Project.Scripts.Game.WheelGame.Data.Provider.Editor
{
    [CustomEditor(typeof(WheelItemProvider))]
    public class WheelItemProviderEditor : UnityEditor.Editor
    {
        private WheelZoneType m_selectedZoneType;
        private ItemQuality m_selectedQuality;
        private bool m_foldout;
        private int m_seed;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            m_foldout = EditorGUILayout.BeginFoldoutHeaderGroup(m_foldout, "Testing");

            if (m_foldout)
            {
                m_selectedZoneType = (WheelZoneType)EditorGUILayout.EnumPopup("Zone Type", m_selectedZoneType);
                m_selectedQuality = (ItemQuality)EditorGUILayout.EnumPopup("Target Quality", m_selectedQuality);
                m_seed = EditorGUILayout.IntField("Seed", m_seed);

                if (GUILayout.Button("Generate"))
                {
                    WheelItemProvider provider = (WheelItemProvider)target;
                    WheelItemResult[] results = provider.Provide(m_selectedZoneType, m_selectedQuality, m_seed);

                    Debug.Log($"[WheelItemProvider] Generated items for Zone: {m_selectedZoneType}");

                    if (results != null)
                    {
                        for (int i = 0; i < results.Length; i++)
                        {
                            WheelItemResult result = results[i];
                            if (result.Item != null)
                            {
                                Debug.Log($"Item[{i}]: Id = {result.Item.Id} Type = {result.Item.Type} Amount = {result.Amount}");
                            }
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Returned item array is null.");
                    }
                }
            }

            EditorGUILayout.EndFoldoutHeaderGroup();
        }
    }
}