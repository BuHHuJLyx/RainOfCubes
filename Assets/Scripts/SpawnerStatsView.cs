using TMPro;
using UnityEngine;

public class SpawnerStatsView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private string _name;
    
    public void UpdateView(int spawned, int created, int active)
    {
        _text.text = $"{_name}\n Заспавнено: {spawned}\n Создано: {created}\n Активно: {active}";
    }
}