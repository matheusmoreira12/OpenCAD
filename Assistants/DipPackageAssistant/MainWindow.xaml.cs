using OpenCAD.OpenCADFormat.Measures;
using OpenCAD.Utils;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DipPackageAssistant
{
    public class ToolInputData
    {
        public ToolInputData(Point cursorPosition)
        {
            CursorPosition = cursorPosition;
        }

        public Point CursorPosition { get; }
    }

    public abstract class Tool
    {
        public Tool()
        {
            Manager = null;
        }

        public abstract void Start();

        public abstract void Cancel();

        public abstract void End();

        public abstract void NextStep();

        public abstract void PreviousStep();

        public abstract void PreviewStep();

        internal ToolManager Manager;
    }

    public sealed class LineTool : Tool
    {
        public LineTool()
        {
            Points = new List<Point> { };
        }

        public override void Cancel()
        {
        }

        public override void End()
        {
        }

        public override void NextStep()
        {
            Points.Add(Manager.MouseState.CursorPosition);
        }

        public override void PreviewStep()
        {
            if (Points.Count == 0) return;

            Points[Points.Count - 1] = Manager.MouseState.CursorPosition;
        }

        public override void PreviousStep()
        {
            if (Points.Count == 0) return;

            Points.RemoveAt(Points.Count - 1);
        }

        public override void Start()
        {
        }

        public List<Point> Points { get; }
    }

    public class ToolMouseState
    {
        public MouseButtonState LeftButtonState { get; internal set; }

        public MouseButtonState MiddleButtonState { get; internal set; }

        public MouseButtonState RightButtonState { get; internal set; }

        public MouseButtonState ButtonX1State { get; internal set; }

        public MouseButtonState ButtonX2State { get; internal set; }

        public Point CursorPosition { get; internal set; }
    }

    public class ToolKeyboardState
    {
        public Key PressedKeys { get; internal set; }
    }

    public sealed class ToolManager
    {
        public ToolManager(Control host)
        {
            Host = host;

            Host.MouseDown += Host_OnMouseDown;
            Host.MouseUp += Host_MouseUp;
            Host.MouseMove += Host_MouseMove;
            Host.MouseDoubleClick += Host_DoubleClick;

            Host.KeyDown += Host_KeyDown;
            Host.KeyUp += Host_KeyUp;

            CurrentTool = null;

            MouseState = new ToolMouseState();
            KeyboardState = new ToolKeyboardState();
        }

        public void StartTool(Tool tool)
        {
            CurrentTool = tool ?? throw new ArgumentNullException(nameof(tool));

            CurrentTool.Manager = this;

            CurrentTool.Start();
        }

        public void CancelTool()
        {
            if (CurrentTool is null) return;

            CurrentTool.Cancel();

            CurrentTool = null;
        }

        public void EndTool()
        {
            if (CurrentTool is null) return;

            CurrentTool.End();

            CurrentTool = null;
        }

        public void NextStep()
        {
            if (CurrentTool is null) return;

            CurrentTool.NextStep();
        }

        public void PreviousStep()
        {
            if (CurrentTool is null) return;

            CurrentTool.PreviousStep();
        }

        public void PreviewStep()
        {
            if (CurrentTool is null) return;

            CurrentTool.PreviewStep();

            Host.InvalidateVisual();
        }

        private void Host_KeyUp(object sender, KeyEventArgs e)
        {
            KeyboardState.PressedKeys &= ~e.Key;

            PreviewStep();
        }

        private void Host_KeyDown(object sender, KeyEventArgs e)
        {
            KeyboardState.PressedKeys |= e.Key;

            switch (e.Key)
            {
                case Key.Escape:
                    CancelTool();
                    break;
                case Key.Return:
                    EndTool();
                    break;
            }

            PreviewStep();
        }

        private void Host_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            PreviewStep();
        }

        private void Host_MouseMove(object sender, MouseEventArgs e)
        {
            MouseState.CursorPosition = e.GetPosition(Host);

            PreviewStep();
        }

        private void Host_MouseUp(object sender, MouseButtonEventArgs e)
        {
            switch (e.ChangedButton)
            {
                case MouseButton.Left:
                    MouseState.LeftButtonState = MouseButtonState.Released;
                    break;

                case MouseButton.Middle:
                    MouseState.MiddleButtonState = MouseButtonState.Released;
                    break;

                case MouseButton.Right:
                    MouseState.RightButtonState = MouseButtonState.Released;
                    break;

                case MouseButton.XButton1:
                    MouseState.ButtonX1State = MouseButtonState.Released;
                    break;

                case MouseButton.XButton2:
                    MouseState.ButtonX2State = MouseButtonState.Released;
                    break;
            }

            if (e.ClickCount == 1)
            {
                //Mouse received a single click

                switch (e.ChangedButton)
                {
                    case MouseButton.Left:
                        NextStep();
                        break;
                    case MouseButton.Right:
                        PreviousStep();
                        break;
                }
            }

            PreviewStep();
        }

        private void Host_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            switch (e.ChangedButton)
            {
                case MouseButton.Left:
                    MouseState.LeftButtonState = MouseButtonState.Pressed;
                    break;

                case MouseButton.Middle:
                    MouseState.MiddleButtonState = MouseButtonState.Pressed;
                    break;

                case MouseButton.Right:
                    MouseState.RightButtonState = MouseButtonState.Pressed;
                    break;

                case MouseButton.XButton1:
                    MouseState.ButtonX1State = MouseButtonState.Pressed;
                    break;

                case MouseButton.XButton2:
                    MouseState.ButtonX2State = MouseButtonState.Pressed;
                    break;
            }

            PreviewStep();
        }

        public Control Host { get; }

        public Tool CurrentTool { get; private set; }

        internal ToolMouseState MouseState { get; private set; }

        internal ToolKeyboardState KeyboardState { get; private set; }
    }

    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var quantity = Quantities.Length;

            var dimension = DerivedQuantityDimension.Parse("l*t^2/t^2");
            var derived = new DerivedQuantity("Area", "A", dimension);

            toolManager = new ToolManager(this);

            toolManager.StartTool(new LineTool());
        }

        ToolManager toolManager;

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (toolManager.CurrentTool is LineTool)
            {
                LineTool lineTool = toolManager.CurrentTool as LineTool;

                Pen pen = new Pen(Brushes.Black, 2);

                for (int i = 0; i < lineTool.Points.Count - 1; i++)
                    drawingContext.DrawLine(pen, lineTool.Points[i], lineTool.Points[i + 1]);
            }
        }
    }
}
