// using Ganss.Xss;
// using NUglify;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;

// namespace LSP.Core.Utilities
// {
//     public static class HtmlSanitizeHelper
//     {
//         public static string HtmlSanitized(string content)
//         {
//             #region Sanitize
//             var options = new HtmlSanitizerOptions
//             {
//                 AllowedAttributes = new HashSet<string> { "src", "target", "alt", "href", "style" },
//                 AllowedTags = new HashSet<string> { "br", "p", "div", "blockquote", "h1", "h2", "h3", "h4", "h5", "h6", "ol", "ul", "li", "hr", "img", "iframe", "table", "thead", "tbody", "tr", "th", "td", "a", "b", "i", "u", "span" },
//                 AllowedCssProperties = new HashSet<string> {"align-content",
//                 " align-items",
//                 " align-self",
//                 " background",
//                 " background-color",
//                 " border",
//                 " border-radius",
//                 " color",
//                 " display",
//                 " font-size",
//                 " font-style",
//                 " font-weight",
//                 " justify-content",
//                 " letter-spacing",
//                 " line-break",
//                 " line-height",
//                 " list-style",
//                 " margin",
//                 " margin-bottom",
//                 " margin-left",
//                 " margin-right",
//                 " margin-top",
//                 " object-fit",
//                 " outline",
//                 " padding",
//                 " padding-bottom",
//                 " padding-left",
//                 " padding-right",
//                 " padding-top",
//                 " quotes",
//                 " text-align",
//                 " text-align-last",
//                 " text-decoration",
//                 " text-decoration-color",
//                 " text-decoration-line",
//                 " text-decoration-skip",
//                 " text-decoration-style",
//                 " text-indent",
//                 " text-justify",
//                 " text-orientation",
//                 " text-overflow",
//                 " white-space",
//                 " word-break",
//                 " word-spacing",
//                 " word-wrap",
//                 " z-index",
//                 },

//             };
//             var sanitizer = new HtmlSanitizer(options);
//             var sanitizedContent = sanitizer.Sanitize(content);
//             #endregion
//             #region NUglify
//             var cleanContent = Uglify.Html(sanitizedContent);
//             #endregion
//             return cleanContent.Code;
//         }
//     }
// }
