using Effects;
using UnityEditor;
using UnityEngine;

namespace Test.Editor
{
    [CustomEditor(typeof(AudioTester))]
    public class AudioTesterEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            AudioTester e = target as AudioTester;
            AudioSource audioSource = e.GetComponent<AudioSource>();
        
            if (GUILayout.Button("Play sound"))
            {
                audioSource.Play();
            }
        }
    }
}