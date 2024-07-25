using Microsoft.UI.Input;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakim.View.Controls
{
    class CustomButton : Button
    {
        public CustomButton()
        {
            this.DefaultStyleKey = typeof(Button);
        }

        public void ChangeCursor(InputCursor cursor)
        {
            this.ProtectedCursor = cursor;
        }

    }
}
