using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private float _highPosY = 1f;
    private float _lowPosY = -1f;

    private float _holeSizeMin = 1f;
    private float _holeSizeMax = 3f;

    [SerializeField]
    private Transform _topObject;
    [SerializeField]
    private Transform _bottomObject;
    [SerializeField]
    private float _widthPadding = 4f;

    public Vector3 SetRandomPlace(Vector3 lastPosition)
    {
        float holeSize = Random.Range(_holeSizeMin, _holeSizeMax);
        float halfHoleSize = holeSize / 2;

        _topObject.localPosition = new Vector3(0, halfHoleSize);
        _bottomObject.localPosition = new Vector3(0, -halfHoleSize);

        Vector3 placePosition = lastPosition + new Vector3(_widthPadding, 0);
        placePosition.y = Random.Range(_lowPosY, _highPosY);

        transform.position = placePosition;

        return placePosition;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        MiniGamePlayer player = collision.GetComponent<MiniGamePlayer>();
        if (player != null)
        {
            player.AddScore(1);
        }
    }


}
