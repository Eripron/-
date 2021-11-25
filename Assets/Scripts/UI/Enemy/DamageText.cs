using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    [SerializeField] Text damageText;
    [SerializeField] float speed;
    [SerializeField] float duration;

    float startTime = 0.0f;

    System.Action<DamageText> OnReturn;

    void Update()
    {
        if((startTime += Time.deltaTime) <= duration)
        {
            transform.Translate(Vector2.up * Time.deltaTime * speed);
        }
        else
        {
            ResetText();
            gameObject.SetActive(false);
            OnReturn?.Invoke(this);
        }
    }

    public void OnInit(float _damage, Vector3 worldPos)
    {
        startTime = 0.0f;
        this.transform.position = Camera.main.WorldToScreenPoint(worldPos);
        damageText.text = ((int)_damage).ToString();

        damageText.fontSize = Random.Range(25, 40);

        gameObject.SetActive(true);
    }

    void ResetText()
    {
        damageText.text = string.Empty;
    }

    public void Return(System.Action<DamageText> func)
    {
        OnReturn = func;
    }
}
