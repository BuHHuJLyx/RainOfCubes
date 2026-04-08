using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Rigidbody), typeof(Explosion))]
public class Bomb : Item<Bomb>
{
    [SerializeField] private float _explosionRadius = 5f;
    [SerializeField] private float _explosionForce = 500f;
    
    private Explosion _explosion;
    private Material _material;
    private Color _color;

    protected override void Awake()
    {
        base.Awake();

        _explosion = GetComponent<Explosion>();

        _material = Renderer.material;
        _color = _material.color;
        
        SetFade();
    }

    private void SetFade()
    {
        _material.SetFloat("_Mode", 2);
        
        _material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        _material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        _material.SetInt("_ZWrite", 0);
        
        _material.DisableKeyword("_ALPHATEST_ON");
        _material.EnableKeyword("_ALPHABLEND_ON");
        _material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        
        _material.renderQueue = 3000;
    }

    protected override IEnumerator LifeRoutine()
    {
        float lifeTime = Random.Range(MinLifeTime, MaxLifeTime);
        float timer = 0f;

        while (timer < lifeTime)
        {
            timer += Time.deltaTime;
            
            float alpha = Mathf.Lerp(1f, 0f, timer / lifeTime);
            
            _color.a = alpha;
            _material.color = _color;
            
            yield return null;
        }
        
        _explosion.Explode(transform.position, _explosionRadius, _explosionForce);
        
        LifeEnded?.Invoke(this);
    }
}