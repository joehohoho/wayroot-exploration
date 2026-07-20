using UnityEngine;

namespace Wayroot.Combat
{
    public sealed class PrototypeWorldHealthBar : MonoBehaviour
    {
        private PrototypeEnemy _enemy = null!;
        private Transform _fill = null!;

        public void Configure(PrototypeEnemy enemy, Transform fill)
        {
            _enemy = enemy;
            _fill = fill;
        }

        private void Update()
        {
            float ratio = _enemy.IsDefeated ? 0f : (float)_enemy.Health / _enemy.MaxHealth;
            _fill.GetComponent<Renderer>().enabled = !_enemy.IsDefeated;
            _fill.localScale = new Vector3(ratio, 1f, 1f);
            _fill.localPosition = new Vector3((ratio - 1f) * 0.5f, 1.8f, 0f);
        }
    }
}
