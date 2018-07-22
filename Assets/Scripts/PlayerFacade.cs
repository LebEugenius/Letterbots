using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Random = UnityEngine.Random;
using Zenject;

public class PlayerFacade : MonoBehaviour
{
    [SerializeField] private GameObject _graphics;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private AudioClip _attackSound;
    [SerializeField] private LineRenderer Ray;

    private float _timer;
    public void Attack()
    {
        _animator.SetTrigger("Attack");
        _audioSource.PlayOneShot(_attackSound);

        StopCoroutine("ShootRay");
        StartCoroutine(ShootRay(0.1f));
    }

    IEnumerator ShootRay(float seconds)
    {
        var randomHeight = Random.Range(-0.4f, 0.4f);
        var position = Ray.GetPosition(1);
        Ray.SetPosition(1, new Vector3(position.x, randomHeight));
        Ray.enabled = true;
        yield return new WaitForSeconds(seconds);
        Ray.enabled = false;
    }

    public void OnPrepare()
    {
        _animator.SetTrigger("Prepare");
    }

    public void OnVictory()
    {
        _animator.SetTrigger("Win");
    }

    public void OnDefeat()
    {
        _animator.SetTrigger("Lose");
    }

    public void OnRecover()
    {
        _animator.SetTrigger("Recover");
    }
}
