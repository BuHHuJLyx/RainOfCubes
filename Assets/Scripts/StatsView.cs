using TMPro;
using UnityEngine;

public class StatsView<T> : MonoBehaviour where T : Item<T>
{
    [SerializeField] private Spawner<T> _spawner;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private string _name;
    
    private int _spawned;
    private int _created;
    private int _active;

    private void OnEnable()
    {
        _spawner.SpawnedChanged += OnSpawnedChanged;
        _spawner.CreatedChanged += OnCreatedChanged;
        _spawner.ActiveChanged += OnActiveChanged;
    }

    private void OnDisable()
    {
        _spawner.SpawnedChanged -= OnSpawnedChanged;
        _spawner.CreatedChanged -= OnCreatedChanged;
        _spawner.ActiveChanged -= OnActiveChanged;
    }

    private void OnSpawnedChanged(int value)
    {
        _spawned = value;
        UpdateView();
    }
    
    private void OnCreatedChanged(int value)
    {
        _created = value;
        UpdateView();
    }
    
    private void OnActiveChanged(int value)
    {
        _active = value;
        UpdateView();
    }
    
    private void UpdateView()
    {
        _text.text = $"{_name}\n Заспавнено: {_spawned}\n Создано: {_created}\n Активно: {_active}";
    }
}