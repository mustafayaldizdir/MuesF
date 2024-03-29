#pragma checksum "C:\Users\yaldi\Documents\Visual Studio 2017\Projects\AppDatabasesGuide\AppDatabasesGuide\Views\Home\Databases.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d5f2de6d603f4bd600aedd6fc661ac7ebef593dd"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Databases), @"mvc.1.0.view", @"/Views/Home/Databases.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/Databases.cshtml", typeof(AspNetCore.Views_Home_Databases))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Users\yaldi\Documents\Visual Studio 2017\Projects\AppDatabasesGuide\AppDatabasesGuide\Views\_ViewImports.cshtml"
using AppDatabasesGuide;

#line default
#line hidden
#line 2 "C:\Users\yaldi\Documents\Visual Studio 2017\Projects\AppDatabasesGuide\AppDatabasesGuide\Views\_ViewImports.cshtml"
using AppDatabasesGuide.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d5f2de6d603f4bd600aedd6fc661ac7ebef593dd", @"/Views/Home/Databases.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3afcd73e6dbb7f55dab211c9f915f44c32210402", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Databases : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<AppDatabasesGuide.Models.AppDatabase>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "AddDatabase", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(58, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\Users\yaldi\Documents\Visual Studio 2017\Projects\AppDatabasesGuide\AppDatabasesGuide\Views\Home\Databases.cshtml"
  
    ViewData["Title"] = "Databases";

#line default
#line hidden
            BeginContext(105, 33, true);
            WriteLiteral("\r\n<h2>Databases</h2>\r\n\r\n<p>\r\n    ");
            EndContext();
            BeginContext(138, 45, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4b9122ba89a1435eaed7cd10d118a8b1", async() => {
                BeginContext(166, 13, true);
                WriteLiteral("Yeni Database");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(183, 217, true);
            WriteLiteral("\r\n</p>\r\n<table class=\"table\">\r\n    <thead>\r\n        <tr>\r\n            <th>Database Adı\r\n            </th>\r\n            <th>Açıklama\r\n            </th>\r\n            <th></th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
            EndContext();
#line 23 "C:\Users\yaldi\Documents\Visual Studio 2017\Projects\AppDatabasesGuide\AppDatabasesGuide\Views\Home\Databases.cshtml"
 foreach (var item in Model) {

#line default
#line hidden
            BeginContext(432, 48, true);
            WriteLiteral("        <tr>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(481, 39, false);
#line 26 "C:\Users\yaldi\Documents\Visual Studio 2017\Projects\AppDatabasesGuide\AppDatabasesGuide\Views\Home\Databases.cshtml"
           Write(Html.DisplayFor(modelItem => item.Name));

#line default
#line hidden
            EndContext();
            BeginContext(520, 24, true);
            WriteLiteral("\r\n                <ul>\r\n");
            EndContext();
#line 28 "C:\Users\yaldi\Documents\Visual Studio 2017\Projects\AppDatabasesGuide\AppDatabasesGuide\Views\Home\Databases.cshtml"
                     foreach (var tables in item.AppDatabaseTables())
                    {


#line default
#line hidden
            BeginContext(640, 28, true);
            WriteLiteral("                        <li>");
            EndContext();
            BeginContext(669, 11, false);
#line 31 "C:\Users\yaldi\Documents\Visual Studio 2017\Projects\AppDatabasesGuide\AppDatabasesGuide\Views\Home\Databases.cshtml"
                       Write(tables.Name);

#line default
#line hidden
            EndContext();
            BeginContext(680, 7, true);
            WriteLiteral("</li>\r\n");
            EndContext();
#line 32 "C:\Users\yaldi\Documents\Visual Studio 2017\Projects\AppDatabasesGuide\AppDatabasesGuide\Views\Home\Databases.cshtml"

                    }

#line default
#line hidden
            BeginContext(712, 74, true);
            WriteLiteral("                   </ul>\r\n</td>foreach\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(787, 46, false);
#line 37 "C:\Users\yaldi\Documents\Visual Studio 2017\Projects\AppDatabasesGuide\AppDatabasesGuide\Views\Home\Databases.cshtml"
           Write(Html.DisplayFor(modelItem => item.Description));

#line default
#line hidden
            EndContext();
            BeginContext(833, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(889, 46, false);
#line 40 "C:\Users\yaldi\Documents\Visual Studio 2017\Projects\AppDatabasesGuide\AppDatabasesGuide\Views\Home\Databases.cshtml"
           Write(Html.DisplayFor(modelItem => item.CreatedDate));

#line default
#line hidden
            EndContext();
            BeginContext(935, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(991, 61, false);
#line 43 "C:\Users\yaldi\Documents\Visual Studio 2017\Projects\AppDatabasesGuide\AppDatabasesGuide\Views\Home\Databases.cshtml"
           Write(Html.ActionLink("Edit", "UpdateDatabase", new { id=item.Id }));

#line default
#line hidden
            EndContext();
            BeginContext(1052, 129, true);
            WriteLiteral(" | <a href=\"/Home/DeleteDatabase\" onclick=\"return confirm(\'Silinecek Emin Misiniz?\')\">Sil</a>\r\n            </td>\r\n        </tr>\r\n");
            EndContext();
#line 46 "C:\Users\yaldi\Documents\Visual Studio 2017\Projects\AppDatabasesGuide\AppDatabasesGuide\Views\Home\Databases.cshtml"
}

#line default
#line hidden
            BeginContext(1184, 24, true);
            WriteLiteral("    </tbody>\r\n</table>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<AppDatabasesGuide.Models.AppDatabase>> Html { get; private set; }
    }
}
#pragma warning restore 1591
