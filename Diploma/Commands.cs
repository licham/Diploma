using System.Windows.Input;

namespace Diploma
{
    public static class Commands
    {
        public static RoutedCommand OpenCommand = new RoutedCommand();
        public static RoutedCommand SaveCommand = new RoutedCommand();
        public static RoutedCommand NextCommand = new RoutedCommand();
        public static RoutedCommand PreviousCommand = new RoutedCommand();
        //public static RoutedCommand SculptCommand = new RoutedCommand();
        //public static RoutedCommand WireframeCommand = new RoutedCommand();

        static Commands()
        {
            OpenCommand.InputGestures.Add(new KeyGesture(Key.O, ModifierKeys.Control));
            SaveCommand.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
            NextCommand.InputGestures.Add(new KeyGesture(Key.N, ModifierKeys.Control));
            PreviousCommand.InputGestures.Add(new KeyGesture(Key.P, ModifierKeys.Control));
            //SculptCommand.InputGestures.Add(new KeyGesture(Key.O, ModifierKeys.Control));
            //WireframeCommand.InputGestures.Add(new KeyGesture(Key.O, ModifierKeys.Control));
        } 
    }
}
