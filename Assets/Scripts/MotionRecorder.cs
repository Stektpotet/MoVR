using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.Animations;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Experimental.Animations;

public class MotionRecorder : MonoBehaviour
{
    Animator animator;



    public Animator previewAnimator;

    [SerializeField]
    private GameObject[] indicators;

    [SerializeField]
    private AnimationClip m_clip;
    private GameObjectRecorder m_recorder;
    private bool m_recording = false;
    private Object off;

    private void Start()
    {
        animator = GetComponent<Animator>();
        ResetRecorder();
    }


    public void ResetRecorder()
    {
        //it's totally a good sign when the references says "TODO"
        m_recorder = new GameObjectRecorder(gameObject);
        m_recorder.BindComponentsOfType<Transform>(gameObject, true); //this results in an animation that "works", but not on humanoids :(
    }

    //TODO
    public void StartRecording()
    {
        if (m_clip != null) {
            SteamVR_Controller.Input(Input.GetButtonDown("Button1") ? 1 : 2).TriggerHapticPulse(1000);
            m_recording = true;
            
            previewAnimator.SetTrigger("StopPreview");

            m_clip.ClearCurves();
            foreach (GameObject indicator in indicators) {
                indicator.SetActive(true);
            }
        }
    }

    public void StopRecording()
    {
        m_recording = false;
        if (m_recorder.isRecording)
        {
            SteamVR_Controller.Input(Input.GetButtonDown("Button1") ? 1 : 2).TriggerHapticPulse(1000);
            m_recorder.SaveToClip(m_clip);
            m_recorder.ResetRecording();
            ResetRecorder();
            if (!previewAnimator.isInitialized)
                previewAnimator.Rebind();

            if (previewAnimator.gameObject.activeSelf && previewAnimator.isActiveAndEnabled)
            {
                previewAnimator.SetTrigger("PlayPreview");
            }
            

            foreach (GameObject indicator in indicators) {
                indicator.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Button1") || Input.GetButtonDown("Button3"))
        {
            Debug.Log("Toggle Recording! Turning " + (m_recording ? "off" : "on"));
            if (m_recording)
                StopRecording();
            else
                StartRecording();
        }
    }

    void LateUpdate()
    {
        if (m_recording)
            m_recorder.TakeSnapshot(Time.deltaTime);
    }

    private void OnDisable()
    {
        StopRecording();
    }
}