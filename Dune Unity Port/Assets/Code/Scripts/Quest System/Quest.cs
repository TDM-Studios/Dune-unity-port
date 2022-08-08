using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using UnityEditor;

public class Quest : ScriptableObject
{
    [System.Serializable]
    public struct Info
    {
        public string name;
        [TextArea] public string description;
    }

    [Header("Info")] public Info information;

    public bool isMainQuest;
    public bool completed { get; protected set; }
    public QuestCompletedEvent questCompleted;

    public abstract class QuestGoal : ScriptableObject
    {
        [TextArea] protected string description;
        public int currAmount { get; protected set; }
        public int requiredAmount = 1;

        public bool completed { get; protected set; }
        [HideInInspector] public UnityEvent goalCompleted;

        public virtual string GetDescription()
        {
            return description;
        }

        public virtual void Initialize()
        {
            completed = false;
            goalCompleted = new UnityEvent();
        }

        protected void Evaluate()
        {
            if (currAmount >= requiredAmount)
            {
                Complete();
            }
        }

        private void Complete()
        {
            completed = true;
            goalCompleted.Invoke();
            goalCompleted.RemoveAllListeners();
        }
    }

    public List<QuestGoal> goals;

    public void Initialize()
    {
        completed = false;
        questCompleted = new QuestCompletedEvent();

        foreach (var goal in goals)
        {
            goal.Initialize();
            goal.goalCompleted.AddListener(delegate { CheckGoals(); });
        }
    }

    private void CheckGoals()
    {
        completed = goals.All(g => g.completed);
        if (completed)
        {
            questCompleted.Invoke(this);
            questCompleted.RemoveAllListeners();
        }
    }
}

public class QuestCompletedEvent : UnityEvent<Quest>
{

}

#if UNITY_EDITOR
[CustomEditor(typeof(Quest))]
public class QuestEditor : Editor
{
    SerializedProperty m_questInfoProperty;

    List<string> m_questGoalType;
    SerializedProperty m_questGoalListProperty;

    [MenuItem("Assets/Quest", priority = 0)]
    public static void CreateQuest()
    {
        var newQuest = CreateInstance<Quest>();

        ProjectWindowUtil.CreateAsset(newQuest, "quest.asset");
    }

    private void OnEnable()
    {
        m_questInfoProperty = serializedObject.FindProperty(nameof(Quest.information));

        m_questGoalListProperty = serializedObject.FindProperty(nameof(Quest.goals));

        var lookUp = typeof(Quest.QuestGoal);
        m_questGoalType = System.AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(lookUp))
            .Select(type => type.Name)
            .ToList();
    }

    public override void OnInspectorGUI()
    {
        var child = m_questInfoProperty.Copy();
        var depth = child.depth;
        child.NextVisible(true);

        EditorGUILayout.LabelField("Quest Info", EditorStyles.boldLabel);
        while (child.depth > depth)
        {
            EditorGUILayout.PropertyField(child, true);
            child.NextVisible(false);
        }

        int choice = EditorGUILayout.Popup("Add new Quest Goal", -1, m_questGoalType.ToArray());

        if (choice != -1)
        {
            var newInstance = ScriptableObject.CreateInstance(m_questGoalType[choice]);

            AssetDatabase.AddObjectToAsset(newInstance, target);

            m_questGoalListProperty.InsertArrayElementAtIndex(m_questGoalListProperty.arraySize);
            m_questGoalListProperty.GetArrayElementAtIndex(m_questGoalListProperty.arraySize - 1)
                .objectReferenceValue = newInstance;
        }

        Editor ed = null;
        int toDelete = -1;
        for (int i = 0; i < m_questGoalListProperty.arraySize; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();
            var item = m_questGoalListProperty.GetArrayElementAtIndex(i);
            SerializedObject obj = new SerializedObject(item.objectReferenceValue);

            Editor.CreateCachedEditor(item.objectReferenceValue, null, ref ed);

            ed.OnInspectorGUI();
            EditorGUILayout.EndVertical();

            if (GUILayout.Button("-", GUILayout.Width(32)))
            {
                toDelete = i;
            }
            EditorGUILayout.EndHorizontal();
        }

        if (toDelete != -1)
        {
            var item = m_questGoalListProperty.GetArrayElementAtIndex(toDelete).objectReferenceValue;
            DestroyImmediate(item, true);

            m_questGoalListProperty.DeleteArrayElementAtIndex(toDelete);
            m_questGoalListProperty.DeleteArrayElementAtIndex(toDelete);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
