namespace TriPeakSolitaire.Tools
{
    using UnityEngine;
    using TriPeakSolitaire.Cards;
    using UnityEditor;
    using System.IO;

    public class CardDataCreatorWindow : EditorWindow
    {
        private DefaultAsset spriteFolder;
        private string folderPathToCreateSO;

        [MenuItem("Tools/ Card Data Creator")]
        public static void ShowWindow()
        {
            GetWindow<CardDataCreatorWindow>();
        }


        private void OnGUI()
        {
            GUILayout.Space(20);

            spriteFolder = (DefaultAsset)EditorGUILayout.ObjectField("Sprite Folder: ", spriteFolder, typeof(DefaultAsset), false);

            GUILayout.Space(20);

            folderPathToCreateSO = EditorGUILayout.TextField("Output path: ", folderPathToCreateSO);

            GUILayout.Space(20);

            if(GUILayout.Button("Generate SO"))
            {
                GenerateScriptableObjects();
            }
        }

        private void GenerateScriptableObjects()
        {
            if (spriteFolder == null)
            {
                Debug.LogError("Sprite folder reference is null. Please select a valid folder path to the sprites");
                return;
            }            
            
            if (folderPathToCreateSO == null)
            {
                Debug.LogError("Invalid folder path. Please write a valid path to create the Scriptable-Objects in.");
                return;
            }

            string spriteFolderPath=AssetDatabase.GetAssetPath(spriteFolder);

            string[] guids = AssetDatabase.FindAssets("t:sprite", new[] {spriteFolderPath });

            if (!UnityEngine.Windows.Directory.Exists(folderPathToCreateSO))
            {
                UnityEngine.Windows.Directory.CreateDirectory(folderPathToCreateSO);
            }

            Undo.IncrementCurrentGroup();
            int undoGroup = Undo.GetCurrentGroup();

            foreach (string guid in guids)
            {
                string spritePath=AssetDatabase.GUIDToAssetPath(guid);
                Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(spritePath);

                if (sprite == null) continue;

                string cardName = sprite.name;
                string filePath = Path.Combine(folderPathToCreateSO, cardName+".asset");
                CardData cardData = ScriptableObject.CreateInstance<CardData>();
                cardData.cardName = cardName;
                cardData.frontSprite = sprite;

                AssetDatabase.CreateAsset(cardData, filePath);

                Undo.RegisterCreatedObjectUndo(cardData, "Create Card Data");
            }

            Undo.CollapseUndoOperations(undoGroup);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log($"Created Scriptable-objects for cards at {spriteFolderPath} successfully");
        }
    }
}
