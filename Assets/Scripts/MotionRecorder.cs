using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.Animations;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Experimental.Animations;

public class MotionRecorder : MonoBehaviour
{
    Animator animator;

    [SerializeField]
    private AnimationClip m_clip;
    private GameObjectRecorder m_recorder;
    private bool m_recording;
    private Object off;

    private void Start()
    {
        animator = GetComponent<Animator>();
        
        //it's totally a good sign when the references says "TODO"
        m_recorder = new GameObjectRecorder(gameObject);
        
        m_recorder.BindComponentsOfType<Transform>(gameObject, true); //this results in an animation that "works", but not on humanoids :(
        if(m_clip == null)
        {
            m_clip = new AnimationClip
            {
                
                name = "New Clip",
                frameRate = 60,
            };
        }
    }

    //TODO
    public void StartRecording()
    {

    }

    public void StopRecording()
    {
        if (m_recorder.isRecording)
        {
            m_recorder.SaveToClip(m_clip);
            m_recorder.ResetRecording();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_recording = !m_recording;
            Debug.Log($"Toggle Recording! {m_recording}");
            if (!m_recording)
                StopRecording();
        }
    }

    void LateUpdate()
    {
        if (m_recording && m_clip != null)
            m_recorder.TakeSnapshot(Time.deltaTime);
    }

    private void OnDisable()
    {
        StopRecording();
    }
}