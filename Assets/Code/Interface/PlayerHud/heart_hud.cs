using Lyra;
using UnityEngine;
using UnityEngine.UI;

namespace Triheroes.Code
{

    public class heart_hud : moon
    {

        RectTransform heart_prefab;
        float heart_size;

        RectTransform hearts_container;
        Image[] hearts;
        float hp = -1;
        float hp_capacity;

        public heart_hud(RectTransform _heart_container, RectTransform _heart_prefab, float _heart_size)
        {
            hearts_container = _heart_container;
            heart_prefab = _heart_prefab;
            heart_size = _heart_size;
        }

        public void set(float HP)
        {
            if (hp == HP) return;
            hp = HP;

            if (HP > hp_capacity)
                populate(HP);

            for (int i = 0; i < hearts.Length; i++)
            {
                float heart_hp = HP - i * 2;
                if (heart_hp >= 2)
                {
                    hearts[i].enabled = true;
                    hearts[i].fillAmount = 1;
                }
                else if (heart_hp > 0)
                {
                    hearts[i].enabled = true;
                    hearts[i].fillAmount = heart_hp / 2;
                }
                else
                {
                    hearts[i].enabled = false;
                }
            }

        }

        public void populate(float _heart_capacity)
        {
            clear();

            hp_capacity = _heart_capacity;
            hearts = new Image[Mathf.CeilToInt(hp_capacity / 2)];

            for (int i = 0; i < hearts.Length; i++)
            {
                hearts[i] = Object.Instantiate(heart_prefab, hearts_container).GetComponent<Image>();
                hearts[i].enabled = false;
                hearts[i].transform.SetParent(hearts_container);
                hearts[i].rectTransform.anchoredPosition = new Vector2(i * heart_size, 0);
            }

        }

        void clear() {
            if (hearts == null) return;
            for (int i = 0; i < hearts.Length; i++)
                Object.Destroy(hearts[i].gameObject);
        }

    }
}