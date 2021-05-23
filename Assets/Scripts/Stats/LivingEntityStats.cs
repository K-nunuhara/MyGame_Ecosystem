using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntityStats : ScriptableObject
{
    public float MAX_HEALTH = 100;
    public float MIN_HEALTH = 0;
    public float BASE_CALORIE;
    public float MAX_CALORIE;
    public float MIN_CALORIE;
    public float CONSUME_CALORIE;
    public float BASE_WATER;
    public float MAX_WATER;
    public float MIN_WATER;
    public float CONSUME_WATER;
    // TODO: Implement Biomes
    //public Biomes[] LIKE_BIOMES;
    //public Biomes[] DISLIKE_BIOMES;
    //public Species[] LIKE_SPECIES;
    //public Species[] DISLIKE_SPECIES;
}
