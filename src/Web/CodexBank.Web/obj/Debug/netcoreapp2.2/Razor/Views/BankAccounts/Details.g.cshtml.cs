#pragma checksum "C:\Projects\CodexBank\src\Web\CodexBank.Web\Views\BankAccounts\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "41ddcb25c013287bffabd7807ee99efbc0c2e416"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_BankAccounts_Details), @"mvc.1.0.view", @"/Views/BankAccounts/Details.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/BankAccounts/Details.cshtml", typeof(AspNetCore.Views_BankAccounts_Details))]
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
#line 1 "C:\Projects\CodexBank\src\Web\CodexBank.Web\Views\_ViewImports.cshtml"
using CodexBank.Web;

#line default
#line hidden
#line 2 "C:\Projects\CodexBank\src\Web\CodexBank.Web\Views\_ViewImports.cshtml"
using CodexBank.Web.Models;

#line default
#line hidden
#line 3 "C:\Projects\CodexBank\src\Web\CodexBank.Web\Views\_ViewImports.cshtml"
using CodexBank.Common;

#line default
#line hidden
#line 4 "C:\Projects\CodexBank\src\Web\CodexBank.Web\Views\_ViewImports.cshtml"
using CodexBank.Web.Models.BankAccount;

#line default
#line hidden
#line 5 "C:\Projects\CodexBank\src\Web\CodexBank.Web\Views\_ViewImports.cshtml"
using CodexBank.Web.Infrastructure.Collections;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"41ddcb25c013287bffabd7807ee99efbc0c2e416", @"/Views/BankAccounts/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"65b2b29b8c6b1fbe0fae17b5de9f83e90db39b09", @"/Views/_ViewImports.cshtml")]
    public class Views_BankAccounts_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<BankAccountDetailsViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_PaginationPartial", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(36, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\Projects\CodexBank\src\Web\CodexBank.Web\Views\BankAccounts\Details.cshtml"
  
    ViewData["Title"] = Model.UniqueId;

#line default
#line hidden
            BeginContext(86, 149, true);
            WriteLiteral("\r\n<div class=\"card mb-3\">\r\n    <div class=\"card-body hover-parent\">\r\n        <h2 class=\"card-title text-center\">\r\n            <span id=\"accountName\">");
            EndContext();
            BeginContext(236, 10, false);
#line 10 "C:\Projects\CodexBank\src\Web\CodexBank.Web\Views\BankAccounts\Details.cshtml"
                              Write(Model.Name);

#line default
#line hidden
            EndContext();
            BeginContext(246, 487, true);
            WriteLiteral(@"</span>
            <i class=""fas fa-pen fa-xs cursor-pointer hover-visible text-muted text-primary-hover""
               data-toggle=""modal"" data-target=""#editModal"">
            </i>
        </h2>

        <div class=""row"">
            <div class=""col-12 col-md-6 col-lg-5"">
                <h5 class=""text-center"">Account details</h5>
                <div class=""d-flex flex-row justify-content-between"">
                    <p>Name</p>
                    <p class=""ml-3"">");
            EndContext();
            BeginContext(734, 18, false);
#line 21 "C:\Projects\CodexBank\src\Web\CodexBank.Web\Views\BankAccounts\Details.cshtml"
                               Write(Model.UserFullName);

#line default
#line hidden
            EndContext();
            BeginContext(752, 193, true);
            WriteLiteral("</p>\r\n                </div>\r\n                <div class=\"d-flex flex-row justify-content-between\">\r\n                    <p>Current balance</p>\r\n                    <p class=\"text-green ml-3\">€");
            EndContext();
            BeginContext(946, 13, false);
#line 25 "C:\Projects\CodexBank\src\Web\CodexBank.Web\Views\BankAccounts\Details.cshtml"
                                           Write(Model.Balance);

#line default
#line hidden
            EndContext();
            BeginContext(959, 180, true);
            WriteLiteral("</p>\r\n                </div>\r\n                <div class=\"d-flex flex-row justify-content-between\">\r\n                    <p>Account number</p>\r\n                    <p class=\"ml-3\">");
            EndContext();
            BeginContext(1140, 14, false);
#line 29 "C:\Projects\CodexBank\src\Web\CodexBank.Web\Views\BankAccounts\Details.cshtml"
                               Write(Model.UniqueId);

#line default
#line hidden
            EndContext();
            BeginContext(1154, 199, true);
            WriteLiteral("</p>\r\n                </div>\r\n                <div class=\"d-flex flex-row justify-content-between\">\r\n                    <p>Date of creation</p>\r\n                    <p class=\"ml-3 auto-format-date\">");
            EndContext();
            BeginContext(1354, 29, false);
#line 33 "C:\Projects\CodexBank\src\Web\CodexBank.Web\Views\BankAccounts\Details.cshtml"
                                                Write(Model.CreatedOn.ToString("O"));

#line default
#line hidden
            EndContext();
            BeginContext(1383, 184, true);
            WriteLiteral("</p>\r\n                </div>\r\n                <div class=\"d-flex flex-row justify-content-between\">\r\n                    <p>Total transactions</p>\r\n                    <p class=\"ml-3\">");
            EndContext();
            BeginContext(1568, 23, false);
#line 37 "C:\Projects\CodexBank\src\Web\CodexBank.Web\Views\BankAccounts\Details.cshtml"
                               Write(Model.TransactionsCount);

#line default
#line hidden
            EndContext();
            BeginContext(1591, 324, true);
            WriteLiteral(@"</p>
                </div>
            </div>
            <div class=""col-12 col-md-6 offset-lg-2 col-lg-5"">
                <h5 class=""text-center"">Transaction information</h5>
                <div class=""d-flex flex-row justify-content-between"">
                    <p>Name</p>
                    <p class=""ml-3"">");
            EndContext();
            BeginContext(1916, 18, false);
#line 44 "C:\Projects\CodexBank\src\Web\CodexBank.Web\Views\BankAccounts\Details.cshtml"
                               Write(Model.UserFullName);

#line default
#line hidden
            EndContext();
            BeginContext(1934, 173, true);
            WriteLiteral("</p>\r\n                </div>\r\n                <div class=\"d-flex flex-row justify-content-between\">\r\n                    <p>Account</p>\r\n                    <p class=\"ml-3\">");
            EndContext();
            BeginContext(2108, 14, false);
#line 48 "C:\Projects\CodexBank\src\Web\CodexBank.Web\Views\BankAccounts\Details.cshtml"
                               Write(Model.UniqueId);

#line default
#line hidden
            EndContext();
            BeginContext(2122, 175, true);
            WriteLiteral("</p>\r\n                </div>\r\n                <div class=\"d-flex flex-row justify-content-between\">\r\n                    <p>Bank name</p>\r\n                    <p class=\"ml-3\">");
            EndContext();
            BeginContext(2298, 14, false);
#line 52 "C:\Projects\CodexBank\src\Web\CodexBank.Web\Views\BankAccounts\Details.cshtml"
                               Write(Model.BankName);

#line default
#line hidden
            EndContext();
            BeginContext(2312, 178, true);
            WriteLiteral("</p>\r\n                </div>\r\n                <div class=\"d-flex flex-row justify-content-between\">\r\n                    <p>Bank country</p>\r\n                    <p class=\"ml-3\">");
            EndContext();
            BeginContext(2491, 17, false);
#line 56 "C:\Projects\CodexBank\src\Web\CodexBank.Web\Views\BankAccounts\Details.cshtml"
                               Write(Model.BankCountry);

#line default
#line hidden
            EndContext();
            BeginContext(2508, 175, true);
            WriteLiteral("</p>\r\n                </div>\r\n                <div class=\"d-flex flex-row justify-content-between\">\r\n                    <p>Bank code</p>\r\n                    <p class=\"ml-3\">");
            EndContext();
            BeginContext(2684, 14, false);
#line 60 "C:\Projects\CodexBank\src\Web\CodexBank.Web\Views\BankAccounts\Details.cshtml"
                               Write(Model.BankCode);

#line default
#line hidden
            EndContext();
            BeginContext(2698, 90, true);
            WriteLiteral("</p>\r\n                </div>\r\n            </div>\r\n        </div>\r\n\r\n    </div>\r\n</div>\r\n\r\n");
            EndContext();
#line 68 "C:\Projects\CodexBank\src\Web\CodexBank.Web\Views\BankAccounts\Details.cshtml"
 if (!Model.Transactions.Any())
{

#line default
#line hidden
            BeginContext(2824, 81, true);
            WriteLiteral("    <h5 class=\"text-center text-muted\">No transactions have been made yet.</h5>\r\n");
            EndContext();
#line 71 "C:\Projects\CodexBank\src\Web\CodexBank.Web\Views\BankAccounts\Details.cshtml"
}
else
{

#line default
#line hidden
            BeginContext(2917, 515, true);
            WriteLiteral(@"    <h3 class=""text-center"">Last transaction</h3>
    <table class=""table table-hover table-bordered auto-datatable"">
        <thead>
            <tr>
                <th data-priority=""2"">Date</th>
                <th data-priority=""4"">Details</th>
                <th data-priority=""3"">From</th>
                <th data-priority=""5"">To</th>
                <th data-priority=""1"">Amount</th>
                <th class=""none"">Reference number</th>

            </tr>
        </thead>
        <tbody>
");
            EndContext();
#line 88 "C:\Projects\CodexBank\src\Web\CodexBank.Web\Views\BankAccounts\Details.cshtml"
             foreach (var transaction in Model.Transactions)
            {

#line default
#line hidden
            BeginContext(3509, 71, true);
            WriteLiteral("                <tr>\r\n                    <td class=\"auto-format-date\">");
            EndContext();
            BeginContext(3581, 32, false);
#line 91 "C:\Projects\CodexBank\src\Web\CodexBank.Web\Views\BankAccounts\Details.cshtml"
                                            Write(transaction.MadeOn.ToString("O"));

#line default
#line hidden
            EndContext();
            BeginContext(3613, 54, true);
            WriteLiteral("</td>\r\n                    <td class=\"word-break-all\">");
            EndContext();
            BeginContext(3668, 23, false);
#line 92 "C:\Projects\CodexBank\src\Web\CodexBank.Web\Views\BankAccounts\Details.cshtml"
                                          Write(transaction.Description);

#line default
#line hidden
            EndContext();
            BeginContext(3691, 60, true);
            WriteLiteral("</td>\r\n                    <td>\r\n                        <p>");
            EndContext();
            BeginContext(3752, 22, false);
#line 94 "C:\Projects\CodexBank\src\Web\CodexBank.Web\Views\BankAccounts\Details.cshtml"
                      Write(transaction.SenderName);

#line default
#line hidden
            EndContext();
            BeginContext(3774, 33, true);
            WriteLiteral("</p>\r\n                        <p>");
            EndContext();
            BeginContext(3808, 18, false);
#line 95 "C:\Projects\CodexBank\src\Web\CodexBank.Web\Views\BankAccounts\Details.cshtml"
                      Write(transaction.Source);

#line default
#line hidden
            EndContext();
            BeginContext(3826, 86, true);
            WriteLiteral("</p>\r\n                    </td>\r\n                    <td>\r\n                        <p>");
            EndContext();
            BeginContext(3913, 25, false);
#line 98 "C:\Projects\CodexBank\src\Web\CodexBank.Web\Views\BankAccounts\Details.cshtml"
                      Write(transaction.RecipientName);

#line default
#line hidden
            EndContext();
            BeginContext(3938, 33, true);
            WriteLiteral("</p>\r\n                        <p>");
            EndContext();
            BeginContext(3972, 23, false);
#line 99 "C:\Projects\CodexBank\src\Web\CodexBank.Web\Views\BankAccounts\Details.cshtml"
                      Write(transaction.Destination);

#line default
#line hidden
            EndContext();
            BeginContext(3995, 59, true);
            WriteLiteral("</p>\r\n                    </td>\r\n                    <td>\r\n");
            EndContext();
#line 102 "C:\Projects\CodexBank\src\Web\CodexBank.Web\Views\BankAccounts\Details.cshtml"
                         if (transaction.Amount < 0)
                        {

#line default
#line hidden
            BeginContext(4135, 51, true);
            WriteLiteral("                            <span class=\"text-red\">");
            EndContext();
            BeginContext(4187, 18, false);
#line 104 "C:\Projects\CodexBank\src\Web\CodexBank.Web\Views\BankAccounts\Details.cshtml"
                                              Write(transaction.Amount);

#line default
#line hidden
            EndContext();
            BeginContext(4205, 13, true);
            WriteLiteral(" EUR</span>\r\n");
            EndContext();
#line 105 "C:\Projects\CodexBank\src\Web\CodexBank.Web\Views\BankAccounts\Details.cshtml"
                        }
                        else
                        {

#line default
#line hidden
            BeginContext(4302, 54, true);
            WriteLiteral("                            <span class=\"text-green\">+");
            EndContext();
            BeginContext(4357, 18, false);
#line 108 "C:\Projects\CodexBank\src\Web\CodexBank.Web\Views\BankAccounts\Details.cshtml"
                                                 Write(transaction.Amount);

#line default
#line hidden
            EndContext();
            BeginContext(4375, 13, true);
            WriteLiteral(" EUR</span>\r\n");
            EndContext();
#line 109 "C:\Projects\CodexBank\src\Web\CodexBank.Web\Views\BankAccounts\Details.cshtml"
                        }

#line default
#line hidden
            BeginContext(4415, 51, true);
            WriteLiteral("                    </td>\r\n                    <td>");
            EndContext();
            BeginContext(4467, 27, false);
#line 111 "C:\Projects\CodexBank\src\Web\CodexBank.Web\Views\BankAccounts\Details.cshtml"
                   Write(transaction.ReferenceNumber);

#line default
#line hidden
            EndContext();
            BeginContext(4494, 30, true);
            WriteLiteral("</td>\r\n                </tr>\r\n");
            EndContext();
#line 113 "C:\Projects\CodexBank\src\Web\CodexBank.Web\Views\BankAccounts\Details.cshtml"
            }

#line default
#line hidden
            BeginContext(4539, 32, true);
            WriteLiteral("        </tbody>\r\n    </table>\r\n");
            EndContext();
            BeginContext(4573, 4, true);
            WriteLiteral("    ");
            EndContext();
            BeginContext(4577, 65, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "41ddcb25c013287bffabd7807ee99efbc0c2e41617909", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#line 117 "C:\Projects\CodexBank\src\Web\CodexBank.Web\Views\BankAccounts\Details.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model = Model.Transactions;

#line default
#line hidden
            __tagHelperExecutionContext.AddTagHelperAttribute("model", __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(4642, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 118 "C:\Projects\CodexBank\src\Web\CodexBank.Web\Views\BankAccounts\Details.cshtml"
}

#line default
#line hidden
            BeginContext(4647, 524, true);
            WriteLiteral(@"
<div id=""editModal"" class=""modal fade"" tabindex=""-1"" role=""dialog"">
    <div class=""modal-dialog"" role=""document"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h5 class=""modal-title"">Edit account display name</h5>
                <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                    <span aria-hidden=""true"">&times;</span>
                </button>
            </div>
            <div class=""modal-body"">
                ");
            EndContext();
            BeginContext(5171, 636, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "41ddcb25c013287bffabd7807ee99efbc0c2e41620374", async() => {
                BeginContext(5191, 59, true);
                WriteLiteral("\r\n                    <input type=\"hidden\" name=\"accountId\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 5250, "\"", 5267, 1);
#line 131 "C:\Projects\CodexBank\src\Web\CodexBank.Web\Views\BankAccounts\Details.cshtml"
WriteAttributeValue("", 5258, Model.Id, 5258, 9, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(5268, 532, true);
                WriteLiteral(@">
                    <div class=""form-group"">
                        <label>Name</label>
                        <input required class=""form-control"" type=""text"" name=""name"">
                    </div>
                    <div class=""text-center d-flex justify-content-between"">
                        <button type=""button"" class=""btn btn-secondary mr-auto"" data-dismiss=""modal"">Cancel</button>
                        <button type=""submit"" class=""btn btn-primary"">OK</button>
                    </div>
                ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(5807, 60, true);
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n");
            EndContext();
            DefineSection("Scripts", async() => {
                BeginContext(5886, 580, true);
                WriteLiteral(@"
    <script>
        $('#editModal').on('show.bs.modal',
            function() {
                let modal = $(this);
                let name = $('#accountName').text();

                modal.find('.modal-body input[name=name]').val(name);
            });

        $('#editModal').on('shown.bs.modal',
            function() {
                $(this).find('.modal-body input[name=name]').trigger('focus');
            });

        $('#editModal form').submit(function(event) {
            event.preventDefault();

            $.ajax({
                url: '");
                EndContext();
                BeginContext(6467, 52, false);
#line 166 "C:\Projects\CodexBank\src\Web\CodexBank.Web\Views\BankAccounts\Details.cshtml"
                 Write(Url.Action("ChangeAccountNameAsync", "BankAccounts"));

#line default
#line hidden
                EndContext();
                BeginContext(6519, 474, true);
                WriteLiteral(@"',
                type: 'post',
                data: $(event.target).serialize()

            }).always(function() {
                $('#editModal').modal('hide');
            }).done(function(response) {
                if (!response.success) {
                    return;
                }

                let newName = $('#editModal input[name=name]').val();

                $('#accountName').text(newName);
            });
        });
    </script>
");
                EndContext();
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<BankAccountDetailsViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
