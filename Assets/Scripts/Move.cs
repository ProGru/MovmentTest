using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    public PutItemsOnScrean putItemsOnScrean;
    public FisrtPlayerMovment fisrtPlayerMovment;

    private void Start()
    {
        putItemsOnScrean =(PutItemsOnScrean) GetComponent<PutItemsOnScrean>();
        fisrtPlayerMovment = (FisrtPlayerMovment) GetComponent<FisrtPlayerMovment>();
    }
}
