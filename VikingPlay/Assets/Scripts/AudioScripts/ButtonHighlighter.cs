using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHighlighter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _onPointerEnterSound;
    [SerializeField] private AudioClip _onPointerExitSound;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        _audioSource.clip = _onPointerEnterSound;
        _audioSource.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _audioSource.clip = _onPointerExitSound;
        _audioSource.Play();
    }
}
