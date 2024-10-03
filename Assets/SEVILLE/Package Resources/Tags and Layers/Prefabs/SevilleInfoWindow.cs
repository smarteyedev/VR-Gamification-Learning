using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
public class SevilleInfoWindow : EditorWindow
{
    private Texture2D logo;
    private string judul = "<b>Masa Trial Anda Habis!</b>";
    private string description = "Jangan khawatir, dapatkan akses penuh \nke <b>Seville</b> sekarang";
    private string buyNowLink = "https://assetstore.unity.com/packages/tools/physics/smarteye-virtual-learning-266871";
    private string yourUrl = "https://seville.svcc.io/";

    [MenuItem("Window/Seville Full Access")]
    public static void ShowWindowFromMenu()
    {
        ShowWindow();
    }

    public static void ShowWindow()
    {
        SevilleInfoWindow window = GetWindow<SevilleInfoWindow>("Seville Full Access");
        window.minSize = new Vector2(420, 400);
        window.maxSize = new Vector2(420, 400);
        window.Show();
    }

    private void OnEnable()
    {
        // Load logo
        logo = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/SEVILLE/Package Resources/Tags and Layers/Prefabs/seville.png");
    }

    private void OnGUI()
    {
        GUILayout.Space(10);

        // Display logo with appropriate size
        if (logo != null)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label(logo, GUILayout.Width(400), GUILayout.Height(160));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
        else
        {
            GUILayout.Label("Logo not found");
        }

        GUILayout.Space(10);

        // Set up judul style to make "Masa Trial Anda Habis!" larger
        GUIStyle judulStyle = new GUIStyle(GUI.skin.label);
        judulStyle.richText = true;
        judulStyle.fontSize = 24;
        judulStyle.alignment = TextAnchor.MiddleCenter;

        // Display the larger judul
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label(judul, judulStyle, GUILayout.Width(380));
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.Space(10);

        // Set up description style for normal font size
        GUIStyle descriptionStyle = new GUIStyle(GUI.skin.label);
        descriptionStyle.richText = true;
        descriptionStyle.fontSize = 16;
        descriptionStyle.alignment = TextAnchor.MiddleCenter;

        // Display the description with the custom style
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label(description, descriptionStyle, GUILayout.Width(380));
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.Space(30);

        // Create button style
        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.fixedWidth = 150;
        buttonStyle.fixedHeight = 40;
        buttonStyle.alignment = TextAnchor.MiddleCenter;

        // "Beli Sekarang" button
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Beli Sekarang", buttonStyle))
        {
            Application.OpenURL(buyNowLink);
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.Space(15);

        // Set up the "Pelajari Selengkapnya" style manually with underline effect
        GUIStyle moresStyle = new GUIStyle(GUI.skin.label);
        moresStyle.normal.textColor = new Color(0.69f, 0.78f, 1f);  // Color #B0c6FF
        moresStyle.alignment = TextAnchor.MiddleCenter;
        moresStyle.fontSize = 15;

        // Display the "Pelajari Selengkapnya" text
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label("Informasi Selengkapnya", moresStyle);
        var rect = GUILayoutUtility.GetLastRect();
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        // Draw underline manually
        Handles.BeginGUI();
        Handles.color = new Color(0.69f, 0.78f, 1f);  // Color #B0c6FF
        Handles.DrawLine(new Vector3(rect.xMin, rect.yMax), new Vector3(rect.xMax, rect.yMax));
        Handles.EndGUI();

        // Handle click manually on the label
        EditorGUIUtility.AddCursorRect(rect, MouseCursor.Link);
        if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition))
        {
            Application.OpenURL(yourUrl);
        }
    }
}
#endif