using ManyCore0404.CellularAutomaton.Logic;
using System;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Win2DCanvas = Microsoft.Graphics.Canvas;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x411 を参照してください

namespace ManyCore0404.CellularAutomaton
{
  /// <summary>
  /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
  /// </summary>
  public sealed partial class MainPage : Page
  {
    const int SIZE = 512;

    LangtonsLoops _langtonsLoops;

    Color[] _bitmapColors = new Color[SIZE * SIZE];

    readonly Color[] CellColors = {
      Colors.Black, Colors.Blue, Colors.Red, Colors.Green,
      Colors.Yellow, Colors.Magenta, Colors.White, Colors.Cyan,
    };



    public MainPage()
    {
      this.InitializeComponent();

      InitializeLangtonsLoops();
    }

    private void InitializeLangtonsLoops()
    {
      _langtonsLoops = new LangtonsLoops(SIZE);
      UpdateBitmap(_langtonsLoops.Lives);

      _langtonsLoops.PropertyChanged += LangtonsLoops_PropertyChanged;
    }

    private void UpdateBitmap(byte[,] lives)
    {
      int count = 0;
      foreach (var live in lives)
        _bitmapColors[count++] = CellColors[live];

      this.Canvas1.Invalidate();
    }



    private async void StartStopButton_Tapped(object sender, TappedRoutedEventArgs e)
    {
      const string START = "START";
      const string STOP = "STOP";

      var button = sender as Button;
      var label = button.Content as string;
      if (label == START)
      {
        button.Content = STOP;

        await RunLoopsAsync();

        button.Content = START;
      }
      else
      {
        await _langtonsLoops.StopAsync();
      }
    }

    private async void ResetButton_Tapped(object sender, TappedRoutedEventArgs e)
    {
      await _langtonsLoops.StopAsync();
      InitializeLangtonsLoops();
    }



    DateTimeOffset _startTime;
    int _stepCount;
    int _drawCount;

    private async Task RunLoopsAsync()
    {
      _startTime = DateTimeOffset.Now;
      _stepCount = 0;
      _drawCount = 0;

      await _langtonsLoops.RunLoopsAsync();
    }



    Windows.UI.Core.CoreDispatcher _thisDispatcher = Window.Current.Dispatcher;
    bool _isUpdating;

    async void LangtonsLoops_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
      if (e.PropertyName == nameof(LangtonsLoops.Lives))
      {
        _stepCount++;

        if (_isUpdating)
          return;

        _isUpdating = true;

        await _thisDispatcher.RunAsync(
            Windows.UI.Core.CoreDispatcherPriority.Normal,
            () =>
            {
              UpdateBitmap(_langtonsLoops.Lives);

              _drawCount++;
              TimeSpan duration = DateTimeOffset.Now.Subtract(_startTime);
              this.textCycleTime.Text = string.Format("{0:0.0000}秒", duration.TotalMilliseconds / _stepCount / 1000.0);
              int drawRate = (int)(_drawCount * 100.0 / _stepCount);
              this.textDrawRate.Text = string.Format("{0}% ({1}/{2})", drawRate, _drawCount, _stepCount);
            }
          );

        _isUpdating = false;
      }
    }



    Win2DCanvas.CanvasBitmap _win2dBitmap;

    private void CanvasControl_CreateResources(Win2DCanvas.UI.Xaml.CanvasControl sender, Win2DCanvas.UI.CanvasCreateResourcesEventArgs args)
    {
      _win2dBitmap = Win2DCanvas.CanvasBitmap.CreateFromColors(sender, _bitmapColors, SIZE, SIZE);
    }

    private void CanvasControl_Draw(Win2DCanvas.UI.Xaml.CanvasControl sender, Win2DCanvas.UI.Xaml.CanvasDrawEventArgs args)
    {
      _win2dBitmap.SetPixelColors(_bitmapColors);
      args.DrawingSession.DrawImage(_win2dBitmap, 0, 0);
    }
  }
}
