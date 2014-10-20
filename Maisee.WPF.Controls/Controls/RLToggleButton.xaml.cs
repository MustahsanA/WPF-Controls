#region external libraries 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
#endregion

namespace Maisee.WPF.Controls.Controls
{
    /// <summary>
    /// RLToggleButton or Right Left Toggle Button is a button extended to have two toggled states apart from UnClicked and dissabled.  
    /// 1st state id un toggled 
    /// 2nd state is toggled from Right mouse button Click
    /// 3nd state is toggled from Left mouse button Click
    /// </summary>
    public partial class RLToggleButton : UserControl
    {

        #region enum decleration
        /// <summary>
        /// Available states for the Right Left Toggle Button
        /// </summary>
        public enum ButtonState
        {
            UnClicked,
            RightToggeled,
            LeftToggeled
        }
        #endregion

        #region Dependency Properties Objects

        /// <summary>
        /// Get/Set the current Button State
        /// </summary>
        public ButtonState State
        {
            get { return (ButtonState)GetValue(ButtonStateProperty); }
            set { SetValue(ButtonStateProperty, value); }
        }

        /// <summary>
        /// Image for unclicked button
        /// </summary>
        public ImageSource NormalImage
        {
            get { return (ImageSource)GetValue(NormalImageProperty); }
            set { SetValue(NormalImageProperty, value); }
        }

        /// <summary>
        /// Image for Toggeled by Right Click
        /// </summary>
        public ImageSource RightToggeledImage
        {
            get { return (ImageSource)GetValue(RightToggeledImageProperty); }
            set { SetValue(RightToggeledImageProperty, value); }
        }

        /// <summary>
        /// Image for Toggeled by Left Click
        /// </summary>
        public ImageSource LeftToggeledImage
        {
            get { return (ImageSource)GetValue(LeftToggeledImageProperty); }
            set { SetValue(LeftToggeledImageProperty, value); }
        }

        /// <summary>
        /// Image to be displayed when Diassabled
        /// </summary>
        public ImageSource DisabledImage
        {
            get { return (ImageSource)GetValue(DisabledImageProperty); }
            set { SetValue(DisabledImageProperty, value); }
        }
        #endregion

        #region Dependency Properties

        /// <summary>
        /// Current State of the button
        /// </summary>
        public static DependencyProperty ButtonStateProperty =
             DependencyProperty.Register("State", typeof(ButtonState), typeof(RLToggleButton),
             new PropertyMetadata(ButtonState.UnClicked, ValueChanged),
             new ValidateValueCallback(RLToggleButton.ValidateValue));

        /// <summary>
        /// Image for untoggeled state
        /// </summary>
        public static readonly DependencyProperty NormalImageProperty =
             DependencyProperty.Register("NormalImage", typeof(ImageSource),
             typeof(RLToggleButton), new UIPropertyMetadata(ImageValueChanged));

        /// <summary>
        /// Image for Right button toggeled state
        /// </summary>
        public static readonly DependencyProperty RightToggeledImageProperty =
            DependencyProperty.Register("RightToggeledImage", typeof(ImageSource),
            typeof(RLToggleButton), new UIPropertyMetadata(ImageValueChanged));


        /// <summary>
        /// Image for left button toggeled state
        /// </summary>
        public static readonly DependencyProperty LeftToggeledImageProperty =
            DependencyProperty.Register("LeftToggeledImage", typeof(ImageSource),
            typeof(RLToggleButton), new UIPropertyMetadata(ImageValueChanged));

        /// <summary>
        /// Image for Dissabled state
        /// </summary>
        public static readonly DependencyProperty DisabledImageProperty =
            DependencyProperty.Register("DisabledImage", typeof(ImageSource),
            typeof(RLToggleButton), new UIPropertyMetadata(ImageValueChanged));

        #endregion

        #region Property Change Events

        /// <summary>
        /// Called when ever the change in Image Values binding are observed
        /// </summary>
        /// <param name="d">DependencyObject</param>
        /// <param name="e">DependencyPropertyChangedEventArgs</param>
        private static void ImageValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RLToggleButton z = d as RLToggleButton;
            if (z != null)
            {
                if (z.NormalImage != null)
                    z.SetImage();
            }
        }

        /// <summary>
        /// Called when ever the change in button state binding are observed
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RLToggleButton z = d as RLToggleButton;
            if (z != null)
            {
                z.SetImage();
            }
        }

        /// <summary>
        /// Called when ever the change in button state binding are observed toi validate the change
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static bool ValidateValue(object value)
        {
            return true;
        }
        #endregion

        /// <summary>
        /// Mouse event for Right Click
        /// </summary>
        public event RoutedEventHandler RightClick;

        /// <summary>
        /// Constructor for the Right Left Toggle Button
        /// </summary>
        public RLToggleButton()
        {
            // initialize controls
            InitializeComponent();
            // attach events
            this.MouseRightButtonDown += new MouseButtonEventHandler(RLToggleButton_MouseRightButtonDown);
            //this.IsEnabledChanged += RLToggleButton_IsEnabledChanged;
        }

        /// <summary>
        /// Button Click Event for left click handle
        /// </summary>
        /// <param name="sender">Sender object reference</param>
        /// <param name="e">RoutedEventArgs</param>
        private void button_Click(object sender, RoutedEventArgs e)
        {
            // left button Clicked 

            // if button was already left enabled then set to normal 
            // else set to left enabled
            if (State == ButtonState.LeftToggeled)
                State = ButtonState.UnClicked;
            else
                State = ButtonState.LeftToggeled;
        }

        /// <summary>
        /// Subclasses can't invoke this event directly, so supply this method
        /// </summary>
        /// <param name="e">MouseButtonEventArgs</param>
        protected void TriggerRightClickEvent(MouseButtonEventArgs e)
        {
            if (RightClick != null)
                RightClick(this, e);
        }

        /// <summary>
        ///  Right buttown down event for right click handle
        /// </summary>
        /// <param name="sender">Sender object reference</param>
        /// <param name="e">MouseButtonEventArgs</param>
        void RLToggleButton_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Right button Clicked 

            // if button was already right enabled then set to normal 
            // else set to left enabled
            if (State == ButtonState.RightToggeled)
                State = ButtonState.UnClicked;
            else
                State = ButtonState.RightToggeled;
        }

        /// <summary>
        /// Change the image depending on the button state 
        /// </summary>
        private void SetImage()
        {
            // first check if button is enabled 
            if (!this.IsEnabled)
            {
                DisplayImage.Source = DisabledImage;
                return;
            }

            // change image for other states
            switch (State)
            {
                case ButtonState.LeftToggeled:
                    DisplayImage.Source = LeftToggeledImage;
                    break;
                case ButtonState.RightToggeled:
                    DisplayImage.Source = RightToggeledImage;
                    break;
                case ButtonState.UnClicked:
                    DisplayImage.Source = NormalImage;
                    break;
                default:
                    break;
            }
        }
    }
}
