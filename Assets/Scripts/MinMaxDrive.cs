using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;

//-------------------------------------------------------------------------
[RequireComponent(typeof(Interactable))]
public class MinMaxDrive : MonoBehaviour
{
    public Transform startPosition;
    public Transform endPosition;
    public LinearMapping linearMapping;
    public bool repositionGameObject = true;
    public bool maintainMomemntum = true;
    public float momemtumDampenRate = 5.0f;
    public float initialMapping = 0.5f;

    public float min, max;


    private float range { get { return max - min; } }
    private float normalizedValue;


    private int numMappingChangeSamples = 5;
    private float[] mappingChangeSamples;
    private float prevMapping = 0.0f;
    private float mappingChangeRate;
    private int sampleCount = 0;


    public UnityEvent onStartDrag;
    public UnityEvent onEndDrag;
    [System.Serializable]
    public class FloatEvent : UnityEvent<float> { }
    public FloatEvent onDrag;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (max < min)
            max = min + 1;
    }
#endif

    //-------------------------------------------------
    void Awake()
    {
        mappingChangeSamples = new float[numMappingChangeSamples];
    }


    //-------------------------------------------------
    void Start()
    {
        if (linearMapping == null)
        {
            linearMapping = GetComponent<LinearMapping>();
        }

        if (linearMapping == null)
        {
            linearMapping = gameObject.AddComponent<LinearMapping>();
        }

        if (repositionGameObject)
        {
            UpdateLinearMapping(transform);
        }
    }


    //-------------------------------------------------
    private void HandHoverUpdate(Hand hand)
    {
        if (hand.GetStandardInteractionButtonDown())
        {
            onStartDrag.Invoke();
            hand.HoverLock(GetComponent<Interactable>());

            initialMapping = normalizedValue - CalculateLinearMapping(hand.transform);
            sampleCount = 0;
            mappingChangeRate = 0.0f;
        }

        if (hand.GetStandardInteractionButtonUp())
        {
            hand.HoverUnlock(GetComponent<Interactable>());

            CalculateMappingChangeRate();
            onEndDrag.Invoke();
        }

        if (hand.GetStandardInteractionButton())
        {
            UpdateLinearMapping(hand.transform);
            onDrag.Invoke(linearMapping.Value);
        }
    }


    //-------------------------------------------------
    private void CalculateMappingChangeRate()
    {
        //Compute the mapping change rate
        mappingChangeRate = 0.0f;
        int mappingSamplesCount = Mathf.Min(sampleCount, mappingChangeSamples.Length);
        if (mappingSamplesCount != 0)
        {
            for (int i = 0; i < mappingSamplesCount; ++i)
            {
                mappingChangeRate += mappingChangeSamples[i];
            }
            mappingChangeRate /= mappingSamplesCount;
        }
    }

    public void UpdateMapping(float hz)
    {
        mappingChangeRate = hz;
    }
    //-------------------------------------------------
    private void UpdateLinearMapping(Transform tr)
    {
        prevMapping = normalizedValue;
        normalizedValue = Mathf.Clamp01(initialMapping + CalculateLinearMapping(tr));
        linearMapping.Value = normalizedValue * range + min;

        mappingChangeSamples[sampleCount % mappingChangeSamples.Length] = (1.0f / Time.deltaTime) * (normalizedValue - prevMapping);
        sampleCount++;

        if (repositionGameObject)
        {
            transform.position = Vector3.Lerp(startPosition.position, endPosition.position, normalizedValue);
        }
    }


    //-------------------------------------------------
    private float CalculateLinearMapping(Transform tr)
    {
        Vector3 direction = endPosition.position - startPosition.position;
        float length = direction.magnitude;
        direction.Normalize();

        Vector3 displacement = tr.position - startPosition.position;

        return Vector3.Dot(displacement, direction) / length;
    }


    //-------------------------------------------------
    void Update()
    {
        if (maintainMomemntum && mappingChangeRate != 0.0f)
        {
            //Dampen the mapping change rate and apply it to the mapping
            mappingChangeRate = Mathf.Lerp(mappingChangeRate, 0.0f, momemtumDampenRate * Time.deltaTime);
            normalizedValue = Mathf.Clamp01(normalizedValue + (mappingChangeRate * Time.deltaTime));
            linearMapping.Value = normalizedValue * range + min;


            if (repositionGameObject)
            {
                transform.position = Vector3.Lerp(startPosition.position, endPosition.position, normalizedValue);
            }
        }
    }
}
