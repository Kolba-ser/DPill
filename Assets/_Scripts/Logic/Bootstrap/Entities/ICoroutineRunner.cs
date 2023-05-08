using System.Collections;
using UnityEngine;

namespace Infrastructure.Bootstrap
{
    public interface ICoroutineRunner
    {
        public Coroutine StartCoroutine(IEnumerator coroutine);
        public void StopCoroutine(IEnumerator coroutine);
    }
}