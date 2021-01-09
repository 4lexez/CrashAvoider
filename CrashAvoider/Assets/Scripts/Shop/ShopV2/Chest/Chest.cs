using System;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class Chest : MonoBehaviour
{
    [SerializeField] private int Price;

    [SerializeField] private SkinContainer skinContainer;     //ссылка на скинконтейнер, там все скины
     //  Для примера возьмем такие числа
    [SerializeField] private float shanceUsual; //70              
    [SerializeField] private float shanceRare;  //90               
    [SerializeField] private float shanceMystic; //98
    [SerializeField] private float shanceLegendary; //100

    public static GameObject buffer = null;  // буфер объекта, что б потом удалять было удобнее
    [SerializeField] private Transform Show;  // место, куда ставится скин, что б визуально был, так проще всего, пока, не сделаю кастомные шины
    [SerializeField] private Animation anim;   // понятное дело

    Random random = new Random();          //более нормальный рандом

    private void OnMouseDown()
    {
        Open();
    }
    public void Open()
    {
        if (CountCoins.Coin >= Price)
        {
            CountCoins.Coin = -Price;
            CountCoins.Save();
        } 

            int WinNum = random.Next(1, 101);       // генерирует число, например 45

            if (WinNum <= shanceUsual)                                               // проверяем число 45, меньше оно чем 70? если да, то
            {
            Roll(skinContainer.UsualSkins.Length, skinContainer.UsualSkins);         // ролим между баттанскинами и выбираем между обычных, возьмем другое число 89, оно меньше 70? нет, значит елсе иф срабатывает
            }
            else if (WinNum <= shanceRare)                       //Оно равно или меньше шанса рарки? 89 меньше 90, поетому да
            {
            Roll(skinContainer.RareSkins.Length, skinContainer.RareSkins);                           // роллим
            }
            else if (WinNum<=shanceMystic && WinNum >= shanceRare)                                            // число 94 меьшн  98 - да, больше 90 - да
            {
            Roll(skinContainer.MysticSkins.Length, skinContainer.MysticSkins);                                   // роллим
            }
            else if (WinNum <= shanceLegendary && WinNum >= shanceMystic)
            {
            Roll(skinContainer.LegendarySkins.Length, skinContainer.LegendarySkins);
            }
    }

    private void Roll(int max, ButtonSkin[] buttons)
    { 
        ButtonSkin skin = buttons[random.Next(0, max)];

        if (skin.isBuy)                                   //проверяем куплен ли скин
        {
            CountCoins.Coin = skin.Price / 2;
            CountCoins.Save();                           // получаем половину стоимости
        }
        else                                         //Нет?
        {
            if (buffer != null) Destroy(buffer);       // проверяем буфер, если что-то есть, удаляем
            skin.Buy();                                //покупаем скин

            buffer = Instantiate(skin.gameObject.transform.GetChild(0).gameObject, Show); // получаем визуально модельку скина
            anim.Play();
         }
    }
}
