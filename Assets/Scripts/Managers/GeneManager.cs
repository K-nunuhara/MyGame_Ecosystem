using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneManager : MonoBehaviour
{
    // Singleton
    public static GeneManager instance { get; private set; }

    public Gene Inherite(Gene momGene, Gene dadGene)
    {
        // TODO
        return null;
    }

    public Genders.Type RundomGender()
    {
        Genders.Type gender;
        float val = Random.Range(0f, 1f);
        if (val <= 0.45)
        {
            gender = Genders.Type.Female;
        }
        else
        {
            gender = Genders.Type.Male;
        }
        return gender;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
