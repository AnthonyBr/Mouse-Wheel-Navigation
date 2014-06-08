using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text.Editor;
using System;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace MouseWheelNavigation
{
    public class MouseWheelNavigation : MouseProcessorBase
    {
        private IWpfTextView _view;

        private int currentDelta = 0;

        public MouseWheelNavigation(IWpfTextView view)
        {
            _view = view;
        }

        public override void PreprocessMouseWheel(MouseWheelEventArgs e)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Shift) == 0)
            {
                return;
            }

            currentDelta += e.Delta;
    
            var step = currentDelta / Mouse.MouseWheelDeltaForOneLine;

            if (step != 0)
            {
                currentDelta -= (step * Mouse.MouseWheelDeltaForOneLine);

                var dte = ServiceProvider.GlobalProvider.GetService(typeof(DTE)) as DTE2;

                string command = step < 0 ? "View.NavigateBackward" : "View.NavigateForward";
                
                try
                {
                    for (var i = 0; i < Math.Abs(step); i++)
                    {
                        dte.ExecuteCommand(command);
                    }
                }
                catch (COMException)
                {
                    // 戻れない/進めない時にコマンドを呼び出すと COMException (E_FAIL) が発生する。
                    // 回避方法が不明なため、とりあえず例外をつぶす。
                }
            }

            e.Handled = true;
        }
    }
}
