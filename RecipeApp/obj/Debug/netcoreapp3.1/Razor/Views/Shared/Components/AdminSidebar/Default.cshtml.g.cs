#pragma checksum "C:\Users\euler\source\repos\RecipeApp\RecipeApp\Views\Shared\Components\AdminSidebar\Default.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "73422885b5bf7aaabea498c84964760f9367640b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Components_AdminSidebar_Default), @"mvc.1.0.view", @"/Views/Shared/Components/AdminSidebar/Default.cshtml")]
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
#nullable restore
#line 1 "C:\Users\euler\source\repos\RecipeApp\RecipeApp\Views\_ViewImports.cshtml"
using RecipeApp;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\euler\source\repos\RecipeApp\RecipeApp\Views\_ViewImports.cshtml"
using RecipeApp.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"73422885b5bf7aaabea498c84964760f9367640b", @"/Views/Shared/Components/AdminSidebar/Default.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0d0f70f5016ecb478cf8c0a7440a26d6c7ed4b82", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Components_AdminSidebar_Default : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<div id=""layoutSidenav_nav"">
    <nav class=""sb-sidenav accordion sb-sidenav-dark"" id=""sidenavAccordion"">
        <div class=""sb-sidenav-menu"">
            <div class=""nav"">
                <div class=""sb-sidenav-menu-heading"">User</div>
                <a class=""nav-link""");
            BeginWriteAttribute("href", " href=\"", 280, "\"", 315, 1);
#nullable restore
#line 7 "C:\Users\euler\source\repos\RecipeApp\RecipeApp\Views\Shared\Components\AdminSidebar\Default.cshtml"
WriteAttributeValue("", 287, Url.Action("index","admin"), 287, 28, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                    Accounts\r\n                </a>\r\n                <a class=\"nav-link\"");
            BeginWriteAttribute("href", " href=\"", 406, "\"", 441, 1);
#nullable restore
#line 10 "C:\Users\euler\source\repos\RecipeApp\RecipeApp\Views\Shared\Components\AdminSidebar\Default.cshtml"
WriteAttributeValue("", 413, Url.Action("roles","admin"), 413, 28, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                    Roles\r\n                </a>\r\n                <div class=\"sb-sidenav-menu-heading\">Recipe</div>\r\n                <a class=\"nav-link collapsed\"");
            BeginWriteAttribute("href", " href=\"", 606, "\"", 643, 1);
#nullable restore
#line 14 "C:\Users\euler\source\repos\RecipeApp\RecipeApp\Views\Shared\Components\AdminSidebar\Default.cshtml"
WriteAttributeValue("", 613, Url.Action("recipes","admin"), 613, 30, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" data-bs-toggle=\"collapse\" data-bs-target=\"#collapseLayouts\" aria-expanded=\"false\" aria-controls=\"collapseLayouts\">\r\n                    Recipes\r\n                </a>\r\n                <a class=\"nav-link collapsed\"");
            BeginWriteAttribute("href", " href=\"", 857, "\"", 897, 1);
#nullable restore
#line 17 "C:\Users\euler\source\repos\RecipeApp\RecipeApp\Views\Shared\Components\AdminSidebar\Default.cshtml"
WriteAttributeValue("", 864, Url.Action("categories","admin"), 864, 33, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" data-bs-toggle=\"collapse\" data-bs-target=\"#collapseLayouts\" aria-expanded=\"false\" aria-controls=\"collapseLayouts\">\r\n                    Recipe Categories\r\n                </a>\r\n            </div>\r\n        </div>\r\n    </nav>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
