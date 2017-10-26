# Xam.Forms.ScreenSize

So I inherited a project where I had to create a dialpad of sorts with round buttons. 
Now there are some problems with this.

1. On android buttons inherit from ```C# Xamarin.Forms.Platform.Android.AppCompat.ButtonRenderer ``` which means that in our shared code 
   the buttons on android will ignore borderradius, borderwidth etc and we will end up looking like a normal button.
2. With round buttons we have to set a given height/width togheter widh borderradius this means when a screen size gets smaller or
   larger the UI won't scale.
3. How do we get screensize.

There are probably a bunch of different ways of achieving this and this is just one way of many.

1. Let button inherit from  ```C# Xamarin.Forms.Platform.Android.ButtonRenderer``` instead. 
   Create customrenderer only for android. Now if you want all buttons to inherit from Android.ButtonRenderer instead of AppCompat.Buttonrenderer you
   can uncomment the commented line in the android renderer.
   This class can be found in Android project and the map "Renderers". In the 
```C#
    //[assembly:ExportRenderer(typeof(Xamarin.Forms.Button), typeof(DialPad.Droid.Renderers.RoundBtnRenderer))] //use this one to have all buttons inherit from android buttonrenderer instead of appcompat.
    [assembly: ExportRenderer(typeof(RoundBtn), typeof(DialPad.Droid.Renderers.RoundBtnRenderer))] 
    namespace DialPad.Droid.Renderers
    {
        public class RoundBtnRenderer : Xamarin.Forms.Platform.Android.ButtonRenderer
        {

        }
    }
  ```
  
  2/3. What I did to get the screensize was to in my app.xaml.cs add these 3 properties: 

   ```C#
        public static double DisplayScreenWidth = 0f;
        public static double DisplayScreenHeight = 0f;
        public static double DisplayScaleFactor = 0f;
    ```

      And then in my MainActivity on Android :     

   ```C#
        App.DisplayScreenWidth = (double)Resources.DisplayMetrics.WidthPixels / (double)Resources.DisplayMetrics.Density;
        App.DisplayScreenHeight = (double)Resources.DisplayMetrics.HeightPixels / (double)Resources.DisplayMetrics.Density;
        App.DisplayScaleFactor = (double)Resources.DisplayMetrics.Density;
    ```
    
      And then in AppDelegate in iOS : 
    ```C#
        App.DisplayScreenWidth = (double)UIScreen.MainScreen.Bounds.Width;
        App.DisplayScreenHeight = (double)UIScreen.MainScreen.Bounds.Height;
        App.DisplayScaleFactor = (double)UIScreen.MainScreen.Scale;
    ```

      And then in my code behind for my DialPad (DialPadPage.xaml.cs) I call a function when the page appears to scale the UI and do a bit of coding.
    ```C#              
        private void ScaleUI()
        {
            CreateButtons();
            AddBtnsToUI(_btnList);

            double screenHeight = App.DisplayScreenHeight;
            int size = 0;
            if (screenHeight < 600)
            {
                size = 60;
                SetGridDefinitions(size);
                foreach (var btn in _btnList)
                {
                    SetBtnSize(btn, size);
                }
            }
            else if (screenHeight > 600 && screenHeight < 700)
            {
                size = 70;
                SetGridDefinitions(size);
                foreach (var btn in _btnList)
                {
                    SetBtnSize(btn, size);
                }
            }
            else if (screenHeight > 700)
            {
                size = 80;
                SetGridDefinitions(size);
                foreach (var btn in _btnList)
                {
                    SetBtnSize(btn, size);
                }
            }
        }
      ```        
      
      Now some might say that there should be no code in the code behind when implementing MVVM, but since the code located there is affecting the UI I personally believe it is ok.
      
