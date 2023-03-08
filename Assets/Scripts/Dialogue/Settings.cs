using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CreateAssetMenu(fileName = "New Dialogue", menuName = "New Dialogue/Dialogue")] //sintaxe utilizada para criar um scriptable object no projeto
public class Settings : ScriptableObject
{
    [Header("Settings")]
    public GameObject actor;

    [Header("Dialogue")]
    public Sprite speakerSprite;
    public string sentence;

    public List<Sentences> dialogue = new List<Sentences>();
}

[System.Serializable]
public class Sentences
{
    public string actorName;
    public Sprite portrait;
    public Language sentence;
}

[System.Serializable]
public class Language
{
    public string Portuguese;
    public string English;
}

#if UNITY_EDITOR //este bloco de código irá rodar apenas se estiver com a Unity aberta
[CustomEditor(typeof(Settings))]
public class BuilderEditor: Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Settings nbs = (Settings)target;

        Language lang = new Language();
        lang.Portuguese = nbs.sentence;

        Sentences sent = new Sentences();
        sent.portrait = nbs.speakerSprite;
        sent.sentence = lang;

        if(GUILayout.Button("Create Dialogue"))
        {
            if(nbs.sentence != null)
            {
                nbs.dialogue.Add(sent); //insere a fala na lista de sentenças/frases
                nbs.speakerSprite = null;
                nbs.sentence = "";

            }
        }
    }
}


#endif