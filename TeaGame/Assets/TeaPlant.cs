using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TeaPlant : MonoBehaviour
{
    [SerializeField]private GameManager inventoryManager;

    [SerializeField] private SpriteRenderer mySpriteRenderer;

    [SerializeField]private string _plantName;

    [SerializeField] private float pluckDistance;

    private Color spriteColor;

    private void Awake()
    {
        inventoryManager = GameManager.FindInstance();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        spriteColor = mySpriteRenderer.material.color;
        _plantName = transform.tag;
    }

    private void Update()
    {

    }

    private void OnMouseDown()
    {
        StartCoroutine($"{_plantName}Add");
        gameObject.SetActive(false);
    }

    private IEnumerator HakaAdd()
    {
        inventoryManager.hakaCount++;
        PickPlant();
        //Debug.Log("HakaAdd");
        yield break;
    }

    private IEnumerator TallowAdd()
    {
        inventoryManager.tallowCount++;
        PickPlant();
        //Debug.Log("TallowAdd");
        yield break;
    }

    private IEnumerator BomBomAdd()
    {
        inventoryManager.bombomCount++;
        PickPlant();
        //Debug.Log("BomBomAdd");
        yield break;
    }

    private IEnumerator AileAdd()
    {
        inventoryManager.aileCount++;
        PickPlant();
        //Debug.Log("AileAdd");
        yield break;
    }

    private IEnumerator ShnootAdd()
    {
        inventoryManager.shnootCount++;
        PickPlant();
        //Debug.Log("ShnootAdd");
        yield break;
    }

    private IEnumerator PoffAdd()
    {
        inventoryManager.poffCount++;
        PickPlant();
        //Debug.Log("PoffAdd");
        yield break;
    }

    private void PickPlant()
    {
        gameObject.transform.DOMoveY(.5f, 0.5f).SetEase(Ease.InOutCubic);
        for (float f = 1; f > 0; f -= 0.01f)
        {
            Color c = mySpriteRenderer.material.color;
            c.a = f;
            mySpriteRenderer.material.color = c;
        }
    }

}
