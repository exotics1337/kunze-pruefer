using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace kunze_prüfer.Models
{
    public static class TextBoxChecker
    {
        public static bool AreAllTextBoxesFilled(DependencyObject parent)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is TextBox textBox && string.IsNullOrWhiteSpace(textBox.Text))
                {
                    return false;
                }
                else if (child is DependencyObject && !AreAllTextBoxesFilled(child))
                {
                    return false;
                }
            }
            return true;
        }
    }
}