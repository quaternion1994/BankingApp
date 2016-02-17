// Decompiled with JetBrains decompiler
// Type: BankingApp.Web.BundleConfig
// Assembly: BankingApp.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9A4B3E1F-7AD0-4C56-B678-FB62B9356578
// Assembly location: C:\Users\quate_000\Documents\Visual Studio 2013\Projects\BankingApp\BankingApp.Web\bin\BankingApp.Web.dll

using System.Web.Optimization;

namespace BankingAppNew.Web
{
  public class BundleConfig
  {
    public static void RegisterBundles(BundleCollection bundles)
    {
      bundles.Add(new ScriptBundle("~/bundles/modernizr").
          Include("~/Scripts/modernizr-*", new IItemTransform[0]));
      bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"
                ));

      bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/site.css"
                ));
    }
  }
}
