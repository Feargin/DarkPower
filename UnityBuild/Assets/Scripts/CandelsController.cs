using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CandelsController : MonoBehaviour
{
    [SerializeField] private GameObject [] _candles;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _failPanel;
    [SerializeField] private Image _background;
    [SerializeField] private Sprite [] _backgroundSprites;
    public void Start()
    {
        
    }

    private void ChangeCountCandels (int countPower)
    {
        for (int i = 0; i < _candles.Length; i++)
        {
            if(_candles[i].activeSelf == false && i < countPower ) 
            {
                ActivatedAnimations (i);
                _candles[i].SetActive(true);
            }
            else if (_candles[i].activeSelf == false)
            {
                break;
            }
        }
    }
    private IEnumerator ActivatedAnimations (int index)
    {
        yield return new WaitForSeconds(Random.Range(0.1f, 1.5f));
         _candles[index].GetComponent<Animation>().enabled = true;
    }

    private void OnEnable()
    {
        ResourceHolder.Instance.OnResourceChange += ChekcCurrentResource;
    }

    private void OnDisable()
    {
        ResourceHolder.Instance.OnResourceChange -= ChekcCurrentResource;
    }

    private void ChekcCurrentResource(ResourceHolder.ResourceType resourceType, int amount)
    {
        if(ResourceHolder.Instance.GetResource(ResourceHolder.ResourceType.Candles) < 9 
        && ResourceHolder.Instance.GetResource(ResourceHolder.ResourceType.Minions) <= 0 
        && ResourceHolder.Instance.GetResource(ResourceHolder.ResourceType.PowerDise) >= 12)
            _failPanel.SetActive(true);
        if(ResourceHolder.Instance.GetResource(ResourceHolder.ResourceType.Candles) >= 9)
            _winPanel.SetActive(true);
        if(ResourceHolder.ResourceType.Candles == resourceType) 
            ChangeCountCandels(ResourceHolder.Instance.GetResource(ResourceHolder.ResourceType.Candles));
        if(ResourceHolder.Instance.GetResource(ResourceHolder.ResourceType.Candles) > 0) _background.sprite = _backgroundSprites[0];
        if(ResourceHolder.Instance.GetResource(ResourceHolder.ResourceType.Candles) > 3) _background.sprite = _backgroundSprites[1];
        if(ResourceHolder.Instance.GetResource(ResourceHolder.ResourceType.Candles) > 5) _background.sprite = _backgroundSprites[2];
    }

}
