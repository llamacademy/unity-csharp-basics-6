using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class AutoDestroyText : MonoBehaviour
{
    public float AutoDestroyTime;

    protected TextMeshProUGUI Text;
    private float SpawnTime;

    public delegate void DeathEvent(AutoDestroyText Instance, Vector2 Position);
    public DeathEvent OnDeath;

    protected virtual void Update()
    {
        float remainingTime = GetRemainingTime();

        if (remainingTime <= 0)
        {
            gameObject.SetActive(false);
            OnDeath?.Invoke(this, transform.position);
        }
    }

    protected float GetRemainingTime()
    {
        return (SpawnTime + AutoDestroyTime) - Time.time;
    }

    private void Awake()
    {
        Text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        SpawnTime = Time.time;
    }
}