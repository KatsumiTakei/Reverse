using UnityEngine;

public class GradientSprite : MonoBehaviour
{
    Material material = null;

    [SerializeField]
    float runk1 = 0f;

    [SerializeField]
    float runk2 = 0f;

    readonly string runk1Str = "_Runk1";
    readonly string runk2Str = "_Runk2";

    readonly string alphaStr = "_Alpha";

    float alphaGradientSecond = -1f;
    float alphaGradientCnt = 0f;

    void Start()
    {
        material = GetComponent<Renderer>().material;
        Reset();
    }


    void Update()
    {
        if (runk1 < 1f)
        {
            runk1 += Time.deltaTime;
            material.SetFloat(runk1Str, runk1);
        }
        else
        {
            runk2 += Time.deltaTime;
            material.SetFloat(runk2Str, runk2);
        }

        if(alphaGradientSecond > 0f)
        {
            alphaGradientCnt += Time.deltaTime;
            material.SetFloat(alphaStr, Mathf.Lerp(1f, 0f, alphaGradientCnt / alphaGradientSecond));
        }

    }

    public void Reset()
    {
        runk1 = 0f;
        runk2 = 0f;
        alphaGradientSecond = -1;

        material.SetFloat(runk1Str, runk1);
        material.SetFloat(runk2Str, runk2);
        material.SetFloat(alphaStr, 1f);

    }

    public bool IsFinishGradient()
    {
        return (runk2 >= 1f);
    }

    public void SetAlphaGradient(float second)
    {
        Debug.Assert(second > 0f, "invalid value...");
        alphaGradientSecond = second;
    }

}
