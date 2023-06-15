using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
namespace Notes.Interface
{
    public interface Interface
    {
        // установка цвета статус-бара и определение его стиля
        void SetStatusBarColor(Color color, bool darkStatusBarTint);

        // color - объект типа Color, представляющий цвет, который нужно установить для статус-бара.
        // darkStatusBarTint - логическое значение, указывающее, должен ли текст статус-бара быть темным(true) или светлым(false).
    }
}
