using System.Collections;
using UnityEngine;

namespace _Elementa.Attack
{
    
public class PeriodicDamageEffect : MonoBehaviour
{
    [Header("Damage Settings")]
    public float damagePerTick = 10f; // Урон за тик
    public float tickInterval = 1f; // Интервал между тиками в секундах
    public float effectDuration = 5f; // Общая длительность эффекта

    [Header("Visual Effect")]
    public GameObject visualEffectPrefab; // Префаб визуального эффекта (например, огонь)
    private GameObject instantiatedEffect; // Ссылка на созданный эффект

    private IDamageable target; // Цель, которая будет получать урон
    private Coroutine damageCoroutine; // Ссылка на запущенную корутину

    private float elapsedTime = 0f; // Время с момента запуска эффекта

    // Метод для запуска эффекта
    public void ApplyEffect(IDamageable damageableTarget, Transform targetTransform)
    {
        // Проверяем, чтобы не было дубликатов
        if (target != null) return;

        target = damageableTarget;

        // Создаем визуальный эффект
        if (visualEffectPrefab != null && targetTransform != null)
        {
            instantiatedEffect = Instantiate(visualEffectPrefab, targetTransform);
            instantiatedEffect.transform.localPosition = Vector3.zero;
        }

        // Запускаем корутину урона
        damageCoroutine = StartCoroutine(DamageOverTime());
    }

    private IEnumerator DamageOverTime()
    {
        elapsedTime = 0f;

        while (elapsedTime < effectDuration)
        {
            // Наносим урон цели
            target?.Damage(damagePerTick);

            // Ждем интервал между тиками
            yield return new WaitForSeconds(tickInterval);

            elapsedTime += tickInterval;
        }

        // Завершаем эффект
        EndEffect();
    }

    private void EndEffect()
    {
        // Удаляем визуальный эффект
        if (instantiatedEffect != null)
        {
            Destroy(instantiatedEffect);
        }

        // Сбрасываем цель
        target = null;

        // Останавливаем корутину, если она все еще работает
        if (damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
        }
    }

    // Метод для досрочного завершения эффекта
    public void InterruptEffect()
    {
        if (damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
        }
        EndEffect();
    }

    // Методы для изменения параметров во время выполнения
    public void SetDamagePerTick(float newDamage)
    {
        damagePerTick = newDamage;
    }

    public void SetTickInterval(float newInterval)
    {
        tickInterval = newInterval;

        // Перезапуск эффекта с новым интервалом
        if (damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = StartCoroutine(DamageOverTime());
        }
    }

    public void SetEffectDuration(float newDuration)
    {
        effectDuration = newDuration;
    }

    // Метод для проверки оставшегося времени действия эффекта
    public float GetRemainingDuration()
    {
        return Mathf.Max(effectDuration - elapsedTime, 0f);
    }
}
}