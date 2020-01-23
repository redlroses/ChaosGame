using System.Collections;
using UnityEngine;

public class FractalController : MonoBehaviour
{
    public CustomRenderTexture Texture;
    public Material FractEffectMaterial;

    [SerializeField] private float radius;
    [SerializeField] private int pointsCount;
    [SerializeField] private Transform[] points;
    [SerializeField] [Range(0, 1)] private float proportion;

    [Header ("Speed")]
    [SerializeField] private float lowSpeed;
    [SerializeField] private float midSpeed;

    [Header("Colors")]
    [SerializeField] private Color white;
    [SerializeField] private Color blue;
    [SerializeField] private Color yellow;
    [SerializeField] private Color red;
    [SerializeField] private Color green;
    [SerializeField] private Color purple;

    private Color color;
    private float speed;
    private bool IsFast = true;
    private Coroutine fratcTask;

    private void Start()
    {
        color = white;
        Texture.Initialize();
    }

    public void BeginIterations()
    {
        fratcTask = StartCoroutine(FractalTask());
    }

    public void StopIterations()
    {
        StopCoroutine(fratcTask);
    }

    public void ClearField()
    {
        Texture.Initialize();
    }

    public void SetPointsCount(float value)
    {
        if (value >= 3 && value <= 6)
        {
            pointsCount = (int)value;

            for (int i = 0; i < 6; i++)
            {
                if (i < value)
                {
                    points[i].gameObject.SetActive(true);
                }
                else
                {
                    points[i].gameObject.SetActive(false);
                }
            }
        }
        else
        {
            Debug.LogError($"Incorrect points count: {value}");
        }
    }

    public void SetProportion(float value)
    {
        if (value >= 0 && value <= 1)
        {
            proportion = value;
        }
        else
        {
            Debug.LogError($"Incorrect proportion: {value}");
        }
    }

    public void SetPointSize(float value)
    {
        radius = value / 10;
    }

    public void SetColor(int value)
    {
        switch (value)
        {
            case 0: 
                color = white;
                break;
            case 1:
                color = blue;
                break;
            case 2:
                color = yellow;
                break;
            case 3:
                color = red;
                break;
            case 4:
                color = green;
                break;
            case 5:
                color = purple;
                break;
            default: Debug.LogError($"Unexpected color index {value}");
                break;
        }
    }

    public void SetSpeed(int value)
    {
        switch (value)
        {
            case 0:
                speed = lowSpeed;
                IsFast = false;
                break;
            case 1:
                speed = midSpeed;
                IsFast = false;
                break;
            case 2:
                IsFast = true;
                break;
            default:
                Debug.LogError($"Unexpected Speed Index {value}");
                break;
        }
    }

    IEnumerator FractalTask()
    {
        Vector2 drawPos = new Vector2(5f, 5f);
        Vector2 pointPos = GetPointPosition(pointsCount);
       
        while (true)
        {
            drawPos += Vector2.Scale(pointPos - drawPos, new Vector2(proportion, proportion)); ;
            Draw(drawPos);
            pointPos = GetPointPosition(pointsCount);
            Texture.Update();

            if (IsFast)
            {
                yield return null;
            }
            else
            {
                yield return new WaitForSeconds(speed);
            }
        }
    }

    private Vector2 GetPointPosition(int maxCount)
    {
        int index = Random.Range(0, maxCount);
        return new Vector2(points[index].position.x, points[index].position.y);
    }

    private void Draw(Vector2 pos)
    {
        Vector2 drawPos = new Vector2(pos.x / 10, pos.y / 10);
        FractEffectMaterial.SetFloat("_radius", radius / 100);
        FractEffectMaterial.SetVector("_DrawColor", color);
        FractEffectMaterial.SetVector("_DrawPosition", drawPos);
    }
}
