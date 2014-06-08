using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;

namespace MouseWheelNavigation
{
    [Export(typeof(IMouseProcessorProvider))]
    [Name("Mouse Wheel Processor")]
    [ContentType("code")]
    [TextViewRole(PredefinedTextViewRoles.Interactive)]
    internal sealed class MouseWheelNavigationFactory : IMouseProcessorProvider
    {
        public IMouseProcessor GetAssociatedProcessor(IWpfTextView wpfTextView)
        {
            return new MouseWheelNavigation(wpfTextView);
        }
    }
}
