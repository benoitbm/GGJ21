using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PositionRange
{
    public float minRange;
    public float maxRange;
}
public class PlacementRandomaizer : MonoBehaviour
{
    // Start is called before the first frame update
    public PositionRange[] XRandomPlacementRangeList;
    public PositionRange[] YRandomPlacementRangeList;
    public PositionRange[] ZRandomPlacementRangeList;
    public bool randomFlip = true;
    public float chanceToShow = 100f;

    void Start()
    {
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        float randomPositionx = GetRandomPosition(XRandomPlacementRangeList);
        float randomPositiony = GetRandomPosition(YRandomPlacementRangeList);
        float randomPositionz = GetRandomPosition(ZRandomPlacementRangeList);

        if (randomPositionx != 0)
            transform.localPosition = new Vector3(randomPositionx, transform.localPosition.y, transform.localPosition.z);

        if (randomPositiony != 0)
            transform.localPosition = new Vector3(transform.localPosition.x, randomPositiony, transform.localPosition.z);

        if (randomPositionz != 0)
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, randomPositionz);

        if (randomFlip)
        {
            bool randomBool= (Random.value > 0.5f);
            if (randomBool)
                transform.Rotate(new Vector3(0, -180, 0));
        }

        if(chanceToShow != 100)
        {
            float randomRange = Random.Range(0, 100);
            if(randomRange > chanceToShow)
            {
                Destroy(this);
            }
        }
    }
    float GetRandomPosition(PositionRange[] randomPlacementRangeList)
    {
        if (randomPlacementRangeList.Length != 0)
        {
            int i = Random.Range(0, randomPlacementRangeList.Length);
            i = Random.Range(0, randomPlacementRangeList.Length);
            return Random.Range(randomPlacementRangeList[i].minRange, randomPlacementRangeList[i].maxRange);
        }
        return 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
