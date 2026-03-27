using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWall : MonoBehaviour,IItemWallInst
{
    public float speed = 80f;

    private void Awake()
    {

    }

    public int currentValue;
    public ItemAttributeType currentItemAttribute;
    public ItemWall partnerWall;

    public void RecyclePair()
    {
        ItemWall partner = partnerWall;
        partnerWall = null;

        if (partner != null && partner.gameObject.activeInHierarchy)
        {
            partner.partnerWall = null;
            partner.RecyclePair();
        }

        PoolItemSource poolSource = GetComponent<PoolItemSource>();
        if (poolSource != null && ObjectPool.Instance != null && poolSource.sourcePrefab != null)
        {
            ObjectPool.Instance.Release(poolSource.sourcePrefab, gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Initialize(ItemWallData data, ItemAttributeType itemType, Sprite itemSprite)
    {
        this.currentItemAttribute = itemType;

        if (itemType == ItemAttributeType.Health)
        {
            currentValue = 1; // Health 牆只能是 1
        }
        else
        {
            currentValue = Random.Range(1, 6);
        }

        if (data.wallType == ItemWallType.Bad)
        {
            currentValue = -currentValue; 
        }

        string displayText = currentValue > 0 ? "+" + currentValue.ToString() : currentValue.ToString();

        Renderer mainRenderer = GetComponent<Renderer>();
        if (mainRenderer == null)
        {
            foreach (Renderer r in GetComponentsInChildren<Renderer>())
            {
                if (r.GetComponent<TMPro.TextMeshPro>() == null)
                {
                    mainRenderer = r;
                    break;
                }
            }
        }

        if (mainRenderer != null && data.WallMaterial != null)
        {
            mainRenderer.sharedMaterial = data.WallMaterial;
        }

        TMPro.TextMeshPro textMesh = GetComponentInChildren<TMPro.TextMeshPro>();
        if (textMesh != null)
        {
            textMesh.text = displayText;
        }

        TMPro.TextMeshProUGUI uiText = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        if (uiText != null)
        {
            uiText.text = displayText;
        }

        if (itemSprite != null)
        {
            SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = itemSprite;
            }
            else
            {
                UnityEngine.UI.Image uiImage = GetComponentInChildren<UnityEngine.UI.Image>();
                if (uiImage != null)
                {
                    uiImage.sprite = itemSprite;
                }
            }
        }
    }
    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
