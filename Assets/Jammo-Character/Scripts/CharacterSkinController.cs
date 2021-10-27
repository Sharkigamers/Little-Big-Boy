using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkinController : MonoBehaviour
{
    #region Singleton
    public static CharacterSkinController instance;

    private void Awake() {
        if (instance != null) {
            Debug.LogWarning("More than one Inventory of character skin controller found!");
            return;
        }
        instance = this;
    }

    #endregion
    
    Animator animator;
    Renderer[] characterMaterials;

    public Texture2D[] albedoList;
    [ColorUsage(true,true)]
    public Color[] eyeColors;

    public enum EyePosition {
        normal,
        happy,
        angry,
        dead
    }
    public Dictionary<EyePosition, string> mappingEyePosition = new Dictionary<EyePosition, string>()
    {
        {EyePosition.normal, "normal"},
        {EyePosition.happy, "happy"},
        {EyePosition.angry, "angry"},
        {EyePosition.dead, "dead"}
    };
    EyePosition eyeState;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterMaterials = GetComponentsInChildren<Renderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEyes();
    }

    public void UpdateEyes(EyePosition? position = null) {
        if ((position != null && position == EyePosition.normal) || Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeMaterialSettings(0);
            ChangeEyeOffset(EyePosition.normal);
            ChangeAnimatorIdle("normal");
            eyeState = EyePosition.normal;
        }
        if ((position != null && position == EyePosition.angry) || Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeMaterialSettings(1);
            ChangeEyeOffset(EyePosition.angry);
            ChangeAnimatorIdle("angry");
            eyeState = EyePosition.angry;
        }
        if ((position != null && position == EyePosition.happy) || Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeMaterialSettings(2);
            ChangeEyeOffset(EyePosition.happy);
            ChangeAnimatorIdle("happy");
            eyeState = EyePosition.happy;
        }
        if ((position != null && position == EyePosition.dead) || Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeMaterialSettings(3);
            ChangeEyeOffset(EyePosition.dead);
            ChangeAnimatorIdle("dead");
            eyeState = EyePosition.dead;
        }
    }

    void ChangeAnimatorIdle(string trigger)
    {
        animator.SetTrigger(trigger);
    }

    void ChangeMaterialSettings(int index)
    {
        for (int i = 0; i < characterMaterials.Length; i++)
        {
            if (characterMaterials[i].transform.CompareTag("PlayerEyes"))
                characterMaterials[i].material.SetColor("_EmissionColor", eyeColors[index]);
            else
                characterMaterials[i].material.SetTexture("_MainTex",albedoList[index]);
        }
    }

    void ChangeEyeOffset(EyePosition pos)
    {
        Vector2 offset = Vector2.zero;

        switch (pos)
        {
            case EyePosition.normal:
                offset = new Vector2(0, 0);
                break;
            case EyePosition.happy:
                offset = new Vector2(.33f, 0);
                break;
            case EyePosition.angry:
                offset = new Vector2(.66f, 0);
                break;
            case EyePosition.dead:
                offset = new Vector2(.33f, .66f);
                break;
            default:
                break;
        }

        for (int i = 0; i < characterMaterials.Length; i++)
        {
            if (characterMaterials[i].transform.CompareTag("PlayerEyes"))
                characterMaterials[i].material.SetTextureOffset("_MainTex", offset);
        }
    }
    
    public EyePosition EyeState
    {
        get {
            return eyeState;
        }
    }
}
