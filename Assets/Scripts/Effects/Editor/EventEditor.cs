using Effects;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TestEffectsManager))]
public class EventEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = Application.isPlaying;

        TestEffectsManager e = target as TestEffectsManager;
        EffectsManager effectsManager = e.effectManager;
        
        if (GUILayout.Button("Produce puddle effect"))
        {
            effectsManager.ProducePuddleEffect(new PuddleArgument());
        }

        if (GUILayout.Button("Produce shake effect"))
        {
            effectsManager.ProduceShakeEffect(new ShakeArgument());
        }
    }
}