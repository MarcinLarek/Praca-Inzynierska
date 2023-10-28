using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class HooverEvent : MonoBehaviour, IPointerEnterHandler
{
    public TextMeshProUGUI header;
    public TextMeshProUGUI description;
    public InfoType info;

    private string infoHeader;
    private string infoDescription;

    public enum InfoType
    {
        HP,
        AP,
        STR,
        END,
        AGI,
        LUCK,
        INT,
        ClassDPS,
        ClassSUPPORT,
        ClassTANK
    }

    private void Start()
    {
        UpdateInfo();
    }

    private void UpdateInfo()
    {
        switch (info)
        {
            case InfoType.HP: infoHeader = InfoDescriptions.labelHp; infoDescription = InfoDescriptions.descriptionHp; break;
            case InfoType.AP: infoHeader = InfoDescriptions.labelAP; infoDescription = InfoDescriptions.descriptionAP; break;
            case InfoType.STR: infoHeader = InfoDescriptions.labelStr; infoDescription = InfoDescriptions.descriptionStr; break;
            case InfoType.END: infoHeader = InfoDescriptions.labelEnd; infoDescription = InfoDescriptions.descriptionEnd; break;
            case InfoType.AGI: infoHeader = InfoDescriptions.labelAgi; infoDescription = InfoDescriptions.descriptionAgi; break;
            case InfoType.LUCK: infoHeader = InfoDescriptions.labelLuck; infoDescription = InfoDescriptions.descriptionLuck; break;
            case InfoType.INT: infoHeader = InfoDescriptions.labelInt; infoDescription = InfoDescriptions.descriptionInt; break;
            case InfoType.ClassDPS: infoHeader = InfoDescriptions.labelClassDMG; infoDescription = InfoDescriptions.descriptionClassDMG; break;
            case InfoType.ClassSUPPORT: infoHeader = InfoDescriptions.labelClassSUPPORT; infoDescription = InfoDescriptions.descriptionClassSUPPORT; break;
            case InfoType.ClassTANK: infoHeader = InfoDescriptions.labelClassTANK; infoDescription = InfoDescriptions.descriptionClassTANK; break;
            default: break;

        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UpdateInfo();
        header.text = infoHeader;
        description.text = infoDescription;
    }
}
