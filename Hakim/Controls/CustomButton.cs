using Microsoft.UI.Input;
using Microsoft.UI.Xaml.Controls;

namespace Hakim.Controls
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
