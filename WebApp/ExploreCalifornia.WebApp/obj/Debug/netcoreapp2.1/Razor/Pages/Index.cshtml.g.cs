#pragma checksum "C:\Users\Work\source\repos\RabbitMQTest\ExploreCalifornia.WebApp\Pages\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a8b84e80ff3aa8ede5cd1415eaf99040c3443d96"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(ExploreCalifornia.WebApp.Pages.Pages_Index), @"mvc.1.0.razor-page", @"/Pages/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure.RazorPageAttribute(@"/Pages/Index.cshtml", typeof(ExploreCalifornia.WebApp.Pages.Pages_Index), null)]
namespace ExploreCalifornia.WebApp.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Users\Work\source\repos\RabbitMQTest\ExploreCalifornia.WebApp\Pages\_ViewImports.cshtml"
using ExploreCalifornia.WebApp;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a8b84e80ff3aa8ede5cd1415eaf99040c3443d96", @"/Pages/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"889949180b08da63ba2a27ee60b067df1ebfab35", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Index : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 3 "C:\Users\Work\source\repos\RabbitMQTest\ExploreCalifornia.WebApp\Pages\Index.cshtml"
  
    Layout = "_HomePageLayout";
    ViewData["Title"] = "Home page";

#line default
#line hidden
            BeginContext(104, 1296, true);
            WriteLiteral(@"
<article id=""mainArticle""> 
    <h1>Tour Spotlight</h1>
    <p class=""spotlight"">This month's spotlight package is Cycle California. Whether you are looking for some serious downhill thrills to a relaxing ride along the coast, you'll find something to love in Cycle California.<br /> <span class=""accent""><a href=""tours_cycle.htm"" title=""Cycle California"">tour details</a></span></p>
    <h1>Explorer's Podcast</h1>
    <video controls poster=""_video/podcast_poster.jpg"" width=""512"" height=""288"" preload=""none"">
        <source src=""_video/podcast_teaser.mp4"" type=""video/mp4"" />
        <source src=""_video/podcast_teaser.webm"" type=""video/webm"" />
        <source src=""_video/podcast_teaser.theora.ogv""type=""video/ogg"" />
    </video>
    <p class=""videoText"">Join us each month as we publish a new Explore California video podcast, with featured tours, customer photos, and some exciting new features<span class=""accent""><a href=""_video/EC_podcast_full version.mov"" title=""Video Podcast"">Watch the full video</");
            WriteLiteral(@"a></span></p>
    <p class=""videoText""><span class=""accent""><a href=""_video/EC_podcast_full version.mp3"" title=""Listen to audio podcast"">Listen to audio podcast</a><a href=""_video/EC_podcast_full version.mov"" title=""Video Podcast""><br>
    </a></span></p>
	
</article>");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IndexModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<IndexModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<IndexModel>)PageContext?.ViewData;
        public IndexModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
