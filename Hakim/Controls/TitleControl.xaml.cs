using Microsoft.UI.Xaml.Controls;

namespace Hakim.Controls;

public sealed partial class TitleControl : UserControl
{
    public TitleControl()
    {
        this.InitializeComponent();
    }

    public TitleControl(string title)
    {
        this.InitializeComponent();
        Title.Text = title;
    }

    public TitleControl(string title, FontIcon icon)
    {
        this.InitializeComponent();
        Title.Text = title;
        this.icon.Glyph = icon.Glyph;
    }
}
