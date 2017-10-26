using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using DialPad.CustomRenderers;

//[assembly:ExportRenderer(typeof(Xamarin.Forms.Button), typeof(DialPad.Droid.Renderers.RoundBtnRenderer))] //use this one to have all buttons inherit from android buttonrenderer instead of appcompat.
[assembly: ExportRenderer(typeof(RoundBtn), typeof(DialPad.Droid.Renderers.RoundBtnRenderer))] 
namespace DialPad.Droid.Renderers
{
    public class RoundBtnRenderer : Xamarin.Forms.Platform.Android.ButtonRenderer
    {

    }
}