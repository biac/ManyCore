using ManyCore0401.CellularAutomaton.Logic;
using System;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Win2DCanvas = Microsoft.Graphics.Canvas;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x411 を参照してください

namespace ManyCore0401.CellularAutomaton
{
  /// <summary>
  /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
  /// </summary>
  public sealed partial class MainPage : Page
  {
    const int SIZE = 512;

    LangtonsLoops _langtonsLoops;

    Color[] _bitmapColors = new Color[SIZE * SIZE];

    readonly Color[] CellColors =  {
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
    }

    private void UpdateBitmap(int[,] lives)
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
        await StopAsync();
      }
    }

    private async void ResetButton_Tapped(object sender, TappedRoutedEventArgs e)
    {
      await StopAsync();
      InitializeLangtonsLoops();
    }



    private bool _isRunning;
    private bool _isStopped = true;

    private async Task RunLoopsAsync()
    {
      _isRunning = true;
      _isStopped = false;

      DateTimeOffset startTime = DateTimeOffset.Now;
      int count = 0;

      while (_isRunning)
      {
        _langtonsLoops.Update();
        UpdateBitmap(_langtonsLoops.Lives);

        count++;
        TimeSpan duration = DateTimeOffset.Now.Subtract(startTime);
        this.textCycleTime.Text 
          = $"{duration.TotalMilliseconds / count / 1000.0:0.0000}秒";

        await Task.Delay(1); //画面更新の機会を与える (お行儀悪っ!!)
      }

      _isStopped = true;
    }

    private async Task StopAsync()
    {
      _isRunning = false;

      while (!_isStopped)
        await Task.Delay(100);
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
