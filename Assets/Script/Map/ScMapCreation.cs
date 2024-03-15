using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScMapCreation : MonoBehaviour {
    [Header("Generation info")]
    [SerializeField] int _width;
    [SerializeField] int _levelHeight;
    [SerializeField] float _chanceSpawn;
    [SerializeField] int _seed;
    [SerializeField] float _chanceSpawnCrate;

    [Header("Generation Componment")]
    public GameObject block;
    public Transform playerTrans;

    private void Start() {
        _seed = Random.Range(-100000, 10000);
        Random.InitState(_seed);
        GenerateMap();
    }

    private void GenerateMap() { 
        for (int y = (int)playerTrans.position.y + 10; y > -_levelHeight; y--) { 
            for (int x = -_width; x < _width +1; x++) {
                float _noise = Mathf.PerlinNoise(x * 0.2f + _seed, y * 0.2f + _seed);
                if( _noise > _chanceSpawn) { 
                    if (x < -2 || x > 2) {
                        RaycastHit2D hit = Physics2D.Raycast(new Vector2(x, y + 1), Vector2.up, 1f);
                        if (hit.collider == null){                            
                            if (Random.value < _chanceSpawnCrate) {
                                GameObject _newBlock = Instantiate(block, new Vector2(x, y+1), Quaternion.identity);
                                _newBlock.GetComponent<ScGround>().type = ScGround.BlockType.crate;
                            }
                            else {
                                GameObject _newBlock1 = Instantiate(block, new Vector2(x, y + 1), Quaternion.identity);
                                _newBlock1.GetComponent<ScGround>().type = ScGround.BlockType.normal;
                            }
                        }
                        GameObject _newBlock2 = Instantiate(block, new Vector2(x, y), Quaternion.identity);
                        _newBlock2.GetComponent<ScGround>().type = ScGround.BlockType.normal;
                    }
                    else { 
                        RaycastHit2D hit = Physics2D.Raycast(new Vector2(x, y + 1), Vector2.up, 1f);
                        if (hit.collider == null){                            
                            if (Random.value < _chanceSpawnCrate) {
                                GameObject _newBlock = Instantiate(block, new Vector2(x, y+1), Quaternion.identity);
                                _newBlock.GetComponent<ScGround>().type = ScGround.BlockType.crate;
                            } 
                            else {
                                GameObject _newBlock2 = Instantiate(block, new Vector2(x, y + 1), Quaternion.identity);
                                _newBlock2.GetComponent<ScGround>().type = ScGround.BlockType.breakable;
                            }
                        }
                        GameObject _newBlock1 = Instantiate(block, new Vector2(x, y), Quaternion.identity);
                        _newBlock1.GetComponent<ScGround>().type = ScGround.BlockType.breakable;
                    }
                }
            }
        }
    }


}