using Inventory.Model;
using Inventory.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipSlotMouseHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject equipHoverPanel;
    [SerializeField] private TMP_Text hoverTitle;
    [SerializeField] private TMP_Text hoverStats;
    [SerializeField] private GameObject PlayerObj;
    [SerializeField] private Canvas canvas;

    private EquippedMeleeSlot equippedMeleeSlot;

    private bool isMouseOver = false;

    private PlayerWeapon weaponHandler;
    private RectTransform panelTransform;

    private void Awake()
    {
        canvas = transform.root.GetComponent<Canvas>();
    }

    private void Start()
    {
        equippedMeleeSlot = this.gameObject.GetComponent<EquippedMeleeSlot>();
        weaponHandler = PlayerObj.GetComponent<PlayerWeapon>();
        panelTransform = equipHoverPanel.GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (isMouseOver && !equipHoverPanel.activeSelf)
        {
            equipHoverPanel.SetActive(true);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StringBuilder sb = new StringBuilder();
        List<ItemParameter> weaponState = weaponHandler.weaponCurrentState;
        List<ItemParameter> weaponParameters = weaponHandler.weaponParameters;

        for (int i = 0; i < weaponState.Count; i++)
        {
            ItemParameter state = weaponState[i];
       
            switch (state.itemParameter.ParameterName)
            {
                case "Durability":
                    float maxDurability = 0f;
                    for (int j = 0; j < weaponParameters.Count; j++)
                    {
                        if (weaponParameters[j].itemParameter.ParameterName == "Durability")
                            maxDurability = weaponParameters[j].value;
                    }
                    sb.AppendLine(
                        $"{state.itemParameter.ParameterName}" +
                        $": {state.value} / " +
                        $"{maxDurability}"
                    );
                    break;

                case "Melee Damage":
                    sb.AppendLine(
                        $"{state.itemParameter.ParameterName}" +
                        $": {state.value}"
                    );
                    break;

                case "Critical Chance":
                    sb.AppendLine(
                        $"{state.itemParameter.ParameterName}" +
                        $": {state.value}%"
                    );
                    break;

                case "Critical Multiplier":
                    sb.AppendLine(
                        $"{state.itemParameter.ParameterName}" +
                        $": +{state.value}x"
                    );
                    break;
            }

            hoverTitle.text = weaponHandler.GetWeapon().Name;
            hoverStats.text = sb.ToString();
        }

        isMouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        equipHoverPanel.SetActive(false);
        isMouseOver = false;
    }

}
