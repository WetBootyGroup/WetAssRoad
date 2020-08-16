using Effects;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TestEffectsManager))]
public class TestEffectManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = Application.isPlaying;

        TestEffectsManager e = target as TestEffectsManager;
        EffectsManager effectsManager = e.effectManager;
        
        if (GUILayout.Button("Produce puddle effect"))
        {;
            effectsManager.ProducePuddleEffect(e.puddleArgument);
        }

        if (GUILayout.Button("Produce shake effect"))
        {
            effectsManager.ProduceShakeEffect(e.shakeArgument);
        }
    }
}