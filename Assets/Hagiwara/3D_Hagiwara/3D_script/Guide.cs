using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide : MonoBehaviour
{
    public GameObject Dice_StartBotton;
    public GameObject Dice_Image;
    public GameObject Dice_Text;


    public GameObject Mass_select;
    public GameObject Mass_Image;

    public GameObject chat;
    public GameObject chat_Image;

    public GameObject Item;
    public GameObject Item_Image;

    public GameObject HopUp_Image;

    public GameObject warp;
    public GameObject warp_Image;

    public GameObject option;
    public GameObject option_Image;

    public GameObject rade;
    public GameObject rade_Image;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Dice_BottonStart()
    {
        Dice_StartBotton.SetActive(true);
        Dice_Image.SetActive(true);
        Dice_StartBotton.GetComponent<Animator>().SetBool("GuideDice", true);
    }

    public void Dice_BottonFinish()
    {
        Dice_StartBotton.GetComponent<Animator>().SetBool("GuideDice", false);
        Dice_StartBotton.SetActive(false);
        Dice_Image.SetActive(false);
    }

    public void MassSelecet_Start()
    {
        Mass_select.SetActive(true);
        Mass_Image.SetActive(true);
        Mass_select.GetComponent<Animator>().SetBool("GuideMass", true);
    }

    public void MassSelecet_Finish()
    {
        Mass_select.GetComponent<Animator>().SetBool("GuideMass", false);
        Mass_select.SetActive(false);
        Mass_Image.SetActive(false);
    }

    public void chat_Start()
    {
        chat.SetActive(true);
        chat_Image.SetActive(true);
        chat.GetComponent<Animator>().SetBool("Guidechat", true);
    }

    public void chat_Finish()
    {
        chat.GetComponent<Animator>().SetBool("Guidechat", false);
        chat.SetActive(false);
        chat_Image.SetActive(false);
    }

    public void Item_Cstart()
    {
        StartCoroutine(Item_Coroutine());
    }

    public void Item_Start()
    {
        Item.SetActive(true);
        Item_Image.SetActive(true);
        Item.GetComponent<Animator>().SetBool("GuideItem", true);
    }

    public void Item_Finish()
    {
        Item.GetComponent<Animator>().SetBool("GuideItem", false);
        Item.SetActive(false);
        Item_Image.SetActive(false);
    }

    IEnumerator Item_Coroutine()
    {
        Item_Start();
        yield return new WaitForSeconds(5);     //1•b‘Ò‚Â
        Item_Finish();
    }

    public void Hopup_Start()
    {
        HopUp_Image.SetActive(true);
    }

    public void Hopup_Finish()
    {
        HopUp_Image.SetActive(false);
    }

    public void warp_BottonStart()
    {
        warp.SetActive(true);
        warp_Image.SetActive(true);
        warp.GetComponent<Animator>().SetBool("Guidewarp", true);
    }

    public void warp_BottonFinish()
    {
        warp.GetComponent<Animator>().SetBool("Guidewarp", false);
        Dice_StartBotton.SetActive(false);
        warp.SetActive(false);
    }

    public void rady_BottonStart()
    {
        warp.SetActive(true);
        warp_Image.SetActive(true);
        warp.GetComponent<Animator>().SetBool("Guiderady", true);
    }

    public void rady_BottonFinish()
    {
        warp.GetComponent<Animator>().SetBool("Guiderady", false);
        Dice_StartBotton.SetActive(false);
        warp.SetActive(false);
    }

    public void option_BottonStart()
    {
        warp.SetActive(true);
        warp_Image.SetActive(true);
        warp.GetComponent<Animator>().SetBool("Guideoption", true);
    }

    public void option_BottonFinish()
    {
        warp.GetComponent<Animator>().SetBool("Guideoption", false);
        Dice_StartBotton.SetActive(false);
        warp.SetActive(false);
    }
}
