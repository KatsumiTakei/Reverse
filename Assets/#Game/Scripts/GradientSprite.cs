using UnityEngine;

public class GradientSprite : MonoBehaviour
{
    Material material = null;

    [SerializeField]
    float rank1 = 0f;

    [SerializeField]
    float rank2 = 0f;

    string rank1Str = "_Rank1";
    string rank2Str = "_Rank2";

    void Start()
    {
        material = GetComponent<Renderer>().material;
        Reset();
    }


    void Update()
    {
        if (rank1 < 1f)
        {
            rank1 += Time.deltaTime;
            material.SetFloat(rank1Str, rank1);
        }
        else
        {
            rank2 += Time.deltaTime;
            material.SetFloat(rank2Str, rank2);
        }

    }

    void Reset()
    {
        rank1 = 0f;
        rank2 = 0f;

        material.SetFloat(rank1Str, rank1);
        material.SetFloat(rank2Str, rank2);

    }

}
