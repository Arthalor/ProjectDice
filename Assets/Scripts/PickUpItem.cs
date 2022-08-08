using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;

public class PickUpItem : MonoBehaviour
{
    [SerializeField]
    [Tooltip("squared distance")]
    private float minDistance = default;
    [SerializeField]
    private SpriteRenderer sRender = default;
    [SerializeField]
    private Dice dice = default;

    private void Start()
    {
        dice = new Dice(true);
        sRender.sprite = Manager.Instance.diceIcons[0];
    }

    private void Update()
    {
        float sqrDisctanceToPlayer = VectorFunctions.SqrDistance(Manager.Instance.playerTransform.position, transform.position);
        if (sqrDisctanceToPlayer <= minDistance)
        {
            sRender.color = Color.Lerp(Color.white, Color.gray, Mathf.PingPong(Time.time * 2, 1));
            if (Input.GetButtonDown("Interact"))
            {
                Manager.Instance.playerStats.diceInventory = new Dice(true);

                GetComponent<Collider2D>().enabled = false;
                sRender.sprite = null;
                Destroy(gameObject, 4);
            }
        }
        else sRender.color = Color.white;
    }
}