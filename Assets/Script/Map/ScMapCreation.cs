using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScMapCreation : MonoBehaviour {
    [Header("~~~~~~Generation info~~~~~~")]
    [SerializeField] int _width;
    [SerializeField] int _levelHeight;
    [SerializeField] float _chanceSpawn;
    [SerializeField] float _chanceSpawnBreakable;
    [SerializeField] float _chanceSpawnCrate;
    [SerializeField] int _seed;
    [SerializeField] int _seed2;


    [Header("~~~~~~Generation Componment~~~~~~")]
    public GameObject block;
    public Transform playerTrans;

    [Header("~~~~~~Parents~~~~~~")]
    public Transform parentNormal;
    public Transform parentSemi;
    public Transform parentCrate;
    public Transform parentBreakable;

    private void Start() {
        _seed = Random.Range(-100000, 10000);
        _seed2 = Random.Range(-100000, 10000);
        Random.InitState(_seed);
        Random.InitState(_seed2);
        GenerateMap();
    }

    private void GenerateMap(){
        for (int y = (int)playerTrans.position.y + 10; y > -_levelHeight; y--){
            for (int x = -_width; x < _width + 1; x++){
                float noise1 = Mathf.PerlinNoise(x * 0.2f + _seed, y * 0.2f + _seed);
                float noise2 = Mathf.PerlinNoise(x * 0.2f + _seed2, y * 0.2f + _seed2);

                if (noise1 > _chanceSpawn) {
                    RaycastHit2D hit = Physics2D.Raycast(new Vector2(x, y + 1), Vector2.up, 1f);
                    if (hit.collider == null){
                        if (Random.value < _chanceSpawnCrate) {
                            Generate(new Vector2(x, y + 1), ScGround.BlockType.crate);
                        }
                        else{
                            if (noise2 > _chanceSpawnBreakable) { 
                                Generate(new Vector2(x, y + 1), ScGround.BlockType.breakable);
                            }
                            else{
                                Generate(new Vector2(x, y + 1), ScGround.BlockType.normal);
                            }
                        }
                    }
                    if (noise2 > _chanceSpawnBreakable) {
                        Generate(new Vector2(x, y), ScGround.BlockType.breakable);
                    }
                    else {
                        Generate(new Vector2(x, y), ScGround.BlockType.normal);
                    }
                }
            }
        }
    }

    private void Generate(Vector2 pos, ScGround.BlockType blockType) {
        GameObject _newBlock = Instantiate(block, pos, Quaternion.identity);
        _newBlock.GetComponent<ScGround>().type = blockType;
        switch (blockType) {
            case ScGround.BlockType.crate:
                _newBlock.transform.parent = parentCrate;
                break;
            case ScGround.BlockType.breakable:
                _newBlock.transform.parent = parentBreakable;
                break;
            case ScGround.BlockType.normal:
                _newBlock.transform.parent = parentNormal;
                break;
            case ScGround.BlockType.semi:
                _newBlock.transform.parent = parentSemi;
                break;
        }
    }


}