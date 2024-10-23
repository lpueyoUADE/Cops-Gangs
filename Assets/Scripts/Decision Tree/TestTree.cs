using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTree : MonoBehaviour
{
    public int bullets;
    public bool enemySpotted;
    public int hasLife;
    ITreeNode root;

    private void Start()
    {
        InitializeTree();    
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            root.Execute();
        }    
    }

    private void InitializeTree()
    {
        //Acciones
        var shoot = new ActionTree(Shoot);
        var reload = new ActionTree(() =>
        {
            print("Reload");
            bullets++;

        });

        var patrol = new ActionTree(()=> print("Patrol"));
        var death = new ActionTree(()=> print("Death"));

        //Preguntas
        var qHasBullet = new QuestionTree(HasBullets, shoot, reload);
        var qAmmoInClip = new QuestionTree(HasBullets, patrol, reload);
        var qEnemyInView = new QuestionTree(()=> enemySpotted, qHasBullet, qAmmoInClip);
        var qHasLife = new QuestionTree(() => hasLife > 0, qEnemyInView, death);

        root = qHasLife;
    }

    void Shoot()
    {
        print("Shoot");
        bullets--;
    }

    bool HasBullets()
    {
        return bullets > 0;
    }
}
