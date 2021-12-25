using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Formation))]
public class Formation_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        Formation formation = (Formation)target;

        formation.indicator = (GameObject)EditorGUILayout.ObjectField("Indicator Field", formation.indicator, typeof(GameObject), true);
        formation.spawnEffect = (GameObject)EditorGUILayout.ObjectField("Spawn Effect", formation.spawnEffect, typeof(GameObject), true);
        formation.onAddSound = (AudioSource)EditorGUILayout.ObjectField("Spawn Sound", formation.onAddSound, typeof(AudioSource), true);
        formation.formationAgent = (FormationAgent)EditorGUILayout.ObjectField("Formation Agent", formation.formationAgent, typeof(FormationAgent), true);
        EditorGUILayout.LabelField("");

        formation.formationShape = (FormationShape)EditorGUILayout.EnumPopup("Formation Type", formation.formationShape);   
        var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold,  wordWrap = true};
        switch (formation.FormationShapeValue)
        {
            case FormationShape.StraightLine:
                GUILayout.BeginHorizontal(EditorUtilities.SolidBackgroundStyle_White);
                GUI.color = Color.black;
                EditorGUILayout.LabelField("StraightLine Properties", style);
                GUI.color = Color.white;
                GUILayout.EndHorizontal();

                EditorGUILayout.BeginVertical(EditorUtilities.SolidBackgroundStyle_Green);
                GUI.enabled = !Application.isPlaying;
                formation.numberToSpawn = EditorGUILayout.IntField("Amount To Spawn: ", formation.numberToSpawn);
                GUI.enabled = true;
                formation.lenght = EditorGUILayout.FloatField("Line Length: ", formation.lenght);
                break;
            case FormationShape.Circle:
                GUILayout.BeginHorizontal(EditorUtilities.SolidBackgroundStyle_White);
                GUI.color = Color.black;
                EditorGUILayout.LabelField("Circle Properties", style);
                GUI.color = Color.white;
                GUILayout.EndHorizontal();

                EditorGUILayout.BeginVertical(EditorUtilities.SolidBackgroundStyle_Green);
                GUI.enabled = !Application.isPlaying;
                formation.numberToSpawn = EditorGUILayout.IntField("Amount To Spawn: ", formation.numberToSpawn);
                GUI.enabled = true;
                formation.radius = EditorGUILayout.FloatField("Circle Radius: ", formation.radius);
                break;
            case FormationShape.Square:
                GUILayout.BeginHorizontal(EditorUtilities.SolidBackgroundStyle_White);
                GUI.color = Color.black;
                EditorGUILayout.LabelField("Square Properties", style);
                GUI.color = Color.white;
                GUILayout.EndHorizontal();
                
                EditorGUILayout.BeginVertical(EditorUtilities.SolidBackgroundStyle_Green);
                GUI.enabled = !Application.isPlaying;
                formation.numberToSpawn = EditorGUILayout.IntField("Amount To Spawn: ", formation.numberToSpawn);
                GUI.enabled = true;
                formation.lenght = EditorGUILayout.FloatField("Square Lenght: ", formation.lenght);
                break;

            case FormationShape.Cone:
                GUILayout.BeginHorizontal(EditorUtilities.SolidBackgroundStyle_White);
                GUI.color = Color.black;
                EditorGUILayout.LabelField("Cone Properties", style);
                GUI.color = Color.white;
                GUILayout.EndHorizontal();

                EditorGUILayout.BeginVertical(EditorUtilities.SolidBackgroundStyle_Green);
                GUI.enabled = !Application.isPlaying;
                formation.numberToSpawn = EditorGUILayout.IntField("Amount To Spawn: ", formation.numberToSpawn);
                GUI.enabled = true;
                formation.angle = EditorGUILayout.Slider("Cone Angle",formation.angle, 0f, 360f);
                formation.slantHeight = EditorGUILayout.FloatField("Cone Slant Height: ", formation.slantHeight);
                break;
            default:
                break;
        }

        EditorGUILayout.BeginHorizontal(EditorUtilities.SolidBackgroundStyle_Green);
        GUI.enabled = Application.isPlaying;
        formation.amountToSpawn = EditorGUILayout.IntField("Add Quantity: ", formation.amountToSpawn);
        if (GUILayout.Button("Add"))
        {
            formation.AddFormationAgent(formation.amountToSpawn);
        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal(EditorUtilities.SolidBackgroundStyle_Green);
        formation.amountToRemove = EditorGUILayout.IntField("Remove Quantity: ", formation.amountToRemove);
        if (GUILayout.Button("Remove"))
        {
            formation.RemoveFormationAgent(formation.amountToRemove);
        }

        EditorGUILayout.EndHorizontal();
        GUI.enabled = true;

        EditorGUILayout.EndVertical();

        if(Application.isPlaying)
        {
            EditorGUILayout.LabelField("Application is Playing!!!", style);
        }
    }
}

public static class EditorUtilities
{
    public static readonly GUIStyle SolidBackgroundStyle_White = new GUIStyle { margin = { left = -4, right = -4, top = -2, bottom = -2 }, normal = { background = Texture2D.whiteTexture } };
    public static readonly GUIStyle SolidBackgroundStyle_Black = new GUIStyle { margin = { left = -4, right = -4, top = -2, bottom = -2 }, normal = { background = Texture2D.blackTexture } };
    public static readonly GUIStyle SolidBackgroundStyle_Green = new GUIStyle { margin = { left = -4, right = -4, top = -2, bottom = -2 }, normal = { background = MakeTex(10,10, new Color(0.119f,0.247f,0.132f,1f)) } };
    public static readonly GUIStyle SolidBackgroundStyleFix = new GUIStyle { padding = { left = -4, right = -4, top = -2, bottom = -2 } };

    public static Texture2D MakeTex(int width, int height, Color col)
    {
        var pix = new Color[width * height];
        
        for (int i = 0; i < pix.Length; i++)
        {
            pix[i] = col;
        }

        var result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();
        return result;
    }
}

