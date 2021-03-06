using System;

using System.Reflection;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Controls;
#if MOONLIGHT_DESKTOP
using Moonlight.Gtk;
#endif
using Mono.MoonDesk;
using MoonBase.Examples;

namespace MyApp
{
  class MainClass
  {
    public static void Main (string[] args)
    {

      Gtk.Application.Init ();
      Mono.MoonDesk.MoonBase.Init();

      var anames = Application.Current.GetType().Assembly.GetReferencedAssemblies();
      foreach ( var aname in anames ){
        Console.WriteLine("load {0}", aname );
        var a = Assembly.Load( aname );
        Mono.MoonDesk.MoonBase.Assemblies.Add( a );
      }
      var asm = System.Reflection.Assembly.LoadFile("/usr/local/lib/mono/moonlight/System.Windows.Controls.dll");
      Mono.MoonDesk.MoonBase.Assemblies.Add( asm );

      IWindow win = new Mono.MoonDesk.AWindow();

      ViewMappings.Resolver =  new ViewLoader( win.Host );


      var homevvm = ViewMappings.Resolver.LoadViewViewModel<HomeViewModel>("/Views;component/Views/Home.xaml");
      win.Content = homevvm.View;

      win.Show ();
      win.Resize( 440,350 );
      Gtk.Application.Run ();
      
    }
  }
}
