using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private Sprite _spriteOffice;
    [SerializeField] private Sprite _spriteWard;
    [Space]
    [SerializeField] private OfficeLevel _officeLevel;
    [SerializeField] private WardLevel _wardLevel;

    private void Awake()
    {
        _background.sprite = _spriteOffice;
        _officeLevel.gameObject.SetActive(true);
        _officeLevel.OnEndLevel += OpenWard;
    }

    private void OpenWard()
    {
        _background.sprite = _spriteWard;
        _officeLevel.gameObject.SetActive(false);
        _wardLevel.gameObject.SetActive(true);
    }
}
